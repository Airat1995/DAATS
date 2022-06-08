namespace DAATS.Component.Interface
{
    public interface IMovableEnemySpawnPoint : ISpawnPoint
    {
        public float Speed { get; set; }
        
        public IMovableEnemy AssociatedEnemy { get; }
        
        public bool EnabledFromStart { get; }
        
        void AddAssociatedEnemy(IMovableEnemy associatedEnemy);

    }
}