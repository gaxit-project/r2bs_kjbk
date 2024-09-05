Shader "Custom/StencilOutline" {
    Properties
    {
        _OutlineWidth("Outline Width", float) = 0.1 // �A�E�g���C���̕����w�肷��v���p�e�B
    }
    SubShader
    {
        // �����_�����O�̃^�C�v�𓧖��ɐݒ肵�A�`��L���[�𓧖��ɐݒ�
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

        // �A�E�g���C����`�悷�邽�߂̍ŏ��̃p�X
        Pass
        {
            // �X�e���V���o�b�t�@��1���������ސݒ�
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            // ���ʂ݂̂�`�悷�邽�߂̐ݒ�
            Cull Front
            ZWrite Off // �[�x�������݂𖳌���
            ZTest Off  // �[�x�e�X�g�𖳌���
            Blend SrcAlpha OneMinusSrcAlpha // ���߃u�����h�̐ݒ�

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION; // ���_�̈ʒu
                half3 normal : NORMAL;   // �@���x�N�g��
            };

            struct v2f
            {
                half4 pos : SV_POSITION; // �N���b�v��Ԃł̈ʒu
            };

            half _OutlineWidth; // �A�E�g���C���̕�

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;

                // �@�������ɒ��_���A�E�g���C���������ړ�������
                o.pos = UnityObjectToClipPos(v.vertex +  normalize(v.normal) * _OutlineWidth);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return 0; // ���S�ɓ����ȐF��Ԃ��i�A�E�g���C�������̕`��j
            }
            ENDCG
        }

        // �I�u�W�F�N�g���̂�`�悷�邽�߂�2�ڂ̃p�X
        Pass
        {
            // �X�e���V���o�b�t�@��2���������ސݒ�
            Stencil
            {
                Ref 2
                Comp always
                Pass replace
            }

            // �\�ʂ݂̂�`�悷�邽�߂̐ݒ�
            Cull Back
            ZWrite Off // �[�x�������݂𖳌���
            ZTest Off  // �[�x�e�X�g�𖳌���
            Blend SrcAlpha OneMinusSrcAlpha // ���߃u�����h�̐ݒ�

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION; // ���_�̈ʒu
                half3 normal : NORMAL;   // �@���x�N�g��
            };

            struct v2f
            {
                half4 pos : SV_POSITION; // �N���b�v��Ԃł̈ʒu
            };

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;

                // �I�u�W�F�N�g�̒��_�����̂܂܃N���b�v��Ԃɕϊ�
                o.pos = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return 0; // ���S�ɓ����ȐF��Ԃ��i�I�u�W�F�N�g���̂̕`��j
            }
            ENDCG
        }
    }
}
