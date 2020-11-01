namespace BlazorFlappyBirdApp.Models
{
    public class BirdModel
    {
        public int DistanceFromGround { get; private set; } = 100;

        public int JumpStrength { get; private set; } = 50;

        public int Height { get; private set; } = 45;

        public int Width { get; private set; } = 60;

        public int Left { get; private set; } = 220;

        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }

        public void Jump()
        {
            if (DistanceFromGround <= 530)
                DistanceFromGround += JumpStrength;
        }

        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }
    }
}
