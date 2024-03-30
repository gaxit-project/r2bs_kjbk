Shader "Custom/CombinedShader" {
    Properties {
        _MainTex("Main Texture", 2D) = "white" {}
        _TextureWidth ("TextureWidth", int) = 32
        _TextureHeight("TextureHeight", int) = 32
        _ColorResolution("ColorResolution", int) = 16
    }

    Category {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
        }
        Blend SrcAlpha OneMinusSrcAlpha // Alpha Blended
        ColorMask RGB
        Cull Off
        Lighting Off
        ZWrite Off

        SubShader {
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_particles
                #pragma multi_compile_fog
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                fixed4 _TintColor;

                struct appdata_t {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    float4 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    float2 texcoord2 : TEXCOORD1;
                    UNITY_FOG_COORDS(1)
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                float4 _MainTex_ST;

                v2f vert(appdata_t v) {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.color = v.color;
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.texcoord2.xy = v.texcoord.zw; // Copy TEXCOORD0.zw
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 texColor = i.color * tex2D(_MainTex, i.texcoord);
                    fixed4 col = i.color * step(i.texcoord2.x, texColor.r);
                    col.rgb *= col.a;
                    UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0, 0, 0, 0)); // fog towards black due to our blend mode
                    return col;
                }
                ENDCG
            }
        }

        SubShader {
            Tags {
                "Queue" = "Transparent"
            }
            Pass {
                Cull Back
                ZWrite Off
                BlendOp Add
                Blend SrcAlpha OneMinusSrcAlpha

                GLSLPROGRAM
                uniform int _TextureWidth;
                uniform int _TextureHeight;
                uniform int _ColorResolution;
                uniform sampler2D _MainTexture;

                #ifdef VERTEX
                out vec4 textureCoordinates;
                void main() {
                    gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                    textureCoordinates = gl_MultiTexCoord0;
                }
                #endif

                #ifdef FRAGMENT
                in vec4 textureCoordinates;
                void main() {
                    float x = floor(textureCoordinates.x * _TextureWidth) / _TextureWidth;
                    float y = floor(textureCoordinates.y * _TextureHeight) / _TextureHeight;
                    vec4 color = texture2D(_MainTexture, vec2(x, y));
                    float r = floor(color.r * _ColorResolution) / _ColorResolution;
                    float g = floor(color.g * _ColorResolution) / _ColorResolution;
                    float b = floor(color.b * _ColorResolution) / _ColorResolution;
                    float a = floor(color.a * _ColorResolution) / _ColorResolution;
                    gl_FragColor = vec4(r, g, b, a);
                }
                #endif

                ENDGLSL
            }
        }
    }
}