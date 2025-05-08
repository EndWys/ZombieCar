using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public class EnemyDamageable : MonoBehaviour, IHealthHolder
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        public event Action OnHealthGone;
        public event Action OnHealthChanged;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TackeAttack(int damage)
        {
            if (_currentHealth > damage)
            {
                _currentHealth -= damage;
                OnHealthChanged?.Invoke();
            }
            else
            {
                _currentHealth = 0;
                OnHealthChanged?.Invoke();
                OnHealthGone?.Invoke();
            }
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }
    }
}