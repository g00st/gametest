#version 400 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec2 vc;
uniform mat4 u_MVP;
out vec3 fragPosition;
out vec2 VC;
void main()
{
    fragPosition = inPosition;
    VC = vc;
    gl_Position =  u_MVP* vec4(inPosition, 1.0);
}