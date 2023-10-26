using OpenTK.Mathematics;

namespace Gametest;

public class Map: DrawObject
{
    private DrawInfo _drawInfo;
    private Vector2 Size;
    private Vector2 Spritesize;
    
    public DrawInfo DrawInfo
    {
        get { return _drawInfo; }
    }

    public Map(DrawInfo drawInfo, Vector2 spritesize)
    {
        _drawInfo = drawInfo;

    }
    
}