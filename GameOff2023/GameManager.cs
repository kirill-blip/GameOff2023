using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using System;
using System.Collections.Generic;

namespace GameOff2023
{
    public class GameManager : SyncScript
    {
        public List<Ghost> Ghosts = new();
        public PlayerController PlayerController;

        public bool IsMouseActive = false;

        public event EventHandler<int> GhostsCountChanged;
        public event EventHandler GameOvered;

        public override void Start()
        {
            GhostsCountChanged?.Invoke(this, Ghosts.Count);

            foreach (Ghost ghost in Ghosts)
            {
                ghost.GhostKilled += GhostKilled;
                ghost.Player = PlayerController;
            }

            Input.MousePosition = new Vector2(.5f, .5f);

            IsMouseActive = true;
            Game.IsMouseVisible = false;

            PlayerController.Health.OnPlayerKilled += OnPlayerKilled;
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Keys.Escape))
            {
                EnableOrDiactivateMouse();
            }
        }

        private void EnableOrDiactivateMouse()
        {
            IsMouseActive = !IsMouseActive;
            Game.IsMouseVisible = !IsMouseActive;

            if (!IsMouseActive)
            {
                Input.UnlockMousePosition();
            }
            else
            {
                Input.LockMousePosition();
            }
        }

        private void GhostKilled(object sender, Ghost ghost)
        {
            ghost.GhostKilled -= GhostKilled;

            Ghosts.Remove(ghost);
            Entity.Scene.Entities.Remove(ghost.Entity);

            GhostsCountChanged?.Invoke(this, Ghosts.Count);

            if (Ghosts.Count == 0)
            {
                PlayerController.Health.Kill();
                StopGame();
            }
        }

        private void OnPlayerKilled(object sender, EventArgs e)
        {
            StopGame();
        }

        private void StopGame()
        {
            EnableOrDiactivateMouse();
            GameOvered?.Invoke(this, null);
        }
    }
}
