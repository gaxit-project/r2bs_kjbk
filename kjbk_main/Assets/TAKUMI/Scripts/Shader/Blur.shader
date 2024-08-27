Shader "Custom/Blur"
{
    Properties
    {
        // �e�N�X�`�����w�肷��v���p�e�B
        _MainTex("Texture", 2D) = "white" {}
        // �u���[�̋����𐧌䂷��v���p�e�B
        _Blur("Blur", Float) = 10
    }
    SubShader
    {
        // ���̃^�O�́A�I�u�W�F�N�g�������Ȃ��̂Ƃ��ă����_�����O����鏇�����w��
        Tags{ "Queue" = "Transparent" }

        // ��ʏ�̌��݂̉f�����L���v�`�����邽�߂̃p�X
        GrabPass
        {   
        }

        // ���������̃u���[��K�p���邽�߂̃p�X
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // �A�v���P�[�V�����f�[�^�̍\���́i���_�f�[�^�j
            struct appdata
            {
                float4 vertex : POSITION;  // ���_�̈ʒu
                float2 uv : TEXCOORD0;     // �e�N�X�`�����W
                fixed4 color : COLOR;      // ���_�J���[
            };

            // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ւ̏o�̓f�[�^�̍\����
            struct v2f
            {
                float4 grabPos : TEXCOORD0; // �O���u���ꂽ�X�N���[�����W
                float4 pos : SV_POSITION;   // �N���b�v��Ԃ̒��_�ʒu
                float4 vertColor : COLOR;   // ���_�J���[
            };

            // ���_�V�F�[�_�[
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);  // �N���b�v��Ԃɕϊ�
                o.grabPos = ComputeGrabScreenPos(o.pos); // �O���u�p�X�̃X�N���[�����W���v�Z
                o.vertColor = v.color;                  // ���_�J���[��n��
                return o;
            }

            // �L���v�`�����ꂽ�X�N���[���e�N�X�`���̃T���v���[
            sampler2D _GrabTexture;
            // �e�N�X�`���̃T�C�Y���
            fixed4 _GrabTexture_TexelSize;

            // �u���[�̋����������ϐ�
            float _Blur;

            // �t���O�����g�V�F�[�_�[�i���������̃u���[�K�p�j
            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;  // �u���[���x���擾
                blur = max(1, blur); // �u���[���x��1�����ɂȂ�Ȃ��悤�ɂ���

                // �������F���F�Ɠ���
                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0; // �d�݂̍��v

                // ���������Ƀu���[��K�p
                [loop]
                for (float x = -blur; x <= blur; x += 1)
                {
                    float distance_normalized = abs(x / blur); // �����̐��K��
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0); // �K�E�X���z�Ɋ�Â��d�݌v�Z
                    weight_total += weight; // �d�݂����v�ɒǉ�
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(x * _GrabTexture_TexelSize.x, 0, 0, 0)) * weight; // �d�݂�K�p���ĐF���擾
                }

                col /= weight_total; // ���K�����ĐF���m��
                return col; // �v�Z���ʂ̐F��Ԃ�
            }
            ENDCG
        }

        // ������x��ʏ�̉f�����L���v�`�����邽�߂̃p�X
        GrabPass
        {   
        }

        // ���������̃u���[��K�p���邽�߂̃p�X
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // �����͏��Pass�Ɠ������e�Ȃ̂ŏȗ�
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

            // �t���O�����g�V�F�[�_�[�i���������̃u���[�K�p�j
            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;
                blur = max(1, blur);

                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0;

                // ���������Ƀu���[��K�p
                [loop]
                for (float y = -blur; y <= blur; y += 1)
                {
                    float distance_normalized = abs(y / blur); // �����̐��K��
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0); // �K�E�X���z�Ɋ�Â��d�݌v�Z
                    weight_total += weight; // �d�݂����v�ɒǉ�
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(0, y * _GrabTexture_TexelSize.y, 0, 0)) * weight; // �d�݂�K�p���ĐF���擾
                }

                col /= weight_total; // ���K�����ĐF���m��
                return col; // �v�Z���ʂ̐F��Ԃ�
            }
            ENDCG
        }
    }
}
