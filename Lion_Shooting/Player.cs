using System;
using System.Collections.Generic;

namespace Lion_Shooting
{
    internal class Player: GameObject
    {
        public PlayerMove playerMove = new PlayerMove();


        private  bool isGround = true;
        private string playerBody = "⭐";
        private string name;

        private int defaultGround = Console.WindowHeight - 2;
        private int currentGroundY;
        private int score = 0;

        public int Score => score;

        public Player(string name)
        {
            this.name = name;
            isGround = true;

            x = (Console.WindowWidth / 2) -1 ;
            y = defaultGround;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(x,y);
            Console.Write(playerBody);
        }


        private bool CheckGrounded(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.isActive && tile.y == this.y + 1 && tile.x == this.x )
                {
                    if (!tile.isScored)
                    {
                        score = score + 10; 
                        MarkPlatformAsScored(tiles, tile.y); 
                    }

                    currentGroundY = tile.y - 1;
                    return true;
                }
            }
            if (this.y >= defaultGround)
            {
                currentGroundY = defaultGround;
                return true;
            }
            return false;
        }

        public void PlayerMove(List<Tile> tiles)
        {
            isGround = CheckGrounded(tiles);
            playerMove.Move(ref x, ref y, isGround,currentGroundY);
            Draw();
        }

        private void MarkPlatformAsScored(List<Tile> tile, int targetY)
        {
            foreach (var obj in tile)
            {
                if (obj is Tile t && t.y == targetY)
                    t.isScored = true;
            }
        }
    }
}
