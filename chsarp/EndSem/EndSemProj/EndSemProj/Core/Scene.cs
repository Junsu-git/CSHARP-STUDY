using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Core
{
    // 2. 씬의 기본 규격 (추상 클래스)
    abstract class Scene
    {
        public abstract void Enter(); // 진입
        public abstract void Draw(); // 그리기
        public abstract void HandleInput(ConsoleKeyInfo key);
    }
}
