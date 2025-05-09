using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.GameManagement
{
    public class RoadFinish : CachedMonoBehaviour
    {
        public event Action OnFinishReached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CarController car))
            {
                OnFinishReached?.Invoke();
            }
        }
    }
}