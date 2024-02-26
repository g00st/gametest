using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Gametest.Menues
{
    public class Button : DrawInfo
    {
        private DrawInfo drawInfo;
        private Vector2 size;
        private Vector2 spriteSize;
        Button(DrawInfo _drawInfo, Vector2 _spriteSize)
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
