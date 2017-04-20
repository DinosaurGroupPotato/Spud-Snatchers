using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.State;

namespace SpudSnatch.Model.Objects
{

    public enum HomerState
        {
            Standing,
            Jumping,
            Ducking
        }

    public class Homer : Character
    {

        public HomerState State;

        public int momentumY;
        public int Health;
        public int delay;
        public EventHandler HomerUpdated;

        public string Serialize()
        {
            string data = "hm" + Convert.ToString(PositionX) + "," + Convert.ToString(PositionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Homer ida = new Homer(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            GameController.Instance.level.Player = ida;
        }

        public Homer(int x, int y)
        {
            ID = 0;
            PositionX = x;
            PositionY = y;
            State = HomerState.Standing;
            delay = 0;
        }

        public static void GrabTater()
        {
            Homer player = GameController.Instance.level.GetHomer();
            while (!GameController.Instance.GameOver)
            {
                foreach (Potato tater in GameController.Instance.level.GetPotatoes())
                {
                    if (tater.PositionX - 5 < player.PositionX && player.PositionX < tater.PositionX + 5)
                    {
                        if (tater.PositionY - 500 < player.PositionY && player.PositionY < tater.PositionY + 500)
                        {
                            tater.CollectPotato();
                        }
                    }
                }
            }

        }
        
        public void Jump()
        {
            if (State == HomerState.Standing)
            {
                State = HomerState.Jumping;
                momentumY = -35;
            }
        }

        public HomerState GetHomerState()
        {
            return State;
        }

        public bool Walk(Direction dir, int distance)
        {
            //Walking left
            if (dir == Direction.Left && PositionX - distance > -500)
            {
                playerDirection = dir;
                PositionX -= distance;
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionX += distance;
                        return false;
                    }
                }
                return true;
            }

            //Walking right
            if (dir == Direction.Right && PositionX + distance + 20 < 500)
            {
                playerDirection = dir;
                PositionX += distance;
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionX -= distance;
                        return false;
                    }
                }
                return true;
            }
            //Jumping up
            if (dir == Direction.Down)
            {
                PositionY -= distance;
                if (PositionY + distance < -200)
                {
                    PositionY = 0;
                    State = HomerState.Standing;
                    return false;
                }
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionY += distance;
                        momentumY = 0;
                        return false;
                    }
                }
                return true;
            }

            //Falling down
            if (dir == Direction.Up)
            {

                PositionY += distance;
                if (PositionY + distance > GameController.Instance.level.GetFloor())
                {
                    PositionY = GameController.Instance.level.GetFloor();
                    State = HomerState.Standing;
                    momentumY = 0;
                    return false;
                }
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionY -= distance;
                        State = HomerState.Standing;
                        momentumY = 0;
                        return false;
                    }
                }
                return true;
            }


            return false;

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
            if ((KeyboardState.S == KeyState.Down || KeyboardState.Down == KeyState.Down) && State == HomerState.Standing)
            {
                State = HomerState.Ducking;
            }
            if (KeyboardState.S == KeyState.Up && KeyboardState.Down == KeyState.Up && State == HomerState.Ducking)
            {
                State = HomerState.Standing;
            }

            if (Walk(Direction.Up, momentumY))
            {
                momentumY += 3;
            }

            // If he's colided with a damaging object subtract something from his health
            if (!GameController.Instance.IsCheatMode && delay == 0)
            {
                foreach (Obstacle obs in GameController.Instance.level.GetObstacles())
                {
                    if (obs is DamagingObstacle && IsCollidedObs(obs))
                    {
                        Health -= 1;
                        delay = 100;
                    }
                }
                //Collide with enemies
                foreach (Enemy en in GameController.Instance.level.GetEnemies())
                {
                    if (IsCollidedChar(en))
                    {
                        Health -= 1;
                        delay = 100;
                    }
                }
            }
            if (delay < 0) delay -= 1;  
            
            foreach (Potato spud in GameController.Instance.level.GetPotatoes())
            {
                if (IsCollidedChar(spud))
                {
                    spud.CollectPotato();
                }
            }    

            HomerUpdated?.Invoke(this, null);
        }
    }
}
