using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace GameOff2023
{
    public class ObjectScaler : SyncScript
    {
        private StaticColliderComponent _collider;

        public override void Start()
        {
            _collider = Entity.Get<StaticColliderComponent>();
        }

        public override void Update() { }

        public void Increase()
        {
            if (Entity.Transform.Scale == new Vector3(8, 8, 8)) return;

            Entity.Transform.Scale *= 2;
            _collider.ColliderShape.Scaling *= 2;
        }

        public void Decrease()
        {
            if (Entity.Transform.Scale == new Vector3(.25f, .25f, .25f)) return;

            Entity.Transform.Scale /= 2;
            _collider.ColliderShape.Scaling /= 2;
        }
    }
}
