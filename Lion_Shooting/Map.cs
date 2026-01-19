using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Lion_Shooting
{
    internal class Map
    {

        private int startPosY = Console.WindowHeight - 1;
        public List<Tile> tilePool = new List<Tile>();

        private int lastX, lastY;
        private int curDir = 1;
        private int dirCount = 0;
        private int maxDirCount = 0;
        private int maxTile = 100;
        Random rand = new Random();

        public Map()
        {
            for (int i = 0; i < maxTile; i++) tilePool.Add(new Tile { isActive = false });
        }

        public void CreatMap()
        {
            foreach (var t in tilePool) t.isActive = false;

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
            int tiles = 3;

            for (int i = 0; i < tiles; i++)
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

        private Tile GetInactiveTile() => tilePool.FirstOrDefault(t => !t.isActive);

        public void Render()
        {
            foreach (var tile in tilePool)
            {
                if (tile.isActive)
                {
                    tile.Draw();
                }
            }
        }

        public void ScrollDown()
        {
            foreach (var t in tilePool)
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
