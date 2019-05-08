//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows;
using System.Windows.Threading;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class App : Application
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ErrorWindow errorWindow = new ErrorWindow(e.Exception);
            errorWindow.ShowDialog();
        }

        #endregion
    }
}
