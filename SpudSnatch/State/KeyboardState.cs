using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.State
{
    enum KeyState
    {
        Down,
        Up
    }
    class KeyboardState
    {

        public static KeyState W { get; set; }
        public static KeyState A { get; set; }
        public static KeyState S { get; set; }
        public static KeyState D { get; set; }
        public static KeyState Up { get; set; }
        public static KeyState Left { get; set; }
        public static KeyState Down { get; set; }
        public static KeyState Right { get; set; }
        public static KeyState Space { get; set; }
        public static KeyState C { get; set; }

        public static void InitializeKeys()
        {
            W = KeyState.Up;
            A = KeyState.Up;
            S = KeyState.Up;
            D = KeyState.Up;
            Up = KeyState.Up;
            Left = KeyState.Up;
            Down = KeyState.Up;
            Right = KeyState.Up;
            Space = KeyState.Up;
            C = KeyState.Up;
        }

    }
}
