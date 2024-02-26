#version 420  core


 layout(binding = 0) uniform sampler2D tex;
layout(binding = 1) uniform sampler2D texture2;

uniform vec2 u_Origin;
uniform float u_Multiplier;
uniform float u_Rotation ;
in vec2 VC;
out vec4 fragColor;



vec2 getNtPointsalongVectors(vec2 v1 , vec2 v2 ,int setp, int steps) {
    vec2 v = v2 - v1;
    return  v1 + float(setp) / float(steps)* v;
}


float alignment(vec2 texture_coordinate, vec2 origin, float rotation) {
    // Calculate the direction from the texture coordinate to the origin
    vec2 direction = normalize(origin - texture_coordinate);

    // Convert rotation from degrees to radians
    float rotationRadians = radians(rotation);

    // Calculate the direction of rotation
    vec2 rotationDir = vec2(cos(rotationRadians), sin(rotationRadians));

    // Calculate the dot product between the direction of rotation and the direction to the origin
    float alignment = dot(rotationDir, direction);

    return alignment;
}
float remap(float value, float oldMin, float oldMax, float newMin, float newMax) {
    return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
}

float remapTo360(float value) {
    return remap(value, 0.1, 1.0, 0.0, 360.0);
}

void main()
{
    vec2 texCoord = VC;
    float distance = length(VC - u_Origin);
    distance = distance * u_Multiplier;
    vec4 sum = vec4(0.0);
    float newRed = 0.0;
    float a = alignment(texCoord, u_Origin, u_Rotation + 180.0);
    int beamwidth = 1;
    
    vec2 jj = vec2(0.0,0.0);
    vec4 mm = vec4(0.0);
    if(remapTo360(a ) >360-beamwidth ) {
               newRed = texture(tex,VC).g *  remap( remapTo360(a),360-beamwidth,360,0,1);
                for (int i = 0;  i< 100; i++) {
                             jj = getNtPointsalongVectors(u_Origin,VC,i,100);
                             mm +=  texture(tex,jj)*distance;
                             //mm +=  texture(tex,jj)*10;
                       } 
                
        }
    if(remapTo360(a ) >360-beamwidth*0.1 ) {
               newRed += 0.1;
                
        }
        
    float maxenergy = 1.0*exp(-1 * distance);
        
 
    maxenergy = clamp(maxenergy-mm.g,0.0,1.0 ) ;
    // maxenergy = remap( maxenergy-mm.g,0.0,0.5,0.0,1.0);
           
    clamp(newRed,0.0,1.0);
    newRed = newRed * maxenergy;
    
    
        
    //fragColor = vec4(maxenergy*0.1, newRed + texture(texture2,VC ).g*0.9 ,texture(tex,VC).g, 1.0);
    fragColor = vec4(maxenergy*1, newRed,texture(tex, VC ).r, 1.0);
  
}










