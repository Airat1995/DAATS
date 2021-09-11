using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class SimpleSpawnPoint : MonoBehaviour, ISpawnPoint
    {
        public Transform SpawnTransform => transform;
    }
}