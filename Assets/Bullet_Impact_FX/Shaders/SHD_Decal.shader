Shader "BulletFX/Decal"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
	    _Metallic("Metallic", Range(0,1)) = 0.0
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_ColorTint("Tint", Color) = (1.0, 1.0, 1.0, 1.0)
	}

    SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "ForceNoShadowCasting" = "True" }
		LOD 200
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		//ColorMask RGB
		Offset -20.0,-20.0

		CGPROGRAM
		#pragma surface surf Standard decal:blend 
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed4 _ColorTint;
		fixed  _Specular;

		half _Glossiness;
		half _Metallic;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo	 = c.rgb * _ColorTint.rgb;
			o.Normal	 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Metallic	 = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha		 = c.a;
		}
		ENDCG
	}
}