using Assets._Project.Scripts.Core.Common;
using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    public interface ICarHealth : IHealthHolder { }
    public interface ICarFinisher
    {
        public bool IsOnFinish { get; set; }
    }
    public interface IEnemiesTarget : IAttackTarget
    {
        public Vector3 GetTargetPosition();
        public Vector3 GetClosestTargetPoint(Vector3 nearestTo);

        public bool IsPossibleToChase();
    }

    public class CarAttackTarget : CachedMonoBehaviour, IEnemiesTarget, ICarHealth, ICarFinisher
    {
        [SerializeField] private Collider carCollider;
        [SerializeField] int maxHealth = 30;

        private int _currentHealth;

        public event Action OnHealthGone;
        public event Action OnHealthChanged;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        public bool IsOnFinish { get; set; }

        public bool IsPossibleToChase()
        {
            return _currentHealth != 0 && !IsOnFinish;
        }

        public Vector3 GetTargetPosition()
        {
            return CachedTrasform.position;
        }

        public Vector3 GetClosestTargetPoint(Vector3 nearestTo)
        {
            return carCollider.ClosestPoint(nearestTo);
        }

        public void TackeAttack(int damage)
        {
            if (_currentHealth > damage)
            {
                _currentHealth -= damage;
                OnHealthChanged?.Invoke();
            }
            else if(_currentHealth > 0)
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