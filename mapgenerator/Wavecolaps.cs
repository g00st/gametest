using System.Collections;
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
        height = Y;
        simspace = new Wavecell[X, Y];

    }

    public void runSim()
    {
        int lvl = 0;
        InitSimspace();
        printMap(simspace);
        while (!NextStep(simspace, lvl))
        {
            Console.WriteLine("HM");
        };
        printMap(simspace);
    }

    void printMap(Wavecell[,] simspace2)
    {
        Console.Write( "------------------------------------------------------------------------------------------------------------------------------- \n");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if ((simspace2[i, j] & Wavecell.Collapsed) == Wavecell.None )
                {
                    Console.BackgroundColor = (System.ConsoleColor) calcPoss(simspace2[i,j]);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(calcPoss(simspace2[i,j]).ToString("D2"));
                }
                else
                {
                    string outputString = "??";
                    Console.BackgroundColor = System.ConsoleColor.Black;
                    Console.ForegroundColor = (System.ConsoleColor.DarkYellow);
                    switch (simspace2[i, j] & Wavecell.AllTerain)
                    {
                        case Wavecell.Solid:
                            outputString = "##";
                            break;
                        case Wavecell.Solid_B_R:
                            outputString = "+|";
                            break;
                        case Wavecell.Solid_B_T:
                            outputString = "--";
                            break;
                        case Wavecell.Solid_B_L:
                            outputString = "|+";
                            break;
                        case Wavecell.Solid_B_B:
                            outputString = "__";
                            break;
                        case Wavecell.Solid_C_LT:
                            outputString = "|-";
                            break;
                        case Wavecell.Solid_C_RT:
                            outputString = "-|";
                            break;
                        case Wavecell.Solid_C_LB:
                            outputString = "|_";
                            break;
                        case Wavecell.Solid_C_RB:
                            outputString = "_|";
                            break;
                        case Wavecell.Solid_E_LT:
                            outputString = "'+";
                            break;
                        case Wavecell.Solid_E_LB:
                            outputString = ",+";
                            break;
                        case Wavecell.Solid_E_RT:
                            outputString = "+'";
                            break;
                        case Wavecell.Solid_E_RB:
                            outputString = "+,";
                            break;
                        case Wavecell.Water:
                            Console.ForegroundColor = (System.ConsoleColor.Blue);
                            outputString = "~";
                            break;
                        default:
                            outputString = "??";
                            break;

                           
                    } 
                    Console.Write(outputString);
                }
                
            }

            Console.Write("\n");
        }
        Console.Write( "------------------------------------------------------------------------------------------------------------------------------- \n");
    }

    void InitSimspace()
    {
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                simspace[i, j] = Wavecell.AllTerain;
            }
        }

        simspace[height / 2, width / 2] = Wavecell.Water | Wavecell.Collapsed;
        simspace[height-1,0] = Wavecell.Solid  | Wavecell.Collapsed;
        simspace[height-1,width-1] = Wavecell.Solid  | Wavecell.Collapsed;
        simspace[0,width-1] = Wavecell.Solid  | Wavecell.Collapsed;
        simspace[0,0] = Wavecell.Solid  | Wavecell.Collapsed;

    }
    
    int countUncolapse(Wavecell[,] curarray)
    {
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if ((curarray[i, j] & Wavecell.Collapsed) != Wavecell.Collapsed) count++;
            }
        }

        return count;
    }

    bool NextStep(Wavecell[,] curarray, int lvl)
    {
       // Console.ReadLine();
        Console.WriteLine(lvl);
        int lvlc = lvl + 1;
        // printMap(curarray);
         if (!CalcEntropiesrek(ref curarray))
        {
            Console.WriteLine("backtrack");
            return false;
            
        }
        
        List<Vector2> mindexs = findMinArray(curarray);
        if (mindexs.Count == 0)
        {
            simspace = curarray;
            return true;
        }
        while (mindexs.Count>0)
        {
            int selectedCell = rand.Next(0, mindexs.Count);
            Wavecell cell = curarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y];
            List<Wavecell> Possarray =new List<Wavecell>(0);

            bool containswater = false;

            foreach (Wavecell types in Enum.GetValues(typeof(Wavecell)))
            {
                if ((types & cell) != Wavecell.None && types != Wavecell.AllTerain)
                {
                    if (types == Wavecell.Water) containswater = true;
                    Possarray.Add(types);
                }
            }

            int selectedTile =0;
            if (rand.Next(1, 3) == 1 | !containswater )
            {
                 selectedTile = rand.Next(0, Possarray.Count);
            }
            else
            {
                Console.WriteLine("water!");
                 selectedTile = Possarray.FindIndex(e => e== Wavecell.Water);
            }

            Wavecell[,] nextarray=  (Wavecell[,])curarray.Clone();
                //set cell
            nextarray[(int)mindexs[selectedCell].X, (int)mindexs[selectedCell].Y] = Possarray[selectedTile] | Wavecell.Collapsed;
            if (NextStep(nextarray, lvlc))
            {
                    return true;
            }
            else
            {
                    Possarray.RemoveAt(selectedTile);
            }
            
            mindexs.RemoveAt(selectedCell);
        }
        return false;
    }

    int calcPoss(Wavecell target)
    {
        int count = 0;
        int intValue = Convert.ToInt32(target & Wavecell.AllTerain);

        while (intValue != 0)
        {
            intValue &= (intValue - 1);
            count++;
        }

        return count;
    }
    
    List<Vector2> findMinArray(Wavecell[,] curarray)
    {
        int minvalue = 15;
        List<Vector2> mindexs =new List<Vector2>();
       
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((curarray[x, y] & Wavecell.Collapsed) == Wavecell.Collapsed) continue;
                int count = calcPoss(curarray[x, y]);
                if ( count < minvalue)
                {
                    mindexs = new List<Vector2>();
                    mindexs.Add(new Vector2(x,y));
                    minvalue = count;
                }
                else if (count == minvalue)
                {
                    mindexs.Add(new Vector2(x,y));
                }
            }
        }
        return mindexs;
    }

    bool CalcEntropies( ref Wavecell[,] curr)
    {
        Wavecell[,] next = new Wavecell[width, height];
        
        for (int x = 0; x < height; x++)
        {
           //  Console.Write("\n ");
            for (int y = 0; y < width; y++)
            {
                if ((curr[x, y] & Wavecell.Collapsed) == Wavecell.Collapsed)
                {
                    next[x, y] = curr[x,y];
                   // Console.Write(" |------------|" );
                    continue;
                }

                
                Wavecell result = Wavecell.AllTerain;
                
                                if (x != 0 ) { result = Tiles.getneigbours('R', curr[x - 1, y]) & result; }

                             //   Console.Write("  R " + result);
                                 if (x !=  width-1 ) { result = Tiles.getneigbours('L', curr[x + 1, y]) & result;  }
                                 
                             //    Console.Write( "  L " +calcPoss(result));
                                 if (y != height - 1) { result = Tiles.getneigbours('B', curr[x, y+1]) & result; }
                             //    Console.Write( "  B "+ calcPoss(result));
                                 if (y != 0) { result = Tiles.getneigbours('T', curr[x , y-1]) & result; }
                             //    Console.Write( "  T " +calcPoss(result));
                                 next[x, y] = result;
                           //      Console.Write( "|");
                   if(result == Wavecell.None)
                    return false;
            }
            
        }
        curr = next;
        return true;
    }

    bool CalcEntropiesrek( ref Wavecell[,] curr)
    {
        Wavecell[,] next = new Wavecell[width, height];
        bool change = true;

        while (change)
        {
            change = false;
            
            for (int x = 0; x < height; x++)
            {
                //  Console.Write("\n ");
                for (int y = 0; y < width; y++)
                {
                    if ((curr[x, y] & Wavecell.Collapsed) == Wavecell.Collapsed)
                    {
                        next[x, y] = curr[x, y];
                        // Console.Write(" |------------|" );
                        continue;
                    }


                    Wavecell result = Wavecell.AllTerain;

                    if (x != 0)
                    {
                        result = Tiles.getneigbours('R', curr[x - 1, y]) & result;
                    }

                    //   Console.Write("  R " + result);
                    if (x != width - 1)
                    {
                        result = Tiles.getneigbours('L', curr[x + 1, y]) & result;
                    }

                    //    Console.Write( "  L " +calcPoss(result));
                    if (y != height - 1)
                    {
                        result = Tiles.getneigbours('B', curr[x, y + 1]) & result;
                    }

                    //    Console.Write( "  B "+ calcPoss(result));
                    if (y != 0)
                    {
                        result = Tiles.getneigbours('T', curr[x, y - 1]) & result;
                    }

                    //    Console.Write( "  T " +calcPoss(result));
                    if (next[x, y] != result) change = true;
                    next[x, y] = result;
                    //      Console.Write( "|");
                    if (result == Wavecell.None)
                        return false;
                }

            }
        }

        curr = next;
        return true;
    }
    public int[,] getArray()
    {
        int[,] retarr = new int[width, height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int result = 0;
                foreach (var types in Enum.GetValues(typeof(Wavecell)))
                {
                    if ((simspace[i, j] & (Wavecell)types) != Wavecell.None)
                    {
                        switch (simspace[i, j] & Wavecell.AllTerain)
                        {
                            case Wavecell.Solid:
                                result = 0;
                                break;
                            case Wavecell.Solid_B_R:
                                Console.WriteLine(simspace[i, j]);
                                result = 1;
                                break;
                            case Wavecell.Solid_B_T:
                                Console.WriteLine(simspace[i, j]);
                                result = 2;
                                break;
                            case Wavecell.Solid_B_L:
                                Console.WriteLine(simspace[i, j]);
                                result = 3;
                                break;
                            case Wavecell.Solid_B_B:
                                Console.WriteLine(simspace[i, j]);
                                result = 4;
                                break;
                            case Wavecell.Solid_C_LT:
                                Console.WriteLine(simspace[i, j]);
                                result = 5;
                                break;
                            case Wavecell.Solid_C_RT:
                                Console.WriteLine(simspace[i, j]);
                                result = 6;
                                break;
                            case Wavecell.Solid_C_LB:
                                Console.WriteLine(simspace[i, j]);
                                result = 7;
                                break;
                            case Wavecell.Solid_C_RB:
                                Console.WriteLine(simspace[i, j]);
                                result = 8;
                                break;
                            case Wavecell.Solid_E_LT:
                                Console.WriteLine(simspace[i, j]);
                                result = 9;
                                break;
                            case Wavecell.Solid_E_LB:
                                Console.WriteLine(simspace[i, j]);
                                result = 10;
                                break;
                            case Wavecell.Solid_E_RT:
                                Console.WriteLine(simspace[i, j]);
                                result = 11;
                                break;
                            case Wavecell.Solid_E_RB:
                                Console.WriteLine(simspace[i, j]);
                                result = 12;
                                break;
                            case Wavecell.Water:
                                Console.WriteLine(simspace[i, j]);
                                result = 13;
                                break;

                        }
                        
                       
                        retarr[i, j] = result;
                    }
                }
            }
        }
        printMap(simspace);
        return retarr;
    }
    
}