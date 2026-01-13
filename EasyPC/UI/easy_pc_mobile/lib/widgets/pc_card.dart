import 'dart:convert';
import 'package:easy_pc/models/pc.dart';
import 'package:easy_pc/providers/user_provider.dart';
import 'package:easy_pc/providers/wishlist_provider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

const yellow = Color(0xFFDDC03D);

class PcCard extends StatelessWidget {
  final PC pc;
  final VoidCallback onAddToCart;
  final VoidCallback onSeeDetails;

  const PcCard({
    super.key,
    required this.pc,
    required this.onAddToCart,
    required this.onSeeDetails,
  });

  @override
  Widget build(BuildContext context) {
    final userProvider = Provider.of<UserProvider>(context);
    final wishlistProvider = Provider.of<WishlistProvider>(context);
    final isLoggedIn = userProvider.user != null;
    final isInWishlist = pc.id != null && wishlistProvider.isInWishlist(pc.id!);

    return Container(
      decoration: BoxDecoration(
        color: const Color(0xFF2B2B2B),
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: yellow, width: 1),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withValues(alpha: 0.25),
            blurRadius: 16,
            offset: const Offset(0, 6),
          ),
        ],
      ),
      child: Stack(
        children: [
          Padding(
            padding: const EdgeInsets.all(10),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  pc.name ?? '',
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 16,
                    fontWeight: FontWeight.w700,
                  ),
                ),
                const SizedBox(height: 3),
                Expanded(child: _buildImage()),
                const SizedBox(height: 3),
                _buildStarsWithCount(pc.averageRating ?? 0, pc.ratingCount ?? 0),
                const SizedBox(height: 3),
                Text(
                  'Price: ${pc.price}\$',
                  style: const TextStyle(
                    color: yellow,
                    fontWeight: FontWeight.w800,
                  ),
                ),
                const SizedBox(height: 5),
                _buildAddToCartButton(),
                const SizedBox(height: 2),
                _buildSeeDetailsButton(),
              ],
            ),
          ),
          // Wishlist heart button
          if (isLoggedIn && pc.id != null)
            Positioned(
              top: 8,
              right: 8,
              child: _WishlistButton(
                pc: pc,
                isInWishlist: isInWishlist,
              ),
            ),
        ],
      ),
    );
  }

  Widget _buildImage() {
    return ClipRRect(
      borderRadius: BorderRadius.circular(8),
      child: pc.picture == null || pc.picture!.isEmpty
          ? Container(
              color: const Color(0xFF3A3A3A),
              child: const Center(
                child: Icon(Icons.desktop_windows, color: Colors.white54, size: 64),
              ),
            )
          : _buildBase64Image(pc.picture!),
    );
  }

  Widget _buildBase64Image(String base64String) {
    try {
      final bytes = base64Decode(base64String);
      return Image.memory(
        bytes,
        fit: BoxFit.cover,
        width: double.infinity,
        errorBuilder: (_, __, ___) => Container(
          color: const Color(0xFF3A3A3A),
          child: const Center(
            child: Icon(Icons.broken_image, color: Colors.white54, size: 48),
          ),
        ),
      );
    } catch (e) {
      return Container(
        color: const Color(0xFF3A3A3A),
        child: const Center(
          child: Icon(Icons.broken_image, color: Colors.white54, size: 48),
        ),
      );
    }
  }

  Widget _buildStarsWithCount(int rating, int reviewCount) {
    return Row(
      children: [
        ...List.generate(5, (i) {
          if (i < rating) {
            return const Icon(Icons.star, size: 18, color: yellow);
          } else {
            return const Icon(Icons.star_border, size: 18, color: yellow);
          }
        }),
        const SizedBox(width: 6),
        Text(
          '($reviewCount)',
          style: const TextStyle(
            color: Colors.white70,
            fontSize: 12,
          ),
        ),
      ],
    );
  }

  Widget _buildAddToCartButton() {
    return SizedBox(
      width: double.infinity,
      child: ElevatedButton(
        onPressed: onAddToCart,
        style: ElevatedButton.styleFrom(
          backgroundColor: yellow,
          foregroundColor: Colors.black,
          padding: const EdgeInsets.symmetric(vertical: 10),
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
        ),
        child: const Text(
          'Add To Cart',
          style: TextStyle(fontWeight: FontWeight.w700),
        ),
      ),
    );
  }

  Widget _buildSeeDetailsButton() {
    return SizedBox(
      width: double.infinity,
      child: OutlinedButton(
        onPressed: onSeeDetails,
        style: OutlinedButton.styleFrom(
          side: const BorderSide(color: yellow, width: 1.5),
          foregroundColor: yellow,
          padding: const EdgeInsets.symmetric(vertical: 10),
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
        ),
        child: const Text(
          'See Details',
          style: TextStyle(fontWeight: FontWeight.w700),
        ),
      ),
    );
  }

}

