Shader "Meta/HoloShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}

        _BackSurfaceColor ("Back surface color", Color) = (0.0, 0.0, 0.0, 0.0)
        _BackRimColor ("Back rim color", Color) = (1.0, 1.0, 1.0, 0.0)
        _BackRimPower ("Back rim Power", Range(0.5, 8.0)) = 3.0
        _BackRimStrength ("Back rim Strength", Range(0.0, 5.0)) = 1.0
        _BackBorderSize ("Back border Size", Range(0.0, 1.0)) = 0.0

        _FrontSurfaceColor ("Front surface color", Color) = (0.0, 0.0, 0.0, 0.0)
        _FrontRimColor ("Front rim color", Color) = (1.0, 1.0, 1.0, 0.0)
        _FrontRimPower ("Front rim Power", Range(0.5, 8.0)) = 3.0
        _FrontRimStrength ("Front rim Strength", Range(0.0, 5.0)) = 1.0
        _FrontBorderSize ("Front border Size", Range(0.0, 1.0)) = 0.0
    }
    SubShader {
        Pass {
            Name "BackFacesFill"
            Tags {"RenderType"="Transparent" "Queue"="Transparent"}
            LOD 200

            Cull Front
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM 

            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct ShaderData {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _BackSurfaceColor;

            ShaderData vert(appdata_tan v) {
                ShaderData o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            float4 frag(ShaderData f) : COLOR {
                return tex2D(_MainTex, f.uv) * _BackSurfaceColor;
            }
            ENDCG
        }

        Pass {
            Name "BackFacesRim"
            //Tags {"RenderType"="Transparent" "Queue"="Transparent"}
            Tags { "RenderType"="Opaque" }
            LOD 200

            Cull Front
            ZWrite Off
            Blend One One
            //Blend SrcAlpha OneMinusSrcAlpha
 
            CGPROGRAM 
 
            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct ShaderData {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            float4 _BackRimColor;
            float _BackRimPower;
            float _BackRimStrength;
            float _BackBorderSize;

            ShaderData vert(appdata_tan v) {
                ShaderData o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.normal = normalize(v.normal);
                o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
                return o;
            }
 
            float4 frag(ShaderData f) : COLOR 
            {
                half rim = 1 - abs(dot(normalize(f.viewDir), f.normal));
                half3 edge = saturate(_BackRimStrength * _BackRimColor.rgb * pow(rim * (1 + _BackBorderSize), _BackRimPower));
                return float4(edge.r, edge.g, edge.b, _BackRimColor.a);
            }
            ENDCG
        }

        Pass {
            Name "FrontFacesFill"
            Tags {"RenderType"="Transparent" "Queue"="Transparent"}
            LOD 200

            Cull Back
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM 

            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct ShaderData {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _FrontSurfaceColor;

            ShaderData vert(appdata_tan v) {
                ShaderData o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            float4 frag(ShaderData f) : COLOR {
                return tex2D(_MainTex, f.uv) * _FrontSurfaceColor;
            }
            ENDCG
        }

        Pass {
            Name "FrontFacesRim"
            //Tags {"RenderType"="Transparent" "Queue"="Transparent"}
            Tags { "RenderType"="Opaque" }
            LOD 200

            Cull Back
            ZWrite Off
            Blend One One
            //Blend SrcAlpha OneMinusSrcAlpha
 
            CGPROGRAM 
 
            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct ShaderData {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            float4 _FrontRimColor;
            float _FrontRimPower;
            float _FrontRimStrength;
            float _FrontBorderSize;

            ShaderData vert(appdata_tan v) {
                ShaderData o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.normal = normalize(v.normal);
                o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
                return o;
            }
 
            float4 frag(ShaderData f) : COLOR 
            {
                half rim = 1 - abs(dot(normalize(f.viewDir), f.normal));
                half3 edge = saturate(_FrontRimStrength * _FrontRimColor.rgb * pow(rim * (1 + _FrontBorderSize), _FrontRimPower));
                return float4(edge.r, edge.g, edge.b, _FrontRimColor.a);
            }
            ENDCG
        }
    } 
    FallBack "Diffuse"
}
