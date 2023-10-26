#version 400 core

layout(location = 0) in vec2 inPosition;
layout(location = 1) in vec2 Text_cords;
uniform mat4 u_MVP;
out vec3 fragPosition;
out vec2 text_cords;
void main()
{
    fragPosition = vec3(inPosition,0);
    text_cords =Text_cords;
    gl_Position =  u_MVP* vec4(inPosition,0.0, 1.0);
}