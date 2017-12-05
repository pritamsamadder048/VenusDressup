Shader "Custom/SmoothOutlineShader" {
	Properties
	{
		_Color("main Color",Color)=(.5,.5,.5,1)
		_MainTex("Texture",2D)="white"{}
		_OutlineColor("Outline color",Color)=(0,0,0,1)
		_OutlineWidth("Outline Width",Range(1.0,5.0))=1.01
	}
	

	CGINCLUDE
	

	ENDCG

	SubShader
	{

	Tags{"Queue"="Transparent"}
		Pass //Outline Render
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;

			};

			struct v2f
			{
				float4 pos:POSITION;
				float3 normal:NORMAL;
			};

			float _OutlineWidth;
			float4 _OutlineColor;

			v2f vert(appdata v)
			{
				v.vertex.xyz*=_OutlineWidth;

				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
		

				return o;
			}
			half4 frag(v2f i): COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}

		Pass //Normal Render
		{
			ZWrite Off

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]

			}

			Lighting Off

			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
			}
			SetTexture[_MainTex]
			{
				//Combine previous * primary DOUBLE
				Combine texture
			}
			

			
		}
	}
	
}
