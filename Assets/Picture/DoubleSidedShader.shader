Shader "Custom/DoubleSidedShader" 
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader {

			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert addshadow

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG




				// Enable double-sided rendering
				Cull Off

				// Fallback shader
				Pass {
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					struct appdata {
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
					};

					struct v2f {
						float4 pos : POSITION;
						float2 uv : TEXCOORD0;

					};



					sampler2D _MainTex;
					float4 _Color;

					v2f vert(appdata v) {
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = v.uv;
						return o;
					}

					half4 frag(v2f i) : SV_Target {

						half4 texColor = tex2D(_MainTex, i.uv);
						return texColor * _Color;
					}
					ENDCG
				}
	}
		FallBack "Diffuse"
}
