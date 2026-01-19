using System;

namespace Lion_Shooting
{
    internal class Program
    {
        static int Width = 40;
        static int Height = 25;

        static void Main()
        {
            GameManager gameManager = GameManager.instance;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);

            Console.CursorVisible = false;

            gameManager.GameStart();

        }
    }
}




