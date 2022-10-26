using UnityEngine;

namespace Player
{
    public class VelocityChangeDeformation : MonoBehaviour
    {
        [SerializeField] private Transform assForceTransform;
        [SerializeField] private Vector3 multipliers;

        private Transform _transform;
        private Vector3 _velocityChange;

        private void Awake()
        {
            _transform = this.transform;
        }

        private void FixedUpdate()
        {
            _velocityChange = assForceTransform.InverseTransformPoint(_transform.position);
            _transform.localScale = new Vector3(1, 1+_velocityChange.y*multipliers.y, 1);
            _transform.localEulerAngles =
                new Vector3(-_velocityChange.z * multipliers.x, 0, _velocityChange.x * multipliers.z);
        }
    }
}