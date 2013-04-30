// Per pixel bumped refraction.
// Uses a normal map to distort the image behind, and
// an additional texture to tint the color.

Shader "HeatDistort" {
Properties {
	_BumpAmt  ("Distortion", range (0,128)) = 10
	_MainTex ("Tint Color (RGB)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
}

#LINE 43


Category {

	// We must be transparent, so other objects are drawn before this one.
	Tags { "Queue"="Transparent+100" "RenderType"="Opaque" }


	SubShader {

		// This pass grabs the screen behind the object into a texture.
		// We can access the result in the next pass as _GrabTexture
		GrabPass {							
			Name "BASE"
			Tags { "LightMode" = "Always" }
 		}
 		
 		// Main pass: Take the texture grabbed above and use the bumpmap to perturb it
 		// on to the screen
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }
			
Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 17 to 17
//   d3d9 - ALU: 18 to 18
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
"!!ARBvp1.0
# 17 ALU
PARAM c[13] = { { 0, 0.5 },
		state.matrix.mvp,
		state.matrix.texture[1],
		state.matrix.texture[2] };
TEMP R0;
TEMP R1;
DP4 R1.z, vertex.position, c[4];
DP4 R1.x, vertex.position, c[1];
DP4 R1.y, vertex.position, c[2];
MOV R0.w, R1.z;
DP4 R0.z, vertex.position, c[3];
MOV R0.x, R1;
MOV R0.y, R1;
MOV result.position, R0;
MOV result.texcoord[0].zw, R0;
ADD R1.xy, R1.z, R1;
MOV R0.zw, c[0].x;
MOV R0.xy, vertex.texcoord[0];
DP4 result.texcoord[1].y, R0, c[6];
DP4 result.texcoord[1].x, R0, c[5];
DP4 result.texcoord[2].y, R0, c[10];
DP4 result.texcoord[2].x, R0, c[9];
MUL result.texcoord[0].xy, R1, c[0].y;
END
# 17 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [glstate_matrix_texture1]
Matrix 8 [glstate_matrix_texture2]
"vs_2_0
; 18 ALU
def c12, 0.50000000, 0.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dp4 r1.z, v0, c3
dp4 r1.x, v0, c0
dp4 r1.y, v0, c1
mov r0.y, r1
mov r0.w, r1.z
dp4 r0.z, v0, c2
mov r0.x, r1
mov oPos, r0
mov oT0.zw, r0
mov r1.y, -r1
add r1.xy, r1.z, r1
mov r0.zw, c12.y
mov r0.xy, v1
dp4 oT1.y, r0, c5
dp4 oT1.x, r0, c4
dp4 oT2.y, r0, c9
dp4 oT2.x, r0, c8
mul oT0.xy, r1, c12.x
"
}

SubProgram "gles " {
Keywords { }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;
#define gl_TextureMatrix1 glstate_matrix_texture1
uniform mat4 glstate_matrix_texture1;
#define gl_TextureMatrix2 glstate_matrix_texture2
uniform mat4 glstate_matrix_texture2;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;



attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_2.xy = ((tmpvar_3.xy + tmpvar_3.w) * 0.5);
  tmpvar_2.zw = tmpvar_3.zw;
  highp vec4 tmpvar_4;
  tmpvar_4.zw = vec2(0.0, 0.0);
  tmpvar_4.x = tmpvar_1.x;
  tmpvar_4.y = tmpvar_1.y;
  highp vec4 tmpvar_5;
  tmpvar_5.zw = vec2(0.0, 0.0);
  tmpvar_5.x = tmpvar_1.x;
  tmpvar_5.y = tmpvar_1.y;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = (gl_TextureMatrix1 * tmpvar_4).xy;
  xlv_TEXCOORD2 = (gl_TextureMatrix2 * tmpvar_5).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp float _BumpAmt;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform highp vec4 _GrabTexture_TexelSize;
uniform sampler2D _GrabTexture;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.zw = xlv_TEXCOORD0.zw;
  mediump vec4 tint_2;
  mediump vec4 col_3;
  mediump vec2 bump_4;
  lowp vec2 tmpvar_5;
  tmpvar_5 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0).xy;
  bump_4 = tmpvar_5;
  tmpvar_1.xy = ((((bump_4 * _BumpAmt) * _GrabTexture_TexelSize.xy) * xlv_TEXCOORD0.z) + xlv_TEXCOORD0.xy);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_1.xyw;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2DProj (_GrabTexture, tmpvar_6);
  col_3 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD2);
  tint_2 = tmpvar_8;
  gl_FragData[0] = (col_3 * tint_2);
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;
#define gl_TextureMatrix1 glstate_matrix_texture1
uniform mat4 glstate_matrix_texture1;
#define gl_TextureMatrix2 glstate_matrix_texture2
uniform mat4 glstate_matrix_texture2;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;



attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_2.xy = ((tmpvar_3.xy + tmpvar_3.w) * 0.5);
  tmpvar_2.zw = tmpvar_3.zw;
  highp vec4 tmpvar_4;
  tmpvar_4.zw = vec2(0.0, 0.0);
  tmpvar_4.x = tmpvar_1.x;
  tmpvar_4.y = tmpvar_1.y;
  highp vec4 tmpvar_5;
  tmpvar_5.zw = vec2(0.0, 0.0);
  tmpvar_5.x = tmpvar_1.x;
  tmpvar_5.y = tmpvar_1.y;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = (gl_TextureMatrix1 * tmpvar_4).xy;
  xlv_TEXCOORD2 = (gl_TextureMatrix2 * tmpvar_5).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp float _BumpAmt;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform highp vec4 _GrabTexture_TexelSize;
uniform sampler2D _GrabTexture;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.zw = xlv_TEXCOORD0.zw;
  mediump vec4 tint_2;
  mediump vec4 col_3;
  mediump vec2 bump_4;
  lowp vec3 normal_5;
  normal_5.xy = ((texture2D (_BumpMap, xlv_TEXCOORD1).wy * 2.0) - 1.0);
  normal_5.z = sqrt(((1.0 - (normal_5.x * normal_5.x)) - (normal_5.y * normal_5.y)));
  lowp vec2 tmpvar_6;
  tmpvar_6 = normal_5.xy;
  bump_4 = tmpvar_6;
  tmpvar_1.xy = ((((bump_4 * _BumpAmt) * _GrabTexture_TexelSize.xy) * xlv_TEXCOORD0.z) + xlv_TEXCOORD0.xy);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_1.xyw;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2DProj (_GrabTexture, tmpvar_7);
  col_3 = tmpvar_8;
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_MainTex, xlv_TEXCOORD2);
  tint_2 = tmpvar_9;
  gl_FragData[0] = (col_3 * tint_2);
}



