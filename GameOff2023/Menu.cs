using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Games;
using Stride.UI;
using Stride.UI.Controls;

namespace GameOff2023
{
    public class Menu : StartupScript
    {
        public LevelLoader LevelLoader;

        private Button _startButton;
        private Button _exitButton;

        public override void Start()
        {
            if (!Game.Window.IsFullscreen)
            {
                Game.Window.SetSize(new Int2(1920, 1080));
                Game.Window.PreferredFullscreenSize = new Int2(1920, 1080);
                Game.Window.IsFullscreen = true;
            }

            var page = Entity.Get<UIComponent>().Page;

            _startButton = page.RootElement.FindVisualChildOfType<Button>("StartButton");
            _exitButton = page.RootElement.FindVisualChildOfType<Button>("ExitButton");

            _startButton.Click += LoadLevel;
            _exitButton.Click += Exit;
        }

        private void LoadLevel(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            LevelLoader.LoadLevel();
        }

        private void Exit(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            if (Game.Window.IsFullscreen)
            {
                Game.Window.Visible = false;
                Game.Window.IsFullscreen = false;
                Game.Window.Visible = true;
            }
            ((GameBase)Game).Exit();
        }
    }
}
