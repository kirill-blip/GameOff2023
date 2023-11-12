using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;
using System.Threading.Tasks;

namespace GameOff2023
{
    public class Ghost : AsyncScript
    {
        public int Time;
        public int Damage = 10;

        public float MinScale { get; set; } = .25f;

        public PlayerController Player;


        public override async Task Execute()
        {
            while (Game.IsRunning)
            {
                if (Vector3.Distance(Entity.Transform.Position, Player.Entity.Transform.Position) <= 1.5f)
                {
                    Player.Health.Damage(Damage);
                    await Task.Delay(Time);
                }

                await Script.NextFrame();
            }
        }

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