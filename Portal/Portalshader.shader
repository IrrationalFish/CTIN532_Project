Shader "Custom/PortalShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader{
			Pass {
				CGPROGRAM

				#pragma target 3.0
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				float2 _MainTex_TexelSize;

				struct appdata {
					float4 vertex: POSITION;
				};

				struct v2f {
					float4 ScreenVertex : SV_POSITION;
					float4 uv : TEXCOORD0;
				};

				v2f vert(appdata v) {
					v2f o;
					//float4 clipSpacePosition = UnityObjectToClipPos(v.vertex);
					//o.pos = clipSpacePosition;
					//// Copy of clip space position for fragment shader
					//o.uv = clipSpacePosition;
					o.ScreenVertex = UnityObjectToClipPos(v.vertex);
					o.uv = ComputeScreenPos(o.ScreenVertex);
					return o;
				}

				half4 frag(v2f i) : SV_Target {
					// Perspective divide (Translate to NDC - (-1, 1))
					float2 uv = i.uv.xy / i.uv.w;
					//// Map -1, 1 range to 0, 1 tex coord range
					//uv = (uv + float2(1.0, 1.0)) * 0.5;
					//if (_ProjectionParams.x < 0.0)
					//{
					//    uv.y = 1.0 - uv.y;
					//}
					//uv.x = 0.5f + (0.5f - uv.x);

					return tex2D(_MainTex, uv);
				}
				ENDCG
			}
	}
}