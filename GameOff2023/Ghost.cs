using Stride.Audio;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;
using System;
using System.Threading.Tasks;

namespace GameOff2023
{
    public class Ghost : AsyncScript
    {
        public float Distance = 2.5f;
        public float RotationSpeed;
        public int Time;
        public int Damage = 10;

        public float MinScale { get; set; } = .25f;

        public Sound Sound;
        public PlayerController Player;

        private SoundInstance _soundInstance;
        private StaticColliderComponent _collider;

        private bool _isDestroyed = false;

        public event EventHandler<Ghost> GhostKilled;

        public override async Task Execute()
        {
            _collider = Entity.Get<StaticColliderComponent>();

            _soundInstance = Sound.CreateInstance();

            while (Game.IsRunning && !_isDestroyed)
            {
                if (Player is not null && Vector3.Distance(Entity.Transform.Position, Player.Entity.Transform.Position) <= Distance)
                {
                    Player.Health.Damage(Damage);
                    await Task.Delay(Time);
                }

                float deltaTime = (float)Game.UpdateTime.WarpElapsed.TotalSeconds;
                Entity.Transform.Rotation *= Quaternion.RotationYawPitchRoll(
                    MathUtil.DegreesToRadians(RotationSpeed * deltaTime),
                    0.0f,
                    0.0f
                );
                await Script.NextFrame();
            }
        }

        public void Scale()
        {
            if (Entity.Transform.Scale == new Vector3(MinScale, MinScale, MinScale))
            {
                _isDestroyed = true;

                _soundInstance.Play();

                Task.Delay(500);

                GhostKilled?.Invoke(this, this);

                return;
            }

            Entity.Transform.Scale /= 2;
        }
    }
}
