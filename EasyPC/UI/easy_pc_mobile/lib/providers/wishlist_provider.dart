import 'dart:convert';
import 'package:easy_pc/models/wishlist.dart';
import 'package:easy_pc/services/wishlist_service.dart';
import 'package:flutter/material.dart';

class WishlistProvider extends ChangeNotifier {
  final WishlistService _wishlistService = const WishlistService();
  
  List<Wishlist> _wishlistItems = [];
  Set<int> _wishlistPcIds = {};
  bool _isLoading = false;
  String? _error;

  List<Wishlist> get wishlistItems => [..._wishlistItems];
  Set<int> get wishlistPcIds => {..._wishlistPcIds};
  bool get isLoading => _isLoading;
  String? get error => _error;
  int get itemCount => _wishlistItems.length;
  bool get isEmpty => _wishlistItems.isEmpty;

  bool isInWishlist(int pcId) => _wishlistPcIds.contains(pcId);

  Future<void> loadWishlist(int userId, {Map<String, String>? headers}) async {
    _isLoading = true;
    _error = null;
    notifyListeners();

    try {
      _wishlistItems = await _wishlistService.getByUserId(userId, headers: headers);
      _wishlistPcIds = _wishlistItems.map((w) => w.pcId).toSet();
      _error = null;
    } catch (e) {
      _error = e.toString();
      _wishlistItems = [];
      _wishlistPcIds = {};
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }

  Future<bool> addToWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    try {
      final result = await _wishlistService.addToWishlist(userId, pcId, headers: headers);
      if (result != null) {
        _wishlistPcIds.add(pcId);
        // Reload to get full PC data
        await loadWishlist(userId, headers: headers);
        return true;
      }
      return false; // Already in wishlist
    } catch (e) {
      _error = e.toString();
      notifyListeners();
      return false;
    }
  }

  Future<bool> removeFromWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    try {
      final result = await _wishlistService.removeFromWishlist(userId, pcId, headers: headers);
      if (result) {
        _wishlistItems.removeWhere((w) => w.pcId == pcId);
        _wishlistPcIds.remove(pcId);
        notifyListeners();
        return true;
      }
      return false;
    } catch (e) {
      _error = e.toString();
      notifyListeners();
      return false;
    }
  }

  Future<bool> toggleWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    if (isInWishlist(pcId)) {
      return await removeFromWishlist(userId, pcId, headers: headers);
    } else {
      return await addToWishlist(userId, pcId, headers: headers);
    }
  }

  void clearWishlist() {
    _wishlistItems = [];
    _wishlistPcIds = {};
    _error = null;
    notifyListeners();
  }
}
