using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour {

	public Image markerImage;
	public Image sourceImage;
	public Image destinationImage;


	public void cropSprite() {
		if (markerImage == null)
			Debug.Log("markerImage is null");
		if (markerImage.rectTransform == null)
			Debug.Log("markerImage.rectTransform is null");
		Rect spriteRect = markerImage.rectTransform.rect;

		int pixelsToUnits = 100; // It's PixelsToUnits of sprite which would be cropped

		// Crop sprite  

		Debug.Log("spriteRect " + spriteRect.center);
		Debug.Log(string.Format("width {0} and height {1}", spriteRect.width, spriteRect.height));
		Debug.Log("mainCropImage.rectTransform.anchoredPosition " + markerImage.rectTransform.anchoredPosition);

		Texture2D sourceTexture2D = GetTextture2D();

		//Sprite croppedSprite = Sprite.Create(imageToCrop.sprite.texture, spriteRect, new Vector2(.5f, .5f), 100);
		Sprite croppedSprite = Sprite.Create(sourceTexture2D, new Rect((float)System.Math.Round(markerImage.rectTransform.anchoredPosition.x,2), (float)System.Math.Round(markerImage.rectTransform.anchoredPosition.y,2), spriteRect.width, spriteRect.height), new Vector2(.5f, .5f), 100);
	//	Sprite croppedSprite = Sprite.Create(imageToCrop.sprite.texture, new Rect(0, 0, 400, 400), new Vector2(.5f, .5f), 200/3.333f);
		destinationImage.sprite = croppedSprite;
	}


	Texture2D GetTextture2D(){
		float warpFactor = 1.0F;
		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
		Color[] destPix = new Color[destTex.width * destTex.height];
		int y = 0;
		while (y < destTex.height) {
			int x = 0;
			while (x < destTex.width) {
				float xFrac = x * 1.0F / (destTex.width - 1);
				float yFrac = y * 1.0F / (destTex.height - 1);
				float warpXFrac = Mathf.Pow(xFrac, warpFactor);
				float warpYFrac = Mathf.Pow(yFrac, warpFactor);
				destPix[y * destTex.width + x] = sourceImage.sprite.texture.GetPixelBilinear(warpXFrac, warpYFrac);
				x++;
			}
			y++;
		}
		destTex.SetPixels(destPix);
		destTex.Apply();


		//destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);

		return destTex;
	}
}
