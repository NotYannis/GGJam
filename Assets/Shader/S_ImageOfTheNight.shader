Shader "Unlit/S_ImageOfTheNight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TimeOfDayLightColorRampTex("TimeOfDayLightColorRamp", 2D) = "white"{}
		_TimeOfDay ("Night Transition", Range(0,1)) = 0.0
		
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
			
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};
			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _TimeOfDayLightColorRampTex;
			
			uniform fixed _TimeOfDay;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 texCol = tex2D(_MainTex, i.uv);
				fixed4 skyTint = tex2D(_TimeOfDayLightColorRampTex, fixed2(_TimeOfDay, _TimeOfDay));

				fixed4 col = texCol;
				col.rgb *= skyTint.rgb;
				return col;
			}
			ENDCG
		}
	}
}
