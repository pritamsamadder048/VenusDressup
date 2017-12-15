using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using DG;
using DG.Tweening;





#region NewTouchController




public class TouchController : MonoBehaviour
{

    // Use this for initialization


    [SerializeField]
    private GameObject gameControllerObject;
    private GameController gameController;
    [SerializeField]
    private GameObject maskParentObject;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField]
    private bool isTouching;
    private int touchCount;
    
    public Image mainImage;
    [SerializeField]
    private Image maskImg;
    [SerializeField]
    private bool isMaskEnabled;
    private BoxCollider2D maskImageCollider;
    private BoxCollider2D rotateCollider;
    private BoxCollider2D scaleCollider;

    [SerializeField]
    private GameObject rotatePanel;
    [SerializeField]
    private GameObject scalePanel;
    [SerializeField]
    private Rect croppedSpriteRect;


    [SerializeField]
    private GameObject cropButton;

    [SerializeField]
    private bool isRotating = false;
    //	[SerializeField]
    //	private Image tempImage;

    [SerializeField]
    private Texture tmpTexture;

    private Texture croppedTexture;

    //	private Touch prevTouch0;
    //	private Touch prevTouch1;


    private int minMaskWidth = 40;
    private int minMaskHeight = 40;
    private int maxMaskWidth = 400;
    private int maxMaskHeight = 400;
    private int minMaskScale = 40;
    private int maxMaskScale = 600;

    

    [SerializeField]
    private Image cropMask;
    [SerializeField]
    private Image croppedImage;

    
    public RawImage finalOutImage;
    public RawImage finalOutImage2;
    //[SerializeField]
    //private RawImage finalImage;


    

    public RawImage squareCroppedImage;


    [SerializeField]
    private float rotateAngle = 0f;

    [SerializeField]
    private bool shouldScale = false;
    [SerializeField]
    private bool shouldRotate = false;
    [SerializeField]
    private float scaleFactor = 1f;
    [SerializeField]
    private float rotationFactor = 1.5f;
    [SerializeField]
    private Vector2 previousScaleDelta = Vector2.zero;
    [SerializeField]
    private Vector2 previousRotationeDelta = Vector2.zero;

    [SerializeField]
    private Texture2D maskResized;



    [SerializeField]
    private GameObject sceneEditorControllerObj;





    private Touch startTouch;
    private Touch startRotateTouch;
    private Vector2 startPosition;
    private Vector3 startRotatePosition;

    public Text text;



    public float minX, maxX, minY, maxY;

    UILineRenderer rectangleLine;


    public Vector2 lastTouchPosition = Vector2.zero;


    public GameObject maskPanel;
    public bool isShowingMaskPanel=false;

    public string currentMaskName = "default_mask_00";

    public GameObject[] maskButtons;


    public Texture2D actualImage;

    public float currentActualTextureRotation=0f;

    public Camera currentCam;
    public Canvas mainCanvas;

    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        maskImg.gameObject.SetActive(false);
        isMaskEnabled = false;
        isTouching = false;
        touchCount = 0;
        maskImageCollider = maskImg.gameObject.GetComponent<BoxCollider2D>();
        //rotateCollider = rotatePanel.GetComponent<BoxCollider2D>();
        //scaleCollider = scalePanel.GetComponent<BoxCollider2D>();

        rectangleLine = maskImg.transform.GetChild(0).gameObject.GetComponent<UILineRenderer>();
        //		maskParentObject = maskImg.transform.parent.gameObject;

        print("mainImage : " + (int)mainImage.rectTransform.rect.width + "   " + (int)mainImage.rectTransform.rect.height);
        Debug.Log(string.Format("screen x {0} and height {1}", Screen.width, Screen.height));
        cropButton.SetActive(false);
    }

    public void DrawLine()
    {
        minX = 0 - (((maskImg.rectTransform.sizeDelta.x / 2) * (maskImg.transform.localScale.x)) + (rectangleLine.LineThickness));
        maxX = 0 + (((maskImg.rectTransform.sizeDelta.x / 2) * (maskImg.transform.localScale.x)) + (rectangleLine.LineThickness));

        minY = 0 - (((maskImg.rectTransform.sizeDelta.y / 2) * (maskImg.transform.localScale.y)) + (rectangleLine.LineThickness));
        maxY = 0 + (((maskImg.rectTransform.sizeDelta.y / 2) * (maskImg.transform.localScale.y)) + (rectangleLine.LineThickness));
        Vector2[] linePoints = { new Vector2(minX, minY), new Vector2(maxX, minY), new Vector2(maxX, maxY), new Vector2(minX, maxY), new Vector2(minX, minY) };

        rectangleLine.Points = linePoints;
        //buttomleft.transform.position = linePoints[0];
        //buttomright.transform.position = linePoints[1];
        //topright.transform.position = linePoints[2];
        //topleft.transform.position = linePoints[3];
    }
    // Update is called once per frame
    void Update()
    {

        DragMask();
        DrawLine();
        //ChangeScale();
        //RotateMask();
        //CheckPinch();


        //		text.text = maskImg.gameObject.GetComponent<BoxCollider2D> ().size.ToString();
        //		if (rotateAngle > 0.0f) {
        //			Texture2D ttx = new Texture2D ((int)mainImage.sprite.texture.width, (int)mainImage.sprite.texture.height);
        //			ttx.SetPixels(mainImage.sprite.texture.GetPixels());
        //			ttx.Apply ();
        //			ttx = RotateTexture (ttx, rotateAngle);
        //			ttx.Apply ();
        //			mainImage.sprite=Sprite.Create(ttx,new Rect(0f,0f,ttx.width,ttx.height),new Vector2(0.5f,0.5f),100);
        //			rotateAngle = 0;
        //		}


    }


    public IEnumerator ToggleMaskPanel(int showCode=0)
    {
        switch(showCode)
        {
            case 0:
                {
                    if(maskPanel.activeSelf)
                    {
                        maskPanel.transform.DOScale(new Vector3(.1f, .1f, .1f), .5f);
                        yield return new WaitForSeconds(.5f);
                        maskPanel.SetActive(false);
                        isShowingMaskPanel = false;
                    }
                    else
                    {
                        maskPanel.SetActive(true);
                        maskPanel.transform.DOScale(new Vector3(1f, 1f, 1f), .5f);
                        isShowingMaskPanel = true;
                    }
                    break;
                }
            case 1:
                {
                    maskPanel.SetActive(true);
                    maskPanel.transform.DOScale(new Vector3(1f, 1f, 1f),.5f);
                    isShowingMaskPanel = true;
                    break;
                }
            case 2:
                {
                    maskPanel.transform.DOScale(new Vector3(.1f, .1f, .1f), .5f);
                    yield return new WaitForSeconds(.5f);
                    maskPanel.SetActive(false);
                    isShowingMaskPanel = false;
                    break;
                }
            default:
                {
                    if (maskPanel.activeSelf)
                    {
                        maskPanel.transform.DOScale(new Vector3(.1f, .1f, .1f), .5f);
                        yield return new WaitForSeconds(.5f);
                        maskPanel.SetActive(false);
                        isShowingMaskPanel = false;
                    }
                    else
                    {
                        maskPanel.SetActive(true);
                        maskPanel.transform.DOScale(new Vector3(1f, 1f, 1f), .5f);
                        isShowingMaskPanel = true;
                    }
                    break;
                }
        }
        isShowingMaskPanel = maskPanel.activeSelf;
        
    }

    public void ToggleMask(int visibleStatus = 0)
    {

        //maskImg.rectTransform.sizeDelta = new Vector2(200, 200);
        //maskImg.rectTransform.anchoredPosition = new Vector3(100, 100, 0);

        #region SINGLEMASK
        /*
        if (visibleStatus == 1)
        {
            maskImg.rectTransform.sizeDelta = new Vector3(200f, 200f, 1f);
            maskImg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta / 2f;
            maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta;
            maskImg.rectTransform.anchoredPosition = new Vector2(400f, 745f);
            Texture2D tmptex2d = Resources.Load("images/masks/default_mask_00") as Texture2D;
            //Texture2D tmptex2d = Resources.Load("images/longdresses/female/model_001/images/longdress_1") as Texture2D;
            //print(tmptex2d);
            maskImg.sprite = Sprite.Create(tmptex2d, new Rect(0, 0, tmptex2d.width, tmptex2d.height), new Vector2(0.5f, 0.5f), 100f);

            maskImg.color = new Color(1, 1, 1, 180f / 255f);
            
            
            //Destroy(tmptex2d);
        }
        else if (visibleStatus == 2)
        {
            Texture2D tmptex2d = Resources.Load("images/masks/default_mask_00") as Texture2D;
            //Texture2D tmptex2d = Resources.Load("images/longdresses/female/model_001/images/longdress_1") as Texture2D;
            //print(tmptex2d);
            maskImg.sprite = Sprite.Create(tmptex2d, new Rect(0, 0, tmptex2d.width, tmptex2d.height), new Vector2(0.5f, 0.5f), 100f);
            maskImg.rectTransform.sizeDelta = new Vector3(200f, 200f, 1f);
            maskImg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta / 2f;
            maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta;
            maskImg.rectTransform.anchoredPosition = new Vector2(400f, 745f);
            maskImg.color = new Color(1, 1, 1, 180f / 255f);

        }
        else
        {
            print("in else");
            Texture2D tmptex2d = Resources.Load("images/masks/default_mask_00") as Texture2D;
            //Texture2D tmptex2d = Resources.Load("images/longdresses/female/model_001/images/longdress_1") as Texture2D;
            //print(tmptex2d);
            maskImg.sprite = Sprite.Create(tmptex2d, new Rect(0, 0, tmptex2d.width, tmptex2d.height), new Vector2(0.5f, 0.5f), 100f);
            maskImg.rectTransform.sizeDelta = new Vector3(200f, 200f, 1f);
            maskImg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta / 2f;
            maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta;
            maskImg.rectTransform.anchoredPosition = new Vector2(400f, 745f);
            maskImg.gameObject.SetActive(!(maskImg.gameObject.activeSelf));
            maskImg.color = new Color(1, 1, 1, 180f / 255f);

        }
        isMaskEnabled = maskImg.gameObject.activeSelf;
        if(isMaskEnabled)
        {
            cropButton.SetActive(true);
        }
        else
        {
            cropButton.SetActive(false);
            gameController.HideAcceptCropButton();
        }

        */
        #endregion SINGLEMASK



        if (visibleStatus == 1)
        {
            print("toggle mask 1");

           StartCoroutine( ToggleMaskPanel(2));

            
        }
        else if (visibleStatus == 2)
        {
            StartCoroutine(ToggleMaskPanel(1));
            print("toggle mask 2");
        }
        else
        {
            StartCoroutine(ToggleMaskPanel(0));
            print("toggle mask 0");

        }
        

    }


    public void ToggleActualMask(string maskName)
    {
        if (currentMaskName == maskName)
        {
            
            try
            {
                if (maskImg.gameObject == null)
                {
                    return;
                }
            }
            catch
            {
                return;
            }
            if (maskImg.gameObject.activeSelf)
            {
                maskImg.gameObject.SetActive(false);
            }
            else
            {
                try
                {
                    Texture2D tmptex2d = Resources.Load("images/masks/" + maskName) as Texture2D;
                    
                    if (tmptex2d != null)
                    {
                        maskImg.sprite = Sprite.Create(tmptex2d, new Rect(0, 0, tmptex2d.width, tmptex2d.height), new Vector2(0.5f, 0.5f), 100f);
                        maskImg.rectTransform.sizeDelta = new Vector3(200f, 200f, 1f);
                        maskImg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                        //maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta / 2f;
                        maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta;
                        maskImg.rectTransform.anchoredPosition = new Vector2(500f, 800f);
                        maskImg.gameObject.SetActive(true);
                        maskImg.color = new Color(1, 1, 1, 180f / 255f);
                        currentMaskName = maskName;
                    }
                    else
                    {
                        throw new Exception("Error Occured :  the texture is Null");
                    }

                }
                catch (Exception e)
                {
                    print("Error loading mask : " + e);
                    maskImg.gameObject.SetActive(false);
                }
                maskImg.gameObject.SetActive(true);
            }
            

            
        }
        else
        {
            
            try
            {
                Texture2D tmptex2d = Resources.Load("images/masks/"+maskName) as Texture2D;
                if (tmptex2d!=null)
                {
                    maskImg.sprite = Sprite.Create(tmptex2d, new Rect(0, 0, tmptex2d.width, tmptex2d.height), new Vector2(0.5f, 0.5f), 100f);
                    maskImg.rectTransform.sizeDelta = new Vector3(200f, 200f, 1f);
                    maskImg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    //maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta / 2f;
                    maskImg.gameObject.GetComponent<BoxCollider2D>().size = maskImg.rectTransform.sizeDelta;
                    maskImg.rectTransform.anchoredPosition = new Vector2(500f, 800f);
                    maskImg.gameObject.SetActive(true);
                    maskImg.color = new Color(1, 1, 1, 180f / 255f);
                    currentMaskName = maskName;
                }
                else
                {
                    throw new Exception("Error Occured :  the texture is Null");
                }

            }
            catch (Exception e)
            {
                print("Error loading mask : " + e);
                maskImg.gameObject.SetActive(false);
            }
        }


        isMaskEnabled = maskImg.gameObject.activeSelf;
        if (isMaskEnabled)
        {
            cropButton.SetActive(true);
        }
        else
        {
            cropButton.SetActive(false);
            gameController.HideAcceptCropButton();
        }
    }

    private void OnDisable()
    {
        ToggleActualMask(currentMaskName);
    }

    public void DisableCurrentMask()
    {
        
        ResetAllMaskButtons();
    }

    public void ResetAllMaskButtons()
    {
        foreach(GameObject g in maskButtons)
        {
            g.GetComponent<Toggle>().isOn = false;
            g.transform.GetChild(0).gameObject.SetActive(false);
        }
        maskImg.gameObject.SetActive(false);
    }



   


   



