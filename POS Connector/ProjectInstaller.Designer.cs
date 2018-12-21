namespace POS_Connector
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServicePOSSyncInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServicePOSSync = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServicePOSSyncInstaller
            // 
            this.ServicePOSSyncInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServicePOSSyncInstaller.Password = null;
            this.ServicePOSSyncInstaller.Username = null;
            // 
            // ServicePOSSync
            // 
            this.ServicePOSSync.Description = "POS Background Sync";
            this.ServicePOSSync.DisplayName = "POS Background Sync";
            this.ServicePOSSync.ServiceName = "ServicePOSSync";
            this.ServicePOSSync.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.ServicePOSSync.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ServicePOSSync_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServicePOSSyncInstaller,
            this.ServicePOSSync});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServicePOSSyncInstaller;
        private System.ServiceProcess.ServiceInstaller ServicePOSSync;
    }
}