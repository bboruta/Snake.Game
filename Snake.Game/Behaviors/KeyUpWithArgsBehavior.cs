using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Snake.Game.Behaviors
{
    public class KeyUpWithArgsBehavior : Behavior<UIElement>
    {
        public ICommand KeyDownCommand
        {
            get { return (ICommand)GetValue(KeyDownCommandProperty); }
            set { SetValue(KeyDownCommandProperty, value); }
        }

        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.Register("KeyDownCommand", typeof(ICommand), typeof(KeyUpWithArgsBehavior), new UIPropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.KeyUp += new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyUp -= new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyDownCommand != null)
            {
                KeyDownCommand.Execute(e.Key);
            }
        }
    }
}
