using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShot;
using System.Linq;
using System;
using UnityEngine.Networking;

public class Test : MonoBehaviour
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
        gameController = gameControllerObject.GetComponent<GameController>();
        galleryController = galleryControllerObject.GetComponent<Gallery>();
        touchController = touchControllerObject.GetComponent<TouchController>();
        panels[3].SetActive(false);
        imagePanel.SetActive(false);
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
        }
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
#if UNITY_EDITOR
        StartCoroutine(DownloadImage("http://i.telegraph.co.uk/multimedia/archive/03249/archetypal-female-_3249633c.jpg", orientation));
#elif UNITY_ANDROID
       		path = "file://" + path;
            print("image path is ........ : " + path);
			StartCoroutine(DownloadImage(path,orientation));
#elif UNITY_IPHONE
            path = "file://" + path;
            Debug.Log("image path is ........ : " + path);
			StartCoroutine(DownloadImage(path,orientation));
#endif
    }

    public void OnError(string message)
    {
        InstantiateInfoPopup(message);
    }

    private IEnumerator DownloadImage(string imageUrl, ImageOrientation orientation)
    {
        gameController.ShowLoading();
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
                //Apply rotation technique
                Texture2D newTex = new Texture2D(t2d.width, t2d.height);
                newTex.SetPixels(t2d.GetPixels());
                newTex.Apply();
                if (orientation == ImageOrientation.UP)
                {
                }
                else if (orientation == ImageOrientation.LEFT)
                {
                    newTex = rotateTexture(t2d, true);
                }
                else if (orientation == ImageOrientation.RIGHT)
                {
                    newTex = rotateTexture(t2d, false);
                }
                else
                {
                }
                t2d = new Texture2D(newTex.width,newTex.height);
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
                    GoToImageCrop();
                    DestroyImmediate(t2d, true);
                }
                catch (Exception e)
                {
                    Debug.Log(string.Format("Error while downloading image : ", e));
                }
            }
            www.Dispose();

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
        gameController.HideLoading();
        sceneEditorControllerObj.SetActive(false);
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
        b.onClick.AddListener(() => {
            Destroy(g);
        });
    }



    //Subhradeep
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