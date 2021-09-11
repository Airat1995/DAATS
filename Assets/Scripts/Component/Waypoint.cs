using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class Waypoint : MonoBehaviour, IWaypoint
    {
        public Vector3 Position => transform.position;
    }
}