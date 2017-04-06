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
            positionX = x;
            positionY = y;
        }

        public Homer(){ }
        public void Jump()
        {
            for (int i = 0; i < 6; i++)
            {
                positionY += i;
            }
            for (int ii = 0; ii < 6; ii++)
            {
                positionY -= ii;
            }
        }

        public void Walk(string direction)
        {
            //Walking left
            if (direction == "left")
            {
                positionX += 50;
            }

            //Walking right
            else
            {
                positionX -= 50;
            }
        }

        public void Update()
        {
            bool update = false;
            if (KeyboardState.A == KeyState.Down || KeyboardState.Right == KeyState.Down)
            {
                update = true;
                Walk("left");
            }
            if (KeyboardState.D == KeyState.Down || KeyboardState.Left == KeyState.Down)
            {
                update = true;
                Walk("right");
            }
            if (KeyboardState.W == KeyState.Down || KeyboardState.Up == KeyState.Down)
            {
                update = true;
                Jump();
            }

            if (update == true && HomerUpdated != null)
            {
                HomerUpdated(this, null);
            }
        }
    }
}
