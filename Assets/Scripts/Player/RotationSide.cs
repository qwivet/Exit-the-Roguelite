using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RotationSide : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        
        private Transform _transform;
        private Input _input;
        private Vector2 _oldMoveInput = Vector2.zero;
        private PlayerAttackAnimation _playerAttack;
        
        private void Awake()
        {
            _transform = this.transform;
            _input = new Input();
            _input.Enable();

            _playerAttack = this.GetComponent<PlayerAttackAnimation>();
        }

        private void Update()
        {
            if (_input.Player.Move.ReadValue<Vector2>() == _oldMoveInput) return;
            _oldMoveInput = _input.Player.Move.ReadValue<Vector2>();
            if (_input.Player.Move.ReadValue<Vector2>() == Vector2.zero) return;
            var inputMove = _input.Player.Move.ReadValue<Vector2>();
            StartCoroutine(RotateLerp(Quaternion.LookRotation(playerTransform.forward * inputMove.y +
                                                              playerTransform.right * inputMove.x)));
        }

        private IEnumerator RotateLerp(Quaternion newRotation)
        {
            var oldRotation = _transform.rotation;
            for (var i = 0; i < 10; i++)
            {
                _transform.rotation = Quaternion.Slerp(oldRotation, newRotation, (i+1) * .1f);
                yield return new WaitForSeconds(0.01f);
            }

            _playerAttack.RealRotation = newRotation;
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
