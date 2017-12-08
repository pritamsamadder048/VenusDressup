using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using ImageAndVideoPicker;
using System;
using UnityEngine.Networking;

public class Gallery : MonoBehaviour {

	WWW www;
	private bool isDownloadCompleted=false;
	private Texture defaultBackground;
	private Texture2D photoTaken;



    [SerializeField]
    private GameObject gameControllerObject;
    [SerializeField]
    private GameObject cameraControllerObject;
    [SerializeField]
    private GameObject touchControllerObject;


    private GameController gameController;
    private CameraController cameraController;
    private TouchController touchController;

    [SerializeField]
    private GameObject imagePanel;
    [SerializeField]
    private GameObject[] panels;


    [SerializeField]
    private GameObject sceneEditorControllerObj;

    //	public RawImage background;
    public Image processingImage;
	public Texture2D imageTexture;
	public GameObject sObject;

	public string imageFilePAth = null;

    private void Awake()
    {

            
        PickerEventListener.onImageSelect += OnImageSelect;
        PickerEventListener.onCancel += OnCancel;
        PickerEventListener.onError += OnError;
    }

    // Use this for initialization
    void Start () {
        //		imagePanel.SetActive (false);
        //		mainPanel.SetActive(true);

        gameController = gameControllerObject.GetComponent<GameController>();
        cameraController = cameraControllerObject.GetComponent<CameraController>();
        touchController = touchControllerObject.GetComponent<TouchController>();


    }

	// Update is called once per frame
	void Update () {


	}




    private void GoToImageCrop()
    {
        //Go To Image Panel;
        for (int i = 2; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        gameController.HideAcceptCropButton();
        touchController.ToggleMask(1);
        imagePanel.SetActive(true);
        //gameController.HideLoading();
        www = null;
        sceneEditorControllerObj.SetActive(false);
    }




	public void OnOpenGallery()
	{
        
#if UNITY_EDITOR
            OnPhotoPick("editor");
#elif UNITY_ANDROID

            //AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //AndroidJavaObject ajo = new AndroidJavaObject("com.radikallabs.androidgallery.UnityBinder");
            //ajo.CallStatic("OpenGallery", ajc.GetStatic<AndroidJavaObject>("currentActivity"));


            AndroidPicker.BrowseImage();
#elif UNITY_IPHONE
            IOSPicker.BrowseImage(false);
#endif
        
        sceneEditorControllerObj.SetActive(false);
	}
    public void OnImageSelect(string imgPath, ImageOrientation orientation)
    {
        imgPath = "file://" + imgPath;
        OnPhotoPick(imgPath,orientation);
    }

    public void OnPhotoPick(string filePath,ImageOrientation orientation=ImageOrientation.UP)
	{
		Debug.Log ("filePath : " + filePath);
        //gameController.InstantiateInfoPopup(filePath);

#if UNITY_EDITOR
        gameController.ShowLoading();
        //StartCoroutine(DownloadImage("http://i.telegraph.co.uk/multimedia/archive/03249/archetypal-female-_3249633c.jpg",orientation));
        StartCoroutine(DownloadImage("https://i.pinimg.com/originals/a0/1c/0e/a01c0e0c42c83752ea1533121174db34.jpg", orientation));
#elif UNITY_ANDROID
        StartCoroutine(DownloadImage(filePath,orientation));
        imageFilePAth = filePath;
#elif UNITY_IPHONE
        StartCoroutine(DownloadImage(filePath,orientation));
        imageFilePAth = filePath;
#endif

    }

    private IEnumerator DownloadImage(string imageUrl,ImageOrientation imageOrientation=ImageOrientation.UP)
    {

        bool success = false;

#if UNITY_ANDROID
        Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);

#elif UNITY_IPHONE
        Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.iOSActivityIndicatorStyle.WhiteLarge);

#elif UNITY_TIZEN
            Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
#endif

        Handheld.StartActivityIndicator();

#if !UNITY_EDITOR
        gameController.ShowLoadingPanelOnly();
#endif

