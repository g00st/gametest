namespace mapgenerator;

public class Wavecell
{
    public List<Tile> PossTiles;
    public bool colapsed;

    public Wavecell()
    {
        colapsed = new bool();
        colapsed = false;
        PossTiles = new List<Tile>();
    }
}