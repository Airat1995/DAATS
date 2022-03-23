namespace DAATS.Component.Interface
{
    public interface IEnemyActivatorTile : ISpecialTile<IEnemyActivatorTile>
    {
        public IEnemySpawnPoint[] EnemySpawnPoints { get; }
    }
}