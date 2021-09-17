using System;
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
            _gamepad = Gamepad.current;
            if (_gamepad == null)
                throw new Exception("Unable to find gamepad!");
        }

        public void Update(float deltaTime)
        {
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