using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Player
{
    public class Dash : LatestBack.Movement.Dash
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashTime;
        [SerializeField] private float dashReloadTime;
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private VolumeProfile volume;
        [SerializeField] private MotionBlur normalBlur;
        [SerializeField] private MotionBlur dashBlur;
        
        private Input _input;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _input = new Input();
            _input.Enable();
            _input.Player.Dash.performed += _ => Execute();
            _rigidbody = this.GetComponent<Rigidbody>();
            _transform = this.transform;
        }

        private void Execute()
        {
            if(IsDash) return;
            StartCoroutine(MakeDash());
        }

        private IEnumerator MakeDash()
        {
            var moveVector = _input.Player.Move.ReadValue<Vector2>();
            playerMove.IsMove = false;
            IsDash = true;
            if (volume.Has<MotionBlur>())
                volume.Remove<MotionBlur>();
            volume.components.Add(dashBlur);
            if (moveVector == Vector2.zero)
                _rigidbody.AddForce(Vector3.forward * dashForce, ForceMode.Impulse);
            else
                _rigidbody.AddForce((_transform.forward * moveVector.y + _transform.right * moveVector.x) * dashForce,
                    ForceMode.Impulse);
            yield return new WaitForSeconds(dashTime);
            if (volume.Has<MotionBlur>())
                volume.Remove<MotionBlur>();
            volume.components.Add(normalBlur);
            _rigidbody.velocity = Vector3.up * _rigidbody.velocity.y;
            playerMove.IsMove = true;
            yield return new WaitForSeconds(dashReloadTime);
            IsDash = false;
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