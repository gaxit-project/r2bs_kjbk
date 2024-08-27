Shader "Custom/Blur"
{
    Properties
    {
        // テクスチャを指定するプロパティ
        _MainTex("Texture", 2D) = "white" {}
        // ブラーの強さを制御するプロパティ
        _Blur("Blur", Float) = 10
    }
    SubShader
    {
        // このタグは、オブジェクトが透明なものとしてレンダリングされる順序を指定
        Tags{ "Queue" = "Transparent" }

        // 画面上の現在の映像をキャプチャするためのパス
        GrabPass
        {   
        }

        // 水平方向のブラーを適用するためのパス
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // アプリケーションデータの構造体（頂点データ）
            struct appdata
            {
                float4 vertex : POSITION;  // 頂点の位置
                float2 uv : TEXCOORD0;     // テクスチャ座標
                fixed4 color : COLOR;      // 頂点カラー
            };

            // 頂点シェーダーからフラグメントシェーダーへの出力データの構造体
            struct v2f
            {
                float4 grabPos : TEXCOORD0; // グラブされたスクリーン座標
                float4 pos : SV_POSITION;   // クリップ空間の頂点位置
                float4 vertColor : COLOR;   // 頂点カラー
            };

            // 頂点シェーダー
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);  // クリップ空間に変換
                o.grabPos = ComputeGrabScreenPos(o.pos); // グラブパスのスクリーン座標を計算
                o.vertColor = v.color;                  // 頂点カラーを渡す
                return o;
            }

            // キャプチャされたスクリーンテクスチャのサンプラー
            sampler2D _GrabTexture;
            // テクスチャのサイズ情報
            fixed4 _GrabTexture_TexelSize;

            // ブラーの強さを示す変数
            float _Blur;

            // フラグメントシェーダー（水平方向のブラー適用）
            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;  // ブラー強度を取得
                blur = max(1, blur); // ブラー強度が1未満にならないようにする

                // 初期化：黒色と透明
                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0; // 重みの合計

                // 水平方向にブラーを適用
                [loop]
                for (float x = -blur; x <= blur; x += 1)
                {
                    float distance_normalized = abs(x / blur); // 距離の正規化
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0); // ガウス分布に基づく重み計算
                    weight_total += weight; // 重みを合計に追加
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(x * _GrabTexture_TexelSize.x, 0, 0, 0)) * weight; // 重みを適用して色を取得
                }

                col /= weight_total; // 正規化して色を確定
                return col; // 計算結果の色を返す
            }
            ENDCG
        }

        // もう一度画面上の映像をキャプチャするためのパス
        GrabPass
        {   
        }

        // 垂直方向のブラーを適用するためのパス
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ここは上のPassと同じ内容なので省略
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 vertColor : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.vertColor = v.color;
                return o;
            }

            sampler2D _GrabTexture;
            fixed4 _GrabTexture_TexelSize;

            float _Blur;

            // フラグメントシェーダー（垂直方向のブラー適用）
            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;
                blur = max(1, blur);

                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0;

                // 垂直方向にブラーを適用
                [loop]
                for (float y = -blur; y <= blur; y += 1)
                {
                    float distance_normalized = abs(y / blur); // 距離の正規化
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0); // ガウス分布に基づく重み計算
                    weight_total += weight; // 重みを合計に追加
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(0, y * _GrabTexture_TexelSize.y, 0, 0)) * weight; // 重みを適用して色を取得
                }

                col /= weight_total; // 正規化して色を確定
                return col; // 計算結果の色を返す
            }
            ENDCG
        }
    }
}
