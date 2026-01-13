import 'package:easy_pc/pages/home_page.dart';
import 'package:easy_pc/providers/support_provider.dart';
import 'package:easy_pc/providers/user_provider.dart';
import 'package:easy_pc/providers/wishlist_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:provider/provider.dart';
import 'package:easy_pc/providers/cart_provider.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  
  // Initialize Stripe
  Stripe.publishableKey = 'pk_test_51SoNsfE9HC0B2PBzUNrLYtaZh42OY8fetGVgNmh4pN6AXxBM6IOqYO7ssCgCMCC94c7EgpiOcaHaHhqYIXApO1Zx00W2KHxWkh';
  await Stripe.instance.applySettings();
  
  runApp(
    MultiProvider(providers: [
      ChangeNotifierProvider(create: (_) => CartProvider()),
      ChangeNotifierProvider(create: (_) => UserProvider()),
      ChangeNotifierProvider(create: (_) => SupportProvider()),
      ChangeNotifierProvider(create: (_) => WishlistProvider()),
    ], child: const MyApp())
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.black),
      ),
      home: const HomePage(),
      debugShowCheckedModeBanner: false,
    );
  }
}
