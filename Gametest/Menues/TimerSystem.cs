using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Gametest.Menues
{
    public class TimerSystem : DrawInfo
    {
        private DrawInfo drawInfo;
        private double timerlimit = 61;
        private Vector2 position;
        private double timerManual;
        TimerSystem(DrawInfo _drawInfo, Vector2 _position)
        {
            this.drawInfo = _drawInfo;
            this.position = _position;
            timerManual = timerlimit;
        }

        public void Update(Gamestate gameState)
        {
            switch (gameState)
            {
                case Gamestate.GameIsRunning:
                    // timer start
                    // timerManual -= gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Gamestate.GameOver:
                    // timer reset
                    timerManual = timerlimit;
                    break;
                case Gamestate.Startmenu:
                    // timer reset
                    timerManual = timerlimit;
                    break;
                default:
                    break;
            }
        }

        public bool CheckTimer()
        {
            if (timerManual < 0)
            {
                return true;
            }

            return false;
        }

        public DrawInfo DrawInfo { get { return drawInfo; } }
    }
}
