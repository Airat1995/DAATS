using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.System
{
    public class CameraFollowSystem : ICameraFollowSystem
    {
        private readonly ICameraComponent _camera;
        private readonly Rect _boundBox;        
        private readonly IPlayer _player;

        private Vector3 _previousPlayerPosition;

        public CameraFollowSystem(ICameraComponent camera, IPlayer player, float yPos)
        {
            _camera = camera;
            _player = player;
            _boundBox = camera.BoundingBox;
            _camera.Transform.position = _player.Transform.position + new Vector3(0, yPos, 0);
            _previousPlayerPosition = _player.Transform.position;
        }

        public void Update(float deltaTime)
        {
            var playerInBox = Vector3.zero;
            var projectedPosition = _camera.Camera.WorldToViewportPoint(_player.Transform.position);
            if (projectedPosition.x < _boundBox.min.x)
                playerInBox.x = -1;
           else if(projectedPosition.x > _boundBox.max.x)
                playerInBox.x = 1;
            if(projectedPosition.y < _boundBox.min.y) 
                playerInBox.z = -1;
            else if(projectedPosition.y > _boundBox.max.y)
                playerInBox.z = 1;
            if(playerInBox == Vector3.zero)
                return;

            Vector3 playerPosition = _player.Transform.position;
            float playerDistance = Vector3.Distance(playerPosition, _previousPlayerPosition);
            _previousPlayerPosition = playerPosition;
            float cameraMoveSpeed = _camera.Speed;
            if(playerDistance > _camera.TeleportDistance)
                cameraMoveSpeed = _camera.TeleportSpeed;

            Vector3 cameraFinalPos = _camera.Transform.position + playerInBox * cameraMoveSpeed * deltaTime;
            _camera.Transform.position = cameraFinalPos;
        }
    }
}