import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

const yellow = Color(0xFFDDC03D);

class PayPalWebViewPage extends StatefulWidget {
  final String approvalUrl;
  final String paypalOrderId;

  const PayPalWebViewPage({
    super.key,
    required this.approvalUrl,
    required this.paypalOrderId,
  });

  @override
  State<PayPalWebViewPage> createState() => _PayPalWebViewPageState();
}

class _PayPalWebViewPageState extends State<PayPalWebViewPage> {
  late final WebViewController _controller;
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _initWebView();
  }

  void _initWebView() {
    _controller = WebViewController()
      ..setJavaScriptMode(JavaScriptMode.unrestricted)
      ..setNavigationDelegate(
        NavigationDelegate(
          onProgress: (int progress) {
            if (progress == 100) {
              setState(() => _isLoading = false);
            }
          },
          onPageStarted: (String url) {
            setState(() => _isLoading = true);
          },
          onPageFinished: (String url) {
            setState(() => _isLoading = false);
          },
          onNavigationRequest: (NavigationRequest request) {
            final url = request.url;
            debugPrint('PayPal WebView navigating to: $url');
            
            // PayPal redirects back with token and PayerID when user approves
            // URL format: https://easypc.com/paypal/success?token=XXXXX&PayerID=YYYYY
            if (url.contains('PayerID=')) {
              // Extract PayerID to confirm user actually approved
              final uri = Uri.parse(url);
              final payerId = uri.queryParameters['PayerID'];
              
              if (payerId != null && payerId.isNotEmpty) {
                debugPrint('PayPal payment approved. PayerID: $payerId');
                Navigator.pop(context, PayPalResult(
                  success: true,
                  paypalOrderId: widget.paypalOrderId,
                  payerId: payerId,
                ));
                return NavigationDecision.prevent;
              }
            }
            
            // Check if PayPal redirected to cancel URL
            if (url.contains('easypc.com/paypal/cancel') || url.contains('cancel')) {
              Navigator.pop(context, PayPalResult(
                success: false,
                paypalOrderId: widget.paypalOrderId,
                cancelled: true,
              ));
              return NavigationDecision.prevent;
            }
            
            return NavigationDecision.navigate;
          },
          onWebResourceError: (WebResourceError error) {
            debugPrint('WebView error: ${error.description}');
          },
        ),
      )
      ..loadRequest(Uri.parse(widget.approvalUrl));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFF1F1F1F),
      appBar: AppBar(
        backgroundColor: const Color(0xFF262626),
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.close, color: yellow),
          onPressed: () {
            Navigator.pop(context, PayPalResult(
              success: false,
              paypalOrderId: widget.paypalOrderId,
              cancelled: true,
            ));
          },
        ),
        title: const Row(
          children: [
            Icon(Icons.paypal, color: Colors.blue, size: 28),
            SizedBox(width: 8),
            Text(
              'PayPal Checkout',
              style: TextStyle(color: yellow, fontWeight: FontWeight.w700),
            ),
          ],
        ),
      ),
      body: Stack(
        children: [
          WebViewWidget(controller: _controller),
          if (_isLoading)
            Container(
              color: const Color(0xFF1F1F1F).withOpacity(0.8),
              child: const Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    CircularProgressIndicator(color: yellow),
                    SizedBox(height: 16),
                    Text(
                      'Loading PayPal...',
                      style: TextStyle(color: Colors.white70),
                    ),
                  ],
                ),
              ),
            ),
        ],
      ),
    );
  }
}

class PayPalResult {
  final bool success;
  final String paypalOrderId;
  final bool cancelled;
  final String? payerId;

  PayPalResult({
    required this.success,
    required this.paypalOrderId,
    this.cancelled = false,
    this.payerId,
  });
}
