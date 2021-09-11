using DAATS.Window.FogFollowWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Window.FogFollowWindow
{
    class FogFollowWindowView : MonoBehaviour, IFogFollowWindowView
    {
        [SerializeField]
        private Image _fogImage;

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

        public void UpdateFogPosition(Vector3 imagePosition)
        {
            _fogImage.transform.position = imagePosition;
        }
    }
}
