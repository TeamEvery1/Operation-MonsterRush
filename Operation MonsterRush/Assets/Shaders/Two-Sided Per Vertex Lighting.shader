﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Cg two-sided per-vertex lighting"
{
	Properties 
	{
		_Color ("Front Material Diffuse Color", Color)= (1, 1, 1, 1)
		_SpecColor ("Front Material Specular Color" , Color) = (1, 1, 1, 1)
		_Shininess ("Front Material Shininess", Float) = 10
		_BackColor ("Back Material Diffuse Color", Color) = (1, 1, 1, 1)
		_BackSpecColor ("Back Material Specular Color", Color) = (1, 1, 1, 1)
		_BackShininess ("Back Material Shininess", Float) = 10
	}

	SubShader
	{
		Pass
		{
			Tags 
			{
				"LightMode" = "ForwardBase"
			}
			// pass for ambient light and first light source
			Cull Back // render only front faces
		}

		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"
		uniform float4 _LightColor0;
		// color of light source (from "Lighting.cginc")

		// User-specified properties
		uniform float4 _Color;
		uniform float4 _SpecColor;
		uniform float _Shininess;
		uniform float4 _BackColor;
		uniform float4 _BackSpecColor;
		uniform float _BackShininess;

		struct vertexInput
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float4 col : COLOR;
		};

		vertexOutput vert (vertexInput input)
		{
			vertexOutput output;

			float4x4 modelMatrix = unity_ObjectToWorld;
			float4x4 modelMatrixInverse = unity_WorldToObject;

			float3 normalDirection = normalize (mul (float4 (input.normal, 0.0f), modelMatrixInverse).xyz);
			float3 viewDirection = normalize (_WorldSpaceCameraPos - mul (modelMatrix, input.vertex).xyz);
			float3 lightDirection;
			float attenuation;

			if (0.0 == _WorldSpaceLightPos0.w) // directional light?
			{
			 	attenuation = 1.0; // no attenuation
			 	lightDirection = normalize (_WorldSpaceLightPos0.xyz);
			}
			else // point of spot light
			{
				float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul (modelMatrix, input.vertex).xyz;
				float distance = length (vertexToLightSource);
				attenuation = 1.0 / distance; // linear attenuation
				lightDirection = normalize (vertexToLightSource);
			}

			float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
			float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot (normalDirection , lightDirection));
			float3 specularReflection;

			if( dot (normalDirection, lightDirection) < 0.0) // light source on the wrong side?
			{
				specularReflection = float3 (0.0, 0.0, 0.0);
				// no specular reflection	
			}
			else // light source on the right side
			{
				specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb 
				* pow ( max (0.0, dot (reflect ( -lightDirection, normalDirection),
				viewDirection)), _Shininess);
			}

			output.col = float4 (ambientLighting + diffuseReflection + specularReflection, 1.0);
			output.pos = mul (UNITY_MATRIX_MVP, input.vertex);
			return output;
			}

			float4 frag (vertexOutput input) : COLOR
			{
				return input.col;
			}

			ENDCG

			Pass
			{
				Tags
				{
					"LightMode" = "ForwardAdd" // pass for additional light sources
				}

				Blend One One // additive blending
				Cull Back // render only front faces

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag 

				#include "UnityCG.cginc"

				uniform float4 _LightColor0;
				// color of light source (from "Lighting.cginc")

				//User specified properties
				uniform float4 _Color;
				uniform float4 _SpecColor;
				uniform float _Shininess;
				uniform float4 _BackColor;
				uniform float4 _BackSpecColor;
				uniform float _BackShininess;

				struct vertexInput
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct vertexOutput
				{
					float4 pos : SV_POSITION;
					float4 col : COLOR;
				};

				vertexOutput vert (vertexInput input)
				{
					vertexOutput output;

					float4x4 modelMatrix = unity_ObjectToWorld;
					float4x4 modelMatrixInverse = unity_WorldToObject;

					float3 normalDirection = normalize ( mul (float4 (input.normal, 0.0),
					modelMatrixInverse).xyz);
					float3 viewDirection = normalize (_WorldSpaceCameraPos - mul (modelMatrix, input.vertex).xyz);
					float3 lightDirection;
					float attenuation;

					if (0.0 == _WorldSpaceLightPos0.w) // directional light?
					{
						attenuation = 1.0; // no attenuation
						lightDirection = normalize (_WorldSpaceLightPos0.xyz);
					}
					else // point or spot light
					{
						float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul(modelMatrix, input.vertex).xyz;
						float distance = length (vertexToLightSource);
						attenuation = 1.0 / distance; // linear attenuation
						lightDirection = normalize (vertexToLightSource);
					}

					float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb 
					* max (0.0, dot (normalDirection, lightDirection));
					float specularReflection;

					if(dot (normalDirection, lightDirection) < 0.0) // light source on the wrong side?
					{
						specularReflection = float3 (0.0 ,0.0, 0.0);
						// no specular reflection
					}
					else 
					{
						// light source on the right side
						specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb
						* pow (max (0.0, dot (reflect (-lightDirection, normalDirection),
						viewDirection)), _Shininess);
					}

					output.col = float4 (diffuseReflection + specularReflection, 1.0);
					//no ambient contribution in this pass
					output.pos = mul (UNITY_MATRIX_MVP, input.vertex);
					return output;
				}

				float4 frag(vertexOutput input) : COLOR
				{
					return input.col;
				}

				ENDCG
				}
		}
		Fallback "Specular"
}
