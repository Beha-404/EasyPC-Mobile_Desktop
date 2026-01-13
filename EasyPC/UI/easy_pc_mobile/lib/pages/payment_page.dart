import 'package:easy_pc/models/cart.dart';
import 'package:easy_pc/pages/paypal_webview_page.dart';
import 'package:easy_pc/providers/cart_provider.dart';
import 'package:easy_pc/providers/user_provider.dart';
import 'package:easy_pc/services/order_service.dart';
import 'package:easy_pc/services/payment_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:provider/provider.dart';

const yellow = Color(0xFFDDC03D);

class PaymentPage extends StatelessWidget {
  final String paymentMethod;
  final String address;
  final String city;
  final String postalCode;
  final String notes;
  final List<Cart> cartItems;
  final int totalPrice;
  final int userId;

  const PaymentPage({
    super.key,
    required this.paymentMethod,
    required this.address,
    required this.city,
    required this.postalCode,
    required this.notes,
    required this.cartItems,
    required this.totalPrice,
    required this.userId,
  });

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
        title: Text(
          paymentMethod,
          style: const TextStyle(color: yellow, fontWeight: FontWeight.w700),
        ),
      ),
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(
                _getIcon(),
                size: 100,
                color: yellow,
              ),
              const SizedBox(height: 24),
              Text(
                _getTitle(),
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: 16),
              Text(
                _getDescription(),
                style: const TextStyle(color: Colors.white70, fontSize: 14),
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: 32),
              SizedBox(
                width: double.infinity,
                height: 50,
                child: ElevatedButton(
                  onPressed: () => _processPayment(context),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: yellow,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
                  ),
                  child: Text(
                    _getButtonText(),
                    style: const TextStyle(
                      color: Colors.black,
                      fontSize: 16,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  IconData _getIcon() {
    switch (paymentMethod) {
      case 'PayPal':
        return Icons.paypal;
      case 'CreditCard':
        return Icons.credit_card;
      case 'CashOnDelivery':
        return Icons.local_shipping;
      default:
        return Icons.payment;
    }
  }

  String _getTitle() {
    switch (paymentMethod) {
      case 'PayPal':
        return 'PayPal Payment';
      case 'CreditCard':
        return 'Credit Card Payment';
      case 'CashOnDelivery':
        return 'Cash on Delivery';
      default:
        return 'Payment';
    }
  }

  String _getDescription() {
    switch (paymentMethod) {
      case 'PayPal':
        return 'You will be redirected to PayPal to complete your payment.';
      case 'CreditCard':
        return 'Enter your card details to complete the payment.';
      case 'CashOnDelivery':
        return 'Pay when your order is delivered to your address.';
      default:
        return '';
    }
  }

  String _getButtonText() {
    switch (paymentMethod) {
      case 'PayPal':
        return 'Continue to PayPal';
      case 'CreditCard':
        return 'Enter Card Details';
      case 'CashOnDelivery':
        return 'Confirm Order';
      default:
        return 'Continue';
    }
  }

  Future<void> _processPayment(BuildContext context) async {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => const Center(
        child: CircularProgressIndicator(color: yellow),
      ),
    );

    try {
      final userProvider = Provider.of<UserProvider>(context, listen: false);
      final username = userProvider.user?.username;
      final password = userProvider.password;

      if (username == null || password == null) {
        if (context.mounted) {
          Navigator.pop(context);
        }
        if (context.mounted) {
          _showErrorDialog(
              context, 'Authentication Error',
              'You need to be logged in to place an order.');
        }
        return;
      }

      final orderRequest = {
        'paymentMethod': paymentMethod,
        'userId': userId,
        'orderDetails': cartItems
            .map((item) => {
                  'pcId': item.pcId,
                  'quantity': item.quantity,
                  'unitPrice': item.price,
                })
            .toList(),
      };

      if (context.mounted) {
        Navigator.pop(context);
      }

      switch (paymentMethod) {
        case 'PayPal':
          await _handlePayPalPayment(
              context, orderRequest, username, password);
          break;
        case 'CreditCard':
          await _handleStripePayment(
              context, orderRequest, username, password);
          break;
        case 'CashOnDelivery':
          await _handleCashOnDelivery(
              context, orderRequest, username, password);
          break;
        default:
          _showErrorDialog(context, 'Error', 'Unknown payment method');
      }
    } catch (e) {
      if (context.mounted) {
        Navigator.pop(context);
        _showErrorDialog(context, 'Error', 'Failed to process payment: $e');
      }
    }
  }

  Future<void> _handlePayPalPayment(
    BuildContext context,
    Map<String, dynamic> orderRequest,
    String username,
    String password,
  ) async {
    try {
      if (!context.mounted) return;

      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => const Center(
          child: CircularProgressIndicator(color: yellow),
        ),
      );

      final paymentService = const PaymentService();

      // Create PayPal order - this calls the real PayPal API
      final paypalOrderResponse = await paymentService.createPayPalOrder(
        orderRequest,
        username: username,
        password: password,
      );

      final paypalOrderId = paypalOrderResponse['orderId'] as String;
      final approvalUrl = paypalOrderResponse['approvalUrl'] as String?;

      if (!context.mounted) return;
      Navigator.pop(context); // Close loading

      if (approvalUrl == null || approvalUrl.isEmpty) {
        _showErrorDialog(context, 'PayPal Error', 'Failed to get PayPal approval URL');
        return;
      }

      // Navigate to PayPal WebView for user to login and approve
      if (context.mounted) {
        final result = await Navigator.push<PayPalResult>(
          context,
          MaterialPageRoute(
            builder: (context) => PayPalWebViewPage(
              approvalUrl: approvalUrl,
              paypalOrderId: paypalOrderId,
            ),
          ),
        );

        if (result != null && result.success) {
          // User approved the payment - now capture it
          await _capturePayPalPayment(
            context,
            result.paypalOrderId,
            username,
            password,
          );
        } else if (result != null && result.cancelled) {
          if (context.mounted) {
            ScaffoldMessenger.of(context).showSnackBar(
              const SnackBar(
                content: Text('Payment cancelled'),
                backgroundColor: Colors.orange,
              ),
            );
          }
        }
      }
    } catch (e) {
      if (context.mounted) {
        Navigator.of(context).popUntil((route) => route.isFirst == false);
        _showErrorDialog(context, 'PayPal Error', 'Failed to process PayPal payment: $e');
      }
    }
  }

  Future<void> _capturePayPalPayment(
    BuildContext context,
    String paypalOrderId,
    String username,
    String password,
  ) async {
    try {
      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => const Center(
          child: CircularProgressIndicator(color: yellow),
        ),
      );

      final paymentService = const PaymentService();

      // Capture PayPal order - this completes the payment
      final captureResponse = await paymentService.capturePayPalOrder(
        paypalOrderId,
        username: username,
        password: password,
      );

      if (context.mounted) {
        Provider.of<CartProvider>(context, listen: false).clear();
        Navigator.pop(context); // Close loading

        _showSuccessDialog(
          context,
          'Payment Successful!',
          'Your PayPal payment was completed successfully.\n\nOrder ID: ${captureResponse['orderId']}\nPayPal ID: $paypalOrderId',
        );
      }
    } catch (e) {
      if (context.mounted) {
        Navigator.pop(context); // Close loading
        _showErrorDialog(context, 'Payment Error', 'Failed to capture PayPal payment: $e');
      }
    }
  }

  Future<void> _handleStripePayment(
    BuildContext context,
    Map<String, dynamic> orderRequest,
    String username,
    String password,
  ) async {
    try {
      if (!context.mounted) return;

      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => const Center(
          child: CircularProgressIndicator(color: yellow),
        ),
      );

      final paymentService = const PaymentService();

      // Create Stripe payment intent
      final paymentIntentResponse =
          await paymentService.createStripePaymentIntent(
        orderRequest,
        username: username,
        password: password,
      );

      if (!context.mounted) return;
      Navigator.pop(context); // Close loading

      final clientSecret = paymentIntentResponse['clientSecret'] as String;

      // Initialize Stripe payment sheet
      await Stripe.instance.initPaymentSheet(
        paymentSheetParameters: SetupPaymentSheetParameters(
          paymentIntentClientSecret: clientSecret,
          merchantDisplayName: 'EasyPC',
          style: ThemeMode.dark,
          appearance: const PaymentSheetAppearance(
            colors: PaymentSheetAppearanceColors(
              background: Color(0xFF1F1F1F),
              primary: yellow,
              componentBackground: Color(0xFF2A2A2A),
              componentText: Colors.white,
              secondaryText: Colors.white70,
              placeholderText: Colors.white38,
            ),
          ),
        ),
      );

      // Present payment sheet
      await Stripe.instance.presentPaymentSheet();

      // Payment successful - confirm with backend
      if (context.mounted) {
        await _confirmStripePayment(
          context,
          paymentIntentResponse['paymentIntentId'] as String,
          orderRequest,
          username,
          password,
        );
      }
    } on StripeException catch (e) {
      if (context.mounted) {
        if (e.error.code == FailureCode.Canceled) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Payment cancelled'),
              backgroundColor: Colors.orange,
            ),
          );
        } else {
          _showErrorDialog(context, 'Payment Error', e.error.localizedMessage ?? 'Payment failed');
        }
      }
    } catch (e) {
      if (context.mounted) {
        // Close any open dialogs
        Navigator.of(context).popUntil((route) => route.isFirst == false);
        _showErrorDialog(context, 'Stripe Error', 'Failed to process payment: $e');
      }
    }
  }

  Future<void> _confirmStripePayment(
    BuildContext context,
    String paymentIntentId,
    Map<String, dynamic> orderRequest,
    String username,
    String password,
  ) async {
    // Show loading dialog
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => const Center(
        child: CircularProgressIndicator(color: yellow),
      ),
    );

    try {
      final paymentService = const PaymentService();

      // Add Stripe Payment Intent ID to request
      orderRequest['stripePaymentIntentId'] = paymentIntentId;

      // Confirm Stripe payment
      final confirmResponse = await paymentService.confirmStripePayment(
        paymentIntentId,
        orderRequest,
        username: username,
        password: password,
      );

      // Close loading dialog
      if (context.mounted) {
        Navigator.of(context).pop();
      }

      if (context.mounted) {
        Provider.of<CartProvider>(context, listen: false).clear();

        _showSuccessDialog(
          context,
          'Order Placed!',
          'Your Stripe payment was successful. Order ID: ${confirmResponse['orderId']}',
        );
      }
    } catch (e) {
      // Close loading dialog
      if (context.mounted) {
        Navigator.of(context).pop();
      }
      
      if (context.mounted) {
        _showErrorDialog(context, 'Payment Error', 'Failed to confirm Stripe payment: $e');
      }
    }
  }

  Future<void> _handleCashOnDelivery(
    BuildContext context,
    Map<String, dynamic> orderRequest,
    String username,
    String password,
  ) async {
    try {
      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => const Center(
          child: CircularProgressIndicator(color: yellow),
        ),
      );

      orderRequest['paymentMethod'] = 'CashOnDelivery';

      await OrderService().createOrder(
        orderRequest,
        username: username,
        password: password,
      );

      if (context.mounted) {
        Provider.of<CartProvider>(context, listen: false).clear();
        Navigator.pop(context);

        _showSuccessDialog(
          context,
          'Order Placed!',
          'Your order has been placed successfully. You can pay when it is delivered.',
        );
      }
    } catch (e) {
      if (context.mounted) {
        Navigator.pop(context);
        _showErrorDialog(context, 'Error', 'Failed to place order: $e');
      }
    }
  }

  void _showErrorDialog(BuildContext context, String title, String message) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        backgroundColor: const Color(0xFF2A2A2A),
        title: Row(
          children: [
            const Icon(Icons.error, color: Colors.red, size: 32),
            const SizedBox(width: 12),
            Text(title, style: const TextStyle(color: Colors.red)),
          ],
        ),
        content: Text(
          message,
          style: const TextStyle(color: Colors.white70),
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text(
              'OK',
              style: TextStyle(color: yellow),
            ),
          ),
        ],
      ),
    );
  }

  void _showSuccessDialog(
      BuildContext context, String title, String message) {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => AlertDialog(
        backgroundColor: const Color(0xFF2A2A2A),
        title: Row(
          children: [
            const Icon(Icons.check_circle, color: Colors.green, size: 32),
            const SizedBox(width: 12),
            Text(title, style: const TextStyle(color: yellow)),
          ],
        ),
        content: Text(
          message,
          style: const TextStyle(color: Colors.white70),
        ),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.popUntil(context, (route) => route.isFirst);
            },
            child: const Text(
              'OK',
              style: TextStyle(
                color: yellow,
                fontWeight: FontWeight.bold,
                fontSize: 16,
              ),
            ),
          ),
        ],
      ),
    );
  }
}