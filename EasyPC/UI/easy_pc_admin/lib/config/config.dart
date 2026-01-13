import 'package:flutter/foundation.dart' show kIsWeb, defaultTargetPlatform, TargetPlatform;

String get apiBaseUrl {
	const configuredUrl = String.fromEnvironment('API_URL');
	if (configuredUrl.isNotEmpty) return configuredUrl;

	const port = 5285;
	final baseLocalhost = 'http://localhost:$port';
	final baseAndroid = 'http://10.0.2.2:$port';

	if (kIsWeb) return baseLocalhost;

	switch (defaultTargetPlatform) {
		case TargetPlatform.android:
			return baseAndroid;
		default:
			return baseLocalhost;
	}
}