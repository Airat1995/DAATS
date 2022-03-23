namespace DAATS.Component.Interface
{
    public interface IEnemySpawnPoint : ISpawnPoint
    {
        public IEnemy AssociatedEnemy { get; }
        public bool EnabledFromStart { get; }

        void AddAssociatedEnemy(IEnemy associatedEnemy);
    }
}