        using (UnityEngine.Networking.UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                gameController.InstantiateInfoPopup(www.error);
                gameController.HideLoading();
            }
            else
            {
                Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                t2d.Apply();

                try
                {
                    
                    //				photoTaken = new Texture2D (www.texture.width, www.texture.height);
                    //				photoTaken.SetPixels (www.texture.GetPixels ());
                    //				photoTaken.Apply ();
                    //				processingImage.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (www.texture.width, www.texture.height);
                    processingImage.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
                    int rX = (int)(t2d.width);
                    int rY = (int)(t2d.height);

                    Texture2D ttx = MergeImage(t2d);

                    ttx.Apply();

                    Texture2D newTex = new Texture2D(t2d.width, t2d.height);
                    newTex.SetPixels(t2d.GetPixels());
                    newTex.Apply();
                    if (imageOrientation == ImageOrientation.UP)
                    {
                    }
                    else if (imageOrientation == ImageOrientation.LEFT)
                    {
                        newTex = rotateTexture(t2d, true);
                    }
                    else if (imageOrientation == ImageOrientation.RIGHT)
                    {
                        newTex = rotateTexture(t2d, false);
                    }
                    else
                    {
                    }
                    t2d = new Texture2D(newTex.width, newTex.height);
                    t2d.SetPixels(newTex.GetPixels());
                    t2d.Apply();
                    DestroyImmediate(newTex);
                    
                    print(string.Format("current size : {0} , {1}", ttx.width, ttx.height));
                    Debug.Log("Image is downloaded");
                    processingImage.sprite = Sprite.Create(ttx, new Rect(0, 0, ttx.width, ttx.height), new Vector2(0.5f, 0.5f), 100f);

                    Destroy(touchController.actualImage);
                    touchController.actualImage = new Texture2D(t2d.width, t2d.height);
                    touchController.actualImage.SetPixels(t2d.GetPixels());
                    touchController.actualImage.Apply();
                    DestroyImmediate(t2d, true);
                    success = true;
                    //gameController.InstantiateInfoPopup("Image Loading complete..");
                }
                catch (Exception e)
                {
                    success = false;
                    Debug.Log(string.Format("Error while downloading image : {0}", e.Message));
                    gameController.InstantiateInfoPopup(e.Message);
                }

            }
            //www.Dispose();

        }


        if (success)
        {
            yield return new WaitForSeconds(1.5f);
            gameController.HideLoading();
            Handheld.StopActivityIndicator();
            gameController.HideLoadingPanelOnly();
            GoToImageCrop();
        }
        else
        {
            yield return new WaitForFixedUpdate();
            gameController.HideLoading();
            Handheld.StopActivityIndicator();
            gameController.HideLoadingPanelOnly(); 
        }

    }

    public void OnError(string message)
    {
        print(string.Format("Error Occured Selecting Image : {0}", message));
        gameController.InstantiateInfoPopup(message);
        gameController.ToggleHomeSideMenu(1);
    }
    public void OnCancel()
    {
        gameController.InstantiateInfoPopup("you cancelled gallery");
        //gameController.ToggleHomeSideMenu(1);
    }

//    void SetImage()
//	{
////		try{
			
//		if (www != null) {
//			if (www.isDone) {
////				photoTaken = new Texture2D (www.texture.width, www.texture.height);
////				photoTaken.SetPixels (www.texture.GetPixels ());
////				photoTaken.Apply ();
////				processingImage.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (www.texture.width, www.texture.height);
//				processingImage.sprite =Sprite.Create( www.texture,new Rect(0,0,www.texture.width,www.texture.height),new Vector2(0.5f,0.5f),100f);

//				Texture2D ttx = MergeImage (www.texture);
//				processingImage.sprite=Sprite.Create(ttx,new Rect(0,0,ttx.width,ttx.height),new Vector2(0.5f,0.5f),100f);

//                Destroy(touchController.actualImage);
//                touchController.actualImage = new Texture2D(www.texture.width, www.texture.height);
//                touchController.actualImage.SetPixels(www.texture.GetPixels());
//                touchController.actualImage.Apply();
//                GoToImageCrop();

