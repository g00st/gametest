using System.Net;
using System.Numerics;
using System.Transactions;

namespace mapgenerator;

public class Wavecolaps
{
    public Wavecell[,] simspace;
    private int width, height;
    private Random rand = new Random();

  public  Wavecolaps(int X, int Y)
    {
        width = X;
        height = 
            Y;
        simspace = new Wavecell[X, Y];

    }

    public void runSim()
    {
        int lvl = 0;
        InitSimspace();
        while (!NextStep(simspace, lvl))
        {
            Console.WriteLine("HM");
        };
        

        printMap(simspace);
    }

    void printMap(Wavecell[,] simspace2)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!simspace2[i, j].colapsed)
                {
                    Console.ForegroundColor = (System.ConsoleColor)14;
                    Console.Write( "__|");
                }
                else
                {
                    Console.ForegroundColor = (System.ConsoleColor)simspace2[i, j].PossTiles[0].Type;
                    Console.Write( simspace2[i,j].PossTiles[0].Type.ToString("D2") + "|");
                }
                
            }

            Console.Write("       ||     ");
            for (int j = 0; j < width; j++)
            {
                Console.ForegroundColor = (System.ConsoleColor)simspace2[i, j].PossTiles.Count();
                Console.Write( simspace2[i,j].PossTiles.Count().ToString("D2")+ "|");
            }
            Console.Write("\n");
            
        }
    }
    void InitSimspace()
    {
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                simspace[i, j] = new Wavecell();
                for (int k = 0; k < 14; k++)
                {
                    simspace[i, j].PossTiles.Add(new Tile(k));
                }
            }
        }
        simspace[height/2, width/4] = new Wavecell();
        simspace[height/2, width/4].PossTiles.Add(new Tile(13));
        simspace[height / 2, width / 4].colapsed = true;
        
        simspace[height/2, width/4*3] = new Wavecell();
        simspace[height/2, width/4*3].PossTiles.Add(new Tile(13));
        simspace[height / 2, width / 4*3].colapsed = true;
        

    }
    
    int countUncolapse(Wavecell[,] curarray)
    {
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!curarray[i, j].colapsed) count++;
            }
        }

        return count;
    }

    bool NextStep(Wavecell[,] curarray, int lvl)
    {
        Console.WriteLine(lvl);
        int lvlc = lvl + 1;
      //   printMap(curarray); 
        // Console.ReadLine();
        if (!CalcEntropies(ref curarray)) return false;
       
       
        if (countUncolapse(curarray) == 0)
        {  
            simspace = curarray;
            return true;
         
        }

        List<Vector2> mindexs = findMinArray(curarray);
        int minValue = curarray[(int)mindexs[0].X, (int)mindexs[0].Y].PossTiles.Count();

        while (mindexs.Count>0)
        {
            int selectedCell = rand.Next(0, mindexs.Count);

            Wavecell currcell = new Wavecell();
            currcell.PossTiles = new List<Tile>(curarray[(int)mindexs[0].X, (int)mindexs[0].Y].PossTiles);
            while (currcell.PossTiles.Count >0)
            {
                int selectedTile = rand.Next(0, currcell.PossTiles.Count);
                
                Wavecell[,] nextarray= new Wavecell[width, height];
                for (int y = 0; y < height; y++)
                {
                   for (int x = 0; x < width; x++)
                   {
                       nextarray[x, y] = new Wavecell();
                       foreach (var t in curarray[x, y].PossTiles)
                       {
                           nextarray[x, y].PossTiles.Add(new Tile(t.Type));
                           nextarray[x, y].colapsed = curarray[x, y].colapsed;
                       }
                       
                      
                   } 
                }
                
                //set cell
                nextarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y]  = new Wavecell();
                nextarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y].PossTiles  = new List<Tile>();
                nextarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y].PossTiles.Add(new Tile(currcell.PossTiles[selectedTile].Type));
                nextarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y].colapsed = true;

                if (NextStep(nextarray, lvlc))
                {
                    return true;
                }
                else
                {
                    currcell.PossTiles.Remove(currcell.PossTiles[selectedTile]);
                }
                
            }

            mindexs.Remove(mindexs[selectedCell]);

        }

        return false;
    }


    List<Vector2> findMinArray(Wavecell[,] curarray)
    {
        int minvalue = 15;
        List<Vector2> mindexs =new List<Vector2>();
       
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (curarray[x,y].colapsed) continue;
                if ( curarray[x, y].PossTiles.Count < minvalue)
                {
                    mindexs = new List<Vector2>();
                    mindexs.Add(new Vector2(x,y));
                    minvalue = curarray[x, y].PossTiles.Count;
                }
                else if ( curarray[x, y].PossTiles.Count == minvalue)
                {
                    mindexs.Add(new Vector2(x,y));
                    minvalue = curarray[x, y].PossTiles.Count;
                }
            }
        }

        return mindexs;
    }

    bool CalcEntropies( ref Wavecell[,] simspaceg)
    {
        Wavecell[,] simspace2 = new Wavecell[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
            {
                simspace2[i, j] = new Wavecell();
            }
        }

        for (int x = 0; x < height; x++)
        {
          //  Console.Write("\n ");
            for (int y = 0; y < width; y++)
            {
                if (simspaceg[x, y].colapsed == true)
                {
             //       Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
           //         Console.ForegroundColor = ConsoleColor.White;
                }
           //     Console.Write( "|"+simspaceg[x, y].PossTiles.Count);
                if (simspaceg[x, y].colapsed == true)
                {
                    simspace2[x, y] = simspaceg[x, y];
                    continue;
                }

                                List<int> result = new List<int>(13);
                                for (int i = 0; i < 14; i++)
                                {
                                    result.Add(i);
                                }
                                
                
                                 List<int> rightTiles = new List<int>();
                                 List<int> leftTiles = new List<int>();
                                 List<int> topTiles = new List<int>();
                                 List<int> bottomTiles = new List<int>();
                                 if (x != 0 )
                                 {
                                     Wavecell left = simspaceg[x-1, y];
                                     foreach (var tile in left.PossTiles) { leftTiles.AddRange(tile._R); }

                                     result = result.Intersect(leftTiles).ToList();
                                 }
                
                                 
                                

                                 if (x != width - 1)
                                 {
                                      Wavecell right = simspaceg[x + 1, y];
                                      foreach (var tile in right.PossTiles) { rightTiles.AddRange(tile._L); }
                                      result = result.Intersect(rightTiles).ToList();
                                 }

                                 if (y != height - 1)
                                 {
                                     Wavecell top = simspaceg[x, y+1];
                                     foreach (var tile in top.PossTiles) { topTiles.AddRange(tile._B); }
                                     result = result.Intersect(topTiles).ToList();
                                 }
                                 
                                 if (y != 0)
                                 {
                                     Wavecell bottom = simspaceg[x, y-1];
                                     foreach (var tile in bottom.PossTiles) { bottomTiles.AddRange(tile._T); }
                                     result = result.Intersect(bottomTiles).ToList();
                                 }

                                 

            

                for (int i = 0; i < result.Count; i++)
                {
                    simspace2[x, y].PossTiles.Add(new Tile(result[i]));
                }

                simspace2[x, y].colapsed = simspaceg[x, y].colapsed;
               
                if(result.Count == 0)
                {
                   // Console.WriteLine("no possibiility left  " + x + "  " + y);
                    return false;
                }
            }
        }
       // Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        
        simspaceg = simspace2;
        return true;
    }

    public int[,] getArray()
    {
        int[,] retarr = new int[width, height];
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
            {
                retarr[i, j] = simspace[i, j].PossTiles[0].Type;
            }
        }

        return retarr;
    }
    
}