public void DragMaskOriginal()
    {
        Vector3 newPos;
        if (isMaskEnabled)
        {
            //			print ("mask enabled");
            //			print ("mouse press count : " + Input.GetKey (KeyCode.Mouse0));
            if (Input.touchCount == 1 || Input.GetKey(KeyCode.Mouse0))
            {
                //				print ("touching only one");
                isTouching = true;
            }
            else if (Input.touchCount == 0 || !Input.GetKey(KeyCode.Mouse0))
            {
                //				print ("not touching now");
                isTouching = false;
            }
        }
        else
        {
            if (isTouching)
            {
                isTouching = false;
            }

            return;
        }
        if (isMaskEnabled && isTouching)
        {


            if (Application.isMobilePlatform)
            {
                ray = new Ray(Input.GetTouch(0).position, Vector3.forward);
                newPos = Input.GetTouch(0).position;

            }
            else
            {

                ray = new Ray(Input.mousePosition, Vector3.forward);
                newPos = Input.mousePosition;
                //				print (Input.mousePosition);
            }


            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);


            if (maskImageCollider == Physics2D.OverlapPoint(ray.origin))
            {
                //			
                if (ray.origin != maskImg.transform.position)
                {


                    //					maskImg.transform.position =new Vector3( Input.mousePosition.x-50f,Input.mousePosition.y-50f,1f);
                    maskImg.transform.position = newPos;


                }
                //				print ("hit");
            }
            else
            {
                //				print ("not hitting : ");
                //				maskImg.transform.SetParent (maskImgParent.transform);
            }

        }
        else
        {
            //			maskImg.transform.SetParent (maskImgParent.transform);
        }


    }


    public void DragMask()
    {

        Vector2 newPos;
        if (gameObject.activeSelf)
        {
            //			print ("mask enabled");
            //			print ("mouse press count : " + Input.GetKey (KeyCode.Mouse0));
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1)
                {
                    //				print ("touching only one");
                    isTouching = true;
                }
                else
                {
                    //				print ("not touching now");
                    isTouching = false;
                    lastTouchPosition = Vector2.zero;
                }

            }
            else
            {
                if ((Input.GetKey(KeyCode.Mouse0)) && ((!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))))
                {
                    //				print ("touching only one");
                    isTouching = true;
                }
                else
                {
                    //				print ("not touching now");
                    isTouching = false;
                    lastTouchPosition = Vector2.zero;
                }

            }
        }
        else
        {
            if (isTouching)
            {
                isTouching = false;
                lastTouchPosition = Vector2.zero;
            }

            return;
        }
        if (maskImg.gameObject.activeSelf && isMaskEnabled && isTouching)
        {


            if (Application.isMobilePlatform)
            {
                //ray = new Ray(Input.GetTouch(0).position, Vector3.forward);
                newPos = Input.GetTouch(0).position;

            }
            else
            {

                //ray = new Ray(Input.mousePosition, Vector3.forward);
                newPos = Input.mousePosition;

                //				print (Input.mousePosition);
            }

            if (lastTouchPosition == Vector2.zero)
            {
                lastTouchPosition = newPos;
            }
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

            Vector2 deltaMovement = newPos - lastTouchPosition;

            print(string.Format("delta movement : {0}", deltaMovement));

            if (deltaMovement != Vector2.zero)
            {
                maskImg.transform.position += new Vector3(deltaMovement.x, deltaMovement.y, 0);
            }
            lastTouchPosition = newPos;
           

        }
        else
        {
            //			maskImg.transform.SetParent (maskImgParent.transform);
        }


    }

    private Rect GetRectFromCorners(Vector3[] corners)
    {
        Rect rect = new Rect();
        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        float deltaX = maxX - minX;
        float deltaY = maxY - minY;

        rect.x = (int)minX;
        rect.y = (int)minY;
        rect.width = (int)deltaX;
        rect.height = (int)deltaY;

        return rect;
    }

    public void CropImage()
    {
        //Debug.Log("Inside rotated image");
        gameController.ShowLoadingPanelOnly();
        print("Starting Cropping");
        CropImageByTamal();
        
        
        print("finished cropping");
        return;
    }

    void CropImageByTamal()
    {

        Rect spriteRect = maskImg.rectTransform.rect;
        //		print("mask image rect : " + croppedSpriteRect);
        Texture2D mainSourceTexture2D = GetTexture2D();
        mainSourceTexture2D.Apply();
        //if(maskImg.transform.eulerAngles.z != 0)
        //sourceTexture2D = RotateImageTest(sourceTexture2D, maskImg.transform.eulerAngles.z);

        Vector3[] corners = new Vector3[4];
        maskImg.rectTransform.GetWorldCorners(corners);
        //corners = TransformToAspectPoints(corners);
        corners = TransformToAspectPointsForScreenSpaceCamera(corners);

        Rect newMaskRect = GetRectFromCorners(corners);

        Texture2D sourceTexture2D = CropTexture2D(mainSourceTexture2D, newMaskRect);
        sourceTexture2D.Apply();
        squareCroppedImage.texture = (Texture)sourceTexture2D;
        squareCroppedImage.rectTransform.sizeDelta = new Vector2(newMaskRect.width, newMaskRect.height);

        // adjusting the mask
        maskResized = ResizeTexture2D(maskImg.sprite.texture, (int)maskImg.rectTransform.rect.width, (int)maskImg.rectTransform.rect.height);

        Texture2D background = new Texture2D((int)newMaskRect.width, (int)newMaskRect.height);  //background is the equal size of new resized mask rect
        Color fillColor = Color.clear;
        //Color[] fillPixels = new Color[background.width * background.height];
        //for (int i = 0; i < fillPixels.Length; i++)
        //{
        //    fillPixels[i] = fillColor;
        //}
        
        Color[] fillPixels = Enumerable.Repeat(fillColor, (background.width * background.height)).ToArray();

        background.SetPixels(fillPixels);
        background.Apply();

        Vector2 offset = new Vector2(((background.width - maskResized.width) / 2), ((background.height - maskResized.height) / 2));

        for (int y = 0; y < maskResized.height; y++)
        {
            for (int x = 0; x < maskResized.width; x++)
            {
                Color PixelColorFore = maskResized.GetPixel(x, y);// * maskResized.GetPixel(x, y).a;
                background.SetPixel((int)(x + offset.x), (int)(y + offset.y), PixelColorFore);
            }
        }

        background.Apply();
        maskResized = background;
        maskResized.Apply();

        if (maskImg.transform.eulerAngles.z != 0)
        {
            maskResized = RotateImageTest(background, maskImg.transform.eulerAngles.z);
            maskResized.Apply();
        }

        // final masked output
        Texture2D combinedImage = GetMaskedImage(sourceTexture2D, maskResized);

        if (maskImg.transform.eulerAngles.z != 0)
        { 
            combinedImage = RotateImageTest(combinedImage, -maskImg.transform.eulerAngles.z);
        }
        //Sprite cropcombinedSprite = Sprite.Create(combinedImage, new Rect(0, 0, combinedImage.width, combinedImage.height), new Vector2(.5f, .5f), 100);

        if(gameController.currentlySelectedFace==1)
        {
            finalOutImage.texture = (Texture)combinedImage;
            finalOutImage.rectTransform.sizeDelta = new Vector2(newMaskRect.width, newMaskRect.height);
            finalOutImage.gameObject.GetComponent<BoxCollider2D>().size = finalOutImage.rectTransform.sizeDelta / 2f;
            //gameController.currentlySelectedFace = 1;
            maskImg.sprite = Sprite.Create(((Texture2D)finalOutImage.texture), new Rect(0, 0, finalOutImage.texture.width, finalOutImage.texture.height), new Vector2(0.5f, 0.5f), 100f);
        }
        else if(gameController.currentlySelectedFace==2)
        {
            finalOutImage2.texture = (Texture)combinedImage;
            finalOutImage2.rectTransform.sizeDelta = new Vector2(newMaskRect.width, newMaskRect.height);
            finalOutImage2.gameObject.GetComponent<BoxCollider2D>().size = finalOutImage2.rectTransform.sizeDelta / 2f;
            //gameController.currentlySelectedFace = 0;
            maskImg.sprite = Sprite.Create(((Texture2D)finalOutImage2.texture), new Rect(0, 0, finalOutImage2.texture.width, finalOutImage2.texture.height), new Vector2(0.5f, 0.5f), 100f);

        }
        gameController.ShowAcceptCropButton();


        if (gameController.IsUsingCustomFace() && gameController.currentlySelectedFace == 1 && !gameController.IsUsingCustomFace2())
        {
            gameController.currentlyUsingFace = 1;
            return;
        }
        else if (gameController.IsUsingCustomFace2() && gameController.currentlySelectedFace == 2 && !gameController.IsUsingCustomFace())
        {
            gameController.currentlyUsingFace = 1;
            return;
        }
        gameController.currentlyUsingFace += 1;
        if (gameController.currentlyUsingFace > 2)
        {
            gameController.currentlyUsingFace = 2;
        }



        gameController.HideLoadingPanelOnly();
    }

    Texture2D CropTexture2D(Texture2D mTexture, Rect rect)
    {
        //Color[] c = mTexture.Getpi
        int rx = (int)rect.x;
        int ry = (int)rect.y;
        int rw = (int)rect.width;
        int rh = (int)rect.height;

        //print(string.Format("before rx: {0} ry: {1} rw: {2} rh: {3}", rx, ry, rw, rh));
        //if(rx<0)
        //{
        //    rx = 0;
        //}

        //if(ry<0)
        //{
        //    ry = 0;
        //}
        //if((rx+rw)>mTexture.width)
        //{
        //    int m = (rx + rw) - mTexture.width;
        //    rw -= m;
        //}
        //if ((ry + rh) > mTexture.height)
        //{
        //    int m = (ry + rh) - mTexture.height;
        //    rh -= m;
        //}
        //print(string.Format("after rx: {0} ry: {1} rw: {2} rh: {3}", rx, ry, rw, rh));

        //Color[] c = mTexture.GetPixels(rx, ry, rw, rh);

        Texture2D m2Texture = new Texture2D(rw, rh);
        
        for(int y=ry;y<ry+rh;y++)
        {
            for(int x=rx;x<rx+rw;x++)
            {
                Color c2=Color.clear;
                try
                {
                    if(x<0||y<0)
                    {
                        c2 = Color.red;
                        throw new Exception("negative index cant crop from here");
                        
                    }
                    //else if(x>Screen.width || y>Screen.height)
                    else if(x>mainImage.rectTransform.rect.width || y>mainImage.rectTransform.rect.height)
                    {
                        c2 = Color.blue;
                        throw new Exception("index out of screen cant crop from here");
                        
                    }
                    c2 = mTexture.GetPixel(x, y);
                }
                catch(Exception e)
                {
                    //print(string.Format("error : {0}",e.Message));
                    //c2 = Color.clear;
                }
                int i = (x - rx);
                int j = (y - ry);
                
                m2Texture.SetPixel(i,j, c2);
            }
        }
        
        m2Texture.Apply();
        //m2Texture.SetPixels(c);
        
        //m2Texture.Apply();
        return m2Texture;
    }


    Texture2D GetTexture2D()
    {
        float warpFactor = 1.0F;
        //Texture2D destTex = new Texture2D(Screen.width, Screen.height);
        Texture2D destTex = new Texture2D((int)mainImage.rectTransform.rect.width, (int)mainImage.rectTransform.rect.height);

        //		print ("Screen size : " + Screen.width + "   " + Screen.height);
        //Color[] destPix = new Color[destTex.width * destTex.height];
        try
        {
            //non good region
            /*
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
                    destPix[y * destTex.width + x] = mainImage.sprite.texture.GetPixelBilinear(warpXFrac, warpYFrac);
                    x++;
                }
                y++;
                */
                destTex.SetPixels(mainImage.sprite.texture.GetPixels());
            destTex.Apply();
        }
        catch
        {
            destTex = null;
        }

        return destTex;
    }

    public Texture2D ScaleAccordingRatio(Texture2D texture,int width,bool widthControlHeight=true)
    {
        float textureWidth = texture.width;
        float textureHeight = texture.height;

        int ratioX =(int) textureWidth;
        int ratioY =(int) textureHeight;

        GetRatio(ref ratioX,ref ratioY);

        //print(string.Format("Ratio = {0}:{1} ", ratioX, ratioY));

        Texture2D tmpTex=texture ;
        return tmpTex;
    }

    public void GetRatio(ref int x ,ref int y)
    {
        int tx = x;
        int ty = y;

        int a = x;
        int b = y;
        if(a>b)
        {
            a = y;
            b = x;
        }
        int r = 0;

        do
        {
            
            r = b % a;
            //print(string.Format("a : {0} , b : {1} , r : {2}", a, b, r));
            b = a ;
            a = r;
        } while (r > 0);
        tx = x / b;
        ty = y / b;

        x = tx;
        y = ty;
    }

    Texture2D ResizeTexture2D(Texture2D texture, int newWidth, int newHeight)
    {
        float warpFactor = 1.0F;
        //		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
        Texture2D destTex = new Texture2D(newWidth, newHeight);
        //print("resize texture size : " + destTex.width + "   " + destTex.height);
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
        catch (Exception e)
        {
            print("Error In Resize Texture 2D : " + e);
            destTex = null;
        }

        //destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);




        return destTex;
    }


    Texture2D GetMaskedImage(Texture2D orig, Texture2D mask)
    {
        Texture2D finalOutPut = new Texture2D((int)orig.width, (int)orig.height);

        //print("mask img texture size : " + mask.width + "   " + mask.height);
        //print("origImgTexture size : " + orig.width + "    " + orig.height);
        //print("maskImgTexture size : " + mask.width + "    " + mask.height);

        for (int i = 0; i < (int)mask.width; i++)
        {
            for (int j = 0; j < (int)mask.height; j++)
            {
                Color setterColor = orig.GetPixel(i, j);
                Color maskColor = mask.GetPixel(i, j);
                if(setterColor.a==0)
                {
                    setterColor = Color.clear;
                }
                else
                {
                    setterColor.a = maskColor.a;
                }
                finalOutPut.SetPixel(i, j, setterColor);
                /*  //importent it is the real deal
                if (mask.GetPixel(i, j).a == 0f)
                {
                    finalOutPut.SetPixel(i, j, Color.clear);
                }
                else
                {
                    finalOutPut.SetPixel(i, j, orig.GetPixel(i, j));
                }
                */

                //    if (mask.GetPixel(i, j).a > 0f)
                //    {
                //        finalOutPut.SetPixel(i, j, orig.GetPixel(i, j));

                //    }
                //    else
                //    {
                //        finalOutPut.SetPixel(i, j, Color.clear);
                //    }
            }
        }
        finalOutPut.Apply();
        return finalOutPut;
    }

    #region Pritam


    public Texture2D MergeImage(Texture2D Overlay, Texture2D Background = null, Texture2D newTexture = null)
    {
        print(string.Format("overlay width : {0}  overlay height{1}  pimg rect trnsfrm width : {2} pimg rect trnsfrm Height : {3} ", Overlay.width, Overlay.height, mainImage.rectTransform.rect.width, mainImage.rectTransform.rect.height));
        if ((Overlay.width > mainImage.rectTransform.rect.width) || (Overlay.height > mainImage.rectTransform.rect.height))
        {
            //			Overlay.Resize (1300,1300, Overlay.format, false);
            //			Overlay.Apply ();
            int resizeHeight = Overlay.height;
            int resizeWidth = Overlay.width;

            if (Overlay.width > mainImage.rectTransform.rect.width)
            {
                resizeWidth = (int)mainImage.rectTransform.rect.width;
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
            Background = new Texture2D((int)mainImage.rectTransform.rect.width, (int)mainImage.rectTransform.rect.height, TextureFormat.ARGB32, false);
            Color fillColor = Color.clear;
            Color[] fillPixels = new Color[Background.width * Background.height];

            for (int i = 0; i < fillPixels.Length; i++)
            {
                fillPixels[i] = fillColor;
            }

            Background.SetPixels(fillPixels);
            Background.Apply();
        }
        if (newTexture == null)
        {
            newTexture = new Texture2D((int)mainImage.rectTransform.rect.width, (int)mainImage.rectTransform.rect.height, TextureFormat.ARGB32, false);
            print("big image rect : " + mainImage.rectTransform.rect);
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


    Vector3[] TransformToAspectPoints(Vector3[] corners)
    {
        //Vector2[] aspectCorners = new Vector2[4];
        
        for (int i = 0; i < corners.Length; i++)
        {
            print(string.Format("before aspect corners were : {0}", corners[i]));
            corners[i] = new Vector2(AspectX(corners[i].x), AspectY(corners[i].y));
            print(string.Format("after aspect corners were : {0}", corners[i]));
        }
        
        return corners;
    }

    float AspectX(float x)
    {
        Rect source = mainImage.rectTransform.rect;
        x = (source.width / Screen.width) * x;
        return x;
    }

    float AspectY(float y)
    {
        Rect source = mainImage.rectTransform.rect;
        y = (source.height / Screen.height) * y;
        return y;
    }


    Vector3[] TransformToAspectPointsForScreenSpaceCamera(Vector3[] corners)
    {
        //Vector2[] aspectCorners = new Vector2[4];
        float scaleFactor = mainCanvas.scaleFactor;

        for (int i = 0; i < corners.Length; i++)
        {
            print(string.Format("before aspect corners were : {0}", corners[i]));
            //corners[i] = new Vector2(AspectXScreenSpaceCamera(corners[i].x), AspectYScreenSpaceCamera(corners[i].y));
            corners[i] = currentCam.WorldToScreenPoint(corners[i]);
            //corners[i].z = 0f;
            corners[i] = new Vector3(Mathf.CeilToInt(corners[i].x / scaleFactor), Mathf.CeilToInt(corners[i].y / scaleFactor), 0f);
            print(string.Format("after aspect corners were : {0}", corners[i]));
        }

        return corners;
    }


    float AspectXScreenSpaceCamera(float x)
    {
        Rect source = mainImage.rectTransform.rect;
        x = (source.width / Screen.width) * x;

        return x;
    }

    float AspectYScreenSpaceCamera(float y)
    {
        Rect source = mainImage.rectTransform.rect;
        y = (source.height / Screen.height) * y;
        return y;
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

    public void RotateImagePositive90Degree()
    {
        currentActualTextureRotation += 90;
        if(currentActualTextureRotation>270)
        {
            currentActualTextureRotation = 0f;
        }
        RotateActualImage(actualImage, currentActualTextureRotation);
    }
    public void RotateImageNegative90Degree()
    {
        currentActualTextureRotation -= 90;
        if (currentActualTextureRotation < -270)
        {
            currentActualTextureRotation = 0f;
        }
        RotateActualImage(actualImage, currentActualTextureRotation);
    }
    public void RotateActualImage(Texture2D img, float angle)
    {
        if (angle == 0f)
        {
            mainImage.sprite.texture.SetPixels(MergeImage(actualImage).GetPixels());
            mainImage.sprite.texture.Apply();
        }
        else
        {
            Texture2D rotatedMainImage = RotateImageTest(img, angle);
            rotatedMainImage.Apply();
            
            mainImage.sprite.texture.SetPixels(MergeImage(rotatedMainImage).GetPixels());
            mainImage.sprite.texture.Apply();
        }
        
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

    #endregion

}



#endregion











#region PreviousTouchController
//public class TouchController : MonoBehaviour {

//	// Use this for initialization



//	[SerializeField]
//	private GameObject maskParentObject;
//	private Ray ray;
//	private RaycastHit hit;
//	[SerializeField]
//	private bool isTouching;
//	private int touchCount;
//	[SerializeField]
//	private Image mainImage;
//	[SerializeField]
//	private Image maskImg;
//	[SerializeField]
//	private bool isMaskEnabled;
//	private BoxCollider2D maskImageCollider;
//	[SerializeField]
//	private Rect croppedSpriteRect;

//	[SerializeField]
//	private Texture tmpTexture;

//	private Texture croppedTexture;

////	private Touch prevTouch0;
////	private Touch prevTouch1;


//	private int minMaskWidth = 40;
//	private int minMaskHeight = 40;
//	private int maxMaskWidth = 400;
//	private int maxMaskHeight = 400;


//	[SerializeField]
//	private Image cropMask;
//	[SerializeField]
//	private Image croppedImage;

//	[SerializeField]
//	private RawImage finalOutImage;
//	private RawImage finalImage;


//	[SerializeField]
//	private float rotateAngle = 0f;

//	public Text text;
//	void Start () {
//		maskImg.gameObject.SetActive (false);
//		isMaskEnabled = false;
//		isTouching = false;
//		touchCount = 0;
//		maskImageCollider = maskImg.gameObject.GetComponent<BoxCollider2D> ();



//	}

//	// Update is called once per frame
//	void Update () {

//        DragMask();
//        //CheckPinch ();

//    }

//	public void ToggleMask(int visibleStatus=0)
//	{
//		maskImg.rectTransform.sizeDelta = new Vector2 (200,200);
//		maskImg.rectTransform.anchoredPosition = new Vector3 (100, 100, 0);
//		if (visibleStatus == 1) {
//			maskImg.gameObject.SetActive (false);
//		}
//		else if (visibleStatus == 2) {
//			maskImg.gameObject.SetActive (true);
//		}
//		else {
//			print ("in else");
//			maskImg.gameObject.SetActive (!(maskImg.gameObject.activeSelf));
//		}
//		isMaskEnabled = maskImg.gameObject.activeSelf;
//	}



//	public void CheckPinch()
//	{
//		if (!isMaskEnabled) {
//			return;
//		}
//		if (Input.touchCount >= 2) {

//			// Store both touches.
//			Touch touchZero = Input.GetTouch (0);
//			Touch touchOne = Input.GetTouch (1);

//			// Find the position in the previous frame of each touch.
//			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
//			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

//			// Find the magnitude of the vector (the distance) between the touches in each frame.
//			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
//			Vector2 prevTouchDelta = touchZeroPrevPos - touchOnePrevPos;

//			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
//			Vector2 currentTouchDelta = touchZero.position - touchOne.position;

//			Vector2 diffTouchDelta = prevTouchDelta - currentTouchDelta;
//			// Find the difference in the distances between each frame.
//			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
//			float width = 100;
//			float height = 100;
//			if (Mathf.Abs (diffTouchDelta.x) >= Mathf.Abs (diffTouchDelta.y)) {

//				if (deltaMagnitudeDiff != 0) {

//					if (((maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x) < maxMaskWidth) && ((maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x) >= minMaskWidth)) {
//						width = maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x;
//					}
//					else if((maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x) >= maxMaskWidth)
//					{
//						width=maxMaskWidth;
//					}
//					else if ((maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x) <= minMaskWidth){
//						width=minMaskWidth;
//					}

//					maskImg.rectTransform.sizeDelta = new Vector2 (width, maskImg.rectTransform.sizeDelta.y);
//					maskImg.gameObject.GetComponent<BoxCollider2D> ().size = maskImg.rectTransform.sizeDelta;


//				}

//			}
//			else 
//			{
//				if (deltaMagnitudeDiff != 0) {
//					if (((maskImg.rectTransform.sizeDelta.y + diffTouchDelta.y) < maxMaskHeight) && ((maskImg.rectTransform.sizeDelta.y + diffTouchDelta.y) >= minMaskHeight)) {
//						height = maskImg.rectTransform.sizeDelta.y + diffTouchDelta.y;
//					}
//					else if((maskImg.rectTransform.sizeDelta.y + diffTouchDelta.y) >= maxMaskHeight)
//					{
//						height=maxMaskHeight;
//					}
//					else if ((maskImg.rectTransform.sizeDelta.x + diffTouchDelta.x) <= minMaskHeight){
//						width=minMaskHeight;
//					}

//					maskImg.rectTransform.sizeDelta = new Vector2 (maskImg.rectTransform.sizeDelta.x, height);
//					maskImg.gameObject.GetComponent<BoxCollider2D> ().size = maskImg.rectTransform.sizeDelta;

//				}					
//				} 
//			}
//	} 


//	public void DragMask()
//	{
//		Vector3 newPos;
//		if (isMaskEnabled) {

////			print ("mouse press count : " + Input.GetKey (KeyCode.Mouse0));
//			if (Input.touchCount == 1 || Input.GetKey (KeyCode.Mouse0)) {

//				isTouching = true;
//			} else if (Input.touchCount == 0 || !Input.GetKey (KeyCode.Mouse0)) {

//				isTouching = false;
//			}
//		} else {
//			if (isTouching) {
//				isTouching = false;
//			}

//			return;
//		}
//		if (isMaskEnabled && isTouching) {


//			if (Application.isMobilePlatform) {
//				ray = new Ray (Input.GetTouch (0).position, Vector3.forward);
//				newPos = Input.GetTouch (0).position;

//			} else {

//				ray = new Ray (Input.mousePosition, Vector3.forward);
//				newPos = Input.mousePosition;

//			}


//			Debug.DrawRay (ray.origin, ray.direction * 100, Color.green);


//			if (maskImageCollider == Physics2D.OverlapPoint (ray.origin)) {

//				if (ray.origin != maskImg.transform.position) {



//					maskImg.transform.position=Input.mousePosition;


//				}

//			} else {

//			}

//		} 
//		else 
//		{

//		}


//	}
//    private Rect GetRectFromCorners(Vector3[] corners)
//    {
//        Rect rect = new Rect();
//        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
//        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
//        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
//        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

//        float deltaX = maxX - minX;
//        float deltaY = maxY - minY;

//        rect.x = (int)minX;
//        rect.y = (int)minY;
//        rect.width = (int)deltaX;
//        rect.height = (int)deltaY;

//        return rect;
//    }
//    public void CropImage()
//	{


//		Rect spriteRect = maskImg.rectTransform.rect;

//		croppedSpriteRect = spriteRect;
//		print ("mask image rect : " + croppedSpriteRect);
//		Texture2D sourceTexture2D = GetTexture2D();

//		croppedSpriteRect = new Rect ((float)System.Math.Round (maskImg.rectTransform.anchoredPosition.x, 2),(float)System.Math.Round (maskImg.rectTransform.anchoredPosition.y, 2), spriteRect.width, spriteRect.height);
//		croppedSpriteRect.x = croppedSpriteRect.x - croppedSpriteRect.width / 2;
//		croppedSpriteRect.y = croppedSpriteRect.y - croppedSpriteRect.height / 2;
//		print ("cropped sprite rect before : " + croppedSpriteRect);

//		Sprite croppedSprite = Sprite.Create(sourceTexture2D,croppedSpriteRect , new Vector2(.5f, .5f), 100);


//		cropMask.rectTransform.sizeDelta=maskImg.rectTransform.sizeDelta;

//		cropMask.rectTransform.eulerAngles=maskImg.rectTransform.eulerAngles;
//		croppedImage.sprite = croppedSprite;
//		croppedImage.transform.eulerAngles = Vector3.zero;


//		Texture2D finalTexture2d=GetMaskedImage (croppedImage, cropMask);
//		finalTexture2d.Apply ();

//		finalOutImage.texture =(Texture) finalTexture2d;
//		finalOutImage.rectTransform.sizeDelta = new Vector2 (finalTexture2d.width, finalTexture2d.height);




//	}

//	public RawImage GetCroppedRawImage()
//	{
//		return finalOutImage;
//	}


//	Texture2D GetTexture2D(){
//		float warpFactor = 1.0F;
////		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
//		Texture2D destTex = new Texture2D((int)mainImage.rectTransform.rect.width, (int)mainImage.rectTransform.rect.height);
//		print ("dest tex prev size : " + destTex.width + "   " + destTex.height);
////		print ("Screen size : " + Screen.width + "   " + Screen.height);
//		Color[] destPix = new Color[destTex.width * destTex.height];
//		try
//		{
//			int y = 0;
//			while (y < destTex.height) {
//				int x = 0;
//				while (x < destTex.width) {
//					float xFrac = x * 1.0F / (destTex.width - 1);
//					float yFrac = y * 1.0F / (destTex.height - 1);
//					float warpXFrac = Mathf.Pow(xFrac, warpFactor);
//					float warpYFrac = Mathf.Pow(yFrac, warpFactor);
//					destPix[y * destTex.width + x] = mainImage.sprite.texture.GetPixelBilinear(warpXFrac, warpYFrac);
//					x++;
//				}
//				y++;
//			}
//			destTex.SetPixels(destPix);
//			destTex.Apply();

//		}
//		catch {
//			destTex = null;
//		}

//		//destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);

//		return destTex;
//	}

//	Texture2D ResizeTexture2D(Texture2D texture,int newWidth,int newHeight){
//		float warpFactor = 1.0F;
//		//		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
//		Texture2D destTex = new Texture2D(newWidth, newHeight);
//		print ("resize texture size : " + destTex.width + "   " + destTex.height);
//		//		print ("Screen size : " + Screen.width + "   " + Screen.height);
//		Color[] destPix = new Color[destTex.width * destTex.height];
//		try
//		{
//			int y = 0;
//			while (y < destTex.height) {
//				int x = 0;
//				while (x < destTex.width) {
//					float xFrac = x * 1.0F / (destTex.width - 1);
//					float yFrac = y * 1.0F / (destTex.height - 1);
//					float warpXFrac = Mathf.Pow(xFrac, warpFactor);
//					float warpYFrac = Mathf.Pow(yFrac, warpFactor);
//					destPix[y * destTex.width + x] = texture.GetPixelBilinear(warpXFrac, warpYFrac);
//					x++;
//				}
//				y++;
//			}
//			destTex.SetPixels(destPix);
//			destTex.Apply();

//		}
//		catch {
//			destTex = null;
//		}

//		//destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);

//		return destTex;
//	}
//	private Texture2D GetMaskedImage(Image orig,Image mask)
//	{
//		Texture2D fTexture2d=new Texture2D((int)mask.rectTransform.rect.width,(int)mask.rectTransform.rect.height);
//		Texture2D origImgTexture = new Texture2D((int)orig.sprite.textureRect.width,(int)orig.sprite.textureRect.height);
//		Texture2D maskImgTexture =new  Texture2D ((int)mask.sprite.texture.width,(int) mask.sprite.texture.height);
//		maskImgTexture.SetPixels (mask.sprite.texture.GetPixels ());
//		maskImgTexture.Apply ();

//		print ("mask img texture size : "+maskImgTexture.width + "   " + maskImgTexture.height);

//		maskImgTexture = ResizeTexture2D (maskImgTexture, (int)mask.rectTransform.rect.width, (int)mask.rectTransform.rect.height);
//		maskImgTexture.Apply ();
////		return maskImgTexture;

//		Color[] pixels=orig.sprite.texture.GetPixels(  (int)orig.sprite.textureRect.x, 
//			(int)orig.sprite.textureRect.y, 
//			(int)orig.sprite.textureRect.width, 
//			(int)orig.sprite.textureRect.height );

//		origImgTexture.SetPixels (pixels);
//		origImgTexture.Apply ();
////		return origImgTexture;
//		print ("origImgTexture size : " + origImgTexture.width + "    " + origImgTexture.height);
//		print ("maskImgTexture size : " + maskImgTexture.width + "    " + maskImgTexture.height);


////		
//		for (int i = 0; i < (int)maskImgTexture.width; i++) {
//			for (int j = 0; j < (int)maskImgTexture.height; j++) {
//				if (maskImgTexture.GetPixel (i, j).a ==0) {
////					print ("clear");
//					fTexture2d.SetPixel (i, j, Color.clear);
//				}
//				else {
////					print ("color");


//					fTexture2d.SetPixel (i, j, origImgTexture.GetPixel (i, j));
//				}
//			}
//		}
//		fTexture2d.Apply ();
//		return fTexture2d;
//	}

//	Texture2D RotateTexture(Texture2D tex, float angle)
//	{
//		Debug.Log("rotating");
//		Texture2D rotImage = new Texture2D(tex.width, tex.height);
//		int  x,y;
//		float x1, y1, x2,y2;

//		int w = tex.width;
//		int h = tex.height;
//		float x0 = rot_x (angle, -w/2.0f, -h/2.0f) + w/2.0f;
//		float y0 = rot_y (angle, -w/2.0f, -h/2.0f) + h/2.0f;

//		float dx_x = rot_x (angle, 1.0f, 0.0f);
//		float dx_y = rot_y (angle, 1.0f, 0.0f);
//		float dy_x = rot_x (angle, 0.0f, 1.0f);
//		float dy_y = rot_y (angle, 0.0f, 1.0f);


//		x1 = x0;
//		y1 = y0;

//		for (x = 0; x < tex.width; x++) {
//			x2 = x1;
//			y2 = y1;
//			for ( y = 0; y < tex.height; y++) {
//				//rotImage.SetPixel (x1, y1, Color.clear);          

//				x2 += dx_x;//rot_x(angle, x1, y1);
//				y2 += dx_y;//rot_y(angle, x1, y1);
//				rotImage.SetPixel ( (int)Mathf.Floor(x), (int)Mathf.Floor(y), getPixel(tex,x2, y2));
//			}

//			x1 += dy_x;
//			y1 += dy_y;

//		}

//		rotImage.Apply();
//		return rotImage;
//	}

//	private Color getPixel(Texture2D tex, float x, float y)
//	{
//		Color pix;
//		int x1 = (int) Mathf.Floor(x);
//		int y1 = (int) Mathf.Floor(y);

//		if(x1 > tex.width || x1 < 0 ||
//			y1 > tex.height || y1 < 0) {
//			pix = Color.clear;
//		} else {
//			pix = tex.GetPixel(x1,y1);
//		}

//		return pix;
//	}

//	private float rot_x (float angle, float x, float y) {
//		float cos = Mathf.Cos(angle/180.0f*Mathf.PI);
//		float sin = Mathf.Sin(angle/180.0f*Mathf.PI);
//		return (x * cos + y * (-sin));
//	}
//	private float rot_y (float angle, float x, float y) {
//		float cos = Mathf.Cos(angle/180.0f*Mathf.PI);
//		float sin = Mathf.Sin(angle/180.0f*Mathf.PI);
//		return (x * sin + y * cos);
//	}
//}
#endregion