//			}
//		}
////		}
////		catch {
////			print ("caught");
////		}
//	}

	private byte[] LoadBytes(string path)
	{
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader bin = new BinaryReader(fs);
		byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
		bin.Close();
		return result;
	}

	Texture2D ResizeTexture2D(Texture2D texture,int newWidth,int newHeight){
		float warpFactor = 1.0F;
		//		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
		Texture2D destTex = new Texture2D(newWidth, newHeight);
		//print ("resize texture size : " + destTex.width + "   " + destTex.height);
		//		print ("Screen size : " + Screen.width + "   " + Screen.height);
		Color[] destPix = new Color[destTex.width * destTex.height];
		try
		{
			int y = 0;
			while (y < destTex.height) {
				int x = 0;
				while (x < destTex.width) {
					float xFrac = x * 1.0F / (destTex.width - 1);
					float yFrac = y * 1.0F / (destTex.height - 1);
					float warpXFrac = Mathf.Pow(xFrac, warpFactor);
					float warpYFrac = Mathf.Pow(yFrac, warpFactor);
					destPix[y * destTex.width + x] = texture.GetPixelBilinear(warpXFrac, warpYFrac);
					x++;
				}
				y++;
			}
			destTex.SetPixels(destPix);
			destTex.Apply();

		}
		catch {
			destTex = null;
		}

		//destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);

		return destTex;
	}



    private Texture2D MergeImage(Texture2D Overlay, Texture2D Background = null, Texture2D newTexture = null)
    {
        //print(string.Format("overlay width : {0}  overlay height{1}  pimg rect trnsfrm width : {2} pimg rect trnsfrm Height : {3} ", Overlay.width, Overlay.height, processingImage.rectTransform.rect.width, processingImage.rectTransform.rect.height));
        if ((Overlay.width > processingImage.rectTransform.rect.width) || (Overlay.height > processingImage.rectTransform.rect.height))
        {
            //			Overlay.Resize (1300,1300, Overlay.format, false);
            //			Overlay.Apply ();
            int resizeHeight = Overlay.height;
            int resizeWidth = Overlay.width;

            if (Overlay.width > processingImage.rectTransform.rect.width)
            {
                resizeWidth = (int)processingImage.rectTransform.rect.width;
                float r = (float)Overlay.width / (float)resizeWidth;
                resizeHeight = (int)(Overlay.height / r);
                print(string.Format("Width is big Resize width : {0} resize hight {1} when r is : {2}", resizeWidth, resizeHeight, r));
            }
            /*
			if (Overlay.height > processingImage.rectTransform.rect.height) {
				resizeHeight =(int) processingImage.rectTransform.rect.height;
                float r = (float)Overlay.height / (float)resizeHeight;
                resizeWidth = (int)(Overlay.width / r);
                print(string.Format("Height is big Resize width : {0} resize hight {1} when r is : {2}", resizeWidth, resizeHeight, r));
            }
            */



            Texture2D resizedOverlay = ResizeTexture2D(Overlay, resizeWidth, resizeHeight);
            Overlay.Resize(resizeWidth, resizeHeight);
            Overlay.Apply();
            //			Overlay=ResizeTexture2D (Overlay, resizeWidth, resizeHeight);
            Overlay.SetPixels(resizedOverlay.GetPixels());
            Overlay.Apply();
        }

        if (Background == null)
        {
            Background = new Texture2D((int)processingImage.rectTransform.rect.width, (int)processingImage.rectTransform.rect.height, TextureFormat.ARGB32, false);
            Color fillColor = Color.clear;
            //Color[] fillPixels = new Color[Background.width * Background.height];

            //for (int i = 0; i < fillPixels.Length; i++)
            //{
            //	fillPixels[i] = fillColor;
            //}

            Color[] fillPixels = Enumerable.Repeat(Color.clear, Background.width * Background.height).ToArray();

            Background.SetPixels(fillPixels);
            Background.Apply();

        }
        if (newTexture == null)
        {
            newTexture = new Texture2D((int)processingImage.rectTransform.rect.width, (int)processingImage.rectTransform.rect.height, TextureFormat.ARGB32, false);
            //print ("big image rect : "+processingImage.rectTransform.rect);
        }


        Vector2 offset = new Vector2(((newTexture.width - Overlay.width) / 2), ((newTexture.height - Overlay.height) / 2));

        newTexture.SetPixels(Background.GetPixels());

        for (int y = 0; y < Overlay.height; y++)
        {
            for (int x = 0; x < Overlay.width; x++)
            {
                Color PixelColorFore = Overlay.GetPixel(x, y) * Overlay.GetPixel(x, y).a;
                Color PixelColorBack = Background.GetPixel((int)(x + offset.x), (int)(y + offset.y)) * (1 - PixelColorFore.a);
                newTexture.SetPixel((int)(x + offset.x), (int)(y + offset.y), PixelColorBack + PixelColorFore);
            }
        }

        newTexture.Apply();
        return newTexture;
    }




    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

}
