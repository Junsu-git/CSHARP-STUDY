namespace supplement
{
    internal class Airplane
    {
        int speed;
        public Airplane()
        {
        }
        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

        public int GetSpeed() => speed;
    }
}