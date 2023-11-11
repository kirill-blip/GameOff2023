using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;

namespace GameOff2023
{
    public class CameraRotation : SyncScript
    {
        public float MouseSpeed = .6f;
        public float MaxLookUpAngle = -50;
        public float MaxLookDownAngle = 50;

        private Entity _cameraPivot;
        private Vector3 _cameraRotation;
        private bool _isActive = false;
        private Vector2 _maxCameraAnglesRadians;
        private CharacterComponent _character;

        public override void Start()
        {
            _cameraPivot = Entity.FindChild("CameraPivot");

            _maxCameraAnglesRadians = new Vector2(MathUtil.DegreesToRadians(MaxLookUpAngle), MathUtil.DegreesToRadians(MaxLookDownAngle));

            _cameraRotation = Entity.Transform.RotationEulerXYZ;


            Input.MousePosition = new Vector2(.5f, .5f);

            _isActive = true;
            Game.IsMouseVisible = false;

            _character = Entity.Get<CharacterComponent>();
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Keys.Escape))
            {
                _isActive = !_isActive;
                Game.IsMouseVisible = !_isActive;
                Input.UnlockMousePosition();
            }

            if (_isActive)
            {
                Input.LockMousePosition();
                var mouseMovement = Input.MouseDelta * MouseSpeed;

                _cameraRotation.Y += -mouseMovement.X;
                _cameraRotation.X += mouseMovement.Y;
                _cameraRotation.X = MathUtil.Clamp(_cameraRotation.X, _maxCameraAnglesRadians.X, _maxCameraAnglesRadians.Y);

                _character.Orientation = Quaternion.RotationY(_cameraRotation.Y);

                _cameraPivot.Transform.Rotation = Quaternion.RotationX(_cameraRotation.X);
            }
        }
    }
}
