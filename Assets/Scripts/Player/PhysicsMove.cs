using System.Threading.Tasks;
using LatestBack.Movement;
using LatestBack.Movement.Interfaces;
using UnityEngine;

namespace Player
{
    public class PhysicsMove : Moving
    {
        private float _moveTick;

        public PhysicsMove(Rigidbody objectRigidbody, IRotatable mobRotation, float speed, float moveTick) : 
            base(objectRigidbody, mobRotation, speed)
        {
            this._moveTick = moveTick;
        }

        public override void SetVelocity(float forwardVelocity, float rightVelocity)
        {
            ObjectRigidbody.AddForce(
                (rightVelocity * MobRotation.RightPoint + forwardVelocity * MobRotation.ForwardPoint) * Speed,
                ForceMode.Impulse);
        }
        public override void SetVelocity(Vector2 velocity)
        {
            SetVelocity(velocity.y, velocity.x);
        }

        private async Task CountTick()
        {
            CanMove = true;
            await Task.Delay((int)(_moveTick * 1000));
            CanMove = true;
        }
    }
}