class _WishlistButton extends StatefulWidget {
  final PC pc;
  final bool isInWishlist;

  const _WishlistButton({
    required this.pc,
    required this.isInWishlist,
  });

  @override
  State<_WishlistButton> createState() => _WishlistButtonState();
}

class _WishlistButtonState extends State<_WishlistButton> {
  bool _isLoading = false;

  Future<void> _toggleWishlist() async {
    if (_isLoading) return;
    
    final userProvider = Provider.of<UserProvider>(context, listen: false);
    final wishlistProvider = Provider.of<WishlistProvider>(context, listen: false);
    
    if (userProvider.user == null || widget.pc.id == null) return;
    
    setState(() => _isLoading = true);
    
    final username = userProvider.user?.username ?? '';
    final password = userProvider.password ?? '';
    final headers = {
      'Authorization': 'Basic ${base64Encode(utf8.encode('$username:$password'))}',
    };
    
    final wasInWishlist = wishlistProvider.isInWishlist(widget.pc.id!);
    
    final result = await wishlistProvider.toggleWishlist(
      userProvider.user!.id!,
      widget.pc.id!,
      headers: headers,
    );
    
    if (mounted) {
      setState(() => _isLoading = false);
      
      ScaffoldMessenger.of(context).clearSnackBars();
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Row(
            children: [
              Icon(
                result 
                    ? (wasInWishlist ? Icons.heart_broken : Icons.favorite) 
                    : Icons.error,
                color: Colors.white,
              ),
              const SizedBox(width: 12),
              Expanded(
                child: Text(
                  result 
                      ? (wasInWishlist ? 'Removed from wishlist' : 'Added to wishlist')
                      : 'Failed to update wishlist',
                ),
              ),
            ],
          ),
          backgroundColor: result ? (wasInWishlist ? Colors.grey[850] : Colors.red) : Colors.grey[850],
          behavior: SnackBarBehavior.floating,
          duration: const Duration(seconds: 2),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
          margin: const EdgeInsets.all(16),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final wishlistProvider = Provider.of<WishlistProvider>(context);
    final isInWishlist = widget.pc.id != null && wishlistProvider.isInWishlist(widget.pc.id!);
    
    return GestureDetector(
      onTap: _toggleWishlist,
      child: Container(
        padding: const EdgeInsets.all(6),
        decoration: BoxDecoration(
          color: Colors.black.withValues(alpha: 0.5),
          shape: BoxShape.circle,
        ),
        child: _isLoading
            ? const SizedBox(
                width: 20,
                height: 20,
                child: CircularProgressIndicator(
                  strokeWidth: 2,
                  color: Colors.white,
                ),
              )
            : Icon(
                isInWishlist ? Icons.favorite : Icons.favorite_border,
                color: isInWishlist ? Colors.red : Colors.white,
                size: 20,
              ),
      ),
    );
  }
}