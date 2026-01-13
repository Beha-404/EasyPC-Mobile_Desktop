import 'dart:convert';
import 'package:easy_pc/config/config.dart';
import 'package:http/http.dart' as http;

class PaymentService {
  const PaymentService();

  // PayPal endpoints
  Future<Map<String, dynamic>> createPayPalOrder(
    Map<String, dynamic> orderRequest, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/paypal/create-order');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.post(
      uri,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Basic $credentials',
      },
      body: jsonEncode(orderRequest),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception('Failed to create PayPal order: ${response.statusCode} - ${response.body}');
    }
  }

  Future<Map<String, dynamic>> capturePayPalOrder(
    String paypalOrderId, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/paypal/capture-order/$paypalOrderId');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.post(
      uri,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Basic $credentials',
      },
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception('Failed to capture PayPal order: ${response.statusCode} - ${response.body}');
    }
  }

  Future<Map<String, dynamic>> getPayPalOrderStatus(
    String paypalOrderId, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/paypal/order-status/$paypalOrderId');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.get(
      uri,
      headers: {
        'Authorization': 'Basic $credentials',
      },
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception('Failed to get PayPal order status: ${response.statusCode}');
    }
  }

  // Stripe endpoints
  Future<Map<String, dynamic>> createStripePaymentIntent(
    Map<String, dynamic> orderRequest, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/stripe/create-payment-intent');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.post(
      uri,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Basic $credentials',
      },
      body: jsonEncode(orderRequest),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to create Stripe payment intent: ${response.statusCode}');
    }
  }

  Future<Map<String, dynamic>> confirmStripePayment(
    String paymentIntentId,
    Map<String, dynamic> orderRequest, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse('$apiBaseUrl/api/stripe/confirm-payment');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final confirmData = {
      'paymentIntentId': paymentIntentId,
      'orderRequest': orderRequest,
    };

    final response = await http.post(
      uri,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Basic $credentials',
      },
      body: jsonEncode(confirmData),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception('Failed to confirm Stripe payment: ${response.statusCode}');
    }
  }

  Future<Map<String, dynamic>> getPaymentIntentStatus(
    String paymentIntentId, {
    required String username,
    required String password,
  }) async {
    final uri = Uri.parse(
        '$apiBaseUrl/api/stripe/payment-intent/$paymentIntentId');
    final credentials = base64Encode(utf8.encode('$username:$password'));

    final response = await http.get(
      uri,
      headers: {
        'Authorization': 'Basic $credentials',
      },
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to get payment intent status: ${response.statusCode}');
    }
  }
}
