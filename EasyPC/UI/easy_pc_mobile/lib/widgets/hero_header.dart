import 'package:flutter/material.dart';

const yellow = Color(0xFFDDC03D);

class HeroHeader extends StatelessWidget {
  const HeroHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: const [
        Text(
          'Build or Choose Your Dream PC',
          style: TextStyle(
            color: yellow,
            fontSize: 26,
            fontWeight: FontWeight.w900,
            height: 1.1,
          ),
        ),
      ],
    );
  }
}