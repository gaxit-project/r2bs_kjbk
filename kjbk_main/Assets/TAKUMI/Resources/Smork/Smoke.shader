Shader "Unlit/Smoke"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex("NormalTexture", 2D) = "white" {}

        _Alphe ("Alphe" , Float) = 0.5
    }
    SubShader
    {
        //���������K������邽�߂ɕK�v
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                half3 normalWorld : TEXCOORD1; //�@��
                half3 viewDir : TEXCOORD2;
                half3 tangentWorld : TEXCOORD3; //�ڐ�
                half3 binormalWorld : TEXCOORD4; //�]�@��
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NormalTex;
            float _Alphe;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                //�@�������[���h���W�n�ɕϊ�
                o.normalWorld = UnityObjectToWorldNormal(v.normal);

                o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent)).xyz; //�ڐ������[���h���W�n�ɕϊ�
                half3 binormal = cross(v.normal, v.tangent) * v.tangent.w; //�ϊ��O�̖@���Ɛڐ�����]�@�����v�Z
                o.binormalWorld = normalize(mul(unity_ObjectToWorld, binormal)); //�]�@�������[���h���W�n�ɕϊ�

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                i.uv.x += _Time.x * 0.1;
                i.uv.y += _Time.y * 0.1;

                

                

                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = fixed4(0.5, 0.5, 0.9, _Alphe); //�ɂ���
                //if()

                half3 normalmap = UnpackNormal(tex2D(_NormalTex, i.uv));
                half3 normalMapWorld = (i.tangentWorld * normalmap.x) + (i.binormalWorld * normalmap.y) + (i.normalWorld * normalmap.z); 


                //�����o�[�g���˃��f��
                
                //���������x�N�g��
                half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // ���C�g�̐ݒ�l
                half3 ambient = 0.1f;
                half4 lightColor0 = float4(0.9f, 0.9f, 0.9f, 1.0f);

                // ���C�g�̔��f
                float NdotL = dot(normalMapWorld, lightDir);
                //float NdotL = dot(i.normalWorld, lightDir);
                float diffusePower = max(ambient, NdotL);
                half4 diffuse = diffusePower * col * lightColor0;
                

                // �u�����E�t�H�����˃��f��

                // ���������x�N�g���Ǝ��_�����x�N�g���̃n�[�t�x�N�g��
                half3 halfDir = normalize(lightDir + i.viewDir);

                //Blinn �ɂ��X�y�L�����ߎ���
                float NdotH = dot(normalMapWorld, halfDir);
                //float NdotH = dot(i.normalWorld, halfDir);
                float3 specularPower = pow(max(0, NdotH), 50);    // ������50�𒲐�

                half4 specColor = float4(1.0, 1.0, 1.0, 1);    // �����̐F�𒲐�
                half4 specular = float4(specularPower, 0.5) * specColor * lightColor0;
                fixed4 output = diffuse + specular;
                output.a = 0.5;
                


                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, output);
                return output;
            }
            ENDCG
        }
    }
}