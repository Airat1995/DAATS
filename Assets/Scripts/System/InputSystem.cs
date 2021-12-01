using System;
using System.Threading;
using DAATS.System.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DAATS.Initializer.System
{
    public class InputSystem : IInputSystem
    {
        private Vector2 _movementVector;
        public Vector2 InputData => _movementVector;

        private bool _blocked = false;

        private Gamepad _gamepad;

        public InputSystem()
        {
            _movementVector = Vector2.zero;
            var thread = new Thread(Initialize);
            thread.Start();
        }

        private void Initialize()
        {
            while(_gamepad == null)
            {
                _gamepad = Gamepad.current;
                if (_gamepad == null)
                   Thread.Sleep(64);
            }

        }

        public void Update(float deltaTime)
        {
            if(_gamepad == null)
                return;
            if(!_gamepad.added)
                return;
            _movementVector = _gamepad.leftStick.ReadValue();
        }

        public void Block(bool block)
        {
            _blocked = block;
        }
    }
}