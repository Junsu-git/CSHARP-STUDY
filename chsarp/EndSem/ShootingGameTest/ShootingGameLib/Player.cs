namespace ShootingGameLib
{
    public enum PlayerType { P1, P2 }

    public class Player : GameUnit
    {
        public PlayerType Type { get; private set; }

        public Player(string name, PlayerType type) : base(name, 100, 10)
        {
            Type = type;
            if (type == PlayerType.P1) { X = 10; Y = 20; }
            else { X = 30; Y = 20; }
        }

        public override void Action() { }

        public string HandleInput(ConsoleKey key)
        {
            string ? action = null;
            int speed = 1;

            if (Type == PlayerType.P1)
            {
                switch (key)
                {
                    case ConsoleKey.W: Y -= speed; action = "위로 이동"; break;
                    case ConsoleKey.S: Y += speed; action = "아래로 이동"; break;
                    case ConsoleKey.A: X -= speed; action = "왼쪽으로 이동"; break;
                    case ConsoleKey.D: X += speed; action = "오른쪽으로 이동"; break;
                    case ConsoleKey.F: action = $"공격 (Atk {CurrentAttack})"; break;
                    case ConsoleKey.G: action = "필살기"; break;
                }
            }
            else // P2
            {
                switch (key)
                {
                    case ConsoleKey.I: Y -= speed; action = "위로 이동"; break;
                    case ConsoleKey.K: Y += speed; action = "아래로 이동"; break;
                    case ConsoleKey.J: X -= speed; action = "왼쪽으로 이동"; break;
                    case ConsoleKey.L: X += speed; action = "오른쪽으로 이동"; break;
                    case ConsoleKey.Oem1: action = $"공격 (Atk {CurrentAttack})"; break;
                    case ConsoleKey.Oem7: action = "필살기"; break;
                }
            }
            return action != null ? $"[{Name}] {action}" : null;
        }
    }
}