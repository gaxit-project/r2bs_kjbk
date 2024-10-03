Shader "Custom/SmokeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SmokeDensity ("Smoke Density", Range(0, 1)) = 0.5 // ���̔Z�x�i0�œ����A1�ōő�Z�x�j
        _SmokeColor ("Smoke Color", Color) = (0.5, 0.5, 0.5, 1) // ���̐F�Ɠ����x�i�A���t�@�l���܂ށj
        _CenterRadius ("Center Radius", Range(0, 1)) = 0.2 // ���������̔��a�i0�œ��������Ȃ��A1�ŉ�ʑS�̂������j
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // ��������L����
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

            sampler2D _MainTex; // �e�N�X�`��
            float _SmokeDensity; // ���̔Z�x
            float4 _SmokeColor; // ���̐F�Ɠ����x
            float _CenterRadius; // ���������̔��a

            // ���_�V�F�[�_�[
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // ���_���N���b�v���W�ɕϊ�
                o.uv = v.uv; // UV���W��n��
                return o;
            }

            // �t���O�����g�V�F�[�_�[�i�s�N�Z���P�ʂ̌v�Z�j
            half4 frag (v2f i) : SV_Target
            {
                // ��ʂ̒��S���W��ݒ�i���S��(0.5, 0.5)�j
                float2 center = float2(0.5, 0.5);

                // �s�N�Z����UV���W�ƒ��S�Ƃ̋������v�Z
                float dist = distance(i.uv, center);

                // ���S����̋����ɉ����ĉ��̔Z���𒲐�
                // _CenterRadius �������͓����ɂ��A����ȍ~�ŉ���Z�����Ă���
                float smokeFactor = smoothstep(_CenterRadius, 1.0, dist);

                // ���̐F�ƔZ�x���|�����킹�A�����x���܂߂Ē���
                half4 color = half4(_SmokeColor.rgb, _SmokeColor.a * smokeFactor * _SmokeDensity);

                // �����x���Ⴂ�ꍇ�͊��S�ɓ����ɂ���
                if (color.a <= 0.0)
                {
                    discard;
                }

                return color; // �ŏI�I�ȐF��Ԃ�
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
