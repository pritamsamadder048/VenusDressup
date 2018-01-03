using UnityEngine;
using System.Collections;

public class OverFaceSlider : MonoBehaviour
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
        gameController.faceRawImage.GetComponent<CroppedFaceController>().notOverSlider = false;
        gameController.faceRawImage2.GetComponent<CroppedFaceController>().notOverSlider = false;
    }

    private void OnMouseOver()
    {
        print("Mouse Hover");
        gameController.faceRawImage.GetComponent<CroppedFaceController>().notOverSlider = false;
        gameController.faceRawImage2.GetComponent<CroppedFaceController>().notOverSlider = false;
    }
    private void OnMouseExit()
    {
        print("Mouse exit");
        gameController.faceRawImage.GetComponent<CroppedFaceController>().notOverSlider = true;
        gameController.faceRawImage2.GetComponent<CroppedFaceController>().notOverSlider = true;
    }

}
