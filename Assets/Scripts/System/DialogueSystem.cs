using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using DialogueEditor;
using Zenject;

namespace DAATS.System
{
    public class DialogueSystem : IDialogueSystem
    {
        private readonly ConversationManager _conversationManager;
        private readonly List<Action> _onDialogueStarts = new List<Action>();
        private readonly List<Action> _onDialogueEnds = new List<Action>();

        public DialogueSystem(DiContainer container)
        {
            _conversationManager = container.Resolve<ConversationManager>();
        }

        public void Hide()
        {
            _conversationManager.TurnOffUI();
        }

        public void StartDialogue(IDialogueComponent dialogue)
        {
            InitializeProperties(dialogue);
            InitializeCallbacks();
            
            _conversationManager.StartConversation(dialogue.Conversation);
        }

        private void InitializeProperties(IDialogueComponent dialogue)
        {
            _conversationManager.TextSpeed = dialogue.TextSpeed;
            _conversationManager.ScrollSpeed = dialogue.ScrollSpeed;
        }

        private void InitializeCallbacks()
        {
            ConversationManager.OnConversationEnded = () => { };
            ConversationManager.OnConversationStarted = () => { };

            ConversationManager.OnConversationEnded = () =>
            {
                foreach (var onDialogueEnd in _onDialogueEnds)
                    onDialogueEnd.Invoke();
            };
            ConversationManager.OnConversationStarted = () =>
            {
                foreach (var onDialogueStart in _onDialogueStarts)
                    onDialogueStart.Invoke();
            };
        }

        public void SubscribeOnDialogStarts(Action onDialogStarts)
        {
            _onDialogueStarts.Add(onDialogStarts);
        }

        public void SubscribeOnDialogEnds(Action onDialogEnds)
        {
            _onDialogueEnds.Add(onDialogEnds);
        }

        public void UnsubscribeOnDialogStarts(Action onDialogStarts)
        {
            _onDialogueStarts.Add(onDialogStarts);
        }

        public void UnsubscribeOnDialogEnds(Action onDialogEnds)
        {
            _onDialogueEnds.Remove(onDialogEnds);
        }
    }
}