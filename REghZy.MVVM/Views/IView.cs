namespace REghZy.MVVM.Views {
    public interface IView {
        /// <summary>
        /// This view's title/caption
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Shows the view
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the view
        /// </summary>
        /// <param name="force">An optional parameter, usually used to "Fully close" a window, rather than hide</param>
        void Hide(bool force = false);
    }
}