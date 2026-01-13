import 'package:easy_pc/models/pc.dart';
import 'package:easy_pc/models/user.dart';

class Wishlist {
  final int? id;
  final int userId;
  final int pcId;
  final DateTime? dateAdded;
  final User? user;
  final PC? pc;

  Wishlist({
    this.id,
    required this.userId,
    required this.pcId,
    this.dateAdded,
    this.user,
    this.pc,
  });

  factory Wishlist.fromJson(Map<String, dynamic> json) {
    return Wishlist(
      id: json['id'],
      userId: json['userId'],
      pcId: json['pcId'],
      dateAdded: json['dateAdded'] != null
          ? DateTime.parse(json['dateAdded'])
          : null,
      user: json['user'] != null ? User.fromJson(json['user']) : null,
      pc: json['pc'] != null ? PC.fromJson(json['pc']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userId': userId,
      'pcId': pcId,
      'dateAdded': dateAdded?.toIso8601String(),
    };
  }
}
