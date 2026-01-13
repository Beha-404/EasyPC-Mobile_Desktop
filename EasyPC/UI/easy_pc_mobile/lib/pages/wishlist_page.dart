import 'dart:convert';
import 'package:easy_pc/providers/user_provider.dart';
import 'package:easy_pc/providers/wishlist_provider.dart';
import 'package:easy_pc/widgets/dialog/pc_details_dialog.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';

const yellow = Color(0xFFDDC03D);

class WishlistPage extends StatefulWidget {
  const WishlistPage({super.key});

  @override
  State<WishlistPage> createState() => _WishlistPageState();
}

class _WishlistPageState extends State<WishlistPage> {
  @override
  void initState() {
    super.initState();
    _loadWishlist();
  }

  Future<void> _loadWishlist() async {
    final userProvider = Provider.of<UserProvider>(context, listen: false);
    final wishlistProvider = Provider.of<WishlistProvider>(context, listen: false);

    if (userProvider.user == null) {
      Navigator.pop(context);
      return;
    }

    final username = userProvider.user?.username;
    final password = userProvider.password;

    if (username == null || password == null) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Authentication required. Please log in again.'),
            backgroundColor: Colors.red,
          ),
        );
      }
      return;
    }

    final headers = {
      'Authorization': 'Basic ${base64Encode(utf8.encode('$username:$password'))}',
    };

    await wishlistProvider.loadWishlist(userProvider.user!.id!, headers: headers);
  }

  Map<String, String> _getAuthHeaders() {
    final userProvider = Provider.of<UserProvider>(context, listen: false);
    final username = userProvider.user?.username ?? '';
    final password = userProvider.password ?? '';
    return {
      'Authorization': 'Basic ${base64Encode(utf8.encode('$username:$password'))}',
    };
  }

  Future<void> _removeFromWishlist(int pcId) async {
    final userProvider = Provider.of<UserProvider>(context, listen: false);
    final wishlistProvider = Provider.of<WishlistProvider>(context, listen: false);

    if (userProvider.user == null) return;

    final result = await wishlistProvider.removeFromWishlist(
      userProvider.user!.id!,
      pcId,
      headers: _getAuthHeaders(),
    );

    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(
            result ? 'Removed from wishlist' : 'Failed to remove from wishlist',
          ),
          backgroundColor: result ? yellow : Colors.red,
          behavior: SnackBarBehavior.floating,
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFF1F1F1F),
      appBar: AppBar(
        backgroundColor: const Color(0xFF262626),
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back, color: yellow),
          onPressed: () => Navigator.pop(context),
        ),
        title: const Text(
          'My Wishlist',
          style: TextStyle(color: yellow, fontWeight: FontWeight.w700),
        ),
        actions: [
          Consumer<WishlistProvider>(
            builder: (context, wishlist, child) {
              return Padding(
                padding: const EdgeInsets.only(right: 16),
                child: Center(
                  child: Text(
                    '${wishlist.itemCount} items',
                    style: const TextStyle(
                      color: Colors.white70,
                      fontSize: 14,
                    ),
                  ),
                ),
              );
            },
          ),
        ],
      ),
      body: Consumer<WishlistProvider>(
        builder: (context, wishlist, child) {
          if (wishlist.isLoading) {
            return const Center(
              child: CircularProgressIndicator(color: yellow),
            );
          }

          if (wishlist.error != null) {
            return _buildErrorState(wishlist.error!);
          }

          if (wishlist.isEmpty) {
            return _buildEmptyState();
          }

          return RefreshIndicator(
            color: yellow,
            onRefresh: _loadWishlist,
            child: ListView.builder(
              padding: const EdgeInsets.all(16),
              itemCount: wishlist.wishlistItems.length,
              itemBuilder: (context, index) {
                final item = wishlist.wishlistItems[index];
                return _buildWishlistItem(item);
              },
            ),
          );
        },
      ),
    );
  }

  Widget _buildEmptyState() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(
            Icons.favorite_border,
            size: 100,
            color: Colors.white.withValues(alpha: 0.3),
          ),
          const SizedBox(height: 24),
          const Text(
            'Your wishlist is empty',
            style: TextStyle(
              color: Colors.white70,
              fontSize: 20,
              fontWeight: FontWeight.w600,
            ),
          ),
          const SizedBox(height: 12),
          const Text(
            'Save PCs you love to see them here',
            style: TextStyle(color: Colors.white54, fontSize: 14),
          ),
          const SizedBox(height: 32),
          ElevatedButton.icon(
            onPressed: () => Navigator.pop(context),
            icon: const Icon(Icons.shopping_bag, color: Colors.black),
            label: const Text(
              'Browse PCs',
              style: TextStyle(
                color: Colors.black,
                fontWeight: FontWeight.bold,
              ),
            ),
            style: ElevatedButton.styleFrom(
              backgroundColor: yellow,
              padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 16),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildErrorState(String error) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Icon(
            Icons.error_outline,
            size: 80,
            color: Colors.red,
          ),
          const SizedBox(height: 16),
          const Text(
            'Error loading wishlist',
            style: TextStyle(
              color: Colors.white70,
              fontSize: 18,
              fontWeight: FontWeight.w600,
            ),
          ),
          const SizedBox(height: 8),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 32),
            child: Text(
              error,
              style: const TextStyle(color: Colors.white38, fontSize: 14),
              textAlign: TextAlign.center,
            ),
          ),
          const SizedBox(height: 24),
          ElevatedButton.icon(
            onPressed: _loadWishlist,
            icon: const Icon(Icons.refresh, color: Colors.black),
            label: const Text(
              'Try Again',
              style: TextStyle(
                color: Colors.black,
                fontWeight: FontWeight.bold,
              ),
            ),
            style: ElevatedButton.styleFrom(
              backgroundColor: yellow,
              padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 12),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildWishlistItem(item) {
    final pc = item.pc;
    if (pc == null) return const SizedBox.shrink();

    final dateFormat = DateFormat('MMM dd, yyyy');
    final addedDate = item.dateAdded != null 
        ? dateFormat.format(item.dateAdded!) 
        : '';

    return Container(
      margin: const EdgeInsets.only(bottom: 12),
      decoration: BoxDecoration(
        color: const Color(0xFF2A2A2A),
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: Colors.white12),
      ),
      child: InkWell(
        onTap: () => PcDetailsDialog.show(context, pc),
        borderRadius: BorderRadius.circular(12),
        child: Padding(
          padding: const EdgeInsets.all(12),
          child: Row(
            children: [
              // PC Image
              Container(
                width: 80,
                height: 80,
                decoration: BoxDecoration(
                  color: const Color(0xFF1F1F1F),
                  borderRadius: BorderRadius.circular(8),
                ),
                child: pc.picture != null && pc.picture!.isNotEmpty
                    ? ClipRRect(
                        borderRadius: BorderRadius.circular(8),
                        child: _buildBase64Image(pc.picture!),
                      )
                    : const Icon(Icons.computer, color: Colors.white30, size: 40),
              ),
              const SizedBox(width: 12),
              // PC Info
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      pc.name ?? 'Unknown PC',
                      style: const TextStyle(
                        color: Colors.white,
                        fontSize: 16,
                        fontWeight: FontWeight.w600,
                      ),
                      maxLines: 2,
                      overflow: TextOverflow.ellipsis,
                    ),
                    const SizedBox(height: 4),
                    if (pc.pcType != null)
                      Text(
                        pc.pcType!.name ?? '',
                        style: const TextStyle(
                          color: Colors.white54,
                          fontSize: 12,
                        ),
                      ),
                    const SizedBox(height: 4),
                    Row(
                      children: [
                        Text(
                          '\$${pc.price ?? 0}',
                          style: const TextStyle(
                            color: yellow,
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        const Spacer(),
                        if (pc.averageRating != null && pc.averageRating! > 0)
                          Row(
                            children: [
                              const Icon(
                                Icons.star,
                                color: yellow,
                                size: 16,
                              ),
                              const SizedBox(width: 4),
                              Text(
                                '${pc.averageRating}',
                                style: const TextStyle(
                                  color: Colors.white70,
                                  fontSize: 12,
                                ),
                              ),
                            ],
                          ),
                      ],
                    ),
                    if (addedDate.isNotEmpty)
                      Padding(
                        padding: const EdgeInsets.only(top: 4),
                        child: Text(
                          'Added: $addedDate',
                          style: const TextStyle(
                            color: Colors.white38,
                            fontSize: 10,
                          ),
                        ),
                      ),
                  ],
                ),
              ),
              const SizedBox(width: 8),
              // Remove button
              IconButton(
                onPressed: () => _showRemoveConfirmDialog(item.pcId),
                icon: const Icon(
                  Icons.favorite,
                  color: Colors.red,
                  size: 28,
                ),
                tooltip: 'Remove from wishlist',
              ),
            ],
          ),
        ),
      ),
    );
  }

  void _showRemoveConfirmDialog(int pcId) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        backgroundColor: const Color(0xFF2A2A2A),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(16),
        ),
        title: const Text(
          'Remove from Wishlist',
          style: TextStyle(color: yellow, fontWeight: FontWeight.bold),
        ),
        content: const Text(
          'Are you sure you want to remove this PC from your wishlist?',
          style: TextStyle(color: Colors.white70),
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text(
              'Cancel',
              style: TextStyle(color: Colors.white54),
            ),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pop(context);
              _removeFromWishlist(pcId);
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.red,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8),
              ),
            ),
            child: const Text(
              'Remove',
              style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildBase64Image(String base64String) {
    try {
      String cleanBase64 = base64String;
      if (base64String.contains(',')) {
        cleanBase64 = base64String.split(',').last;
      }
      final bytes = base64Decode(cleanBase64);
      return Image.memory(
        bytes,
        fit: BoxFit.cover,
        errorBuilder: (_, __, ___) => const Icon(
          Icons.computer,
          color: Colors.white30,
          size: 40,
        ),
      );
    } catch (e) {
      return const Icon(Icons.computer, color: Colors.white30, size: 40);
    }
  }
}
