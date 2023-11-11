using SharpDX.DirectInput;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;

namespace GameOff2023
{
    public class PlayerController : SyncScript
    {
        public float Velocity { get; set; } = 5;
        public float JumpForce { get; set; } = 5;

        private CharacterComponent _character;

        public override void Start()
        {
            _character = Entity.Get<CharacterComponent>();
        }

        public override void Update()
        {
            Vector3 direction = Vector3.Zero;

            if (Input.HasKeyboard)
            {
                if (Input.IsKeyDown(Keys.W))
                {
                    direction.Z++;
                }

                if (Input.IsKeyDown(Keys.S))
                {
                    direction.Z--;
                }

                if (Input.IsKeyDown(Keys.A))
                {
                    direction.X++;
                }

                if (Input.IsKeyDown(Keys.D))
                {
                    direction.X--;
                }
            }

            direction.Normalize();
            direction = Vector3.Transform(direction, Entity.Transform.Rotation);
            _character.SetVelocity(direction * Velocity);


            if (Input.IsKeyPressed(Keys.Space) && _character.IsGrounded)
            {
                _character.Jump(new Vector3(0, 1, 0) * JumpForce);
            }
        }
    }
}
