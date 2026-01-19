using System;

namespace Lion_Shooting
{

    internal class Tile : GameObject
    {
        public bool isScored = false;

        public override void Draw()
        {
            string tileStr;
            if (isActive)
            {
                tileStr = "■";
                Console.SetCursorPosition(x, y);
                Console.Write(tileStr);
            }
            else
            {
                Console.SetCursorPosition(x, y);
                Console.Write("  ");
            }
        }
    }
}
