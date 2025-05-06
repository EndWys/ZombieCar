using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.GameInput
{
    public class TapInput : MonoBehaviour
    {
        public static event Action OnTap;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnTap?.Invoke();
        }
    }
}