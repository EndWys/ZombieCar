using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    public interface IEnemiesTarget
    {
        public Transform Tr { get; }
        public void TackeAttack();
    }

    public interface IHealthHolder
    {

        public event Action OnHealthGone;

        public void ResetHealth();
    }

    public class CarAttackTarget : CachedMonoBehaviour, IEnemiesTarget, IHealthHolder
    {
        private const int DAMAGE = 10;

        [SerializeField] int maxHealth = 30;

        private int _currentHealth;

        public event Action OnHealthGone;
        public Transform Tr => CachedTrasform;

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }

        public void TackeAttack()
        {
            if (_currentHealth > DAMAGE)
            {
                _currentHealth -= DAMAGE;
            }
            else
            {
                _currentHealth = 0;
                OnHealthGone?.Invoke();
            }
        }
    }
}