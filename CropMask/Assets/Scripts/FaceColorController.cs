using System;
using UnityEngine;
using UnityEngine.UI;


public class FaceColorController : MonoBehaviour
{
    public Slider mainSlider;

    public RawImage targetImage;


    public bool activated = false;


    public GameController gameController;

    public bool isFaceColorSlider = false;
    public bool isFaceSaturationSlider = false;


    public void Start()
    {
        //this.mainSlider.onValueChanged.AddListener(delegate
        //{
        //	this.ValueChangeCheck_ColorShift();
        //});

        InitializeGameController();

    }

    public void InitializeGameController()
    {
        GameObject gamecontrollerobj = GameObject.FindGameObjectWithTag("GameController");
        if (gamecontrollerobj != null)
        {
            gameController = gamecontrollerobj.GetComponent<GameController>();
        }
    }

    public void ActivateColorSlider(bool resetSlider = true)
    {
        if (resetSlider)
        {

            base.GetComponent<Slider>().value = 0.5f; //base.GetComponent<Slider>().maxValue;
        }
        activated = true;

        if(!isFaceColorSlider && !isFaceSaturationSlider)
        {
            this.mainSlider.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck_BrightnessShift();
            });
        }
        else if(isFaceColorSlider && !isFaceSaturationSlider)
        {
            this.mainSlider.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck_ColorShift();
            });
        }
        else if(isFaceSaturationSlider)
        {
            this.mainSlider.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck_SaturationShift();
            });
        }

        gameObject.SetActive(true);

    }
    public void DeactivateColorSlider()
    {
        activated = false;

        base.GetComponent<Slider>().value = 0.5f;

        this.mainSlider.onValueChanged.RemoveAllListeners();
        gameObject.SetActive(false);
    }

    public void ValueChangeCheck_BrightnessShift()
    {
        if (activated)
        {

            float value = this.mainSlider.value;
            Color color = this.targetImage.GetComponent<RawImage>().color;
            color.b = value;
            this.targetImage.GetComponent<RawImage>().color = color;
        }
    }



    public void ValueChangeCheck_ColorShift()
    {
        if (activated)
        {

            float value = this.mainSlider.value;
            Color color = this.targetImage.GetComponent<RawImage>().color;
            color.r = value;
            this.targetImage.GetComponent<RawImage>().color = color;
        }
    }

    public void ValueChangeCheck_SaturationShift()
    {
        if (activated)
        {

            float value = this.mainSlider.value;
            Color color = this.targetImage.GetComponent<RawImage>().color;
            color.g = value;
            this.targetImage.GetComponent<RawImage>().color = color;
        }
    }



    public void SetTarget(RawImage targetimage)
    {
        DeactivateColorSlider();
        targetImage = targetimage;
        ActivateColorSlider();
    }
}
