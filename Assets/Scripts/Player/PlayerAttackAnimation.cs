using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAttackAnimation: MonoBehaviour
    {
        [SerializeField] private float attackReload;
        [SerializeField] private float attackTime;
        
        private Input _input;
        private bool _canAttack = true;
        private Transform _transform;
        
        public Quaternion RealRotation { get; set; }
        private void Awake()
        {
            _input = new Input();
            _input.Enable();
            _input.Player.MeleeAttack.performed += _ => AttackMove();

            _transform = this.transform;
        }

        private void AttackMove()
        {
            if (_canAttack)
                StartCoroutine(Attack());
        }
        
        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }

        private IEnumerator Attack()
        {
            _canAttack = false;
            for (var i = 0; i < attackTime*100; i++)
            {
                _transform.Rotate(0, 360*3/(attackTime*100), 0);
                yield return new WaitForSeconds(0.01f);
            }

            _transform.rotation = RealRotation;
            yield return new WaitForSeconds(attackReload);
            _canAttack = true;
        }
    }
}
