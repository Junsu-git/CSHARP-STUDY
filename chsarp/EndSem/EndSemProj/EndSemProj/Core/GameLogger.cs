using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Core
{
    // 3. 로그 관리자 (로그 로직 분리)
    class GameLogger
    {
        private List<string> logs = new List<string>();
        private int maxLines;

        public GameLogger(int maxLines)
        {
            this.maxLines = maxLines;
        }

        public void Add(string message)
        {
            logs.Add(message);
            if (logs.Count > maxLines) logs.RemoveAt(0);
        }

        public void DrawLogs(int startX, int startY)
        {
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                if (i < logs.Count)
                    Console.WriteLine(" " + logs[i].PadRight(88)); // 잔상 제거
                else
                    Console.WriteLine("".PadRight(90));
            }
        }
    }
}
