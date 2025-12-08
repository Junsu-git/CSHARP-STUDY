namespace ShootingGameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. 게임 매니저 생성
            GameManager gameManager = new GameManager();

            // 2. 게임 실행
            gameManager.Run();
        }
    }
}