using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.State;

namespace SpudSnatch.Model.Objects
{
    //States for movement methods
    public enum HomerState

        {
            Standing,
            Jumping,
            Ducking,
            WalkLeft,
            WalkRight
        }

    public class Homer : Character
    {

        public HomerState State;

        public int momentumY; //Momentum variable for gravity
        public int Health; //Player's health
        public int delay; //
        public EventHandler HomerUpdated; //

        public string Serialize()
        {
            string data = "hm," + Convert.ToString(PositionX) + "," + Convert.ToString(PositionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Homer ida = new Homer(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            GameController.Instance.level.Player = ida;
        }

        //Player constructor
        //Takes position to create player at and sets to x- and y-coordinates
        //Initializes state to Standing
        public Homer(int x, int y)
        {
            ID = 0;
            PositionX = x;
            PositionY = y;
            State = HomerState.Standing;
            delay = 0;
        }
        
        //Sets player's state to Jumping
        //Decrements player's y-coordinate
        public void Jump()
        {
            if (State == HomerState.Standing)
            {
                State = HomerState.Jumping;
                momentumY = -35;
            }
        }

        //Returns player's state
        public HomerState GetHomerState()
        {
            return State;
        }

        //Modifies player's x-coordinate either left or right for left and right direction
        //Modifies player's y-coordinate either up or down for jump and falling
        public bool Walk(Direction dir, int distance)
        {
            //Walking left
            if (dir == Direction.Left && PositionX - distance > -650)
            {
                //State = HomerState.WalkLeft;
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
            if (dir == Direction.Right && PositionX + distance + 20 < 650)
            {
                //State = HomerState.WalkRight;
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
                    momentumY = 0;
                    if (State == HomerState.Jumping)
                    {
                        State = HomerState.Standing;
                    }
                    return false;
                }
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionY += distance;
                        State = HomerState.Standing;
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
                    momentumY = 0;
                    if (State == HomerState.Jumping)
                    {
                        State = HomerState.Standing;
                    }
                    return false;
                }
                foreach (PlatformObstacle platform in GameController.Instance.level.obstacles)
                {
                    if (IsCollidedObs(platform))
                    {
                        PositionY -= distance;
                        if (State == HomerState.Jumping)
                            State = HomerState.Standing;
                        momentumY = 0;
                        return false;
                    }
                }
                return true;
            }



            return false;

        }

        //Updates player's position and health based on input data
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
