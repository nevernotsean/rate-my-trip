#ifdef UNITY_4_0 || UNITY_5_0
#import "iPhone_View.h"
#else
extern UIViewController* UnityGetGLViewController();
#endif

// Credit: https://github.com/ChrisMaire/unity-native-sharing

extern "C" void _NativeShare_Share( const char* files[], int filesCount, char* subject, const char* text ) 
{
	NSMutableArray *items = [NSMutableArray new];

	if( strlen( text ) > 0 )
		[items addObject:[NSString stringWithUTF8String:text]];

	// Credit: https://answers.unity.com/answers/862224/view.html
	for( int i = 0; i < filesCount; i++ ) 
	{
		NSString *filePath = [NSString stringWithUTF8String:files[i]];
		UIImage *image = [UIImage imageWithContentsOfFile:filePath];
		if( image != nil )
			[items addObject:image];
		else
			[items addObject:[NSURL fileURLWithPath:filePath]];
	}

	UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:nil];
	if( strlen( subject ) > 0 )
		[activity setValue:[NSString stringWithUTF8String:subject] forKey:@"subject"];

	UIViewController *rootViewController = UnityGetGLViewController();
	if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone ) // iPhone
	{
		[rootViewController presentViewController:activity animated:YES completion:nil];
	}
	else // iPad
	{
		UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activity];
		[popup presentPopoverFromRect:CGRectMake( rootViewController.view.frame.size.width / 2, rootViewController.view.frame.size.height / 4, 0, 0 ) inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
	}
}




extern "C" void _shareBackgroundVideo( const char* files[] )  {

	NSString *filePath = [NSString stringWithUTF8String:files[0]];
    NSData *backgroundVideo = [NSData dataWithContentsOfFile:filePath];

	// Verify app can open custom URL scheme, open if able
	NSURL *urlScheme = [NSURL URLWithString:@"instagram-stories://share"];
	if ([[UIApplication sharedApplication] canOpenURL:urlScheme]) {

		// Assign background image asset and attribution link URL to pasteboard
		NSArray *pasteboardItems = @[@{@"com.instagram.sharedSticker.backgroundVideo" : backgroundVideo,
										@"com.instagram.sharedSticker.contentURL" :  @"http://your-deep-link-url"}];
		NSDictionary *pasteboardOptions = @{UIPasteboardOptionExpirationDate : [[NSDate date] dateByAddingTimeInterval:60 * 5]};
		// This call is iOS 10+, can use 'setItems' depending on what versions you support
		[[UIPasteboard generalPasteboard] setItems:pasteboardItems options:pasteboardOptions];

		[[UIApplication sharedApplication] openURL:urlScheme options:@{} completionHandler:nil];
	} else {
		// Handle older app versions or app not installed case
	}
}