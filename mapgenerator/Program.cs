// See https://aka.ms/new-console-template for more information

using System.Numerics;
using mapgenerator;

Console.WriteLine("Hello, World!");
string fileName = "Tiles.xml";











//float[] readInd, readVert;
//xmlReader.ReadXmlFromFile(fileName, out readInd, out readVert);

//Console.WriteLine("Read ind: " + string.Join(", ", readInd));
//Console.WriteLine("Read vert: " + string.Join(", ", readVert));


List<float> vertl = new List<float>();
List<uint> indl  = new List<uint>();
List<float> textcords = new List<float>();
List<float> tilemapo = new List<float>();



float tileheight = 10;
float tilewidth = 10;
int MapsizeX = 100;
int MapsizeY = 100;


Wavecolaps generator = new Wavecolaps(MapsizeX, MapsizeY);
generator.runSim();
int [,]tilemap = new int[MapsizeX,MapsizeY];
tilemap = generator.getArray();






for (int Y = 0; Y < MapsizeY; Y++)
{
    for (int X = 0; X < MapsizeY; X++)
    {
        
        if (X == 0)
        {
            tilemap[X, Y] = 1;
        } 
        if (X == MapsizeY-1)
        {
            tilemap[X, Y] = 3;
        } 
        if (Y == 0)
        {
   
            tilemap[X, Y] = 2;
        } 
        
        if (Y == MapsizeY-1)
        {
       
            tilemap[X, Y] = 4;
        } 
        
    }
}







for (int Y=0; Y<MapsizeY; Y++){

    for( int X =0; X<MapsizeX;X++)
    {
      
        textcords.AddRange(clacTextcords(X,Y));
        vertl.AddRange(calcVert(X, Y));
        indl.AddRange(calcInd(X,Y));
        tilemapo.Add(tilemap[X,Y]);
        
    }
}



XmlWriter xmlWriter = new XmlWriter();
xmlWriter.WriteXmlToFile(indl.ToArray(), vertl.ToArray(), textcords.ToArray(), tilemapo.ToArray(),fileName);
//float[] vertices = {
 //   1.0f,  1.0f, 0.0f,  // top right
   // 1.0f,  0.0f, 0.0f,  // bottom right
    //0.0f, 0.0f, 0.0f,  // bottom left
    //0.0f,  1f, 0.0f   // top left
//};

List<float> clacTextcords(int X,int Y){
    List<float> temp = new List<float>();
    temp.Add(1.0f/14f+(1.0f/14f)*tilemap[X,Y]);
    temp.Add(1.0f);
    
    temp.Add(1.0f/14f+(1.0f/14f)*tilemap[X,Y]);
    temp.Add(0.0f);
    
    temp.Add(0.0f+(1.0f/14f)*tilemap[X,Y]);
    temp.Add(0.0f);
    
    temp.Add(0.0f+(1.0f/14f)*tilemap[X,Y]);
    temp.Add(1.0f);
    
    return temp;
}

List<float> calcVert(int X, int Y)
{
    List<float> temp = new List<float>();
    temp.Add((X * tilewidth) +tilewidth);  // top right
    temp.Add((Y * tileheight)+tileheight);
    //temp.Add(9.99f);
    
    temp.Add((X * tilewidth) +tilewidth);  // bottom right
    temp.Add((Y * tileheight));
   // temp.Add(9.99f);
    temp.Add((X * tilewidth) );  // bottom left
    temp.Add((Y * tileheight));
    //temp.Add(9.99f);
    temp.Add((X * tilewidth) );  // top Left
    temp.Add((Y * tileheight )+tileheight);
    //temp.Add(9.99f);
    return temp;


}

List<uint> calcInd(int X, int Y)
{
    List<uint> temp = new List<uint>();
    
    int tempi = (4 * X + 4 * MapsizeY * Y);
   // Console.WriteLine(tempi);
    temp.Add(((uint)(int) tempi+0));
    temp.Add(((uint)(int) tempi+1));
    temp.Add(((uint)(int) tempi+3));
    
    temp.Add(((uint)(int) tempi+1));
    temp.Add(((uint)(int) tempi+2));
    temp.Add(((uint)(int)tempi+3));
    return temp;

}

List<int> simspace = new List<int>();