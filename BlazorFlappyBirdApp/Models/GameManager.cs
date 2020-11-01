using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlappyBirdApp.Models
{
    public class GameManager
    {
        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }
        public bool IsRunning { get; private set; } = false;
        private readonly int _gravity = 2;
        public int GameWidth { get; private set; } = 500;
        
        public event EventHandler MainLoopCompleted;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);

                await Task.Delay(20);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }

        }

        public void GameOver()
        {
            IsRunning = false;
        }

        public void Jump()
        {
            if (IsRunning)
                Bird.Jump();
        }

        void MoveObjects()
        {
            Bird.Fall(this._gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        void CheckForCollisions()
        {
            if (Bird.IsOnGround())
                GameOver();

            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());

            if (centeredPipe != null)
            {
                bool hasCollidedWithBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - centeredPipe.Height / 2;
                bool hasCollidedWithTop = Bird.DistanceFromGround + Bird.Height > centeredPipe.GapTop - centeredPipe.Height / 2;

                if (hasCollidedWithBottom || hasCollidedWithTop)
                {
                    GameOver();
                }
            }
        }

        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft < GameWidth/2)
            {
                Pipes.Add(new PipeModel(GameWidth));
            }

            if (Pipes.First().IsOffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }
    }
}
