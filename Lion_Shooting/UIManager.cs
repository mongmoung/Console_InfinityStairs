using System;
using System.Text;
using System.Threading;

namespace Lion_Shooting
{
    internal class UIManager
    {
        private GameManager gameManager => GameManager.instance;
        public static UIManager instance = new UIManager();


        #region 시작 인트로
        public void ShowIntro()
        {

            Console.Clear();
            string[] introText = {
            "====================",
            "올려봐요",
            "코딩의  꿈",
            "===================="
        };

            bool isYellow = false;

            while (!Console.KeyAvailable)
            {
                int centerY = GetCenterY(); // 창 크기 변화 대응봄

                if (isYellow) Console.ForegroundColor = ConsoleColor.Yellow;
                else Console.ForegroundColor = ConsoleColor.White;

                for (int i = 0; i < introText.Length; i++)
                {
                    Console.SetCursorPosition(GetCenterX(introText[i]), centerY - 2 + i);
                    Console.WriteLine(introText[i]);
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                string msg = "아무 키나 누르면 시작합니다...";
                Console.SetCursorPosition(GetCenterX(msg), centerY + 4);
                Console.WriteLine(msg);

                isYellow = !isYellow;
                Thread.Sleep(300);
            }
            Console.ResetColor();
            Console.ReadKey(true);

        }
        #endregion

        public void GameOver()
        {

            while (true)
            {
                WriteAtBottomCenter("Spacebar = ReStart , ESC = END");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Spacebar)
                    {
                        if (gameManager != null)
                            gameManager.GameStart();
                        else
                            GameManager.instance.GameStart();
                        break;
                    }
                    if (key == ConsoleKey.Escape)
                        Environment.Exit(0); 
                }
                Thread.Sleep(100);
            }
        }

        public int GetCenterX(string text) => (Console.WindowWidth / 2) - (GetConsoleWidth(text) / 2);
        private int GetCenterY() => Console.WindowHeight / 2;
        private int GetConsoleWidth(string text) 
        {
            return Encoding.Default.GetByteCount(text);
        }

        public void WriteAtBottomCenter(string text)
        {
            int x = GetCenterX(text);
            int y = Console.WindowHeight - 1;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public void WriteAtCenter(string text, int yOffset)
        {
            int x = GetCenterX(text);
            int y = GetCenterY() + yOffset; 

            if (x < 0) x = 0;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

    }
}
