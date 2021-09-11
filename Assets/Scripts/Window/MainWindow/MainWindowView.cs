using DAATS.Initializer.System.Window.MainWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Initializer.System.Window.MainWindow
{
    public class MainWindowView : MonoBehaviour, IMainWindowView
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _settingsButton;

        public void SetEventReceiver(IMainWindowEventReceiver eventReceiver)
        {
            _startButton.onClick.AddListener(eventReceiver.OnStart);
            _settingsButton.onClick.AddListener(eventReceiver.OnSettings);
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