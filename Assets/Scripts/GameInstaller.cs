using DAATS.Component.Interface;
using DAATS.Initializer.Component;
using DAATS.Initializer.GameWorld.Loader;
using DAATS.Initializer.GameWorld.Loader.Interface;
using DAATS.Initializer.Level;
using DAATS.Initializer.Level.Interface;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.Mangers.Sound;
using DAATS.Initializer.Mangers.Sound.Interface;
using DAATS.Initializer.System.Window;
using DAATS.Initializer.System.Window.Fabric;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.UserData;
using DAATS.UserData.Interface;
using DialogueEditor;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField]
        private LevelCollection _levelCollection;

        [SerializeField]
        private CameraComponent _cameraComponent;

        [SerializeField]
        private ConversationManager _conversationManager;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IWindowResourceManager), typeof(IResourceManager))
                .To<Manager.Resource.ResourceManager>()
                .AsSingle();

            Container.Bind(
                    typeof(IGetUserProgressData),
                    typeof(ISetUserProgressData), 
                    typeof(IGetUserSettingsData), 
                    typeof(ISetUserSettingsData)
                    )
                .To<InternalStorageGetUserProgressProgressData>()
                .AsCached();

            Container.Bind<IAbstractWindowFactory>()
                .To<AbstractWindowFactory>()
                .AsSingle();

            Container.Bind(typeof(ITickable), typeof(IGameWorldLoader))
                .To<GameWorldLoader>()
                .AsSingle()
                .NonLazy();

            Container.Bind<WindowManager>()
                .AsSingle().NonLazy();

            Container.Bind<ISoundManager>()
                .FromInstance(_soundManager)
                .AsCached();

            Container.Bind<ILevelCollection>()
                .FromInstance(_levelCollection)
                .AsSingle();

            Container.Bind<ICameraComponent>()
                .FromInstance(_cameraComponent)
                .AsSingle();

            Container.Bind<ConversationManager>()
                .FromInstance(_conversationManager)
                .AsSingle();
        }
    }
}