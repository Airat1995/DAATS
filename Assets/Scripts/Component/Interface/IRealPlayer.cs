using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IRealPlayer : IPlayer
    {
        CharacterController CharacterController { get; }
    }
}