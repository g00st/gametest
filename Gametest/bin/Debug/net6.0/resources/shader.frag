#version 400 core

in vec3 fragPosition;
out vec4 fragColor;
uniform vec4 u_Color;
uniform sampler2D tex;
in vec2 VC;
void main()
{

    // Set a fixed color (e.g., red)
    fragColor =   texture(tex,VC);
    //fragColor =(vec4(1.0,0.0,0.9,1.0));
}