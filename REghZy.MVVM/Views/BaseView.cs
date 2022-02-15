using REghZy.MVVM.ViewModels;

namespace REghZy.MVVM.Views {
    /// <summary>
    /// An interface that a view can implement for easy access to their ViewModel
    /// <para>
    /// This can just be used for convenience, rather than having to cast a view's DataContext
    /// </para>
    /// </summary>
    /// <typeparam name="ViewModel">The ViewModel class that extends <see cref="BaseViewModel"/></typeparam>
    public interface BaseView<ViewModel> where ViewModel : BaseViewModel {
        ViewModel Model { get; set; }
    }
}