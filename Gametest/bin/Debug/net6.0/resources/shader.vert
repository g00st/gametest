#version 400 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec2 vc;

out vec3 fragPosition;
out vec2 VC;
void main()
{
    fragPosition = inPosition;
    VC = vc;
    gl_Position = vec4(inPosition, 1.0);
}