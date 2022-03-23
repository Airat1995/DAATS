namespace DAATS.Component.Interface
{
    public interface IEnemyDeactivatorTile : ISpecialTile<IEnemyDeactivatorTile>
    {
        public IEnemyActivatorTile[] ConnectedTile { get; }
    }
}