using UnityEngine;

namespace Assets._Project.Scripts.Core.GameInput
{
    public class TurretInput : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
            {
                Vector2 screenPos = Input.mousePosition;
                Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Direction = (screenPos - center).normalized;
            }
            else
            {
                Direction = Vector2.zero;
            }
#else
        if (Input.touchCount > 0)
        {
            Vector2 screenPos = Input.GetTouch(0).position;
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Direction = (screenPos - center).normalized;
        }
        else
        {
            Direction = Vector2.zero;
        }
#endif
        }
    }
}