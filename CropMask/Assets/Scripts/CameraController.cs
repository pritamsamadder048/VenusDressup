using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShot;
using System.Linq;
using System;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{

    private bool isDownloadCompleted = false;
    [SerializeField]
    private bool camSupported = false;
    private WebCamTexture backCam;
    private WebCamTexture frontCam;
    private WebCamTexture cam;
    private Texture defaultBackground;
    private Texture2D photoTaken;
    private bool cameraIsOpen = false;


    [SerializeField]
    private GameObject gameControllerObject;
    [SerializeField]
    private GameObject galleryControllerObject;
    [SerializeField]
    private GameObject touchControllerObject;


    private GameController gameController;
    private Gallery galleryController;
    private TouchController touchController;

    [SerializeField]
    private GameObject imagePanel;
    [SerializeField]
    private GameObject[] panels;
    [SerializeField]
    private Text debugText;

    public bool isFrontCam = true;
    public RawImage background;
    public Image processingImage;

    public AspectRatioFitter fit;


    [SerializeField]
    private GameObject mainModel;

    private bool isDownloading = false;

    [SerializeField]
    private GameObject sceneEditorControllerObj;
    public GameObject infoPopupPrefab;
    public GameObject canvasObject;

    private void Awake()
    {
        //CameraShotEventListener.onImageLoad += OnImageLoad;
        CameraShotEventListener.onImageSaved += OnImageSaved;
        CameraShotEventListener.onCancel += CancelCamera;
        CameraShotEventListener.onError += OnError;
    }
    // Use this for initialization
    void Start()
    {

        //CameraShotEventListener.onImageLoad += OnImageLoad;


        gameController = gameControllerObject.GetComponent<GameController>();
        galleryController = galleryControllerObject.GetComponent<Gallery>();
        touchController = touchControllerObject.GetComponent<TouchController>();
        panels[3].SetActive(false);
        imagePanel.SetActive(false);
        //mainPanel.SetActive(true);
        //		defaultBackground = background.texture;
        WebCamDevice[] cams = WebCamTexture.devices;

        if (cams.Length == 0)
        {
            camSupported = false;
            print("cam not supported");
            return;
        }
        else
        {
            camSupported = true;
        }

        for (int i = 0; i < cams.Length; i++)
        {
            print(cams[i].name);
            if (cams[i].isFrontFacing)
            {
                frontCam = new WebCamTexture(cams[i].name, 1280, 720);
            }
            else if (!(cams[i].isFrontFacing))
            {
                backCam = new WebCamTexture(cams[i].name, 1280, 720);
            }

            //			if (frontCam != null && backCam != null) {
            //				break;
            //			}
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OpenCamera(bool accessFrontCam = true)
    {


#if UNITY_EDITOR
        
        OnImageSaved(null, ImageOrientation.UP);

#else
        StartNativeCamera();
#endif
        gameController.ToggleCameraDownMenu(2);
        sceneEditorControllerObj.SetActive(false);


    }


    public void StartCamera()
    {
        if (cam != null)
        {
            cam.Play();
            background.texture = cam;
            cameraIsOpen = true;
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(true);

        }
    }

    public void StartNativeCamera()
    {


#if UNITY_ANDROID
        AndroidCameraShot.GetTexture2DFromCamera(false);

#elif UNITY_IPHONE
            Debug.Log("opening camera on iphone");
            IOSCameraShot.GetTexture2DFromCamera(false);
#endif

    }


    public void OnImageSaved(string path, ImageOrientation orientation)
    {
        gameController.CollectGurbage(true);

#if UNITY_EDITOR
        gameController.ShowLoading();
        
        //			DownloadImage ("http://dentedpixel.com/wp-content/uploads/2014/12/Unity5-0.png");
        StartCoroutine(DownloadImage("http://i.telegraph.co.uk/multimedia/archive/03249/archetypal-female-_3249633c.jpg"));
        //DownloadImage("https://i.pinimg.com/originals/99/96/b4/9996b4944a37318dfc00c9a34ed78c6b.png");
        //			DownloadImage("file:///C:/Users/Arun/Desktop/Gimp.jpg");
#elif UNITY_ANDROID
        //Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
        //gameController.ShowLoadingPanelOnly();
        //Handheld.StartActivityIndicator();
        path = "file://" + path;
            print("image path is ........ : " + path);
            StartCoroutine(DownloadImage(path,orientation));

#elif UNITY_IPHONE

        //Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.iOSActivityIndicatorStyle.WhiteLarge);
        //gameController.ShowLoadingPanelOnly();
        //Handheld.StartActivityIndicator();
            path = "file://" + path;
            Debug.Log("image path is ........ : " + path);
            StartCoroutine(DownloadImage(path,orientation));

#endif

    }

    public void OnError(string message)
    {
        InstantiateInfoPopup(message);
        gameController.HideLoading();
    }

    private IEnumerator DownloadImage(string imageUrl, ImageOrientation imageOrientation = ImageOrientation.UP)
    {

        bool success = false;


//#if UNITY_ANDROID
        
        //Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);

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
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                t2d.Apply();


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


                try
                {


                    processingImage.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
                    int rX = (int)(t2d.width);
                    int rY = (int)(t2d.height);

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
                }
                catch (Exception e)
                {
                    success = false;
                    Debug.Log(string.Format("Error while downloading image : ", e));
                    //yield return new WaitForEndOfFrame();
                    //print("stopping loading for camera..");
                    //gameController.HideLoading();
                }

            }

           

        }

        if (success)
        {
            yield return new WaitForSeconds(1.5f);
            Handheld.StopActivityIndicator();
            gameController.HideLoading();
            gameController.HideLoadingPanelOnly();
            GoToImageCrop();
        }
        else
        {
            yield return new WaitForFixedUpdate();
            Handheld.StopActivityIndicator();
            gameController.HideLoadingPanelOnly();
            gameController.HideLoading();
        }
       
        //gameController.HideLoading(); 



    }
    
    public void CloseCamera()
    {
        if (backCam != null && backCam.isPlaying)
        {
            backCam.Stop();
        }
        if (frontCam != null && frontCam.isPlaying)
        {
            frontCam.Stop();
        }
        if (cam != null && cam.isPlaying)
        {
            cam.Stop();
            cam = null;
        }
        else
        {
            cam = null;
        }

        cameraIsOpen = false;
    }


    Texture2D RotateImageTest(Texture2D originTexture, float angle)
    {
        Texture2D result;
        result = new Texture2D(originTexture.width, originTexture.height);
        Color32[] pix1 = result.GetPixels32();
        Color32[] pix2 = originTexture.GetPixels32();
        int W = originTexture.width;
        int H = originTexture.height;
        int x = 0;
        int y = 0;
        Color32[] pix3 = rotateSquare(pix2, (Mathf.PI / 180 * angle), originTexture);
        for (int j = 0; j < H; j++)
        {
            for (var i = 0; i < W; i++)
            {
                //pix1[result.width/2 - originTexture.width/2 + x + i + result.width*(result.height/2-originTexture.height/2+j+y)] = pix2[i + j*originTexture.width];
                pix1[result.width / 2 - W / 2 + x + i + result.width * (result.height / 2 - H / 2 + j + y)] = pix3[i + j * W];
            }
        }
        result.SetPixels32(pix1);
        result.Apply();
        return result;
    }


    Color32[] rotateSquare(Color32[] arr, float phi, Texture2D originTexture)
    {
        int x;
        int y;
        int i;
        int j;
        float sn = Mathf.Sin(phi);
        float cs = Mathf.Cos(phi);
        Color32[] arr2 = originTexture.GetPixels32();
        int W = originTexture.width;
        int H = originTexture.height;
        int xc = W / 2;
        int yc = H / 2;
        for (j = 0; j < H; j++)
        {
            for (i = 0; i < W; i++)
            {
                arr2[j * W + i] = new Color32(0, 0, 0, 0);
                x = (int)(cs * (i - xc) + sn * (j - yc) + xc);
                y = (int)(-sn * (i - xc) + cs * (j - yc) + yc);
                if ((x > -1) && (x < W) && (y > -1) && (y < H))
                {
                    arr2[j * W + i] = arr[y * W + x];
                }
            }
        }

        return arr2;
    }


    public void TakePicture()
    {
        if (cam != null)
        {
            Debug.Log("camara available");
            if (cam.isPlaying)
            {
                Debug.Log("camera playing");
                photoTaken = new Texture2D(background.texture.width, background.texture.height);
                photoTaken.SetPixels(cam.GetPixels());
                photoTaken.Apply();
                photoTaken = RotateImageTest(photoTaken, 90);
                photoTaken.Apply();
                CloseCamera();
                processingImage.sprite = Sprite.Create(photoTaken, new Rect(0, 0, photoTaken.width, photoTaken.height), new Vector2(0.5f, 0.5f), 100f);

                Texture2D ttx = MergeImage(photoTaken);
                processingImage.sprite = Sprite.Create(ttx, new Rect(0, 0, ttx.width, ttx.height), new Vector2(0.5f, 0.5f), 100f);
                photoTaken = null;

                GoToImageCrop();

            }
        }
    }
    private void GoToImageCrop()
    {
        //Go To Image Panel;
        panels[0].SetActive(true);
        panels[1].SetActive(true);
        for (int i = 2; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        touchController.ToggleMask(1);
        gameController.HideAcceptCropButton();
        imagePanel.SetActive(true);

        sceneEditorControllerObj.SetActive(false);
        //gameController.HideLoading();

    }
    public void CancelCamera()
    {
        //CloseCamera ();
        mainModel.SetActive(true);
        gameController.ToggleHomeSideMenu(1);
        panels[3].SetActive(false);
        panels[2].SetActive(true);
        panels[1].SetActive(true);
        panels[0].SetActive(true);
        sceneEditorControllerObj.SetActive(true);
        //		Debug.Log ("closing camera");
    }
    public void UpdateImage()
    {
        if (!cameraIsOpen)
        {
            return;
        }

        //		float ratio = (float)cam.width / (float)cam.height;
        //		fit.aspectRatio = ratio;
        if (!isFrontCam)
        {
            float scaleY = cam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -cam.videoRotationAngle;

            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
        else
        {
            float scaleY = cam.videoVerticallyMirrored ? 1f : -1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
            debugText.text = cam.videoVerticallyMirrored.ToString();
            int orient = -cam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }








    Texture2D ResizeTexture2D(Texture2D texture, int newWidth, int newHeight)
    {
        float warpFactor = 1.0F;
        //		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
        Texture2D destTex = new Texture2D(newWidth, newHeight);
        print("resize texture size : " + destTex.width + "   " + destTex.height);
        //		print ("Screen size : " + Screen.width + "   " + Screen.height);
        Color[] destPix = new Color[destTex.width * destTex.height];
        try
        {
            int y = 0;
            while (y < destTex.height)
            {
                int x = 0;
                while (x < destTex.width)
                {
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
        catch
        {
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



    public void InstantiateInfoPopup(String message)
    {
        GameObject g = Instantiate<GameObject>(infoPopupPrefab, canvasObject.transform);
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
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

















/*
 <?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.test.camerashot" android:installLocation="preferExternal" android:versionName="1.0" android:versionCode="1">
  <supports-screens android:smallScreens="true" android:normalScreens="true" android:largeScreens="true" android:xlargeScreens="true" android:anyDensity="true" />
  <application android:theme="@style/UnityThemeSelector" android:icon="@drawable/app_icon" android:label="@string/app_name" android:debuggable="true">
    <activity android:name="com.unity3d.player.UnityPlayerActivity" android:label="@string/app_name">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
      </intent-filter>
      <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
    </activity>

    <activity 
    android:name="com.astricstore.camerashots.CameraShotActivity"
    android:configChanges="orientation|keyboardHidden|screenSize">
</activity>
    <!--
<activity 
    android:name="eu.janmuller.android.simplecropimage.CropImage"
    android:configChanges="orientation|keyboardHidden|screenSize">
    </activity>
    -->
    <activity 
      android:name= "com.radikallabs.androidgallery.Gallery"
      android:configChanges="orientation|keyboardHidden|screenSize">
    </activity>

    <provider
            android:name="android.support.v4.content.FileProvider"
            android:authorities="com.isis.venus.provider"
            android:exported="false"
            android:grantUriPermissions="true">
            <meta-data
                android:name="android.support.FILE_PROVIDER_PATHS"
                android:resource="@xml/provider_paths"/>
        </provider>

  </application>
  <uses-sdk android:minSdkVersion="9" android:targetSdkVersion="24" />
<uses-feature android:glEsVersion="0x00020000" />
  <uses-permission android:name="android.permission.INTERNET" />
  <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.WRITE_INTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.READ_INTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.CAMERA" />
  <uses-feature android:name="android.hardware.camera" android:required="false" />
  <uses-feature android:name="android.hardware.camera.front" android:required="false" />
</manifest>
     */
