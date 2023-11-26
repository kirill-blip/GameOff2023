using Stride.Core.Serialization;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Panels;

namespace GameOff2023
{
    public class UserInterface : StartupScript
    {
        public LevelLoader LevelLoader;
        public PlayerController PlayerController;
        public GameManager GameManager;

        private TextBlock _healthText;
        private TextBlock _ghostsCountText;

        private Button _restartButton;
        private Button _menuButton;

        private Grid _baseUI;
        private Grid _endPanel;

        public override void Start()
        {
            PlayerController.Health.OnHealthChanged += OnHealthChanged;
            GameManager.GhostsCountChanged += ChangeGhostsCount;

            var page = Entity.Get<UIComponent>().Page;

            _healthText = page.RootElement.FindVisualChildOfType<TextBlock>("HealthText");
            _ghostsCountText = page.RootElement.FindVisualChildOfType<TextBlock>("GhostCount");

            _restartButton = page.RootElement.FindVisualChildOfType<Button>("RestartButton");
            _menuButton = page.RootElement.FindVisualChildOfType<Button>("MenuButton");

            _restartButton.Click += ReloadLevel;
            _menuButton.Click += LoadMenu;

            _baseUI = page.RootElement.FindVisualChildOfType<Grid>("BaseUI");
            _endPanel = page.RootElement.FindVisualChildOfType<Grid>("EndPanel");

            GameManager.GameOvered += GameOvered;
        }

        private void LoadMenu(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            LevelLoader.LoadMenu();
        }

        private void ReloadLevel(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            LevelLoader.LoadLevel();
        }

        private void GameOvered(object sender, System.EventArgs e)
        {
            _baseUI.Visibility = Visibility.Hidden;
            _endPanel.Visibility = Visibility.Visible;
        }

        private void ChangeGhostsCount(object sender, int count)
        {
            _ghostsCountText.Text = $"Ghosts left: {count}";
        }

        private void OnHealthChanged(object sender, int health)
        {
            _healthText.Text = health.ToString();
        }
    }
}
