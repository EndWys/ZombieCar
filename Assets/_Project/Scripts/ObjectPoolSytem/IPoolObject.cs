namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public interface IPoolObject
    {
        public void OnCreate();
        public void OnGetFromPool();

        public void OnReleaseToPool();
    }

    public interface ISelfReleaseObject
    {
        public void SelfRelease();
    }
}