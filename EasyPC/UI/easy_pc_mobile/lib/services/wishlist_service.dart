import 'dart:convert';
import 'package:easy_pc/config/config.dart';
import 'package:easy_pc/models/wishlist.dart';
import 'package:http/http.dart' as http;

class WishlistService {
  const WishlistService();

  Future<List<Wishlist>> getByUserId(int userId, {Map<String, String>? headers}) async {
    final uri = Uri.parse('$apiBaseUrl/api/wishlist/$userId');
    final response = await http.get(uri, headers: headers);
    
    if (response.statusCode == 200) {
      final List<dynamic> json = jsonDecode(response.body);
      return json.map((item) => Wishlist.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load wishlist: ${response.statusCode}');
    }
  }

  Future<Wishlist?> addToWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    final uri = Uri.parse('$apiBaseUrl/api/wishlist');
    final response = await http.post(
      uri,
      headers: {
        'Content-Type': 'application/json',
        ...?headers,
      },
      body: jsonEncode({
        'userId': userId,
        'pcId': pcId,
      }),
    );
    
    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return Wishlist.fromJson(json);
    } else if (response.statusCode == 400) {
      return null; // Already in wishlist
    } else {
      throw Exception('Failed to add to wishlist: ${response.statusCode}');
    }
  }

  Future<bool> removeFromWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    final uri = Uri.parse('$apiBaseUrl/api/wishlist/$userId/$pcId');
    final response = await http.delete(uri, headers: headers);
    
    if (response.statusCode == 200) {
      return true;
    } else if (response.statusCode == 404) {
      return false;
    } else {
      throw Exception('Failed to remove from wishlist: ${response.statusCode}');
    }
  }

  Future<bool> isInWishlist(int userId, int pcId, {Map<String, String>? headers}) async {
    final uri = Uri.parse('$apiBaseUrl/api/wishlist/$userId/check/$pcId');
    final response = await http.get(uri, headers: headers);
    
    if (response.statusCode == 200) {
      return jsonDecode(response.body) as bool;
    } else {
      throw Exception('Failed to check wishlist: ${response.statusCode}');
    }
  }
}
