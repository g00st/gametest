

#version 400 core

in vec3 fragPosition;
out vec4 fragColor;
uniform vec4 u_Color;
uniform sampler2D tex;
in vec2 text_cords;

void main()
{
// Scale the texture coordinates
vec2 scaledTextureCoords = text_cords;
scaledTextureCoords.x += (1.0/14.0)*u_Color.w;

// Sample the texture with the modified coordinates
fragColor = texture(tex, scaledTextureCoords);
}