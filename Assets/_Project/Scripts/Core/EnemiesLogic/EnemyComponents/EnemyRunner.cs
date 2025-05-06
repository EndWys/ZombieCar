using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IPointRunner
    {
        public void RunToPoint(Vector3 targetPoint);
        public float RemainingDistanceToPoint(Vector3 targetPoint);
    }
    public class EnemyRunner : CachedMonoBehaviour, IPointRunner
    {
        [SerializeField] private float speed;

        public void RunToPoint(Vector3 targetPoint)
        {
            Vector3 direction = (targetPoint - CachedTrasform.position).normalized;

            CachedTrasform.position += direction * speed * Time.deltaTime;

            CachedTrasform.forward = Vector3.Lerp(CachedTrasform.forward, direction, 0.1f);
        }

        public float RemainingDistanceToPoint(Vector3 targetPoint)
        {
            return Vector3.Distance(CachedTrasform.position, targetPoint);
        }
    }
}