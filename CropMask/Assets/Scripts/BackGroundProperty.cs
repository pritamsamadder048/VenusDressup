using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class BackGroundProperty : MonoBehaviour
{
    public GameController gameController;
    public string backGroundName;
    public string backGroundPath;
    // Use this for initialization

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(InvokeUseThisBackgroundStatic);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeUseThisBackgroundStatic()
    {
        gameController.ShowLoadingPanelOnly();
        Invoke("UseThisBackgroundStatic", .3f);
    }

    public void UseThisBackgroundStatic()
    {
        if(gameController.currentBackgroundName==backGroundName)
        {
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

        Invoke("HideLoadingPanelOnly", .1f);
    }

    public void HideLoadingPanelOnly()
    {
        gameController.HideLoadingPanelOnly();
    }
}
