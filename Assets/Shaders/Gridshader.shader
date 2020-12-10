Shader "Shaders101/Colored UV"
{
	Properties
	{
		_Width("Width", Range(0.0001,0.05)) = 0.001
		_Size("Object Size", Range(0.01,1000)) = 100
		_MiddlePoint("Middle Point", Range(0, 1)) = 0.6
		_Background("Background Color", Color) = (.2, .2, .2, 1)
		_Foreground("Foreground Color", Color) = (.5, .5, .5, 1)
		_Step("Step", Int) = 5
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"PreviewType" = "Plane"
		}
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 scrPos : TEXCOORD1;
			};

			sampler2D _MainTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float _Width;
			float _Size;
			float4 _Background;
			float _MiddlePoint;
			float4 _Foreground;
			int _Step;

			float4 frag(v2f i) : SV_Target
			{
				float d = length(ObjSpaceViewDir(i.scrPos));
				float w = d * _Width;
				i.uv *= _Size;

				float k = log(d)  / log(_Step);
				float k0 = floor(k);

				float x = d / pow(_Step, k0);

				float s1 = pow(_Step, k0) * .1 / _Step;
				float c1 = clamp(_MiddlePoint * (_Step - x) / (_Step - 1), 0, 1);

				float s2 = s1 * _Step;
				float c2 = clamp((1 - _MiddlePoint) * (_Step - x) / (_Step - 1) + _MiddlePoint, 0, 1);

				float s3 = s2 * _Step;

				float2 uv = i.uv;
				
				float grid1 = clamp(1 - step(w, fmod(uv.x + w * .5, s1)) + 1 - step(w, fmod(uv.y + w * .5, s1)), 0, 1);
				float grid2 = clamp(1 - step(w, fmod(uv.x + w * .5, s2)) + 1 - step(w, fmod(uv.y + w * .5, s2)), 0, 1);
				float grid3 = clamp(1 - step(w, fmod(uv.x + w * .5, s3)) + 1 - step(w, fmod(uv.y + w * .5, s3)), 0, 1);
				grid2 = clamp(grid2 - grid3, 0, 1);
				grid1 = clamp(grid1 - grid2, 0, 1);

				float a = (grid1 * c1 + grid2 * c2 + grid3);

				return (1 - a) * _Background + a * _Foreground;
			}
			ENDCG
		}
	}
}