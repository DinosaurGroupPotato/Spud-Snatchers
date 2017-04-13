﻿using System;
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
            {
                foreach (Potato tater in GameController.level.GetPotatoes())
                {
                    if (tater.positionX - 5 < player.positionX && player.positionX < tater.positionX + 5)
                    {
                        if (tater.positionY - 500 < player.positionY && player.positionY < tater.positionY + 500)
                        {
                            tater.CollectPotato();
                        }
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

        public bool Walk(Direction dir, int distance)
        {
            //Walking left
            if (dir == Direction.Left && positionX - distance > -500)
            {
                playerDirection = dir;
                positionX -= distance;
                return true;
            }

            //Walking right
            if (dir == Direction.Right && positionX + distance + 20 < 500)
            {
                playerDirection = dir;
                positionX += distance;
                return true;
            }

            //Jumping up
            if (dir == Direction.Up && positionY - distance > 375)
            {
                positionY -= distance;
                return true;
            }

            //Falling down
            if (dir == Direction.Down && positionY + distance + 50 < 375)
            {
                positionY += distance;
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
            if ((KeyboardState.S == KeyState.Down || KeyboardState.Down == KeyState.Down) && State != HomerState.Ducking)
            {
                State = HomerState.Ducking;
            }
            if (KeyboardState.S == KeyState.Up && KeyboardState.Down == KeyState.Up && State == HomerState.Ducking)
            {
                State = HomerState.Standing;
            }

            if (Walk(Direction.Up, momentumY))
            { 
                momentumY -= 3;
            }
            
            HomerUpdated?.Invoke(this, null);
        }
    }
}
