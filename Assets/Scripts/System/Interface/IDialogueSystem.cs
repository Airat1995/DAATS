using System;
using DAATS.Component.Interface;

namespace DAATS.System.Interface
{
    public interface IDialogueSystem : ICallableSystem
    {
        void StartDialogue(IDialogueComponent dialogue);

        void Hide();

        void SubscribeOnDialogStarts(Action onDialogStarts);
        void SubscribeOnDialogEnds(Action onDialogEnds);

        void UnsubscribeOnDialogStarts(Action onDialogStarts);
        void UnsubscribeOnDialogEnds(Action onDialogEnds);
    }
}