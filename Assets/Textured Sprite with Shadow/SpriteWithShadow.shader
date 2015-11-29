Shader "2D/SpriteWithShadow"
 {  
     Properties
     {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _SubTex ("Texture", 2D) = "white" {}
        _tiling ("Tiling", float) = 1
        _ShadowColor ("ShadowColor", float) = 1
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
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
  
             sampler2D _MainTex;
             sampler2D _SubTex;
             float _tiling;
             float _ShadowColor;
             float4 _MainTex_TexelSize;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv : TEXCOORD0;
                 float4 color : COLOR;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv : TEXCOORD0;
                 fixed4 color : COLOR;
             };
             
             half isNotEqual(float4 in1, float4 in2)
             {
                float4 dist = in1 - in2;
                half isEqual = dot(dist,dist);
                return isEqual;
             }
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.uv = v.uv;
                 o.color = v.color;
                 return o;
             }
                                                     
             float4 frag(Fragment i) : COLOR
             {
                half sum = 0;
                float depth = 0.005;
                sum += tex2D(_MainTex, float2(i.uv.x - 5.0 * depth, i.uv.y - 5.0 * depth)).w * 0.025;
                sum += tex2D(_MainTex, float2(i.uv.x - 4.0 * depth, i.uv.y - 4.0 * depth)).w * 0.05;
                sum += tex2D(_MainTex, float2(i.uv.x - 3.0 * depth, i.uv.y - 3.0 * depth)).w * 0.09;
                sum += tex2D(_MainTex, float2(i.uv.x - 2.0 * depth, i.uv.y - 2.0 * depth)).w * 0.12;
                sum += tex2D(_MainTex, float2(i.uv.x - 1.0 * depth, i.uv.y - 1.0 * depth)).w * 0.15;
                sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y)).w * 0.16;
                sum += tex2D(_MainTex, float2(i.uv.x + 1.0 * depth, i.uv.y + 1.0 * depth)).w * 0.15;
                sum += tex2D(_MainTex, float2(i.uv.x + 2.0 * depth, i.uv.y + 2.0 * depth)).w * 0.12;
                sum += tex2D(_MainTex, float2(i.uv.x + 3.0 * depth, i.uv.y + 3.0 * depth)).w * 0.09;
                sum += tex2D(_MainTex, float2(i.uv.x + 4.0 * depth, i.uv.y + 4.0 * depth)).w * 0.05;
                sum += tex2D(_MainTex, float2(i.uv.x + 5.0 * depth, i.uv.y + 5.0 * depth)).w * 0.025;
                
                half newsum = sum;
                sum = 0;
                sum += tex2D(_MainTex, float2(i.uv.x - 5.0 * depth, i.uv.y + 5.0 * depth)).w * 0.025;
                sum += tex2D(_MainTex, float2(i.uv.x - 4.0 * depth, i.uv.y + 4.0 * depth)).w * 0.05;
                sum += tex2D(_MainTex, float2(i.uv.x - 3.0 * depth, i.uv.y + 3.0 * depth)).w * 0.09;
                sum += tex2D(_MainTex, float2(i.uv.x - 2.0 * depth, i.uv.y + 2.0 * depth)).w * 0.12;
                sum += tex2D(_MainTex, float2(i.uv.x - 1.0 * depth, i.uv.y + 1.0 * depth)).w * 0.15;
                sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y)).w * 0.16;
                sum += tex2D(_MainTex, float2(i.uv.x + 1.0 * depth, i.uv.y - 1.0 * depth)).w * 0.15;
                sum += tex2D(_MainTex, float2(i.uv.x + 2.0 * depth, i.uv.y - 2.0 * depth)).w * 0.12;
                sum += tex2D(_MainTex, float2(i.uv.x + 3.0 * depth, i.uv.y - 3.0 * depth)).w * 0.09;
                sum += tex2D(_MainTex, float2(i.uv.x + 4.0 * depth, i.uv.y - 4.0 * depth)).w * 0.05;
                sum += tex2D(_MainTex, float2(i.uv.x + 5.0 * depth, i.uv.y - 5.0 * depth)).w * 0.025;
                
                float4 original = tex2D(_MainTex,i.uv);
                float4 o = float4(0,0,0,(sum+newsum)*0.5*frac(_ShadowColor)) + original*original.w;
                float2 tuv = float2(i.uv.x * UNITY_MATRIX_MV[0][0],i.uv.y*UNITY_MATRIX_MV[1][1])*_tiling;
                o = tex2D(_SubTex,tuv).x * o * i.color;
                 return o;
             }
 
             ENDCG
         }
     }
 }