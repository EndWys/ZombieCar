using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.GameInput
{
    public class TapInput : MonoBehaviour
    {
        public static event Action OnTap;

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
                OnTap?.Invoke();
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            OnTap?.Invoke();
#endif
        }
    }
}