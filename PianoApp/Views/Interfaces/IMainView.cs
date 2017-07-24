using System.Windows.Controls;

namespace PianoApp.Views.Interfaces
{
    /// <summary>
    /// spuervisor MainView interface
    /// </summary>
    public interface IMainView : IBaseView
    {
        /// <summary>
        /// Triggers animation for key at given index in array
        /// </summary>
        /// <param name="index"></param>
        void ButtonClickAnimation(int index);

        /// <summary>
        /// Shows the animation fade out
        /// </summary>
        /// <param name="button"></param>
        void ShowAnimationFadeOut(Button button);
    }
}
