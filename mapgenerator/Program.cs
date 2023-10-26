﻿// See https://aka.ms/new-console-template for more information

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


float tileheight = 10;
float tilewidth = 10;



for (int Y=0; Y<1000; Y++){

    for( int X =0; X<1000;X++)
    {
      
        textcords.AddRange(clacTextcords(X,Y));
        vertl.AddRange(calcVert(X, Y));
        indl.AddRange(calcInd(X,Y));
        
    }
}



XmlWriter xmlWriter = new XmlWriter();
xmlWriter.WriteXmlToFile(indl.ToArray(), vertl.ToArray(), textcords.ToArray(), fileName);
//float[] vertices = {
 //   1.0f,  1.0f, 0.0f,  // top right
   // 1.0f,  0.0f, 0.0f,  // bottom right
    //0.0f, 0.0f, 0.0f,  // bottom left
    //0.0f,  1f, 0.0f   // top left
//};

List<float> clacTextcords(int X,int Y){
    List<float> temp = new List<float>();
    temp.Add(1.0f/14f);
    temp.Add(1.0f);
    
    temp.Add(1.0f/14f);
    temp.Add(0.0f);
    
    temp.Add(0.0f);
    temp.Add(0.0f);
    
    temp.Add(0.0f);
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
    
    int tempi = (4 * X + 4 * 1000 * Y);
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