import 'dart:convert';
import 'dart:io';
import 'package:easy_pc/config/config.dart';
import 'package:easy_pc/models/user_gallery.dart';
import 'package:http/http.dart' as http;

class UserGalleryService {
  const UserGalleryService();

  Future<UserGallery> uploadImage({
    required int orderId,
    required File imageFile,
    String? description,
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/usergallery/upload');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    var request = http.MultipartRequest('POST', uri);
    request.headers['Authorization'] = 'Basic $credentials';
    
    request.fields['orderId'] = orderId.toString();
    if (description != null && description.isNotEmpty) {
      request.fields['description'] = description;
    }

    request.files.add(await http.MultipartFile.fromPath('image', imageFile.path));

    final streamedResponse = await request.send();
    final response = await http.Response.fromStream(streamedResponse);

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return UserGallery.fromJson(json);
    } else {
      throw Exception('Failed to upload image: ${response.body}');
    }
  }

  Future<List<UserGallery>> getAllImages() async {
    final uri = Uri.parse('$apiBaseUrl/api/usergallery/all');

    final response = await http.get(uri);

    if (response.statusCode == 200) {
      final List<dynamic> json = jsonDecode(response.body);
      return json.map((item) => UserGallery.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load images: ${response.statusCode}');
    }
  }

  Future<List<UserGallery>> getUserImages({
    required int userId,
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/usergallery/user/$userId');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.get(
      uri,
      headers: {
        'Authorization': 'Basic $credentials',
      },
    );

    if (response.statusCode == 200) {
      final List<dynamic> json = jsonDecode(response.body);
      return json.map((item) => UserGallery.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load user images: ${response.statusCode}');
    }
  }

  Future<bool> deleteImage({
    required int imageId,
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/usergallery/$imageId');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.delete(
      uri,
      headers: {
        'Authorization': 'Basic $credentials',
      },
    );

    if (response.statusCode == 200) {
      return true;
    } else {
      throw Exception('Failed to delete image: ${response.body}');
    }
  }

  String getImageUrl(String imagePath) {
    // Extract filename from path
    final fileName = imagePath.split('\\').last.split('/').last;
    return '$apiBaseUrl/api/usergallery/image/$fileName';
  }
}
