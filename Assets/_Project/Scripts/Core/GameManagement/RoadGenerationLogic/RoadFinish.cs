using Assets._Project.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic
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