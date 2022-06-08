using System;

namespace DAATS.System.Interface
{
    public interface IExitLevelSystem : ICallableSystem
    {
        void SubscribeOnLevelExitReach(Action onExitReach);
    }
}