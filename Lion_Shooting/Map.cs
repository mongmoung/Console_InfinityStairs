using System;
using System.Collections.Generic;
using System.Linq;

namespace Lion_Shooting
{
    internal class Map
    {

        public List<Tile> tiles = new List<Tile>();
        private int startPosY = Console.WindowHeight - 1;

        private int lastX, lastY;
        private int curDir = 1;
        private int dirCount = 0;
        private int maxDirCount = 0;
        Random rand = new Random();

        public Map()
        {
            for (int i = 0; i < 200; i++) tiles.Add(new Tile { isActive = false });
        }

        public void CreatMap()
        {
            foreach (var t in tiles) t.isActive = false;

            for (int x = 0; x < Console.WindowWidth - 1; x++)
            {
                Tile t = GetInactiveTile();
                if (t != null) { t.x = x; t.y = startPosY; t.isActive = true; }
            }

            lastX = Console.WindowWidth / 2;
            lastY = startPosY - 1;
            ChangeDirection();

            for (int i = 0; i < Console.WindowHeight - 3; i++)
            {
                BuildNextStep();
            }
        }

        public void BuildNextStep()
        {
            PlacePlatform(lastX, lastY);
            MoveToNextStep();
        }

        private void MoveToNextStep()
        {
            lastY--;
            dirCount++;

            if (dirCount >= maxDirCount)
            {
                ChangeDirection();
            }

            if (curDir == 0) lastX--;
            else lastX++;

            HandleWallCollision();
        }

        private void ChangeDirection()
        {
            if (curDir == 0) curDir = 1;
            else curDir = 0;

            dirCount = 0;
            maxDirCount = rand.Next(3, 8);
        }

        private void PlacePlatform(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                Tile t = GetInactiveTile();
                if (t != null)
                {
                    t.x = x + i;
                    t.y = y;
                    t.isActive = true;
                    t.isScored = false;
                }
            }
        }

        private void HandleWallCollision()
        {
            if (lastX < 2)
            {
                lastX = 2;
                ChangeDirection();
            }
            if (lastX > Console.WindowWidth - 6)
            {
                lastX = Console.WindowWidth - 6;
                ChangeDirection();
            }
        }

        private Tile GetInactiveTile() => tiles.FirstOrDefault(t => !t.isActive);

        public void Render()
        {
            foreach (var tile in tiles)
            {
                if (tile.isActive)
                {
                    tile.Draw();
                }
            }
        }

        public void ScrollDown()
        {
            foreach (var t in tiles)
            {
                if (t.isActive)
                {
                    t.y++;

                    if (t.y > startPosY)
                        t.isActive = false;
                }
            }
            lastY++; 
        }
    }
}
