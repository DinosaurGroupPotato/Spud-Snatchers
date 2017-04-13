using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.State;

namespace SpudSnatch.Model.Objects
{
    public class Homer: Character
    {
        public enum HomerState
        {
            Standing,
            Jumping,
            Ducking
        }

        public HomerState State;

        public int momentumY;

        public EventHandler HomerUpdated;


        public string Serialize()
        {
            string data = "hm" + Convert.ToString(positionX) + "," + Convert.ToString(positionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Homer ida = new Homer(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            GameController.level.player = ida;
        }

        public Homer(int x, int y)
        {
            ID = 0;
            positionX = x;
            positionY = y;
            State = HomerState.Standing;
        }

        public static void GrabTater()
        {
            Homer player = GameController.level.GetHomer();
            while(!GameController.GameOver)
            foreach (Potato tater in GameController.level.GetPotatoes())
            {
                if(tater.positionX - 7 < player.positionX && player.positionX < tater.positionX + 7)
                {
                    if (tater.positionY - 2 < player.positionY && player.positionY < tater.positionY + 2)
                    {
                        tater.CollectPotato();
                    }
                }
            }
        }

        public Homer(){ }
        public void Jump()
        {
            if (State == HomerState.Standing)
            {
                State = HomerState.Jumping;
                momentumY = 35;
            }
        }

        public HomerState GetHomerState()
        {
            return State;
        }

        public void Walk(Direction dir, int distance)
        {
            //Walking left
            if (dir == Direction.Left)
            {
                playerDirection = dir;
                positionX -= distance;
            }

            //Walking right
            if (dir == Direction.Right)
            {
                playerDirection = dir;
                positionX += distance;
            }
        }

        public void Update()
        {
            if ((KeyboardState.A == KeyState.Down || KeyboardState.Left == KeyState.Down) && State != HomerState.Ducking)
            {
                Walk(Direction.Left, 15);
            }
            if ((KeyboardState.D == KeyState.Down || KeyboardState.Right == KeyState.Down) && State != HomerState.Ducking)
            {
                Walk(Direction.Right, 15);
            }
            if ((KeyboardState.W == KeyState.Down || KeyboardState.Up == KeyState.Down) && State == HomerState.Standing)
            {
                Jump();
            }
            if ((KeyboardState.S == KeyState.Down || KeyboardState.Down == KeyState.Down) && State != HomerState.Ducking)
            {
                State = HomerState.Ducking;
            }

            if (GameController.level.GetFloor() < positionY)
            {
                State = HomerState.Standing;
            }
            else
            {
                positionY -= momentumY;
                momentumY -= 3;
            }
            
            HomerUpdated?.Invoke(this, null);
        }
    }
}
