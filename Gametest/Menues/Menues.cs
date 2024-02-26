using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Gametest.Menues
{
    public class Menues : DrawInfo
    {
        private DrawInfo drawInfo;
        private Vector2 size;
        private Vector2 spriteSize;
        private Button button1;
        private Button button2;
        private Button button3;
        Menues(DrawInfo _drawInfo, Vector2 _spriteSize)
        {
            drawInfo = _drawInfo;
            spriteSize = _spriteSize;
        }
        public DrawInfo DrawInfo
        {
            get { return drawInfo; }
        }
    }
}
