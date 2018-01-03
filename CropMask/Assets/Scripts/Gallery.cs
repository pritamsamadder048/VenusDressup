using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
//using ImageAndVideoPicker;
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


    
    public GameObject sceneEditorControllerObj;

    //	public RawImage background;
    public Image processingImage;
	public Texture2D imageTexture;
	public GameObject sObject;

	public string imageFilePAth = null;

    private void Awake()
    {

            
        //PickerEventListener.onImageSelect += OnImageSelect;
        //PickerEventListener.onCancel += OnCancel;
        //PickerEventListener.onError += OnError;
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

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }










    //public void OnImagePicked(AndroidImagePickResult result)
    //{

    //    Debug.Log("image pick");

    //    if (result.IsSucceeded)
    //    {
    //        gameController.ShowLoadingPanelOnly();


    //        //gameController.CollectGurbage(true);
    //        //AN_PoupsProxy.showMessage("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
    //        //OnPhotoPick(result.ImagePath);
    //        UseImage(result.Image);
    //        //DestroyImmediate(result.Image);
    //    }
    //    else
    //    {
    //        AN_PoupsProxy.showMessage("Image Pick Rsult", "Failed");
    //    }

    //    //gameController.CollectGurbage(true);
    //    AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
    //}


    public void UseImage(Texture2D tex)
    {

        bool success = false;


#if !UNITY_EDITOR
        //gameController.ShowLoadingPanelOnly();
#endif





        Texture2D t2d = new Texture2D(tex.width, tex.height);
        print(string.Format("Tex width : {0} , height : {1}", tex.width, tex.height));
        t2d.SetPixels(tex.GetPixels());
        t2d.Apply();
        DestroyImmediate(tex);


        try
        {


            processingImage.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);


            Texture2D ttx = MergeImage(t2d);

            ttx.Apply();


            print("Got image");
            processingImage.sprite = Sprite.Create(ttx, new Rect(0, 0, ttx.width, ttx.height), new Vector2(0.5f, 0.5f), 100f);

            Destroy(touchController.actualImage);
            //touchController.actualImage = new Texture2D(t2d.width, t2d.height);
            //touchController.actualImage.SetPixels(t2d.GetPixels());
            //touchController.actualImage.Apply();

            DestroyImmediate(t2d, true);
            success = true;
            print("All Success");
        }
        catch (Exception e)
        {
            success = false;
            Debug.Log(string.Format("Error while downloading image : ", e));
            //yield return new WaitForEndOfFrame();
            //print("stopping loading for camera..");
            //gameController.HideLoading();
        }







        if (success)
        {

            Handheld.StopActivityIndicator();
            gameController.HideLoading();
            //gameController.HideLoadingPanelOnly();
            print("going to imagecrop");
            GoToImageCrop();
        }
        else
        {

            Handheld.StopActivityIndicator();
            //gameController.HideLoadingPanelOnly();
            gameController.HideLoading();
            print("Some Error occured");
        }
        gameController.CollectGurbage(true);
        gameController.HideLoadingPanelOnly();
        //gameController.HideLoading(); 



    }




    public void GoToImageCrop()
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
        gameController.HideLoadingPanelOnly();
    }

















    public void OpenAndroidNativeGallery()
    {
       try
        {
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = new AndroidJavaObject("tico.transindia.com.cameralib.ImageProStatic");
            ajo.CallStatic("OpenGallery", ajc.GetStatic<AndroidJavaObject>("currentActivity"), (int)processingImage.rectTransform.rect.width, (int)processingImage.rectTransform.rect.height,50);
        }
        catch(Exception e)
        {
            Debug.Log("Error occured opening Gallery : " + e.Message);
            gameController.InstantiateInfoPopup("Error Opening Gallery");
            gameController.HideLoadingPanelOnly();
        }
    }



    public void OnPhotoPick(string photoPath)
    {
        //Debug.Log("Gallery-----" + photoPath);
        if (!photoPath.Contains("file://"))
        {
            photoPath = "file://" + photoPath;
        }
        Debug.Log("Gallery-----" + photoPath);
        StartCoroutine(DownloadImageAndUse(photoPath));
    }


    public void OnPhotoCancel(string message)
    {
        Debug.Log("You cancelled the Gallery");
        gameController.HideLoadingPanelOnly();
        gameController.ToggleHomeSideMenu(1);
    }

    private IEnumerator DownloadImageAndUse(string imageUrl)
    {

        bool success = false;


        using (UnityEngine.Networking.UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error Occured");
                success = false;

            }
            else
            {
                Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                t2d.Apply();
                Debug.Log(string.Format("width and height of camera image is : {0}  {1}", t2d.width, t2d.height));
                processingImage.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
                success = true;
            }
            //www.Dispose();

        }

        
        gameController.HideLoadingPanelOnly();
        if(success)
        {
            GoToImageCrop();
        }
        else
        {
            Debug.Log("error setting gallery image");
        }
    }


































    public void OnOpenGallery()
	{
        
        
        DestroyImmediate(processingImage.sprite);
        //gameController.CollectGurbage(true);
        
#if UNITY_EDITOR
        OnPhotoPick2("editor");
#elif UNITY_ANDROID

            
        gameController.CollectGurbage(true);
        //AndroidCamera.instance.OnImagePicked += OnImagePicked;
        gameController.ShowLoadingPanelOnly();
        OpenAndroidNativeGallery();


#elif UNITY_IPHONE
            //IOSPicker.BrowseImage(false);
#endif

        sceneEditorControllerObj.SetActive(false);
    }



    public void OnImageSelect(string imgPath)
    {
        
        OnPhotoPick2(imgPath);
    }

    public void OnPhotoPick2(string filePath)
	{

        if(!filePath.Contains("file://"))
        {
            filePath = "file://" + filePath;
        }
        //gameController.CollectGurbage(true);
        Debug.Log ("filePath : " + filePath);
        //gameController.InstantiateInfoPopup(filePath);

#if UNITY_EDITOR
        gameController.ShowLoading();
        //StartCoroutine(DownloadImage("http://i.telegraph.co.uk/multimedia/archive/03249/archetypal-female-_3249633c.jpg",orientation));
        //StartCoroutine(DownloadImage("https://i.pinimg.com/originals/a0/1c/0e/a01c0e0c42c83752ea1533121174db34.jpg"));
        //StartCoroutine(DownloadImage("https://wallpaperscraft.com/image/barbara_palvin_brunette_make-up_face_85254_1080x1920.jpg"));
        StartCoroutine(DownloadImage("https://www.specktra.net/images/a/a1/a1e176fc_20150530_145845.jpeg"));
#elif UNITY_ANDROID
        StartCoroutine(DownloadImage(filePath));
        imageFilePAth = filePath;
#elif UNITY_IPHONE
        StartCoroutine(DownloadImage(filePath));
        imageFilePAth = filePath;
#endif

    }
































    private IEnumerator DownloadImage(string imageUrl)
    {

        bool success = false;

//#if UNITY_ANDROID
//        Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);

#if UNITY_IPHONE
        Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.iOSActivityIndicatorStyle.WhiteLarge);
		Handheld.StartActivityIndicator();

#elif UNITY_TIZEN
            Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
		Handheld.StartActivityIndicator();
#endif

        //Handheld.StartActivityIndicator();

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
                    //processingImage.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
                    //int rX = (int)(t2d.width);
                    //int rY = (int)(t2d.height);

                    Texture2D ttx = MergeImage(t2d);

                    ttx.Apply();

                    
                    
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
        gameController.panels[0].SetActive(true);
    }
    public void OnCancel()
    {
        gameController.InstantiateInfoPopup("you cancelled gallery");
        gameController.ToggleHomeSideMenu(1);
        gameController.panels[0].SetActive(true);
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
