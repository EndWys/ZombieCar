using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic
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

        private int _currentHelth;

        public event Action OnHealthGone;
        public Transform Tr => CachedTrasform;

        public void ResetHealth()
        {
            _currentHelth = maxHealth;
        }

        public void TackeAttack()
        {
            if(_currentHelth > DAMAGE)
            {
                _currentHelth -= DAMAGE;
            }
            else
            {
                _currentHelth = 0;
                OnHealthGone?.Invoke();
            }
        }
    }
}