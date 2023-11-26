using Stride.Core.Mathematics;
using Stride.Engine;
using System;

namespace GameOff2023
{
    public class Health : StartupScript
    {
        public int HitPoints = 100;

        public Entity CameraPosition;
        public Entity Camera;
        public Entity Weapon;

        public event EventHandler<int> OnHealthChanged;
        public event EventHandler OnPlayerKilled;

        public override void Start() { }

        public void Damage(int damage)
        {
            if (damage <= 0) Log.Error("Damage can't be negative");

            HitPoints -= damage;

            OnHealthChanged?.Invoke(this, HitPoints);

            if (HitPoints <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            Camera.SetParent(CameraPosition);
            Camera.Transform.Position = Vector3.Zero;

            Entity.Scene.Entities.Remove(this.Entity);

            Weapon.EnableAll(false, true);

            OnPlayerKilled?.Invoke(this, null);
        }
    }
}
