using System;
using DAATS.Window.FogFollowWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Window.FogFollowWindow
{
    class FogFollowWindowView : MonoBehaviour, IFogFollowWindowView
    {
        [SerializeField]
        private Image _fogImage;

        [SerializeField]
        private Material _material;
        
        [Range(0, 1.2f)]
        public float _radius = 0f;

        public float _horizontal = 16;

        public float _verical = 9;

        public float _duration = 1f;
        

        public void SetEventReceiver(IFogWindowEventReceiver eventReceiver)
        {            
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}
