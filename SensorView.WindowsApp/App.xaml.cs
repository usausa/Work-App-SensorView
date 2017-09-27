namespace SensorView.WindowsApp
{
    using System;
    using System.Configuration;
    using System.Windows;

    using SensorView.Services;
    using SensorView.WindowsApp.Models;
    using SensorView.WindowsApp.Views;

    using Smart.Resolver;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App
    {
        private IResolver resolver;

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

            MainWindow = resolver.Get<MainWindow>();
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
            var config = new ResolverConfig();

            config.Bind<SensorService>().ToConstant(new SensorService(
                ConfigurationManager.AppSettings["Host"],
                Guid.NewGuid().ToString(),
                ConfigurationManager.AppSettings["Topic"])).InSingletonScope();

            config.Bind<SensorManager>().ToSelf().InSingletonScope();

            config.Bind<MainViewModel>().ToSelf().InSingletonScope();
            config.Bind<MainWindow>().ToSelf().InSingletonScope();

            resolver = config.ToResolver();
        }
    }
}
