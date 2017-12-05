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

	public void Start()
	{
		this.mainSlider.onValueChanged.AddListener(delegate
		{
			this.ValueChangeCheck();
		});
	}

	public void ActivateColorSlider(bool resetSlider = true)
	{
		if (resetSlider)
		{
			base.GetComponent<Slider>().value = base.GetComponent<Slider>().minValue;
		}
        activated = true;
        //this.ValueChangeCheck();
        shouldChangeToGrayScale = true;
	}
    public void DeactivateColorSlider()
    {
        activated = false;
        base.GetComponent<Slider>().value = base.GetComponent<Slider>().minValue;
        
    }
	public void ValueChangeCheck()
	{
        if(activated)
        {

            if (shouldChangeToGrayScale)
            {
                ChangeToGrayScale();
                shouldChangeToGrayScale = false;
            }
            float value = this.mainSlider.value;
            string htmlString = UIImageColorPicker.Rainbow(1, value);
            Color color = default(Color);
            ColorUtility.TryParseHtmlString(htmlString, out color);
            this.targetImage.GetComponent<Image>().color = color;
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
