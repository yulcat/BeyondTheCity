Shader "2D/Add"
 {  
     Properties
     {
        _MainTex ("Sprite Texture", 2D) = "white" {}
     }
     SubShader
     {
         Tags 
         { 
             "RenderType" = "Transparent" 
             "Queue" = "Transparent" 
         }
 
         Pass
         {
             ZWrite Off
             Blend DstColor One
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
  
             sampler2D _MainTex;
             fixed4 _AddColor;
             half _ReadyRate;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float4 color : COLOR;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 fixed4 color : COLOR;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.color = v.color;
     
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 float4 o;
                 o = tex2D (_MainTex, IN.uv_MainTex);
				 o.r = o.a;
				 o.b = o.a;
				 o.g = o.a;                
                 return o * IN.color;
             }
 
             ENDCG
         }
     }
 }