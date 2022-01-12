using DialogueEditor;

namespace DAATS.Component.Interface
{
    public interface IDialogueComponent : IComponent
    {
        NPCConversation Conversation { get; }

        float TextSpeed { get; }
        float ScrollSpeed { get; }
    }
}