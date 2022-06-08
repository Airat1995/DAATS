namespace DAATS.System.Interface
{
    public interface ICountableCollectionSystem : ICollectionSystem
    {
        public uint CollectedCount { get; }
    }
}