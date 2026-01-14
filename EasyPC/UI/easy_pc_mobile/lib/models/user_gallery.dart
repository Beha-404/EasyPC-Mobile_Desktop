class UserGallery {
  final int id;
  final int userId;
  final int orderId;
  final String imagePath;
  final DateTime uploadDate;
  final String? description;
  final String? username;

  UserGallery({
    required this.id,
    required this.userId,
    required this.orderId,
    required this.imagePath,
    required this.uploadDate,
    this.description,
    this.username,
  });

  factory UserGallery.fromJson(Map<String, dynamic> json) {
    return UserGallery(
      id: json['id'],
      userId: json['userId'],
      orderId: json['orderId'],
      imagePath: json['imagePath'],
      uploadDate: DateTime.parse(json['uploadDate']),
      description: json['description'],
      username: json['user']?['username'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userId': userId,
      'orderId': orderId,
      'imagePath': imagePath,
      'uploadDate': uploadDate.toIso8601String(),
      'description': description,
      'username': username,
    };
  }
}
