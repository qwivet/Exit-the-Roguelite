using System;
using LatestBack.Combat;

namespace Combat
{
    [Serializable]
    public class PlayerAttack : Damager
    {
        private Input _input;
        
        protected override void AfterAwake()
        {
            _input = new Input();
            _input.Enable();
            _input.Player.MeleeAttack.performed += _ => Attack();
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