using System.Diagnostics;
using Gametest.Template;
using OpenTK.Mathematics;

namespace Gametest;

public class Radar
{
    public Texture _Screentexture;
    public Texture _ScreenTexture2;
    public Texture _ScreenTexture3;
    
    private Texture _kernel;
    private Texture _BaseScenery;
    private float zoom; //in meters
    private float _rotation;
    private Vector2 _position;
    private SubView _baseView;
    private SubView _Screenview;
    private SubView _Screenview2;
    private SubView _Screenview3;
    

    public float maxAngle;
    public float minAngle;
    public bool sweep;
    private int direction;
    
    public Texture DebugTexture;
    public SubView DebugView; 
    private RadarShader _radarShader;
    private RadarBlendShader _radarBlendShader;
    double antnnaRotation;
    public float antnnaSpeed;
    private ColoredRectangle z;
    private ColoredRectangle maxangle;
    private ColoredRectangle minangle;    


    public Radar()
    {
        DebugTexture = new Texture(1000, 1000);
        _BaseScenery = new Texture(1000, 1000);
        _Screentexture= new Texture(1000, 1000);
        _ScreenTexture2 = new Texture(1000, 1000);
        _ScreenTexture3 = new Texture(1000, 1000);
        //_kernel = new Texture( "/resources/radar/Kernel.png");
        _position = new Vector2(0,0);
        _rotation = 0;
         DebugView = new SubView(new VBO(DebugTexture));
         _baseView = new SubView(new VBO(_BaseScenery));
         _Screenview = new SubView(new VBO(_Screentexture));
         _Screenview2 = new SubView(new VBO(_ScreenTexture2));
         _Screenview3 = new SubView(new VBO(_ScreenTexture3));
         
         _radarShader = new RadarShader();
            _radarBlendShader = new RadarBlendShader();
         direction = 1;
         sweep = true;
         minAngle = 0;
         maxAngle = 360;
         
         
         TexturedRectangle t = new TexturedRectangle( new Vector2(0,0), new Vector2(1000,1000), _BaseScenery,_radarShader);
         t.DrawInfo.mesh.Texture = _ScreenTexture3;
         _Screenview.addObject(t);
         
         TexturedRectangle Blender = new TexturedRectangle( new Vector2(0,0), new Vector2(1000,1000), _Screentexture,_radarBlendShader);
         Blender.DrawInfo.mesh.Texture = _ScreenTexture3;
         _Screenview2.addObject(Blender);
         
         z = new ColoredRectangle(_position, new Vector2(1000, 10), Color4.Green);
         maxangle = new ColoredRectangle(_position, new Vector2(1000, 10), Color4.Red);
         minangle = new ColoredRectangle(_position, new Vector2(1000, 10), Color4.Blue);
            
         DebugView.addObject(new TexturedRectangle(new Vector2(0,0), new Vector2(1000,1000), _BaseScenery));
            DebugView.addObject(minangle);
            DebugView.addObject(maxangle);
        // DebugView.addObject(new TexturedRectangle(new Vector2(1000,0), new Vector2(1000,1000), _Screentexture));
         DebugView.addObject(z);
        // _Screenview.addObject(z);
         _Screenview3.addObject(new TexturedRectangle(new Vector2(0,0), new Vector2(1000,1000), _ScreenTexture2));
         z.DrawInfo.Position = new Vector2(500,500);
         minangle.DrawInfo.Position = new Vector2(500,500);
            maxangle.DrawInfo.Position = new Vector2(500,500);
         _radarShader.setOrigin(new Vector2(500,500) );
         _radarBlendShader.setOrigin(new Vector2(500,500) );
       
    }
    
    
    
    public void AddObject(DrawObject obj)
    {
        _baseView.addObject(obj);
    }
    public void Draw( double time)
    {
        
        
        
        antnnaRotation += 0.1 * ((double) antnnaSpeed)  * time * direction;
        
       
        if (sweep == false)
        {
            if (antnnaRotation> maxAngle)
            {
                antnnaRotation = minAngle;
            }
        }else
        {
            if (antnnaRotation> maxAngle)
            {
                antnnaRotation = maxAngle;
                direction = -1;
            }
            if (antnnaRotation< minAngle)
            {
                antnnaRotation = minAngle;
                direction = 1;
            }
        }
       
        //Console.Write(antnnaRotation + "\n");
        z.DrawInfo.Rotation = (float) MathHelper.DegreesToRadians(antnnaRotation);
        minangle.DrawInfo.Rotation = (float) MathHelper.DegreesToRadians(minAngle);
        maxangle.DrawInfo.Rotation = (float) MathHelper.DegreesToRadians(maxAngle);
        _radarShader.setRotation( (float) antnnaRotation);
        _baseView.draw();
        _Screenview.draw();
         _Screenview2.draw();
        _Screenview3.draw();
        DebugView.draw();
       
        
    }
    public void setMaxAngle(float angle)
    {
        if (angle > minAngle && Math.Abs(angle-  minAngle  ) <= 360)
        {
            maxAngle = angle;
        }
        
    }
    public void setMinAngle(float angle)
    {
        if (angle < maxAngle  && Math.Abs( maxAngle- angle) < 360)
        {
            minAngle = angle;
        }
        
    }
    public void setSweep(bool s)
    {
        sweep = s;
    }
    
    public void SetPosition(Vector2 pos)
    {
        _position = pos;
        _baseView.vpossition = pos; 
       // z.DrawInfo.Position = _position + new Vector2(0 , 0);
       // _radarShader.setOrigin(_position );
    }
    
    public void SetZoom(float z)
    {
        _baseView.vsize = new Vector2(z,z);
        float s = (10.0f / 1000.0f) * z;
        _radarBlendShader.setMultiplier( s );
        _radarShader.setMultiplier( s/10f );
    }
}



class RadarShader : Shader
{
    public Vector2 Origin;
    public float multiplier;
    public RadarShader() : base("resources/radar/radar.vert", "resources/radar/radar.frag")
    {
        Origin = new Vector2(0,0);
        multiplier = 1;
        
    
        this.Bind();
        this.setUniformV2f("u_Origin", Origin);
        this.setUniform1v("u_Multiplier", multiplier);
        
    }
   
    public void setOrigin(Vector2 origin)
    {
        Origin = origin/1000;
        this.Bind();
        this.setUniformV2f("u_Origin", Origin);
    }
    public void setMultiplier(float m)
    {
        multiplier = m;
        this.Bind();
        this.setUniform1v("u_Multiplier", multiplier);
    }
    public void setRotation(float r)
    {
        this.Bind();
        this.setUniform1v("u_Rotation", r);
    }
}

class RadarBlendShader : Shader
{
    public Vector2 Origin;
    public float multiplier;
    public RadarBlendShader() : base("resources/radar/radar.vert", "resources/radar/radarBlend.frag")
    {
        Origin = new Vector2(0,0);
        multiplier = 10;
        
    
        this.Bind();
        this.setUniformV2f("u_Origin", Origin);
        this.setUniform1v("u_Multiplier", multiplier);
        
    }
   
    public void setOrigin(Vector2 origin)
    {
        Origin = origin/1000;
        this.Bind();
        this.setUniformV2f("u_Origin", Origin);
    }
    public void setMultiplier(float m)
    {
        multiplier = m;
        this.Bind();
        this.setUniform1v("u_Multiplier", multiplier);
    }
    public void setRotation(float r)
    {
        this.Bind();
        this.setUniform1v("u_Rotation", r);
    }
}