#endif"
}

SubProgram "flash " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [glstate_matrix_texture1]
Matrix 8 [glstate_matrix_texture2]
"agal_vs
c12 0.5 0.0 0.0 0.0
[bc]
bdaaaaaaabaaaeacaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 r1.z, a0, c3
bdaaaaaaabaaabacaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 r1.x, a0, c0
bdaaaaaaabaaacacaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 r1.y, a0, c1
aaaaaaaaaaaaacacabaaaaffacaaaaaaaaaaaaaaaaaaaaaa mov r0.y, r1.y
aaaaaaaaaaaaaiacabaaaakkacaaaaaaaaaaaaaaaaaaaaaa mov r0.w, r1.z
bdaaaaaaaaaaaeacaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 r0.z, a0, c2
aaaaaaaaaaaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r1.x
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
aaaaaaaaaaaaamaeaaaaaaopacaaaaaaaaaaaaaaaaaaaaaa mov v0.zw, r0.wwzw
bfaaaaaaabaaacacabaaaaffacaaaaaaaaaaaaaaaaaaaaaa neg r1.y, r1.y
abaaaaaaabaaadacabaaaakkacaaaaaaabaaaafeacaaaaaa add r1.xy, r1.z, r1.xyyy
aaaaaaaaaaaaamacamaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.zw, c12.y
aaaaaaaaaaaaadacadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov r0.xy, a3
bdaaaaaaabaaacaeaaaaaaoeacaaaaaaafaaaaoeabaaaaaa dp4 v1.y, r0, c5
bdaaaaaaabaaabaeaaaaaaoeacaaaaaaaeaaaaoeabaaaaaa dp4 v1.x, r0, c4
bdaaaaaaacaaacaeaaaaaaoeacaaaaaaajaaaaoeabaaaaaa dp4 v2.y, r0, c9
bdaaaaaaacaaabaeaaaaaaoeacaaaaaaaiaaaaoeabaaaaaa dp4 v2.x, r0, c8
adaaaaaaaaaaadaeabaaaafeacaaaaaaamaaaaaaabaaaaaa mul v0.xy, r1.xyyy, c12.x
aaaaaaaaabaaamaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.zw, c0
aaaaaaaaacaaamaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v2.zw, c0
"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 9 to 9, TEX: 3 to 3
//   d3d9 - ALU: 8 to 8, TEX: 3 to 3
SubProgram "opengl " {
Keywords { }
Vector 0 [_GrabTexture_TexelSize]
Float 1 [_BumpAmt]
SetTexture 0 [_GrabTexture] 2D
SetTexture 1 [_BumpMap] 2D
SetTexture 2 [_MainTex] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
OPTION ARB_fog_exp2;
# 9 ALU, 3 TEX
PARAM c[3] = { program.local[0..1],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEX R0.yw, fragment.texcoord[1], texture[1], 2D;
TEX R1, fragment.texcoord[2], texture[2], 2D;
MAD R0.xy, R0.wyzw, c[2].x, -c[2].y;
MUL R0.xy, R0, c[1].x;
MUL R0.xy, R0, c[0];
MAD R0.xy, R0, fragment.texcoord[0].z, fragment.texcoord[0];
MOV R0.z, fragment.texcoord[0].w;
TXP R0, R0.xyzz, texture[0], 2D;
MUL result.color, R0, R1;
END
# 9 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Vector 0 [_GrabTexture_TexelSize]
Float 1 [_BumpAmt]
SetTexture 0 [_GrabTexture] 2D
SetTexture 1 [_BumpMap] 2D
SetTexture 2 [_MainTex] 2D
"ps_2_0
; 8 ALU, 3 TEX
dcl_2d s1
dcl_2d s0
dcl_2d s2
def c2, 2.00000000, -1.00000000, 0, 0
dcl t0
dcl t1.xy
dcl t2.xy
texld r0, t1, s1
mov r0.x, r0.w
mad_pp r0.xy, r0, c2.x, c2.y
mul r0.xy, r0, c1.x
mul r0.xy, r0, c0
mad r1.xy, r0, t0.z, t0
mov r1.w, t0
texld r0, t2, s2
texldp r1, r1, s0
mul_pp r0, r1, r0
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES"
}

SubProgram "flash " {
Keywords { }
Vector 0 [_GrabTexture_TexelSize]
Float 1 [_BumpAmt]
SetTexture 0 [_GrabTexture] 2D
SetTexture 1 [_BumpMap] 2D
SetTexture 2 [_MainTex] 2D
"agal_ps
c2 2.0 -1.0 0.0 0.0
[bc]
ciaaaaaaaaaaapacabaaaaoeaeaaaaaaabaaaaaaafaababb tex r0, v1, s1 <2d wrap linear point>
aaaaaaaaaaaaabacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r0.w
adaaaaaaaaaaadacaaaaaafeacaaaaaaacaaaaaaabaaaaaa mul r0.xy, r0.xyyy, c2.x
abaaaaaaaaaaadacaaaaaafeacaaaaaaacaaaaffabaaaaaa add r0.xy, r0.xyyy, c2.y
adaaaaaaaaaaadacaaaaaafeacaaaaaaabaaaaaaabaaaaaa mul r0.xy, r0.xyyy, c1.x
adaaaaaaaaaaadacaaaaaafeacaaaaaaaaaaaaoeabaaaaaa mul r0.xy, r0.xyyy, c0
adaaaaaaabaaadacaaaaaafeacaaaaaaaaaaaakkaeaaaaaa mul r1.xy, r0.xyyy, v0.z
abaaaaaaabaaadacabaaaafeacaaaaaaaaaaaaoeaeaaaaaa add r1.xy, r1.xyyy, v0
aaaaaaaaabaaaiacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa mov r1.w, v0
ciaaaaaaaaaaapacacaaaaoeaeaaaaaaacaaaaaaafaababb tex r0, v2, s2 <2d wrap linear point>
aeaaaaaaacaaapacabaaaaoeacaaaaaaabaaaappacaaaaaa div r2, r1, r1.w
ciaaaaaaabaaapacacaaaafeacaaaaaaaaaaaaaaafaababb tex r1, r2.xyyy, s0 <2d wrap linear point>
adaaaaaaaaaaapacabaaaaoeacaaaaaaaaaaaaoeacaaaaaa mul r0, r1, r0
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

}

#LINE 90

		}
	}

	// ------------------------------------------------------------------
	// Fallback for older cards and Unity non-Pro
	
	SubShader {
		Blend DstColor Zero
		Pass {
			Name "BASE"
			SetTexture [_MainTex] {	combine texture }
		}
	}
}

}
