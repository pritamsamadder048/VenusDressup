
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
        //currentActivity.Call("startActivity", intentObject);

        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Would you like to share?");
        currentActivity.Call("startActivity", jChooser);


#endif

    }



    public static void AndroidShareImageAndTextualData(Texture2D imageToPost, string subject, string title, string textToShare)
    {
#if UNITY_ANDROID // || UNITY_EDITOR
        // Save your image on designate path
        byte[] bytes = imageToPost.EncodeToPNG();
        string path = Application.persistentDataPath +"shared/"+ "/sharedimage.png";

        if(!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        File.WriteAllBytes(path, bytes);
        Debug.Log("File saved for share in shared directory");
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        Debug.Log("Set intent class and object");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        Debug.Log("Set ACTION_SEND");
        intentObject.Call<AndroidJavaObject>("setType", "image/*");
        Debug.Log("Set type image");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Media Sharing ");
        Debug.Log("Set putextra Extra_Subject");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Media Sharing ");
        Debug.Log("Set Extra_Title");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Media Sharing Android Demo");
        Debug.Log("Set Extra_Text");

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
        Debug.Log("Set uriclass and file class");
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here

        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

        Debug.Log("Set uri object and file object");
        //			string uriPath =  uriObject.Call<string>("getPath");
        bool fileExist = fileObject.Call<bool>("exists");
        Debug.Log("File exist : " + fileExist);
        if (fileExist)
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log("Starting native sharing activity for android");
        //currentActivity.Call("startActivity", intentObject);

        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Would you like to share?");
        currentActivity.Call("startActivity", jChooser);
#endif
    }
}

