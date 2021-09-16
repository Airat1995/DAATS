using UnityEngine;
using DAATS.Component.Interface;
using UnityEngine.UI;

namespace DAATS.Initializer.Component
{
    public class CameraComponent : MonoBehaviour, ICameraComponent
    {
        [SerializeField]
        private Rect _boundingBox;
        public Rect BoundingBox => _boundingBox;

        [SerializeField]
        private float _speed;
        public float Speed => _speed;

        [SerializeField]
        private float _teleportDistance;
        public float TeleportDistance => _teleportDistance;

        [SerializeField]
        private float _teleportSpeed;
        public float TeleportSpeed => _teleportSpeed;

        [SerializeField]
        private Camera _camera;
        public Camera Camera => _camera;

        public Transform Transform => transform;

#if UNITY_EDITOR

        [SerializeField]
        private Image _image;
        void OnValidate()
        {
            _image.rectTransform.anchorMin = _boundingBox.min;
            _image.rectTransform.anchorMax = _boundingBox.max;
        }
        #endif
    }
}