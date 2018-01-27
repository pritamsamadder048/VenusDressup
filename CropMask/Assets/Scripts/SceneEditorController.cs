using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneEditorController : MonoBehaviour {


    public Image dressImage;
    public Image wigImage;
    public Image ornamentImage;
    public Image shoeImage;

    public Image maleWigImage;
    public Image maleTieImage;




    public Image sceneObjectFace;
    public Image sceneObjectFace2;
    
    public Image sceneObjectDress;
    public Image sceneObjectWig;
    public Image sceneObjectOrnament;
    public Image sceneObjectShoe;

    public Image sceneObjectMaleWig;
    public Image sceneObjectMaleTie;

    
    public int tapTime = 0;

    
    public int maxTapTime = 30;


    
    public GameObject editPopup;

    public GameObject sceneObjectFacePrefab;
    public GameObject sceneObjectFace2Prefab;

    public GameObject sceneObjectDressPrefab;
    public GameObject sceneObjectWigPrefab;
    public GameObject sceneObjectOrnamentPrefab;
    public GameObject sceneObjectShoePrefab;

    public GameObject sceneObjectMaleWigPrefab;
    public GameObject sceneObjectMaleTiePrefab;

    public GameObject container;

    public int totalObjects = 0;

    [SerializeField]
    GameObject gameControllerObj;
    private GameController gameController;


    [SerializeField]
    private bool isShowingSceneObjectsPanel = false;


    [SerializeField]
    private GameObject nothingThereToSelectText;
    


    // Use this for initialization
    void Start () {
        gameController = gameControllerObj.GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckFingerTap();

    }


    public void ShowObjectsInScenePanel()
    {
        totalObjects = 0;
        if(gameController==null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if(gameController.faceRawImage.color.a>0f && gameController.faceRawImage.gameObject.activeSelf && gameController.faceRawImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectFacePrefab, container.transform);
            sceneObjectFace = g.GetComponent<Image>();
            g.GetComponent<Button>().onClick.AddListener(OnClickFaceSceneObject);
            g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(RemoveFaceFromScene);
            sceneObjectFace.sprite = Sprite.Create((gameController.faceRawImage.texture as Texture2D),new Rect(0,0, gameController.faceRawImage.texture.width, gameController.faceRawImage.texture.height),new Vector2(0.5f,0.5f),100f);
            sceneObjectFace.gameObject.SetActive(true);
            totalObjects += 1;
        }
        if (gameController.faceRawImage2.color.a > 0f && gameController.faceRawImage2.gameObject.activeSelf && gameController.faceRawImage2.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectFace2Prefab, container.transform);
            sceneObjectFace2 = g.GetComponent<Image>();
            g.GetComponent<Button>().onClick.AddListener(OnClickFaceSceneObject2);
            g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(RemoveFace2FromScene);
            sceneObjectFace2.sprite = Sprite.Create((gameController.faceRawImage2.texture as Texture2D), new Rect(0, 0, gameController.faceRawImage2.texture.width, gameController.faceRawImage2.texture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectFace2.gameObject.SetActive(true);
            totalObjects += 1;
        }
        if (dressImage.color.a > 0 && dressImage.gameObject.activeSelf && dressImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectDressPrefab, container.transform);
            sceneObjectDress = g.transform.GetChild(0).GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnclickDressSceneObject);
            g.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RemoveDressFromScene);
            sceneObjectDress.sprite = Sprite.Create(gameController.currentDressTexture, new Rect(0, 0, gameController.currentDressTexture.width, gameController.currentDressTexture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectDress.gameObject.SetActive(true);
            totalObjects += 1;
        }

        if (wigImage.color.a > 0 && wigImage.gameObject.activeSelf && wigImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectWigPrefab, container.transform);
            sceneObjectWig = g.transform.GetChild(0).GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnclickWigSceneObject);

            g.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RemoveWigFromScene);
            sceneObjectWig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectWig.gameObject.SetActive(true);
            totalObjects += 1;
        }
        
        if (ornamentImage.color.a > 0 && ornamentImage.gameObject.activeSelf && ornamentImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectOrnamentPrefab, container.transform);
            sceneObjectOrnament = g.GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnclickOrnamentSceneObject);

            g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(RemoveOrnamentFromScene);

            sceneObjectOrnament.sprite = Sprite.Create(gameController.ornament.sprite.texture, new Rect(0, 0, gameController.ornament.sprite.texture.width, gameController.ornament.sprite.texture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectOrnament.gameObject.SetActive(true);
            totalObjects += 1;
        }
        if (shoeImage.color.a > 0 && shoeImage.gameObject.activeSelf && shoeImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectShoePrefab, container.transform);
            sceneObjectShoe = g.GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnclickShoeSceneObject);
            g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(RemoveShoeFromScene);

            sceneObjectShoe.sprite = Sprite.Create(gameController.shoe.sprite.texture, new Rect(0, 0, gameController.shoe.sprite.texture.width, gameController.shoe.sprite.texture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectShoe.gameObject.SetActive(true);
            totalObjects += 1;
        }

        if (maleWigImage.color.a > 0 && maleWigImage.gameObject.activeSelf && maleWigImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectMaleWigPrefab, container.transform);
            sceneObjectMaleWig = g.transform.GetChild(0).GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnClickMaleWigSceneObject);
            g.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RemoveMaleWigFromScene);

            sceneObjectMaleWig.sprite = Sprite.Create(gameController.maleWig.sprite.texture, new Rect(0, 0, gameController.maleWig.sprite.texture.width, gameController.maleWig.sprite.texture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectMaleWig.gameObject.SetActive(true);
            totalObjects += 1;
        }

        if (maleTieImage.color.a > 0 && maleTieImage.gameObject.activeSelf && maleTieImage.transform.parent.gameObject.activeSelf)
        {
            GameObject g = Instantiate<GameObject>(sceneObjectMaleTiePrefab, container.transform);
            sceneObjectMaleTie = g.transform.GetChild(0).GetComponent<Image>();

            g.GetComponent<Button>().onClick.AddListener(OnClickMaleTieSceneObject);
            g.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RemoveMaleTieFromScene);

            sceneObjectMaleTie.sprite = Sprite.Create(gameController.maleTie.sprite.texture, new Rect(0, 0, gameController.maleTie.sprite.texture.width, gameController.maleTie.sprite.texture.height), new Vector2(0.5f, 0.5f), 100f);
            sceneObjectMaleTie.gameObject.SetActive(true);
            totalObjects += 1;
        }

        if (!editPopup.activeSelf)
        {
            if(totalObjects<=0)
            {
                nothingThereToSelectText.SetActive(true);
            }
            editPopup.SetActive(true);
        }
    }


    public void HideObjectsInSceePanel()
    {
        editPopup.SetActive(false);

        Transform[] ts = container.GetComponentsInChildren<Transform>();
        foreach(Transform t in ts)
        {
            if(t!=container.transform)
            {
                try
                {
                    if (t.gameObject != null)
                    {
                        t.GetComponent<Button>().onClick.RemoveAllListeners();
                        DestroyImmediate(t.gameObject);
                    }
                }
                catch
                {

                }
            }
        }
        nothingThereToSelectText.SetActive(false);

        tapTime = 0;
        totalObjects = 0;
        isShowingSceneObjectsPanel = false;
    }


    private void CheckFingerTap()
    {
        if(!Application.isMobilePlatform)
        {
            if (!isShowingSceneObjectsPanel)
            {
                if (gameController.IsInHome() || true)
                {
                    if (Input.GetMouseButton(0))
                    {

                        tapTime += 1;
                        if (tapTime >= maxTapTime)
                        {
                            isShowingSceneObjectsPanel = true;
                            tapTime = 0;
                            ShowObjectsInScenePanel();
                        }
                    }
                }
            }

            if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {
                tapTime = 0;
            }
        }
        else
        {
            if (!isShowingSceneObjectsPanel)
            {
                if (gameController.IsInHome() || true)
                {
                    if(Input.touchCount==1)
                    {
                        if (Input.GetTouch(0).phase==TouchPhase.Stationary)
                        {

                            tapTime += 1;
                            if (tapTime >= maxTapTime)
                            {
                                isShowingSceneObjectsPanel = true;
                                tapTime = 0;
                                ShowObjectsInScenePanel();
                            }
                        }
                    }
                }
            }

            if (Input.touchCount!=1 || Input.GetTouch(0).phase!=TouchPhase.Stationary)
            {
                tapTime = 0;
            }
        }
    }


    public bool IsShownigobjectPanel()
    {
        return isShowingSceneObjectsPanel;
    }

    public void OnClickFaceSceneObject()
    {
        //gameController.faceRawImage.transform.DOPunchScale(new Vector3(2f,2f,2f),.2f);
        //gameController.currentlySelectedFace = 1;
        gameController.GoToHome();
        gameController.faceEditController.EditSelectedFace(1);
    }
    public void RemoveFaceFromScene()
    {
        gameController.RemoveFace1();
        Destroy(sceneObjectFace.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }
    public void OnClickFaceSceneObject2()
    {
        //gameController.faceRawImage2.transform.DOPunchScale(new Vector3(2f, 2f, 2f), .2f);
        //gameController.currentlySelectedFace = 2;
        gameController.GoToHome();
        gameController.faceEditController.EditSelectedFace(2);
    }
    public void RemoveFace2FromScene()
    {
        gameController.RemoveFace2();
        Destroy(sceneObjectFace2.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }


    public void InitGamaController()
    {
        if (gameController == null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
    }


    public void OnclickDressSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        //if(!gameController.panels[6].activeSelf)
        //{
        gameController.selectDressController.dontShowPopup = true;
            gameController.OnPressDressForHerButton(gameController.homeMenuButtonObjects[0]);
        //}
        gameController.selectDressController.OnClickSelectLongDressButton(true);

        Invoke("InvokeEditDress", .3f);
    }
    public void InvokeEditDress()
    {
        gameController.selectDressController.OnClickEditDressButton(true);
        //gameController.selectDressController.ToggleDressColorPanel(false);
    }

    public void RemoveDressFromScene()
    {
        gameController.selectDressController.RemoveDress();
        Destroy(sceneObjectDress.gameObject);

        totalObjects -= 1;
        if(totalObjects<=0)
        {
            HideObjectsInSceePanel();
        }
        
    }

    public void OnclickWigSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        //if (!gameController.panels[6].activeSelf)
        //{
        gameController.selectDressController.dontShowPopup = true;
        gameController.OnPressDressForHerButton(gameController.homeMenuButtonObjects[0]);
        //}
        gameController.selectDressController.OnClickSelectWigButton(true);
        Invoke("InvokeEditWig", .3f);
    }
    public void InvokeEditWig()
    {
        gameController.selectDressController.OnClickEditWigButton(true);
    }
    public void RemoveWigFromScene()
    {
        gameController.selectDressController.RemoveWig();
        Destroy(sceneObjectWig.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }



    public void OnclickOrnamentSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        //if (!gameController.panels[6].activeSelf)
        //{
        gameController.selectDressController.dontShowPopup = true;
        gameController.OnPressDressForHerButton(gameController.homeMenuButtonObjects[0]);
        //}
        gameController.selectDressController.OnClickSelectOrnamentButton(true);
        gameController.selectDressController.editButtons[0].SetActive(false);
        gameController.selectDressController.editButtons[1].SetActive(false);
        //gameController.selectDressController.OnClickEditDressButton(true);
    }
    public void RemoveOrnamentFromScene()
    {
        gameController.selectDressController.RemoveOrnament();
        Destroy(sceneObjectOrnament.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }

    public void OnclickShoeSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        //if (!gameController.panels[6].activeSelf)
        //{
        gameController.selectDressController.dontShowPopup = true;
        gameController.OnPressDressForHerButton(gameController.homeMenuButtonObjects[0]);
        //}
        gameController.selectDressController.OnClickSelectShoeButton(true);
        gameController.selectDressController.editButtons[0].SetActive(false);
        gameController.selectDressController.editButtons[1].SetActive(false);
        //gameController.selectDressController.OnClickEditDressButton(true);
    }
    public void RemoveShoeFromScene()
    {
        gameController.selectDressController.RemoveShoe();
        Destroy(sceneObjectShoe.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }

    public void OnClickMaleWigSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        gameController.OnPressForHimButton(gameController.homeMenuButtonObjects[3]);
        gameController.maleController.OnClickSelectWigsForMale(true);
        gameController.maleController.OnClickEditMaleWigButton();
    }


    public void RemoveMaleWigFromScene()
    {
        gameController.maleController.RemoveMaleWig();
        Destroy(sceneObjectMaleWig.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }


    public void OnClickMaleTieSceneObject()
    {
        InitGamaController();
        gameController.GoToHome();
        gameController.OnPressForHimButton(gameController.homeMenuButtonObjects[3]);
        gameController.maleController.OnClickSelectTiesForMale(true);
        gameController.maleController.OnClickEditMaleTieButton();
    }

    public void RemoveMaleTieFromScene()
    {
        gameController.maleController.RemoveMaleTie();
        Destroy(sceneObjectMaleTie.gameObject);

        totalObjects -= 1;
        if (totalObjects <= 0)
        {
            HideObjectsInSceePanel();
        }

    }
}
