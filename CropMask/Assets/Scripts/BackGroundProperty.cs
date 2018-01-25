using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;


[Serializable]
public class BackGroundProperty : MonoBehaviour, ISerializationCallbackReceiver
{
    [NonSerialized]
    public GameController gameController;
    public string backGroundName;
    public string backGroundPath;

    public bool backgroundButton = false;
    // Use this for initialization

    private void Awake()
    {
        backGroundPath= Path.Combine("images/backgrounds", backGroundName);
        if (backgroundButton)
        {
            InitBackgroundProperty();
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CopyTo(ref BackGroundProperty bp)
    {
        bp = new BackGroundProperty();
        bp.backgroundButton = false;
        bp.backGroundName = this.backGroundName;
        bp.backGroundPath = this.backGroundPath;
        return;
    }

    public void CopyFrom(BackGroundProperty bp)
    {
        
        
        this.backGroundName = bp.backGroundName;
        this.backGroundPath= bp.backGroundPath;

        return;
    }

    public void InitBackgroundProperty(string bn)
    {
        backGroundName = bn;
        backGroundPath= Path.Combine("images/backgrounds", backGroundName);
    }

    public void InitBackgroundProperty()
    {
        GetComponent<Button>().onClick.AddListener(InvokeUseThisBackgroundStatic);
        InitGameController();
    }

    public void InitGameController()
    {
        if(gameController==null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    public void InvokeUseThisBackgroundStatic()
    {
        InitGameController();
        
        if(gameController==null)
        {
            return;
        }
        print("BackgroundProperty(InvokeUseThisBackgroundStatic) >>>  Gamecontroller init successfully");
        gameController.ShowLoadingPanelOnly();
        print("loading panel is shown");
        Invoke("UseThisBackgroundStatic", .3f);
    }

    public void UseThisBackgroundStatic()
    {
        print("in UseThisBackgroundStatic");
        InitGameController();
        if (gameController == null)
        {
            HideLoadingPanelOnly();
            return;
        }

        print("BackgroundProperty(UseThisBackgroundStatic) >>>  Gamecontroller init successfully");

        if (gameController.currentBackgroundName==backGroundName)
        {
            HideLoadingPanelOnly();
            return;
        }
        string finalStaticPath = Path.Combine("images/backgrounds", backGroundName);
        try
        {
            Texture2D tmpTex = Resources.Load<Texture2D>(finalStaticPath);
            tmpTex.Apply();
            if (tmpTex != null)
            {
                gameController.backGroundImage.sprite = Sprite.Create(tmpTex, new Rect(0, 0, tmpTex.width, tmpTex.height), new Vector2(0.5f, 0.5f), 100f);
                gameController.currentBackgroundName = backGroundName;
                CopyTo(ref gameController.currentBackgroundProperty);
            }
            else
            {
                gameController.InstantiateInfoPopup("Could not use this background");
            }
        }
        catch(Exception e)
        {
            gameController.InstantiateInfoPopup(e.Message);
        }

        HideLoadingPanelOnly();
    }

    public void HideLoadingPanelOnly()
    {
        gameController.HideLoadingPanelOnly();
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        
    }
}
