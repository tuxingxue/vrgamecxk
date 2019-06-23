Shader "BulletFX/NoZFight" 
{
	Properties 
	{
      _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
  
	SubShader 
	{
		Tags {"RenderType"="Transparent" "Queue"="Transparent"}
		
		// Render normally
		Pass 
		{
		  ZWrite Off
		  Blend SrcAlpha OneMinusSrcAlpha
		  ColorMask RGB
		  Offset -1,-1
		  
		  SetTexture [_MainTex] 
		  {
		    Combine texture
		  } 
		}
  	 }
}
