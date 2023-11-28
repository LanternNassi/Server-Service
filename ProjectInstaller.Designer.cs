namespace Server_Service
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
            this.IMSProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.IMSSERVER = new System.ServiceProcess.ServiceInstaller();
            // 
            // IMSProcessInstaller
            // 
            this.IMSProcessInstaller.Password = null;
            this.IMSProcessInstaller.Username = null;
            // 
            // IMSSERVER
            // 
            this.IMSSERVER.Description = "This is an IMS server service aimed at cordinating IMS server related services";
            this.IMSSERVER.DisplayName = "IMSSERVER";
            this.IMSSERVER.ServiceName = "IMSSERVER";
            this.IMSSERVER.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.IMSSERVER.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.IMSProcessInstaller,
            this.IMSSERVER});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller IMSProcessInstaller;
        private System.ServiceProcess.ServiceInstaller IMSSERVER;
    }
}