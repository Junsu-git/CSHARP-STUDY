namespace supplement
{
    internal class Jet : Airplane
    {
        const int speedMultiplier = 2;
        public new void SetSpeed(int speed)
        {
            base.SetSpeed(speed * speedMultiplier);
        }

        public void Accelerate()
        {
            base.SetSpeed(GetSpeed() * speedMultiplier);
        }
    }
}