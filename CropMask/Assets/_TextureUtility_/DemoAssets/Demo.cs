//----------------------------------------------------------------------------------------------------------------------------------------------------------  
// Simple Demo-script demonstrates using of TextureUtility asset
//----------------------------------------------------------------------------------------------------------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Demo : MonoBehaviour 
{

	// Important variables
	public Texture2D[] sources;
	private int sourceID;
	private int oldSourceID = -1; 
	private Color[] sourcePixels;

	public Texture UI_hueTexture;

	public Color color1;
	private Color oldColor1;

	public Rect rect;

	public TextureUtility.Alignment alignment;
	public TextureUtility.BlendMode blendMode;
	public TextureUtility.BooleanOperation booleanOperation;
	private TextureUtility.BooleanOperation oldBooleanOperation;

	public float angle;
	private float oldAngle;

	public Texture2D[] masks;
	private int maskID;
	private int oldMaskID; 

	public float contrast;
	private float oldContrast;

	public Vector3 HSB;
	private Vector3 oldHSB;

	public Vector2 colorDiapason;
	private Vector2 oldColorDiapason;

	public float intensity;


	private float oldIntensity;

	private Vector2 scrollPosition = Vector2.zero;

	private int selectedFunction;
	private int previousFunction = -1;

	private Texture2D result;
	private Color[] resultPixels;

	private string[] functions = new string[]
		{"InvertTransparency",
		"MakeColorTransparent",
		"ChangeHSB",
		"Contrast",
		"ColorDiapason",
		"Colorize",
		"Grayscale",
		"Negative",
		"FlipHorizontally",
		"FlipVertically",
		"Expand",
		"Crop",
		"AutoCropTransparency",
		"AutoCropColor",
		"ApplyMask",
		"ApplyBooleanOperation",
		"Rotate",
		"Scale",
		"Stroke",
		"Clear"
	};


	//----------------------------------------------------------------------------------------------------------------------------------------------------------  	
	// Prepare source and result textures
	void Start () 
	{
		sourcePixels = sources[0].GetPixels();
		newResult();

	}

	//----------------------------------------------------------------------------------------------------------------------------------------------------------  	
	// Prepare result texture
	void newResult () 
	{ 
		result = new Texture2D(sources[0].width, sources[0].height, TextureFormat.RGBA32, false);
		result.SetPixels(sourcePixels);
		result.Apply();

	}

	//----------------------------------------------------------------------------------------------------------------------------------------------------------  	
	// Draw and process whole Demo UI	
	void OnGUI () 
	{
		GUI.Label(new Rect(10, 25, 200, 30), "SELECT UTILITY FUNCTION:");
		scrollPosition = GUI.BeginScrollView (new Rect (20,50,170,300), scrollPosition, new Rect (0, 0, 150, 750));
		for (int i = 0; i < functions.Length; i++)
			if (GUI.Button (new Rect (0,i*33,150,30), functions[i]))  selectedFunction = i; 
		GUI.EndScrollView ();


		GUI.Label(new Rect(10, 380, 200, 30), "SOURCE TEXTURE:");
		if(sources[sourceID]) GUI.DrawTexture(new Rect(70, 400, 350, 350),  sources[sourceID] );


		GUI.Label(new Rect(510, 380, 200, 30), "RESULT TEXTURE:");
		if(result) GUI.DrawTexture(new Rect(570, 400, 350, 350),  result );


		GUI.Label(new Rect(200, 745, 800, 25), "IMAGES ABOVE COULD BE DISTORTED TO FIT PREVIEW-AREAS - PLEASE DON'T WORRY ABOUT THIS!");


		GUI.Label(new Rect(425, 25, 800, 300), "SETUP THE FUNCTION: " + functions[(int)selectedFunction].ToString());
		GUI.Box(new Rect(210, 50 , 800, 300),"");

		// Main switch to display specific UI and  preview for	selected functionality
		switch ((int)selectedFunction)
		{

		//-----------------------------------------------------------
		case 0: //InvertTransparency
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 0)  
			{
				sourceID = 6;
				result = TextureUtility.InvertTransparency(sources[sourceID]);
				previousFunction = 0;
			}
			break;

			//-----------------------------------------------------------   	
		case 1: // MakeColorTransparent
			if (previousFunction != 1)  
			{
				color1 = Color.black;
				oldColor1 = Color.white;
				sourceID = 1;
				previousFunction = 1;

				newResult();
			}


			GUI.Label(new Rect(225, 80, 400, 30), "SELECT EXAMPLE-COLOR YOU WANT TO BE TRANSPARENT:"); 
			if (GUI.Button (new Rect (250,120,95,30), "RED"))  color1 = Color.red; 
			if (GUI.Button (new Rect (350,120,95,30), "BLUE"))  color1 = Color.blue; 
			if (GUI.Button (new Rect (450,120,95,30), "CYAN"))  color1 = Color.cyan;

			if (GUI.Button (new Rect (250,155,95,30), "GREEN"))  color1 = Color.green; 
			if (GUI.Button (new Rect (350,155,95,30), "MAGENTA"))  color1 = Color.magenta; 
			if (GUI.Button (new Rect (450,155,95,30), "YELLOW"))  color1 = new Color(1.0f,1.0f,0.0f);


			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (color1 != oldColor1)  
			{
				result = TextureUtility.MakeColorTransparent(sources[sourceID], color1);
				oldColor1 = color1;
			}
			break;

			//-----------------------------------------------------------
		case 2: // ChangeHSB
			if (previousFunction != 2)  
			{
				HSB = Vector3.zero;
				oldHSB = Vector3.one;
				previousFunction = 2;
				sourceID = 0;

				newResult();
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST SLIDERS TO ACHIEVE DESIRED RESULT:"); 

			GUI.Label(new Rect(250, 120, 100, 30), "HUE"); 	
			HSB.x = GUI.HorizontalSlider (new Rect (320, 125, 220, 30), HSB.x, -1.0f, 1.0f);
			GUI.Label(new Rect(560, 120, 25, 20), HSB.x.ToString());

			GUI.Label(new Rect(250, 145, 100, 30), "Saturation"); 
			HSB.y = GUI.HorizontalSlider (new Rect (320, 150, 220, 30), HSB.y, -1.0f, 1.0f);
			GUI.Label(new Rect(560, 145, 25, 20), HSB.y.ToString());

			GUI.Label(new Rect(250, 170, 100, 30), "Brightness"); 
			HSB.z = GUI.HorizontalSlider (new Rect (320, 175, 220, 30), HSB.z, -1.0f, 1.0f);
			GUI.Label(new Rect(560, 170, 25, 20), HSB.z.ToString());

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (HSB != oldHSB)  
			{	
				resultPixels = TextureUtility.ChangeHSB(sourcePixels, HSB.x, HSB.y, HSB.z);
				result.SetPixels(resultPixels);
				result.Apply();

				oldHSB = HSB;
			}
			break;	 

			//----------------------------------------------------------- 	  	
		case 3: // Contrast
			if (previousFunction != 3)  
			{
				contrast = 1;
				oldContrast = 0;
				previousFunction = 3;
				sourceID = 0;

				newResult();
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST SLIDER TO ACHIEVE DESIRED RESULT:"); 

			GUI.Label(new Rect(250, 120, 100, 30), "Contrast"); 	
			contrast = GUI.HorizontalSlider (new Rect (320, 125, 220, 30), contrast, 0.0f, 2.0f);
			GUI.Label(new Rect(560, 120, 25, 20), contrast.ToString());

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (oldContrast != contrast)  
			{	
				resultPixels = TextureUtility.Contrast (sourcePixels, contrast);
				result.SetPixels(resultPixels);
				result.Apply();

				oldContrast = contrast;
			}
			break; 	

			//-----------------------------------------------------------   	
		case 4: // ColorDiapason
			if (previousFunction != 4)  
			{
				colorDiapason = new Vector2 (0, 0.99f);
				oldColorDiapason = Vector2.zero; 
				sourceID = 0; 
				previousFunction = 4;

				newResult();
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST SLIDERS TO ACHIEVE DESIRED RESULT:"); 

			GUI.DrawTexture(new Rect(310, 120, 360, 60), UI_hueTexture);

			GUI.Label(new Rect(250, 115, 100, 30), "Start color"); 	
			colorDiapason.x = GUI.HorizontalSlider (new Rect (318, 122, 345, 20), colorDiapason.x, 0.0f, 0.99f);
			GUI.Label(new Rect(680, 115, 25, 20), Mathf.CeilToInt(colorDiapason.x*360).ToString());

			GUI.Label(new Rect(250, 145, 100, 30), "End color"); 	
			colorDiapason.y = GUI.HorizontalSlider (new Rect (318, 148, 345, 20), colorDiapason.y, 0.0f, 0.99f);
			GUI.Label(new Rect(680, 145, 25, 20), Mathf.CeilToInt(colorDiapason.y*360).ToString());

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (colorDiapason != oldColorDiapason)  
			{	
				resultPixels = TextureUtility.ColorDiapason(sourcePixels, TextureUtility.HSBAtoColor(new Vector4(colorDiapason.x,1,1,1)), TextureUtility.HSBAtoColor(new Vector4(colorDiapason.y,1,1,1)));
				result.SetPixels(resultPixels);
				result.Apply();

				oldColorDiapason = colorDiapason;
			}
			break; 

			//-----------------------------------------------------------	   	
		case 5: // Colorize
			if (previousFunction != 5)  
			{
				newResult();

				colorDiapason = new Vector2 (0, 1);
				oldColorDiapason = Vector2.zero; 
				intensity = 1;
				oldIntensity = 0;
				sourceID = 0;
				previousFunction = 5;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST SLIDERS TO ACHIEVE DESIRED RESULT:"); 

			GUI.DrawTexture(new Rect(310, 120, 360, 60), UI_hueTexture);

			GUI.Label(new Rect(250, 115, 100, 30), "Color"); 	
			colorDiapason.x = GUI.HorizontalSlider (new Rect (318, 122, 345, 20), colorDiapason.x, 0.0f, 0.99f);

			GUI.Label(new Rect(250, 190, 100, 30), "Intensity"); 	
			intensity = GUI.HorizontalSlider (new Rect (320, 195, 340, 20), intensity, 0, 2.0f);
			GUI.Label(new Rect(680, 190, 25, 20), intensity.ToString());


			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (colorDiapason != oldColorDiapason  ||  oldIntensity != intensity)  
			{	
				resultPixels = TextureUtility.Colorize(sourcePixels, TextureUtility.HSBAtoColor(new Vector4(colorDiapason.x,1,1,1)), intensity);
				result.SetPixels(resultPixels);
				result.Apply();

				oldIntensity = intensity;
				oldColorDiapason = colorDiapason;
			}
			break;

			//-----------------------------------------------------------	   	
		case 6: // Grayscale 
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 6)  
			{
				sourceID = 0;
				result = TextureUtility.Grayscale(sources[sourceID]);
				previousFunction = 6;
			}
			break;	 

			//-----------------------------------------------------------   	  	
		case 7: // Negative
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 7)  
			{
				sourceID = 0;
				result = TextureUtility.Negative(sources[sourceID]);
				previousFunction = 7;

			}
			break; 	

			//-----------------------------------------------------------  	
		case 8: //FlipHorizontally
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 8)  
			{
				sourceID = 0;
				result = TextureUtility.FlipHorizontally(sources[sourceID]);
				previousFunction = 8;
			}

			break;

			//-----------------------------------------------------------  	   	
		case 9: // FlipVertically
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 9)  
			{
				sourceID = 0;
				result = TextureUtility.FlipVertically(sources[sourceID]);
				previousFunction = 9;
			}
			break;

			//-----------------------------------------------------------  	   	
		case 10: // Expand
			if (previousFunction != 10)  
			{
				rect.width = sources[sourceID].width;
				rect.height = sources[sourceID].height;
				alignment = TextureUtility.Alignment.TopLeft;
				newResult();
				sourceID = 0;
				previousFunction = 10;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST VALUES AND PRESS 'EXPAND' BUTTON:"); 

			GUI.Label(new Rect(240, 120, 100, 30), "New width: "); 
			rect.width  = int.Parse(GUI.TextField (new Rect (310, 120, 50, 20), rect.width.ToString(), 4));

			GUI.Label(new Rect(240, 150, 100, 30), "New height: "); 
			rect.height = int.Parse(GUI.TextField (new Rect (310, 150, 50, 20), rect.height.ToString(), 4));

			GUI.Label(new Rect(240, 185, 100, 30), "Alignment:"); 
			string[] toolbarStrings = new string [] {"TopLeft", "TopRight", "TopCenter", "BottomLeft", "BottomRight", "BottomCenter"};
			alignment = (TextureUtility.Alignment)GUI.Toolbar (new Rect (310, 180, 600, 30),  (int)alignment, toolbarStrings);

			if (GUI.Button (new Rect (410, 250, 150, 30), "EXPAND")) result = TextureUtility.Expand (sources[sourceID], (int)rect.width, (int)rect.height, alignment);

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;

			break;	 

			//-----------------------------------------------------------  	   	  	
		case 11:  // Crop
			if (previousFunction != 11)  
			{
				rect.width = sources[sourceID].width;
				rect.height = sources[sourceID].height;
				newResult();
				sourceID = 0;
				previousFunction = 11;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST VALUES AND PRESS 'CROP' BUTTON:"); 

			GUI.Label(new Rect(240, 120, 100, 30), "Crop X,Y: "); 
			rect.x  = int.Parse(GUI.TextField (new Rect (340, 120, 50, 20), rect.x.ToString(), 4));
			rect.y = int.Parse(GUI.TextField (new Rect (400, 120, 50, 20), rect.y.ToString(), 4));

			GUI.Label(new Rect(240, 150, 100, 30), "Crop size: "); 
			rect.width  = int.Parse(GUI.TextField (new Rect (340, 150, 50, 20), rect.width.ToString(), 4));
			rect.height = int.Parse(GUI.TextField (new Rect (400, 150, 50, 20), rect.height.ToString(), 4));


			if (rect.x+rect.width > sources[sourceID].width) rect.width = sources[sourceID].width - rect.x; 
			if (rect.y+rect.height > sources[sourceID].height )rect.height = sources[sourceID].height - rect.y; 


			if (GUI.Button (new Rect (410, 250, 150, 30), "CROP"))  result = TextureUtility.Crop (sources[sourceID], rect);

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;
			break; 	

			//-----------------------------------------------------------  	   	
		case 12: // AutoCropTransparency
			GUI.Label(new Rect(430, 150, 800, 300), "THIS FUNCTION HAS NO PARAMETERS TO SETUP");
			if (previousFunction != 12)  
			{
				sourceID = 2;
				result = TextureUtility.AutoCropTransparency(sources[2]);
				previousFunction = 12;
			}
			break;

			//-----------------------------------------------------------  	   	
		case 13: // AutoCropColor
			if (previousFunction != 13)  
			{
				color1 = Color.black;
				oldColor1 = Color.white;

				sourceID = 3;
				newResult();
				previousFunction = 13;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "SELECT SOURCE AND COLOR TO CROP BY:"); 

			for (int i = 3; i < 6; i++)
				if (GUI.Button (new Rect (10 + i*107, 105, 100,100), sources[i]))  sourceID = i;  

			if (color1  == Color.black)  GUI.Label(new Rect(240, 210, 400, 30), "Current crop-color: BLACK"); 
			else
				if (color1  == Color.blue)  GUI.Label(new Rect(240, 210, 400, 30), "Current crop-color: BLUE"); 
				else  GUI.Label(new Rect(240, 210, 400, 30), "Current crop-color: RED"); 

			if (GUI.Button (new Rect (250,230,95,30), "BLACK")) color1 = Color.black; 
			if (GUI.Button (new Rect (350,230,95,30), "BLUE"))  color1 = Color.blue; 
			if (GUI.Button (new Rect (450,230,95,30), "RED"))   color1 = Color.red;

			if (GUI.Button (new Rect (250, 280, 150, 30), "RESET"))  previousFunction = 0;

			if (color1 != oldColor1  ||  oldSourceID != sourceID )  
			{
				result = TextureUtility.AutoCropColor(sources[sourceID], color1 );
				oldSourceID = sourceID;
				oldColor1 = color1;
			}
			break;

			//-----------------------------------------------------------  	   	
		case 14: // ApplyMask
			if (previousFunction != 14)  
			{
				maskID = 0;
				oldMaskID = 1;
				sourceID = 0;
				newResult();
				previousFunction = 14;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "PRESS BUTTON TO SELECT RELATED MASK");

			for (int i = 0; i < masks.Length; i++)
				if (GUI.Button (new Rect (250 + i*107, 120, 100,100), masks[i]))  maskID = i;  	


			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;	 	


			if (maskID != oldMaskID)
			{	
				result = TextureUtility.ApplyMask (sources[sourceID], masks[maskID]);
				oldMaskID = maskID;
			}
			break;	 

			//-----------------------------------------------------------  	   	  	
		case 15: // ApplyBooleanOperation
			if (previousFunction != 15)  
			{
				sourceID = 7;
				maskID = 0;
				oldMaskID = 1;

				booleanOperation = TextureUtility.BooleanOperation.Union;
				oldBooleanOperation = TextureUtility.BooleanOperation.Subtraction;

				colorDiapason = Vector2.zero;
				oldColorDiapason = Vector2.one;

				previousFunction = 15;
			}

			GUI.Label(new Rect(225, 65, 400, 30), "SELECT 2ND OPERATOR, OPERATION TYPE AND OFFSET:");

			GUI.Label(new Rect(240, 90, 100, 30), "Second operator:"); 
			for (int i = 0; i < masks.Length; i++)
				if (GUI.Button (new Rect (250 + i*105, 110, 80,80), masks[i]))  maskID = i; 

			GUI.Label(new Rect(240, 197, 100, 30), "Operation:"); 
			toolbarStrings = new string [] {"Union", "Intersection", "Subtraction"};
			booleanOperation = (TextureUtility.BooleanOperation)GUI.Toolbar (new Rect (310, 200, 600, 20),  (int)booleanOperation, toolbarStrings);


			GUI.Label(new Rect(250, 230, 100, 30), "Offset X:"); 	
			colorDiapason.x = GUI.HorizontalSlider (new Rect (318, 235, 345, 20), colorDiapason.x, -sources[sourceID].width, sources[sourceID].width);
			GUI.Label(new Rect(680, 230, 25, 20), Mathf.CeilToInt(colorDiapason.x).ToString());

			GUI.Label(new Rect(250, 250, 100, 30), "Offset Y:"); 	
			colorDiapason.y = GUI.HorizontalSlider (new Rect (318, 255, 345, 20), colorDiapason.y, -sources[sourceID].height, sources[sourceID].height);
			GUI.Label(new Rect(680, 250, 25, 20), Mathf.CeilToInt(colorDiapason.y).ToString());


			if (GUI.Button (new Rect (250, 290, 150, 30), "RESET"))  previousFunction = 0;	


			if(maskID != oldMaskID  ||  oldColorDiapason != colorDiapason  ||  oldBooleanOperation != booleanOperation)
			{
				result =  TextureUtility.ApplyBooleanOperation (booleanOperation, sources[sourceID], masks[maskID], colorDiapason);
				oldMaskID = maskID;
				oldColorDiapason = colorDiapason;
				oldBooleanOperation = booleanOperation;
			}
			break; 	

			//-----------------------------------------------------------  	   	
		case 16: // Rotate
			if (previousFunction != 16)  
			{
				angle = 0;
				oldAngle = -1;

				previousFunction = 16;
				sourceID = 0;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST SLIDER TO ROTATE:"); 


			GUI.Label(new Rect(250, 115, 100, 30), "Angle"); 	
			angle = GUI.HorizontalSlider (new Rect (318, 122, 343, 20), angle, 0, 360);
			GUI.Label(new Rect(680, 115, 25, 20), angle.ToString());


			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;


			if (oldAngle != angle)  
			{	
				result = TextureUtility.Rotate(sources[sourceID], angle);
				oldAngle = angle;
			}
			break;

			//-----------------------------------------------------------  	   	
		case 17: // Scale
			if (previousFunction != 17)  
			{
				rect.width = sources[sourceID].width;
				rect.height = sources[sourceID].height;
				newResult();

				sourceID = 0;
				previousFunction = 17;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST VALUES AND PRESS 'SCALE' BUTTON:"); 

			GUI.Label(new Rect(240, 120, 100, 30), "New width: "); 
			rect.width  = int.Parse(GUI.TextField (new Rect (310, 120, 50, 20), rect.width.ToString(), 4));

			GUI.Label(new Rect(240, 150, 100, 30), "New height: "); 
			rect.height = int.Parse(GUI.TextField (new Rect (310, 150, 50, 20), rect.height.ToString(), 4));

			if (GUI.Button (new Rect (410, 250, 150, 30), "SCALE")) result = TextureUtility.Scale (sources[sourceID], (int)rect.width, (int)rect.height);
			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;
			break;

			//-----------------------------------------------------------  	   	
		case 18: // Stroke
			if (previousFunction != 18)  
			{
				intensity = 5;
				colorDiapason = new Vector2 (0, 1);

				sourceID = 7;
				result = sources[sourceID];
				previousFunction = 18;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "ADJUST VALUES AND PRESS 'STROKE' BUTTON:"); 


			GUI.DrawTexture(new Rect(310, 120, 360, 60), UI_hueTexture);
			GUI.Label(new Rect(250, 115, 100, 30), "Color"); 	
			colorDiapason.x = GUI.HorizontalSlider (new Rect (318, 122, 345, 20), colorDiapason.x, 0.0f, 0.99f);

			GUI.Label(new Rect(250, 180, 100, 30), "Thickness"); 	
			intensity = GUI.HorizontalSlider (new Rect (320, 185, 340, 20), intensity, 0, 10);
			GUI.Label(new Rect(680, 180, 25, 20), intensity.ToString());



			GUI.Label(new Rect(250, 210, 100, 30), "Blend Mode:"); 
			toolbarStrings = new string [] {"Normal", "Additive", "Subtraction", "Multiply", "Subdivide", "MaskAlpha"};
			blendMode = (TextureUtility.BlendMode)GUI.Toolbar (new Rect (330, 205, 600, 30),  (int)blendMode, toolbarStrings);

			if (GUI.Button (new Rect (410, 250, 150, 30), "STROKE")) result = TextureUtility.Stroke (sources[sourceID], (int)intensity, TextureUtility.HSBAtoColor(new Vector4(colorDiapason.x,1,1,1)), blendMode);

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;
			break;	 

			//-----------------------------------------------------------  	   	  	
		case 19: // Clear 
			if (previousFunction != 19)  
			{
				sourceID = 0;
				result = sources[sourceID];
				colorDiapason = new Vector2 (0, 1);
				previousFunction = 19;
			}

			GUI.Label(new Rect(225, 80, 400, 30), "CHOOSE FILLING COLOR AND PRESS 'CLEAR' BUTTON:"); 


			GUI.DrawTexture(new Rect(310, 120, 360, 60), UI_hueTexture);
			GUI.Label(new Rect(250, 115, 100, 30), "Color"); 	
			colorDiapason.x = GUI.HorizontalSlider (new Rect (318, 122, 345, 20), colorDiapason.x, 0.0f, 0.99f);

			color1 =TextureUtility.HSBAtoColor(new Vector4(colorDiapason.x,1,1,1));
			if (GUI.Button (new Rect (410, 250, 150, 30), "CLEAR")) result = TextureUtility.Clear(sources[sourceID], TextureUtility.HSBAtoColor(new Vector4(colorDiapason.x,1,1,1)));

			if (GUI.Button (new Rect (250, 250, 150, 30), "RESET"))  previousFunction = 0;

			break; 	


		}

		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

	}

}