namespace DAATS.Component.Interface
{
    public interface IEnemyActivatorTile : ISpecialTile<IEnemyActivatorTile>
    {
        public IMovableEnemySpawnPoint[] EnemySpawnPoints { get; }
    }
}