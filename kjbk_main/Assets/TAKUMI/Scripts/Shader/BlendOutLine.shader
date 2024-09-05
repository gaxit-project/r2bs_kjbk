Shader "Custom/BlendOutLine"
{
    Properties
    {
        // アウトラインの色と太さを変更可能にするプロパティ
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Float) = 0.04
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // アウトライン描画用パス
        Pass
        {
            // 前面カリングを使用
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _OutlineColor;

            // 頂点データ
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            // 頂点シェーダーからフラグメントシェーダーへの出力データ
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            // アウトラインの太さを制御するプロパティ
            float _OutlineWidth;

            // 頂点シェーダー：頂点をオブジェクトから離してアウトラインの厚さを表現
            v2f vert (appdata v)
            {
                v2f o;
                v.vertex += float4(v.normal * _OutlineWidth, 0);   // 法線方向にアウトライン分だけ頂点を移動
                o.vertex = UnityObjectToClipPos(v.vertex);  // クリップ空間に変換
                return o;
            }
            
            // フラグメントシェーダー：アウトラインの色を決定
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OutlineColor;  // 指定されたアウトラインの色を使用
                return col;
            }
            ENDCG
        }

        // 通常のオブジェクト描画用パス
        Pass
        {
            // アルファブレンドを使用
            Blend SrcAlpha OneMinusSrcAlpha

            // 背面カリングを使用
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // 頂点データ
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            // 頂点シェーダーからフラグメントシェーダーへの出力データ
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            // 頂点シェーダー：通常の頂点処理
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);  // クリップ空間に変換
                o.normal = UnityObjectToWorldNormal(v.normal);  // 法線をワールド空間に変換
                return o;
            }
            
            // フラグメントシェーダー：光源に基づいたライティング計算
            fixed4 frag (v2f i) : SV_Target
            {                
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));  // 光と法線のドット積によるライティング
                if( nl <= 0.01f ) nl = 0.1f;
                else if( nl <= 0.3f ) nl = 0.3f;
                else nl = 1.0f;
                fixed4 col = fixed4(nl, nl, nl, 1);  // ライティングによる色計算
                return col;
            }
            ENDCG
        }
    }
}
