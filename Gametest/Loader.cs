using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Gametest;
using System;
using System.IO;
using StbImageSharp;
using System.Text;
using System;
using System.Xml.Linq;
using System.Linq;

public static class Loader
{
    public static string  LoadVertexShader(string name)
    {
        return File.ReadAllText (name);
    }
    public static string  LoadFragmentShader(string name)
    {
        return File.ReadAllText (name);
    }


    public static ImageResult LoadTexture(string name)
    {
        StbImage.stbi_set_flip_vertically_on_load(1);


        ImageResult image = ImageResult.FromStream(File.OpenRead(name), ColorComponents.RedGreenBlueAlpha);
        return image;
    }

   public  static Map LoadMap(string name)
    {
        uint[] ind;
        float[] vert; 
        float[] text; 
        
        
        
        
        
       XmlReader filein = new XmlReader ();
       filein.ReadXmlFromFile(name +"Tiles.xml",out  ind,out  vert,out text);
       Console.WriteLine(ind.Length +" "+vert.Length +" " +text.Length);
       Mesh temMesh = new Mesh();
       temMesh.Shader = new Shader(name + "Tiles.vert", name + "Tiles.frag");
       temMesh.Texture = new Texture(name+"Tiles.png");
       temMesh.AddIndecies(ind);
      
       
       Bufferlayout tempb = new Bufferlayout();
       tempb.count = 2;
       tempb.normalized = false;
       tempb.type = VertexAttribType.Float;
       tempb.offset = 0;
       tempb.typesize = sizeof(float);
       temMesh.AddAtribute( tempb,vert);
       
      
       temMesh.AddAtribute( tempb,text);

       DrawInfo TemDrawinfo = new DrawInfo();
       TemDrawinfo.mesh = temMesh;
       TemDrawinfo.Position = new Vector2(0, 0);
       TemDrawinfo.Rotation = 0.0f;
       TemDrawinfo.Size = new Vector2(1, 1);
        
       Console.WriteLine(ind.Length);
       
       Map tMap = new Map(TemDrawinfo, new Vector2(32,32));
       return tMap;

    }
    
}





public class XmlReader
{
    public void ReadXmlFromFile(string fileName, out uint[] ind, out float[] vert,out float[] text)
    {
        XDocument xmlDocument = XDocument.Load(fileName);

        XElement indElement = xmlDocument.Root.Element("Ind");
        XElement vertElement = xmlDocument.Root.Element("Vert");
        XElement TextElement = xmlDocument.Root.Element("Text");

        if (indElement != null && vertElement != null && TextElement != null)
        {
            ind = indElement.Element("Array").Value
                .Split(' ')
                .Select(uint.Parse)
                .ToArray();

            vert = vertElement.Element("Array").Value
                .Split(' ')
                .Select(float.Parse)
                .ToArray();
            text = TextElement.Element("Array").Value
                .Split(' ')
                .Select(float.Parse)
                .ToArray();
        }
        else
        {
            throw new InvalidOperationException("XML file does not have the expected structure.");
        }
    }
}