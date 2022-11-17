using System.Collections;
using LatestBack.Combat.Interfaces;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Combat
{
    public class CubeTestDamagable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float invulnerabilityTime;
        [SerializeField] private float hp;

        private bool _canDamage = true;
        
        private float Hp 
        {
            get => hp;
            set { hp = value; if (hp <= 0)Destroy(); }
        }
        public int Id
        {
            get => 1;
        }

        public void Damage(float damageAmount)
        {
            if (!_canDamage) return;
            Hp -= damageAmount;
            StartCoroutine(DamageReload());
        }

        public void Destroy()
        {
            GameObject.Destroy(this.gameObject);
        }

        private IEnumerator DamageReload()
        {
            _canDamage = false;
            yield return new WaitForSeconds(invulnerabilityTime);
            _canDamage = true;
        }
    }
}