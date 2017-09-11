namespace SensorView.WindowsApp.Views
{
    using System.Collections.ObjectModel;

    using SensorView.WindowsApp.Models;

    public class MainViewModel
    {
        public SensorManager SensorManager { get; }

        public MainViewModel(SensorManager sensorManager)
        {
            SensorManager = sensorManager;
        }
    }
}
