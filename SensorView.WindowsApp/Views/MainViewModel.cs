namespace SensorView.WindowsApp.Views
{
    using SensorView.WindowsApp.Models;

    using Smart.Windows.ViewModels;

    /// <summary>
    ///
    /// </summary>
    public sealed class MainViewModel : DisposableViewModelBase
    {
        /// <summary>
        ///
        /// </summary>
        public SensorManager SensorManager { get; }

        /// <summary>
        ///
        /// </summary>
        public SensorItem SelectedItem { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sensorManager"></param>
        public MainViewModel(SensorManager sensorManager)
        {
            SensorManager = sensorManager;
            SensorManager.Enable = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SensorManager.Enable = false;
        }
    }
}
