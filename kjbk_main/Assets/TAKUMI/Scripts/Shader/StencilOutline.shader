Shader "Custom/StencilOutline" {
    Properties
    {
        _OutlineWidth("Outline Width", float) = 0.1 // アウトラインの幅を指定するプロパティ
    }
    SubShader
    {
        // レンダリングのタイプを透明に設定し、描画キューを透明に設定
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

        // アウトラインを描画するための最初のパス
        Pass
        {
            // ステンシルバッファに1を書き込む設定
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            // 裏面のみを描画するための設定
            Cull Front
            ZWrite Off // 深度書き込みを無効化
            ZTest Off  // 深度テストを無効化
            Blend SrcAlpha OneMinusSrcAlpha // 透過ブレンドの設定

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION; // 頂点の位置
                half3 normal : NORMAL;   // 法線ベクトル
            };

            struct v2f
            {
                half4 pos : SV_POSITION; // クリップ空間での位置
            };

            half _OutlineWidth; // アウトラインの幅

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;

                // 法線方向に頂点をアウトライン幅だけ移動させる
                o.pos = UnityObjectToClipPos(v.vertex +  normalize(v.normal) * _OutlineWidth);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return 0; // 完全に透明な色を返す（アウトライン部分の描画）
            }
            ENDCG
        }

        // オブジェクト自体を描画するための2つ目のパス
        Pass
        {
            // ステンシルバッファに2を書き込む設定
            Stencil
            {
                Ref 2
                Comp always
                Pass replace
            }

            // 表面のみを描画するための設定
            Cull Back
            ZWrite Off // 深度書き込みを無効化
            ZTest Off  // 深度テストを無効化
            Blend SrcAlpha OneMinusSrcAlpha // 透過ブレンドの設定

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION; // 頂点の位置
                half3 normal : NORMAL;   // 法線ベクトル
            };

            struct v2f
            {
                half4 pos : SV_POSITION; // クリップ空間での位置
            };

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;

                // オブジェクトの頂点をそのままクリップ空間に変換
                o.pos = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return 0; // 完全に透明な色を返す（オブジェクト自体の描画）
            }
            ENDCG
        }
    }
}
