#version 400 core

in vec3 fragPosition;
out vec4 fragColor;
uniform sampler2D tex;
in vec2 text_cords;
void main()
{

    // Set a fixed color (e.g., red)
    fragColor =   texture(tex,text_cords);
    //fragColor =(vec4(1.0,0.0,0.9,1.0));
}