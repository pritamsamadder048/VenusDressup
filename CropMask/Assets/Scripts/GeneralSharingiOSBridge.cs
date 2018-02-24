using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class GeneralSharingiOSBridge
{

#if UNITY_IOS
	
	[DllImport("__Internal")]
	private static extern void _TAG_ShareTextWithImage (string iosPath, string message);
	
	[DllImport("__Internal")]
	private static extern void _TAG_ShareSimpleText (string message);
#endif

    public static void IosShareTextualData (string message)
	{
#if UNITY_IOS
        _TAG_ShareSimpleText (message);
#endif
	}

	public static void IosShareImageAndTextualData (string imagePath, string message)
	{
#if UNITY_IOS
        _TAG_ShareTextWithImage (imagePath, message);
#endif
	}
	

}
