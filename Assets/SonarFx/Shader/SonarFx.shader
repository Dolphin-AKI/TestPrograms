//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
Shader "Hidden/SonarFX"
{
	Properties
	{
		_SonarBaseColor("Base Color",  Color) = (0.1, 0.1, 0.1, 0)
		_SonarWaveColor("Wave Color",  Color) = (1.0, 0.1, 0.1, 0)
		_SonarWaveParams("Wave Params", Vector) = (1, 20, 20, 10)
		_SonarWaveVector("Wave Vector", Vector) = (0, 0, 1, 0)
		_SonarAddColor("Add Color",   Color) = (0, 0, 0, 0)
		_SonarTime("Sonar Time", Float) = 0
    }
    SubShader
    {
        Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent" 
		}

        CGPROGRAM

        #pragma surface surf Lambert alpha
        #pragma multi_compile SONAR_DIRECTIONAL SONAR_SPHERICAL

        struct Input
        {
            float3 worldPos;
        };

        float3 _SonarBaseColor;
        float3 _SonarWaveColor;
        float4 _SonarWaveParams; // Amp, Exp, Remnant, Speed
        float3 _SonarWaveVector;
        float3 _SonarAddColor;
		float _SonarTime;

        void surf(Input IN, inout SurfaceOutput o)
        {
#ifdef SONAR_DIRECTIONAL
            float l = dot(IN.worldPos, _SonarWaveVector);
#else
            float l = length(IN.worldPos - _SonarWaveVector);
#endif

			// sonar position
			float wl = _SonarTime * _SonarWaveParams.w;
			float wm = _SonarTime * _SonarWaveParams.w - _SonarWaveParams.z;

			// color weight
			float w = 0;

			if (wl > l && wm < l) {
				w = (l - wm) / _SonarWaveParams.z;
			}

			//ソナー末端での拡散
			w *= _SonarWaveParams.y;

            // Get modulo (w % params.z / params.z)
            //w /= _SonarWaveParams.z;
            //w = w - floor(w);

			



            // Make the gradient steeper.
            //float p = _SonarWaveParams.y;
            //w = (pow(w, p) + pow(1 - w, p * 4)) * 0.5;

            // Amplify.
            //w *= _SonarWaveParams.x;

            // Apply to the surface.
            o.Albedo = _SonarBaseColor * w;
            o.Emission = _SonarWaveColor * w + _SonarAddColor;
			o.Alpha = w;
			
        }

        ENDCG
    } 
    Fallback "Diffuse"
}
