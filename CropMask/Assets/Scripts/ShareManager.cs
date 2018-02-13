
/// <summary>
///  Resource Link : http://www.theappguruz.com/blog/general-sharing-in-android-ios-in-unity
///  Resource Link : https://github.com/tejas123/general-sharing-in-android-ios-in-unity/blob/master/General%20Sharing/Assets/Scripts/GeneralSharing.cs
/// </summary>




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class ShareManager
{
#if UNITY_IOS
    //[DllImport("__Internal")]
    //private static extern void IosShareImageAndTextualData(string iosPath, string message);
    //[DllImport("__Internal")]
    //private static extern void IosShareTextualData(string message);
#endif
    

    public static void AndroidShareTextualData(string subject,string title,string textToShare)
    {
        
        #if UNITY_ANDROID // || UNITY_EDITOR
        // Create Refernece of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        // Create Refernece of AndroidJavaObject class intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
 
        // Set action for intent
        intentObject.Call("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
 
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
 
        //Set Subject of action
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        //Set title of action or intent
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), title);
        // Set actual data which you want to share
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), textToShare);
 
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic< AndroidJavaObject>("currentActivity");
        // Invoke android activity for passing intent to share data
        currentActivity.Call("startActivity", intentObject);
    

        #endif

    }



    public static void AndroidShareImageAndTextualData(Texture2D imageToPost, string subject, string title, string textToShare)
    {
#if UNITY_ANDROID // || UNITY_EDITOR
        // Save your image on designate path
        byte[] bytes = imageToPost.EncodeToPNG();
        string path = Application.persistentDataPath + "/MyImage.png";
        File.WriteAllBytes(path, bytes);
 
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
 
        intentObject.Call("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call("setType", "image/*");
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), title);
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), textToShare);
 
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
 
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
 
        // string uriPath =  uriObject.Call("getPath");
        bool fileExist = fileObject.Call<bool>("exists");
        Debug.Log("File exist : " + fileExist);
        // Attach image to intent
        if (fileExist)
        {
            intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        }
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intentObject);
#endif
    }
}

