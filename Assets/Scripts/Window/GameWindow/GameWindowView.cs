using DAATS.Initializer.System.Window.GameWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowView : MonoBehaviour, IGameWindowView
    {
        [SerializeField]
        private Button _pauseButton;

        public void SetEventReceiver(IGameWindowEventReceiver eventReceiver)
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