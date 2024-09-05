Shader "Custom/BlendOutLine"
{
    Properties
    {
        // �A�E�g���C���̐F�Ƒ�����ύX�\�ɂ���v���p�e�B
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Float) = 0.04
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // �A�E�g���C���`��p�p�X
        Pass
        {
            // �O�ʃJ�����O���g�p
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _OutlineColor;

            // ���_�f�[�^
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ւ̏o�̓f�[�^
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            // �A�E�g���C���̑����𐧌䂷��v���p�e�B
            float _OutlineWidth;

            // ���_�V�F�[�_�[�F���_���I�u�W�F�N�g���痣���ăA�E�g���C���̌�����\��
            v2f vert (appdata v)
            {
                v2f o;
                v.vertex += float4(v.normal * _OutlineWidth, 0);   // �@�������ɃA�E�g���C�����������_���ړ�
                o.vertex = UnityObjectToClipPos(v.vertex);  // �N���b�v��Ԃɕϊ�
                return o;
            }
            
            // �t���O�����g�V�F�[�_�[�F�A�E�g���C���̐F������
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OutlineColor;  // �w�肳�ꂽ�A�E�g���C���̐F���g�p
                return col;
            }
            ENDCG
        }

        // �ʏ�̃I�u�W�F�N�g�`��p�p�X
        Pass
        {
            // �A���t�@�u�����h���g�p
            Blend SrcAlpha OneMinusSrcAlpha

            // �w�ʃJ�����O���g�p
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // ���_�f�[�^
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ւ̏o�̓f�[�^
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            // ���_�V�F�[�_�[�F�ʏ�̒��_����
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);  // �N���b�v��Ԃɕϊ�
                o.normal = UnityObjectToWorldNormal(v.normal);  // �@�������[���h��Ԃɕϊ�
                return o;
            }
            
            // �t���O�����g�V�F�[�_�[�F�����Ɋ�Â������C�e�B���O�v�Z
            fixed4 frag (v2f i) : SV_Target
            {                
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));  // ���Ɩ@���̃h�b�g�ςɂ�郉�C�e�B���O
                if( nl <= 0.01f ) nl = 0.1f;
                else if( nl <= 0.3f ) nl = 0.3f;
                else nl = 1.0f;
                fixed4 col = fixed4(nl, nl, nl, 1);  // ���C�e�B���O�ɂ��F�v�Z
                return col;
            }
            ENDCG
        }
    }
}
