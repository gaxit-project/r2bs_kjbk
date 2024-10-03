Shader "Custom/SmokePanel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex("NormalTexture", 2D) = "white" {}
        _Alphe ("Alphe" , Float) = 1.0    // 全体の透明度を1に設定
        _CenterRadius ("Center Radius", Range(0, 1)) = 0.2 // 透明部分の半径
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"  // UI向けの処理をインクルード

            struct appdata
            {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                half3 normalWorld : TEXCOORD1;
                half3 tangentWorld : TEXCOORD2;
                half3 binormalWorld : TEXCOORD3;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;   // メインテクスチャ
            sampler2D _NormalTex; // ノーマルマップ
            float _CenterRadius;  // 透明部分の半径
            float _Alphe;         // 全体の透明度

            // 頂点シェーダー
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normalWorld = UnityObjectToWorldNormal(v.normal);
                o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent)).xyz;
                half3 binormal = cross(v.normal, v.tangent) * v.tangent.w;
                o.binormalWorld = normalize(mul(unity_ObjectToWorld, binormal));
                return o;
            }

            // フラグメントシェーダー
            fixed4 frag (v2f i) : SV_Target
            {


                // テクスチャカラーを取得
                fixed4 col = tex2D(_MainTex, i.uv);

                

                // ノーマルマップの処理
                half3 normalmap = UnpackNormal(tex2D(_NormalTex, i.uv));
                half3 normalMapWorld = (i.tangentWorld * normalmap.x) + (i.binormalWorld * normalmap.y) + (i.normalWorld * normalmap.z); 

                // ランバート反射モデル
                half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                half3 ambient = 0.1f;
                half4 lightColor0 = float4(0.9f, 0.9f, 0.9f, 1.0f);
                float NdotL = dot(normalMapWorld, lightDir);
                float diffusePower = max(ambient, NdotL);
                half4 diffuse = diffusePower * col * lightColor0;

                // 透明度を調整
                diffuse.a = _Alphe; // アルファ値を直接設定

                // 画面中央の座標
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                float smokeFactor = smoothstep(_CenterRadius, 1.0, dist);

                // 中心部分を透明にする
                diffuse.a = (_Alphe * 2) * smokeFactor; // アルファに煙の濃さを掛ける

                // 透明度が0以下のピクセルは描画しない
                if (diffuse.a <= 0.0)
                {
                    discard;
                }
                

                return diffuse; // 最終的な色を返す
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
