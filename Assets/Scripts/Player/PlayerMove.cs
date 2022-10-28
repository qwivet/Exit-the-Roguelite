using LatestBack.Movement;
using LatestBack.Movement.Rotation;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float moveForce;
        [SerializeField] private float moveTick;
        
        private Input _input;
        private PhysicsMove _move;
        private Brake _brake;
        
        public bool IsMove { set; private get; }
        public PhysicsMove Move { get => _move; }

        private void Awake()
        {
            IsMove = true;
            _brake = new Brake(this.GetComponent<Rigidbody>(), 0.95f);
            _input = new Input();
            _input.Enable();
            _move = new PhysicsMove(this.GetComponent<Rigidbody>(), 
                new StandardRotation(this.transform), moveForce, moveTick);
        }

        private void FixedUpdate()
        {
            if (_input.Player.Move.ReadValue<Vector2>() == Vector2.zero || !_move.CanMove || !IsMove)
                _brake.Execute();
            else
                _move.SetVelocity(_input.Player.Move.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }
    }
}
