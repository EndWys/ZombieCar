using Assets._Project.Scripts.Core.Common;
using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    public interface IEnemiesTarget : IAttackTarget
    {
        public Transform Tr { get; }
    }

    public interface ICarHealth : IHealthHolder { }

    public class CarAttackTarget : CachedMonoBehaviour, IEnemiesTarget, ICarHealth
    {
        [SerializeField] int maxHealth = 30;

        private int _currentHealth;

        public event Action OnHealthGone;
        public Transform Tr => CachedTrasform;

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }

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
    }
}