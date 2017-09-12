namespace SensorView.WindowsApp
{
#if !DEBUG
    using System;
#endif
    using System.Windows;

    using Autofac;

    using SensorView.Models;
    using SensorView.WindowsApp.Views;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App
    {
        private IContainer container;

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
#if !DEBUG
            Current.DispatcherUnhandledException += (s, ea) => HandleException(ea.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, ea) => HandleException(ea.ExceptionObject as Exception);
#endif

            RegisterComponents();

            MainWindow = container.Resolve<MainWindow>();
            MainWindow.Show();
        }

#if !DEBUG
        /// <summary>
        ///
        /// </summary>
        /// <param name="ex"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ex", Justification = "Debug only.")]
        private static void HandleException(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "予期せぬエラー", MessageBoxButton.OK, MessageBoxImage.Error);
        }
#endif

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Ignore")]
        private void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SensorManager>().SingleInstance();

            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<MainWindow>().SingleInstance();

            container = builder.Build();
        }
    }
}
