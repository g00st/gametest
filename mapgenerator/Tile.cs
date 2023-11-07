namespace mapgenerator;

public class Tile
{
    public List<int> _L;
    public List<int> _R;
    public List<int> _T;
    public List<int> _B;
    public int Type;
    
    public Tile(int type)
    {
        Type = type;
        _L = new List<int>();
     _R = new List<int>();
     _T =new List<int>();
     _B = new List<int>();
        
               switch (type)
        {
            case 0:
                _L.Add(3);
                _L.Add(0);
                _L.Add(9);
                _L.Add(10);
                
                _R.Add(1);
                _R.Add(10);
                _R.Add(11);
                _R.Add(0);

                _T.Add(0);
                _T.Add(2);
                _T.Add(9);
                _T.Add(11);
                
                _B.Add(0);
                _B.Add(4);
                _B.Add(10);
                _B.Add(11);
                
                break;
            
            case 1:
                _L.Add(3);
                _L.Add(0);
                _L.Add(9);
                _L.Add(10);
                
                _R.Add(13);
                
                _T.Add(1);
                _T.Add(12);
                _T.Add(6);
                
                _B.Add(1);
                _B.Add(11);
                _B.Add(8);
                break;
            case 2:
                _L.Add(2);
                _L.Add(5);
                _L.Add(11);
                
                _R.Add(2);
                _R.Add(6);
                _R.Add(9);
                
                _T.Add(13);
                
                _B.Add(0);
                _B.Add(4);
                _B.Add(10);
                _B.Add(12);
                break;
            
            case 3:
                _L.Add(13);
                
                _R.Add(0);
                _R.Add(1);
                _R.Add(11);
                _R.Add(12);
                
                _T.Add(3);
                _T.Add(5);
                _T.Add(10);
                
                _B.Add(3);
                _B.Add(9);
                _B.Add(7);
                break;
            case 4:
                _L.Add(4);
                _L.Add(7);
                _L.Add(12);
                
                _R.Add(4);
                _R.Add(8);
                _R.Add(10);
                
                _T.Add(0);
                _T.Add(2);
                _T.Add(9);
                _T.Add(11);
                
                _B.Add(13);
                break;
            case 5:
                _L.Add(13);
                _R.Add(6);
                _R.Add(2);
                _R.Add(9);
                _T.Add(13);
                _B.Add(7);
                _B.Add(3);
                _B.Add(9);
                break;
                
            case 6:
                _L.Add(2);
                _L.Add(11);
                _L.Add(5);
                _R.Add(13);
                _T.Add(13);
                _B.Add(8);
                _B.Add(1);
                _B.Add(11);
                break;
            case 7:
                _L.Add(13);
                _B.Add(13);
                _T.Add(10);
                _T.Add(5);
                _T.Add(3);
                _R.Add(4);
                _R.Add(10);
                _R.Add(8);
                break;
            case 8 :
                _L.Add(4);
                _L.Add(7);
                _L.Add(10);
                _T.Add(1);
                _T.Add(6);
                _T.Add(12);
                _R.Add(13);
                _B.Add(13);
                
                break;
            case 9:
                _L.Add(2);
                _L.Add(5);
                _R.Add(0);
                _R.Add(1);
                _B.Add(0);
                _B.Add(4);
                _T.Add(5);
                _T.Add(3);
                break;
            case 10:
                _L.Add(7);
                _L.Add(4);
                _R.Add(0);
                _R.Add(1);
                _B.Add(3);
                _B.Add(7);
                _T.Add(2);
                _T.Add(0);
                break;
            case 11:
                _L.Add(0);
                _L.Add(3);
                _R.Add(6);
                _R.Add(2);
                _B.Add(4);
                _B.Add(0);
                _T.Add(6);
                _T.Add(1);
                break;
            case 12:
                _L.Add(0);
                _L.Add(3);
                _R.Add(8);
                _R.Add(4);
                _B.Add(8);
                _B.Add(1);
                _T.Add(2);
                _T.Add(0);
                break;
            case 13:
                _L.Add(13);
                _L.Add(1);
                _L.Add(6);
                _L.Add(8);
                _R.Add(13);
                _R.Add(5);
                _R.Add(7);
                _R.Add(3);
                _B.Add(13);
                _B.Add(2);
                _B.Add(5);
                _B.Add(6);
                _T.Add(13);
                _T.Add(4);
                _T.Add(8);
                _T.Add(7);
                
            break;


        }
        
    }
}