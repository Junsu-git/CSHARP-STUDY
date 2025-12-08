using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Core
{
    // 1. 씬(화면) 관리자: 어떤 화면을 보여줄지 결정
    class SceneManager
    {
        private Scene currentScene;

        public void LoadScene(Scene newScene)
        {
            currentScene = newScene;
            currentScene.Enter(); // 씬 진입 시 초기화
        }

        public void Run()
        {
            while (true)
            {
                // 1. 그리기 (Render)
                currentScene.Draw();

                // 2. 입력 (Input)
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // 3. 업데이트 (Update - 입력 처리)
                currentScene.HandleInput(keyInfo);
            }
        }
    }
}
