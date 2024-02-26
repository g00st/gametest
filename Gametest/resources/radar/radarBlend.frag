#version 420  core


 layout(binding = 0) uniform sampler2D tex;
layout(binding = 1) uniform sampler2D texture2;

uniform vec2 u_Origin;
uniform float u_Multiplier;
in vec2 VC;
out vec4 fragColor;

const int kernelSize = 15; // Increased kernel size
const float kernel[kernelSize] = float[kernelSize](
    0.0044299121055113265, 0.00895781211794, 0.0215963866053, 0.0443683338718, 0.0776744219933, 0.115876621105,
    0.147308056121, 0.159576912161, 0.147308056121, 0.115876621105, 0.0776744219933, 0.0443683338718,
    0.0215963866053, 0.00895781211794, 0.0044299121055113265
);

float remap(float value, float oldMin, float oldMax, float newMin, float newMax) {
    return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
}
void main()
{
   
    vec2 TexCoords = VC;
    vec2 texOffset = 1.0 / textureSize(tex, 0);
    vec3 result = texture(tex, TexCoords).rgb * kernel[0];

    // Calculate distance from fragment to origin
    float distanceToOrigin = distance(TexCoords, u_Origin);

    // Calculate blur intensity based on distance
    float blurIntensity =   2;

    // Horizontal blur
    for (int i = 1; i < kernelSize; ++i) {
        float offset = float(i - (kernelSize / 2)) * blurIntensity;
        result += texture(tex, TexCoords + vec2(offset * texOffset.x, 0)).rgb * kernel[i];
    }

    // Vertical blur
    for (int i = 1; i < kernelSize; ++i) {
        float offset = float(i - (kernelSize / 2)) * blurIntensity;
        result += texture(tex, TexCoords + vec2(0, offset * texOffset.y)).rgb * kernel[i];
    } 
    //result = texture(tex, TexCoords).rgb;
    
    float linewidth = 0.05;
    float marker = 0.0;
    float offset = 0.01* 20/ u_Multiplier;


    float l = 0; 
    /*texture(texture2,VC).g*0.99 +marker;
    if( l <= 0.2) {
        l = l*0.95;
    }*/

    l = texture(texture2,VC).g * exp(-0.05);
    
    if (distanceToOrigin < offset && distanceToOrigin > offset -linewidth*2) {
        marker = (remap(linewidth-abs(distanceToOrigin - offset) , 0,linewidth,  0.0, 1.0));
    }else if(distanceToOrigin > offset){
   
    if ( VC.x <     u_Origin.x + linewidth && VC.x > u_Origin.x - linewidth ) {
       marker = (remap(linewidth-abs(u_Origin.x - VC.x) , 0,linewidth,  0.0, 1.0));
    }
    if ( VC.y <     u_Origin.y + linewidth && VC.y > u_Origin.y - linewidth ) {
        marker = (remap(linewidth-abs(u_Origin.y - VC.y) , 0,linewidth,  0.0, 1.0));
    }}
    
  
    
    float circle = 1.0;
    if (distanceToOrigin > 0.5) {
        circle = 0.0;
    }
    
    fragColor = vec4(result.r,result.g + l,texture(tex,VC).b, 1.0);
    fragColor = vec4(result.r*0.0,result.g + l,0.1, circle);

}