Shader "Custom/SmokePanel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex("NormalTexture", 2D) = "white" {}
        _Alphe ("Alphe" , Float) = 1.0    // �S�̂̓����x��1�ɐݒ�
        _CenterRadius ("Center Radius", Range(0, 1)) = 0.2 // ���������̔��a
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
            #include "UnityUI.cginc"  // UI�����̏������C���N���[�h

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

            sampler2D _MainTex;   // ���C���e�N�X�`��
            sampler2D _NormalTex; // �m�[�}���}�b�v
            float _CenterRadius;  // ���������̔��a
            float _Alphe;         // �S�̂̓����x

            // ���_�V�F�[�_�[
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

            // �t���O�����g�V�F�[�_�[
            fixed4 frag (v2f i) : SV_Target
            {


                // �e�N�X�`���J���[���擾
                fixed4 col = tex2D(_MainTex, i.uv);

                

                // �m�[�}���}�b�v�̏���
                half3 normalmap = UnpackNormal(tex2D(_NormalTex, i.uv));
                half3 normalMapWorld = (i.tangentWorld * normalmap.x) + (i.binormalWorld * normalmap.y) + (i.normalWorld * normalmap.z); 

                // �����o�[�g���˃��f��
                half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                half3 ambient = 0.1f;
                half4 lightColor0 = float4(0.9f, 0.9f, 0.9f, 1.0f);
                float NdotL = dot(normalMapWorld, lightDir);
                float diffusePower = max(ambient, NdotL);
                half4 diffuse = diffusePower * col * lightColor0;

                // �����x�𒲐�
                diffuse.a = _Alphe; // �A���t�@�l�𒼐ڐݒ�

                // ��ʒ����̍��W
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                float smokeFactor = smoothstep(_CenterRadius, 1.0, dist);

                // ���S�����𓧖��ɂ���
                diffuse.a = (_Alphe * 2) * smokeFactor; // �A���t�@�ɉ��̔Z�����|����

                // �����x��0�ȉ��̃s�N�Z���͕`�悵�Ȃ�
                if (diffuse.a <= 0.0)
                {
                    discard;
                }
                

                return diffuse; // �ŏI�I�ȐF��Ԃ�
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
