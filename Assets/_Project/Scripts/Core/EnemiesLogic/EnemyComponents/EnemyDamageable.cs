using Assets._Project.Scripts.Core.Common;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public class EnemyDamageable : MonoBehaviour, IHealthHolder, IAttackTarget
    {
        [SerializeField] private int maxHealth;

        private int _currentHealth;

        public event Action OnHealthGone;

        public void TackeAttack(int damage)
        {
            if (_currentHealth > damage)
            {
                _currentHealth -= damage;
            }
            else
            {
                _currentHealth = 0;
                OnHealthGone?.Invoke();
            }
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }
    }
}