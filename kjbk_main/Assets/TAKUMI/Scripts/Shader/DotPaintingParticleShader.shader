Shader "Custom/DotPaintingParticleShader"
{
	Properties 
    {
        _TextureWidth ("TextureWidth", int) = 32
        _TextureHeight("TextureHeight", int) = 32
        _ColorResolution("ColorResolution", int) = 16
        _MainTexture("MainTexture", 2D) = "white"{}
	}
	SubShader 
    {
        Tags
        {
            "Queue" = "Transparent"
        }
        Pass
        {
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
            void main()
            {
                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                textureCoordinates = gl_MultiTexCoord0;
            }
            #endif
            
            #ifdef FRAGMENT
            in vec4 textureCoordinates;
            void main()
            {
                float x = floor(textureCoordinates.x * _TextureWidth) / _TextureWidth;
                float y =  floor(textureCoordinates.y * _TextureHeight) / _TextureHeight;
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