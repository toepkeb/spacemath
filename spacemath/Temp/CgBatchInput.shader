Shader "Custom Shaders/TransparentDiffuseM" {
	Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Color ("Main Color", color) = (1, 1, 1, 1)
}
SubShader {
	Tags {"Queue"="Transparent"  "RenderType"="Opaque" }
	LOD 150
 //"LightMode" = "Always"
CGPROGRAM
#pragma surface surf Lambert alpha noforwardadd
 //noforwardadd
sampler2D _MainTex;
fixed4 _Color;
 
struct Input {
	float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}
 
Fallback "Mobile/VertexLit"
}