using Assets._Project.Scripts.Core.Common;
using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    public interface ICarHealth : IHealthHolder { }
    public interface IEnemiesTarget : IAttackTarget
    {
        public Transform Tr { get; }

        public bool IsPossibleToChase();
    }
    public interface ICarFinisher
    {
        public bool IsOnFinish { get; set; }
    }

    public class CarAttackTarget : CachedMonoBehaviour, IEnemiesTarget, ICarHealth, ICarFinisher
    {
        [SerializeField] int maxHealth = 30;
        private int _currentHealth;
        public event Action OnHealthGone;

        public Transform Tr => CachedTrasform;

        public bool IsOnFinish { get; set; }

        public bool IsPossibleToChase()
        {
            return _currentHealth != 0 && !IsOnFinish;
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

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }
    }
}