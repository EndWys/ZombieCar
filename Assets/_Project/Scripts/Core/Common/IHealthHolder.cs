using System;

namespace Assets._Project.Scripts.Core.Common
{
    public interface IHealthHolder
    {
        public event Action OnHealthGone;
        public void ResetHealth();
    }
}