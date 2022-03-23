using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class TileSubscriber<T> where T : MonoBehaviour
    {
        private readonly T _objectSubscribed;
    }
}