using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GalleryController : MonoBehaviour {

	private Texture defaultBackground;
	private Texture2D photoTaken;
	[SerializeField]
	private GameObject cameraPanel;
	[SerializeField]
	private GameObject mainPanel;
	[SerializeField]
	private GameObject imagePanel;


//	public RawImage background;
	public RawImage processingImage;
	public Texture2D imageTexture;
	public GameObject sObject;


	// Use this for initialization
	void Start () {
//		imagePanel.SetActive (false);
//		mainPanel.SetActive(true);




	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void OpenGallery()
	{
		if (Application.platform == RuntimePlatform.Android) {
		
		
			#region [ Intent intent = new Intent(); ]
			//instantiate the class Intent
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");

			//instantiate the object Intent
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			#endregion [ Intent intent = new Intent(); ]


			#region [ intent.setAction(Intent.ACTION_GET_CONTENT); ]
			//call setAction setting ACTION_SEND as parameter
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_GET_CONTENT"));
			#endregion [ intent.setAction(Intent.ACTION_GET_CONTENT); ]


			#region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
			//instantiate the class Uri
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");

			//instantiate the object Uri with the parse of the url's file
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "content://media/internal/images/media");

			//call putExtra with the uri object of the file
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
			#endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]

			//set the type of file
			intentObject.Call<AndroidJavaObject> ("setType", "image/jpeg");

			#region [ startActivityForResult(intent , 1); ]
			//instantiate the class UnityPlayer
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

			//instantiate the object currentActivity
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");

			//call the activity with our Intent
			currentActivity.Call ("startActivity", intentObject);
			#endregion [ startActivityForResult(intent , REQUEST_FOR_RESLUT); ]
		}

		if (!Application.isMobilePlatform) {
			print ("shoul have opened gallery");

		}
	}





	public void OpenGallery2()
	{
		print ("openning gallery 2");
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaClass ajc = new AndroidJavaClass ("com.unity.player.UnityPlayer");

			AndroidJavaObject ajo = new AndroidJavaObject ("com.radikallabs.androidgallery.AndroidGallery");
			ajo.CallStatic ("OpenGallery", ajc.GetStatic<AndroidJavaObject> ("currentActivity"));
		}
	}

	public void OnPickPhoto(string filePath)
	{
		
	}
	private byte[] LoadBytes(string path)
	{
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader bin = new BinaryReader(fs);
		byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
		bin.Close();
		return result;
	}





}
