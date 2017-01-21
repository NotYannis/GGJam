Shader "Unlit/S_Water"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_WaveMask("WaveMask", 2D) = "white" {}

		_NormalTex1("NormalMap1", 2D) = "bump" {}
		_NormalTex2("NormalMap2", 2D) = "bump" {}
		_NormalTex3("NormalMap3", 2D) = "bump" {}
		_NormalProps1("NormalProps1(DirDegree,Speed,Str,null)", Vector) = (1,1,1,1)
		_NormalProps2("NormalProps2(DirDegree,Speed,Str,null)", Vector) = (1,1,1,1)
		_NormalProps3("NormalProps3(DirDegree,Speed,Str,null)", Vector) = (1,1,1,1)
		_WaveSpeedMult("WaveAgitationMult", float) = 1
		_WaveScaleMult("WavesSizeMult", float) = 1


		_RefrColor("Refraction color", COLOR) = (.34, .85, .92, 1)
		_RefrDistort("RefractionStr", Range(0,3.5)) = 0.40
		_RefrOnecent("Refraction_Onecent", Range(0,1)) = 0.40

		
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
		
#define NB_CODEFEDUVSCROLLERS 5
#include "UnityCG.cginc"
			


			fixed4 _Color;

		uniform sampler2D _WaveMask;
		uniform half4 _WaveMask_ST;

		uniform sampler2D _NormalTex1;
		uniform sampler2D _NormalTex2;
		uniform sampler2D _NormalTex3;
		uniform half4 _NormalTex1_ST;
		uniform half4 _NormalTex2_ST;
		uniform half4 _NormalTex3_ST;
		uniform float4 _NormalProps1;
		uniform float4 _NormalProps2;
		uniform float4 _NormalProps3;
		uniform half _WaveSpeedMult;
		uniform half _WaveScaleMult;

		uniform fixed4 _RefrColor;
		uniform float _RefrDistort;
		uniform fixed _RefrOnecent;

		// Script Controlled Vars
		//Panned Texture UV Coords
		uniform fixed TextureUVCoordinates[NB_CODEFEDUVSCROLLERS * 2];
		//Refl Refr
		uniform sampler2D ReflectionTex;
		uniform sampler2D RefractionTex;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;

			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float4 ref : TEXCOORD2;
				float3 normalWorld : TEXCOORD3;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex); // UnityObjectToClipPos(v.vertex);
				o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.ref = ComputeScreenPos(o.pos);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

			// Calc Normal from surface, textures and ripple
			fixed3 normalDirection = fixed3(1,1,1); // i.normalWorld.xyz;
			// Read normals from textures with UVs panned in script
			fixed4 normalTex1 = tex2D(_NormalTex1, i.worldPos.xy * _WaveScaleMult * _NormalTex1_ST.xy + fixed2(TextureUVCoordinates[0], TextureUVCoordinates[1]));
			fixed4 normalTex2 = tex2D(_NormalTex2, i.worldPos.xy * _WaveScaleMult * _NormalTex2_ST.xy + fixed2(TextureUVCoordinates[2], TextureUVCoordinates[3]));
			fixed4 normalTex3 = tex2D(_NormalTex3, i.worldPos.xy * _WaveScaleMult * _NormalTex3_ST.xy + fixed2(TextureUVCoordinates[4], TextureUVCoordinates[5]));
			fixed4 normalTexAll = (normalTex1 + normalTex2 + normalTex3) *0.33;

			normalDirection *= normalTexAll;
			
			//normalDirection = fixed4(1, 1, 1, 1);
			fixed3 unpackedNormal = UnpackNormal(fixed4(normalDirection.xyz, .5)); // basically does a -0.5. If not done ReflRefr dont quite distort


															//Calc Reflection & Refraction
			fixed4 ReflrColor;
			fixed4 refr;
			{
				fixed4 uv2 = i.ref;
				uv2.xy -= unpackedNormal.xy * _RefrDistort;
				refr = tex2Dproj(RefractionTex, UNITY_PROJ_COORD(uv2)) * _RefrColor;

				ReflrColor = refr;
			}


				fixed4 col = lerp(ReflrColor, _Color, _RefrOnecent);
				//col.rgb = unpackedNormal;// normalTexAll; // tex2D(_NormalTex1, (i.worldPos.xy * _WaveScaleMult * _NormalTex1_ST.xy));
				return col;
			}
			ENDCG
		}
	}
}
