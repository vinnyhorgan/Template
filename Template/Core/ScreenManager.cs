using Template.Core.Transitions;

namespace Template.Core
{
    class ScreenManager
    {
        private static ScreenManager _instance;

        private Screen _currentScreen;
        private Transition _currentTransition;

        private ScreenManager()
        {

        }

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScreenManager();
                }

                return _instance;
            }
        }

        public void LoadScreen(Screen screen, Transition transition)
        {
            if (_currentTransition != null)
            {
                return;
            }

            _currentTransition = transition;
            _currentTransition.StateChanged += (sender, args) => LoadScreen(screen);
            _currentTransition.Completed += (sender, args) => _currentTransition = null;
        }

        public void LoadScreen(Screen screen)
        {
            _currentScreen?.Unload();

            screen.Load();

            _currentScreen = screen;
        }

        public void Update(float dt)
        {
            _currentScreen?.Update(dt);

            _currentTransition?.Update(dt);
        }

        public void Draw()
        {
            _currentScreen?.Draw();

            _currentTransition?.Draw();
        }
    }
}
