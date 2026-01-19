using System;
using System.Threading;
namespace Lion_Shooting
{
    internal class GameManager
    {
        UIManager uiManager = UIManager.instance;
        Player player;
        Map map;

        public static GameManager instance = new GameManager();
        bool isScrollStarted = false;

        public GameManager()
        {
            map = new Map();
        }

        public void GameStart()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            isScrollStarted = false;
            uiManager.ShowIntro();
            Console.Clear();

            int clearTime = 50;

            Console.Write("이름을 입력해 주세요: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName);
            
            map.CreatMap();

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write($"Score: {player.Score}");

                map.Render();

                player.PlayerMove(map.tilePool);
                ScrollDown();

                if (CheckGameOver())
                    break;

                Thread.Sleep(clearTime);
            }

            Console.Clear();

            GameOver(playerName);
        }

        public void GameOver(string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            uiManager.WriteAtCenter("💀 G A M E   O V E R 💀", -3);
            uiManager.WriteAtCenter("============================", -2);

            Console.ForegroundColor = ConsoleColor.White;
            uiManager.WriteAtCenter($"👤 플레이어 : {name}", 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            uiManager.WriteAtCenter($"🏆 최종 점수 : {player.Score}", 1);

            Console.ForegroundColor = ConsoleColor.Red;
            uiManager.WriteAtCenter("============================", 3);

            Console.ResetColor();
            uiManager.GameOver();

        }


        public void ScrollDown()
        {
            if (player.y <= 15)
            {
                isScrollStarted = true;
                map.ScrollDown(); 
                player.y++;       

                map.BuildNextStep();
            }
        }

        private bool CheckGameOver()
        {
            if (isScrollStarted)
            {
                int maxHieght = Console.WindowHeight - 1;
                if (player.y >= maxHieght)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
