using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace GameOff2023
{
    public class UserInterface : StartupScript
    {
        public PlayerController PlayerController;

        private TextBlock _healthText;

        public override void Start()
        {
            PlayerController.Health.OnHealthChanged += OnHealthChanged;

            var page = Entity.Get<UIComponent>().Page;

            _healthText = page.RootElement.FindVisualChildOfType<TextBlock>("HealthText");
        }

        private void OnHealthChanged(object sender, int health)
        {
            _healthText.Text = health.ToString();
        }
    }
}
