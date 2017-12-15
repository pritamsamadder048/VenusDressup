using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneController : MonoBehaviour
{
    public int currentTimer;
    public int gurbageCollectTime;

    // Use this for initialization
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    private void LateUpdate()
    {
        CollectGurbage();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = (false);
        }
#endif
        Application.Quit();
    }


    public void CollectGurbage(bool rightNow = false)
    {
        if (!rightNow)
        {
            this.currentTimer++;
            if (this.currentTimer > this.gurbageCollectTime)
            {
                System.GC.Collect();
                Resources.UnloadUnusedAssets();
                this.currentTimer = 0;
            }
        }
        else
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
            this.currentTimer = 0;
        }
    }
}
