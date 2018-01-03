using System;
using UnityEngine;
using UnityEngine.UI;


public class UIImageColorPicker : MonoBehaviour
{
	public Slider mainSlider;

	public Image targetImage;

    public bool shouldChangeToGrayScale = false;
    public bool activated = false;

	private Sprite mySprite;

	private Texture2D graph;

    public GameController gameController;

    public bool dresscolor = false;
    public bool dressbrightness = false;
    public bool wigcolor = false;
    public bool wigbrightness = false;

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
			if(wigbrightness || dressbrightness)
            {
                base.GetComponent<Slider>().value = 0.5f; //base.GetComponent<Slider>().maxValue;
            }
            else
            {
                base.GetComponent<Slider>().value = 0.5f;// base.GetComponent<Slider>().minValue;
            }
		}
        activated = true;
        //this.ValueChangeCheck();
        shouldChangeToGrayScale = true;

        if (wigbrightness || dressbrightness)
        {
            this.mainSlider.onValueChanged.AddListener(delegate
            {
                BrightnessValueChangeCheck_ColorShift();

            });
        }
        else if(wigcolor || dresscolor)
        {

            this.mainSlider.onValueChanged.AddListener(delegate
            {
                this.ValueChangeCheck_ColorShift();
            });
        }

    }
    public void DeactivateColorSlider()
    {
        activated = false;
        if (wigbrightness || dressbrightness)
        {
            base.GetComponent<Slider>().value = 0.5f;// base.GetComponent<Slider>().maxValue;
        }
        else
        {
            base.GetComponent<Slider>().value = 0.5f;// base.GetComponent<Slider>().minValue;
        }


        this.mainSlider.onValueChanged.RemoveAllListeners();
    }

    public void ValueChangeCheck_ColorShift()
    {
        if (activated)
        {

            if (shouldChangeToGrayScale)
            {
                //ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            //string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = this.targetImage.GetComponent<Image>().color;
            //float h, s, v = 0;

            //Color.RGBToHSV(color, out h, out s, out v);
            //s = 1;
            //print(string.Format("H : {0} S: {1} V : {2}  color : {3} ", h.ToString(), s.ToString(), v.ToString(), color.ToString()));
            //ColorUtility.TryParseHtmlString(htmlString, out color);
            //print(string.Format("new value :{0} , s:{1}, v:{2}", value, s, v));

            if (dresscolor)
            {
                color.r = value;
                gameController.selectDressController.dressColor = value;
            }
            if (wigcolor)
            {
                color.r = value;
                gameController.selectDressController.wigColor = value;
            }
            print("Later color is : " + color.ToString());
            this.targetImage.GetComponent<Image>().color = color;
        }
    }

    public void ValueChangeCheck_Old()
	{
        if(activated)
        {

            if (shouldChangeToGrayScale)
            {
                ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            //string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = this.targetImage.GetComponent<Image>().color;
            float h, s, v = 0;
            
            Color.RGBToHSV(color, out h, out s, out v);
            s = 1;
            print(string.Format("H : {0} S: {1} V : {2}  color : {3} ", h.ToString(), s.ToString(), v.ToString(), color.ToString()));
            //ColorUtility.TryParseHtmlString(htmlString, out color);
            print(string.Format("new value :{0} , s:{1}, v:{2}", value, s, v));
            
            if(dresscolor)
            {
                color = Color.HSVToRGB(value, s, gameController.selectDressController.dressBrightness);
                gameController.selectDressController.dressColor = value;
            }
            if(wigcolor)
            {
                color = Color.HSVToRGB(value, s, gameController.selectDressController.wigBrightness);
                gameController.selectDressController.wigColor = value;
            }
            print("Later color is : " + color.ToString());
            this.targetImage.GetComponent<Image>().color = color;
        }
	}



    public void BrightnessValueChangeCheck_ColorShift()
    {
        if (activated)
        {

            if (shouldChangeToGrayScale)
            {
                //ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            //string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = this.targetImage.GetComponent<Image>().color;
            print("First color is : " + color.ToString());
            color.b = value;
            //float h, s, v = 0;

            //Color.RGBToHSV(color, out h, out s, out v);
            //s = 1;
            //print(string.Format("Brightness H : {0} S: {1} V : {2}  color : {3} ", h.ToString(), s.ToString(), v.ToString(), color.ToString()));
            //ColorUtility.TryParseHtmlString(htmlString, out color);
            //print(string.Format("Brightness new value :{0} , s:{1}, v:{2}", h, s, value));

            if (dressbrightness)
            {
                //color = Color.HSVToRGB(gameController.selectDressController.dressColor, s, value);
                gameController.selectDressController.dressBrightness = value;
            }
            if (wigbrightness)
            {
                //color = Color.HSVToRGB(gameController.selectDressController.wigColor, s, value);
                gameController.selectDressController.wigBrightness = value;
            }
            print("Later Brightness is : " + color.ToString());
            this.targetImage.GetComponent<Image>().color = color;
        }
    }

    public void BrightnessValueChangeCheck()
    {
        if (activated)
        {

            if (shouldChangeToGrayScale)
            {
                //ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            //string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = this.targetImage.GetComponent<Image>().color;
            float h, s, v = 0;

            Color.RGBToHSV(color, out h, out s, out v);
            s = 1;
            print(string.Format("Brightness H : {0} S: {1} V : {2}  color : {3} ", h.ToString(), s.ToString(), v.ToString(), color.ToString()));
            //ColorUtility.TryParseHtmlString(htmlString, out color);
            print(string.Format("Brightness new value :{0} , s:{1}, v:{2}", h, s, value));
            
            if(dressbrightness)
            {
                color = Color.HSVToRGB(gameController.selectDressController.dressColor, s, value);
                gameController.selectDressController.dressBrightness = value;
            }
            if(wigbrightness)
            {
                color = Color.HSVToRGB(gameController.selectDressController.wigColor, s, value);
                gameController.selectDressController.wigBrightness = value;
            }
            print("Later Brightness is : " + color.ToString());
            this.targetImage.GetComponent<Image>().color = color;
        }
    }


    public void ValueChangeCheck_HSB()
    {
        if (activated)
        {

            if (shouldChangeToGrayScale)
            {
                //ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            targetImage.sprite.texture.SetPixels(TextureUtility.ChangeHSB(gameController.currentDressTexture.GetPixels(), value, 0f, 0f));
            targetImage.sprite.texture.Apply();
            string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = default(Color);
            ColorUtility.TryParseHtmlString(htmlString, out color);
            //this.targetImage.GetComponent<Image>().color = color;
        }
    }

    public void ChangeToGrayScale()
    {
        Color[] pixels = this.targetImage.sprite.texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a > 0.5f)
            {
                pixels[i] = new Color(pixels[i].grayscale, pixels[i].grayscale, pixels[i].grayscale, pixels[i].a);
            }
        }
        Texture2D texture2D = new Texture2D(this.targetImage.sprite.texture.width, this.targetImage.sprite.texture.height);
        texture2D.SetPixels(pixels);
        texture2D.Apply();
        this.targetImage.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
    }
	public static string Rainbow(int numOfSteps, float step)
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = (double)step / (double)numOfSteps;
		int num5 = (int)(num4 * 6.0);
		double num6 = num4 * 6.0 - (double)num5;
		double num7 = 1.0 - num6;
		switch (num5 % 6)
		{
		case 0:
			num = 1.0;
			num2 = num6;
			num3 = 0.0;
			break;
		case 1:
			num = num7;
			num2 = 1.0;
			num3 = 0.0;
			break;
		case 2:
			num = 0.0;
			num2 = 1.0;
			num3 = num6;
			break;
		case 3:
			num = 0.0;
			num2 = num7;
			num3 = 1.0;
			break;
		case 4:
			num = num6;
			num2 = 0.0;
			num3 = 1.0;
			break;
		case 5:
			num = 1.0;
			num2 = 0.0;
			num3 = num7;
			break;
		}
		return "#" + ((int)(num * 255.0)).ToString("X2") + ((int)(num2 * 255.0)).ToString("X2") + ((int)(num3 * 255.0)).ToString("X2");
	}
}
