using System;

namespace DAATS.System.Interface
{
    public interface ICameraFlowSystem : ICallableSystem
    {
        void StartFlow();

        void SubscribeOnFlowStart(Action onFlowStart);
        void SubscribeOnFlowEnd(Action onFlowEnd);

        void UnsubscribeOnFlowStart(Action onFlowStart);
        void UnsubscribeOnFlowEnd(Action onFlowEnd);
    }
}