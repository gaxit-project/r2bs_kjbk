Shader "Custom/SmokeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SmokeDensity ("Smoke Density", Range(0, 1)) = 0.5 // 煙の濃度（0で透明、1で最大濃度）
        _SmokeColor ("Smoke Color", Color) = (0.5, 0.5, 0.5, 1) // 煙の色と透明度（アルファ値を含む）
        _CenterRadius ("Center Radius", Range(0, 1)) = 0.2 // 透明部分の半径（0で透明部分なし、1で画面全体が透明）
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // 半透明を有効化
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex; // テクスチャ
            float _SmokeDensity; // 煙の濃度
            float4 _SmokeColor; // 煙の色と透明度
            float _CenterRadius; // 透明部分の半径

            // 頂点シェーダー
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 頂点をクリップ座標に変換
                o.uv = v.uv; // UV座標を渡す
                return o;
            }

            // フラグメントシェーダー（ピクセル単位の計算）
            half4 frag (v2f i) : SV_Target
            {
                // 画面の中心座標を設定（中心は(0.5, 0.5)）
                float2 center = float2(0.5, 0.5);

                // ピクセルのUV座標と中心との距離を計算
                float dist = distance(i.uv, center);

                // 中心からの距離に応じて煙の濃さを調整
                // _CenterRadius より内側は透明にし、それ以降で煙を濃くしていく
                float smokeFactor = smoothstep(_CenterRadius, 1.0, dist);

                // 煙の色と濃度を掛け合わせ、透明度も含めて調整
                half4 color = half4(_SmokeColor.rgb, _SmokeColor.a * smokeFactor * _SmokeDensity);

                // 透明度が低い場合は完全に透明にする
                if (color.a <= 0.0)
                {
                    discard;
                }

                return color; // 最終的な色を返す
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
