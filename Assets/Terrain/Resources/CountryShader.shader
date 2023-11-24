Shader "Unlit/CountryShader"
{
    Properties
    {
        _Alpha ("Alpha", Range(0,1)) = 1
    }
    SubShader
    {
        //write an transparent unlit shader 
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		GrabPass { "_GrabTexture" }
        LOD 100
        
        Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 grabPassPos : TEXCOORD1;
				float4 vertex : SV_POSITION;
				
			};
			
			float _Alpha;
			sampler2D _GrabTexture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.grabPassPos = ComputeGrabScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 white = fixed4(1,1,1,1);
				fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPassPos));
				col = lerp(white, col, 1 - _Alpha);
				return col;
			}
			ENDCG
		}
    }
}
