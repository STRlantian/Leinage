Shader "PaneFlash" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

			//����ͼ
			_MaskTex("Mask Texture",2D) = "white"{}
		//������ɫ
		_LightColor("MoveLightColor", Color) = (1,1,1,1)
			//�����ٶ�
			_Speed("Speed", Range(-5,5)) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _MaskTex;
			float4 _LightColor;
			float _Speed;

			struct Input
			{
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				//ͨ��cos������-1��1֮�����ڼ���
				fixed v = cos(_Time.y * _Speed);
				//��ȡҪ���к�����Ч�����������������ʹ����ɫͨ������������߲���alpha��������Ϊ͸��ͼռ�ڴ�
				fixed maskb = tex2D(_MaskTex, IN.uv_MainTex).b;
				//��������������ӵ���ɫ cos�������-1��1����������Ҫ0��1������ֻ�ܣ����������ֵ+1��/2
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color + (v + 1) * 0.5f * _LightColor * maskb;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}