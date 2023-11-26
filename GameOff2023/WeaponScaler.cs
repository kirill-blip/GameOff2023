using Stride.Audio;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;

namespace GameOff2023
{
    public class WeaponScaler : SyncScript
    {
        public float Distance = 10f;

        public TransformComponent RaycastPosition;

        private AudioEmitterComponent _audioEmitterComponent;
        private AudioEmitterSoundController _soundController;

        private Simulation _simulation;

        public override void Start()
        {
            _audioEmitterComponent = Entity.Get<AudioEmitterComponent>();
            _soundController = _audioEmitterComponent["Gun"];
            _simulation = this.GetSimulation();
        }

        public override void Update()
        {
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                var raycastStart = RaycastPosition.WorldMatrix.TranslationVector;
                var forward = RaycastPosition.WorldMatrix.Forward;
                var raycastEnd = raycastStart + forward * -Distance;

                var result = _simulation.Raycast(raycastStart, raycastEnd);

                _soundController.Play();

                if (result.Succeeded)
                {
                    ObjectScaler objectScaler = result.Collider.Entity.Get<ObjectScaler>();

                    if (objectScaler is not null)
                    {
                        objectScaler.Decrease();
                    }

                    Ghost ghost = result.Collider.Entity.Get<Ghost>();

                    if (ghost is not null)
                    {
                        ghost.Scale();
                    }
                }
            }

            if (Input.IsMouseButtonPressed(MouseButton.Right))
            {
                var raycastStart = RaycastPosition.WorldMatrix.TranslationVector;
                var forward = RaycastPosition.WorldMatrix.Forward;
                var raycastEnd = raycastStart + forward * -100;

                var result = _simulation.Raycast(raycastStart, raycastEnd);

                if (result.Succeeded)
                {
                    ObjectScaler objectScaler = result.Collider.Entity.Get<ObjectScaler>();

                    if (objectScaler is not null)
                    {
                        objectScaler.Increase();
                    }
                }
            }
        }
    }
}
