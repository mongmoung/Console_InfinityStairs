using System;
using System.Runtime.InteropServices;

namespace Lion_Shooting
{
    internal class PlayerMove
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x27;

        float velocityY = 0;
        float gravity = 0.5f;

        float jumpForce = 1f;


        public void Move(ref int x, ref int y, bool isGround,int groundY)
        {

            UpdatePhysics(ref y, groundY, isGround);

            if ((GetAsyncKeyState(VK_LEFT) & 0x8000) != 0)
            {
                x = Math.Max(0, x - 1);
            }
            if ((GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0)
            {
                x = Math.Min(Console.WindowWidth - 2, x + 1); 
            }
        }

        private void UpdatePhysics(ref int y,int groundY, bool isGround)
        {         

            if (!isGround)
            {
                velocityY += gravity;
            }
            else
            {
                velocityY = -jumpForce;
                y = groundY;
            }
            y += (int)velocityY;

            if (y < 0) y = 0;
            if (y > Console.WindowHeight - 1) y = Console.WindowHeight - 1;

        }

    }
    
}
