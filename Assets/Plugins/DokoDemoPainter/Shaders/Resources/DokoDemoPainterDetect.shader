﻿// Copyright (c) 2018 Emiliana (twitter.com/Emiliana_vt)

Shader "DokoDemoPainter/Detect" {
Properties {
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _SurfaceID ("Surface ID", Float) = -4.0
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DokoDemoPainterDetect"="True"}
    LOD 0

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 tangent : TANGENT;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float angle : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _DDPPenLast;
            float4 _MainTex_ST;
            float _SurfaceID;
            float _DDPDontSwitch;
            float _DDPInvisibleAlpha;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                // Thanks to bgolus on the Unity community forums for the angle calculation!
                float3 worldTangent = UnityObjectToWorldDir(v.tangent);
                float3 viewTangent = mul((float3x3) UNITY_MATRIX_V, worldTangent);
                o.angle = trunc(fmod(degrees(atan2(viewTangent.y, viewTangent.x)), 360.0)/2.0);
                o.angle = (o.angle < 0.0) ? o.angle + 180.0 : o.angle;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 last = tex2D(_DDPPenLast, float2(0.5, 0.5));
                float4 tex = tex2D(_MainTex, i.texcoord);
                bool sameID = abs(last.b - _SurfaceID) < 0.1;
                bool sameIDorNone = last.b >= 2.0 ? sameID : true;
                bool accept = _DDPDontSwitch > 0.0 ? sameIDorNone : true;
                i.texcoord = abs(frac(i.texcoord));
                i.texcoord.x = i.texcoord.x * 0.5 + trunc(i.angle);
                float3 col = accept ? float3(i.texcoord, _SurfaceID) : float3(0.0, 0.0, 0.0);
                float invisibleAlpha = (accept && (_DDPInvisibleAlpha > 0.0)) ? 1.0 : 0.0;
                float alpha = (accept && (sameID || tex.a > 0.0)) ? 1.0 : invisibleAlpha;
                return float4(col, alpha);
            }
        ENDCG
    }
}

}