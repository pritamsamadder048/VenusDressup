using UnityEngine;
using System.Collections;

public class OverBodyToneSlider : MonoBehaviour
{
    public GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        print("Mouse enter");
        gameController.sceneEditorController.tapTime = 0;
    }

    private void OnMouseOver()
    {
        print("Mouse Hover");
        gameController.sceneEditorController.tapTime = 0;
    }
    private void OnMouseExit()
    {
        print("Mouse exit");
        
    }

}
