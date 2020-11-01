using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlappyBirdApp.Models
{
    public class PipeModel
    {
        public int DistanceFromLeft { get; private set; } = 500;

        public int DistanceFromBottom { get; private set; } = new Random().Next(0, 100);

        public int Height { get; private set; } = 300;

        public int Width { get; private set; } = 60;

        public int Speed { get; private set; } = 2;

        public int Gap { get; private set; } = 130;

        public int GapBottom => DistanceFromBottom + Height;

        public int GapTop => GapBottom + Gap;

        public int GameWidth { get; private set; } = 500;

        public PipeModel()
        {

        }

        public PipeModel(int gameWidth)
        {
            DistanceFromLeft = gameWidth + Width;
            GameWidth = gameWidth;
        }

        public void Move()
        {
            DistanceFromLeft -= Speed;
        }

        public bool IsOffScreen()
        {
            return DistanceFromLeft <= -60;
        }

        public bool IsCentered()
        {
            bool hasEnteredCenter = DistanceFromLeft <= (GameWidth / 2) + (Width / 2);
            bool hasExitedCenter = DistanceFromLeft <= (GameWidth / 2) - (Width / 2);

            return hasEnteredCenter && !hasExitedCenter;
        }
    }
}
