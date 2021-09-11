using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IVisualElement : IComponent
    {
        Material Material { get; }
    }
}