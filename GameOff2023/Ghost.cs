using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace GameOff2023
{
    public class Ghost : SyncScript
    {
        public float MinScale { get; set; } = .25f;

        public override void Start() { }

        public override void Update() { }

        public void Scale()
        {
            if (Entity.Transform.Scale == new Vector3(MinScale, MinScale, MinScale))
            {
                Entity.Scene.Entities.Remove(Entity);
            }

            Entity.Transform.Scale /= 2;
            Entity.Get<StaticColliderComponent>().ColliderShape.Scaling /= 2;
        }
    }
}