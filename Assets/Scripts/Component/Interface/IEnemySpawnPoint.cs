namespace DAATS.Component.Interface
{
    public interface IEnemySpawnPoint : ISpawnPoint
    {
        public IEnemy AssociatedEnemy { get; }

        void AddAssociatedEnemy(IEnemy associatedEnemy);
    }
}