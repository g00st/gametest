using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using DrawElementsType = OpenTK.Graphics.OpenGL4.DrawElementsType;
using GL = OpenTK.Graphics.OpenGL4.GL;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using Vector3 = System.Numerics.Vector3;

namespace Gametest;

public class Mesh
{
    public Mesh()
    {
        _Vertecies = new List<float[]>();
        _vao = new VAO();
    }
    private int _verteciesLenght;
    private Shader _shader;
    private VAO _vao;
    private Texture _texture;
    private Matrix4 _MVP;
    private List<float[]> _Vertecies;
    private uint[] _Indecies;
    
    
    public Shader Shader
    {
        get { return _shader; }
        set { _shader = value; }
    }

    public Texture Texture
    {
        get { return _texture; }
        set { _texture = value; }
    }


    public void AddAtribute(Bufferlayout bufferlayout, float[] data)
    {
        if (_verteciesLenght != 0 && _verteciesLenght != data.Length/bufferlayout.count)
        {
            throw new ArgumentException("Atributes must be same lenght " + data.Length/bufferlayout.count + "   " + _verteciesLenght );
        }

        _verteciesLenght = data.Length/bufferlayout.count;
        _vao.LinkAtribute(data,bufferlayout);
       _Vertecies.Add(data);
        
    }
    

    public void AddIndecies(uint[] ind)
    {
        _vao.LinkElements(ind);
        _Indecies = ind;
    }


    public void Draw(Matrix4 mvp)
    {
        //uniform callback haben
        //aus zb shader.frag alle uniforms holen
        Random zufall = new Random();
        int zufallszahl = zufall.Next(0, 14);
        _vao.Bind();
        _texture.Bind();
        _shader.Bind();
        _shader.setUniformM4("u_MVP", mvp);         //weil uniform hier kacke
        _shader.setUniform4v("u_Color", 1.0f,1.0f,1.0f,(float)zufallszahl); //weil uniform hier kacke
        GL.DrawElements(PrimitiveType.Triangles, _Indecies.Length, DrawElementsType.UnsignedInt, 0);

    }




}