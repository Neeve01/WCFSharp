namespace WCFSharp.GUI
{
    internal partial class ConfigForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.PluginsCheckboxes = new System.Windows.Forms.TreeView();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.ReloadBtn = new System.Windows.Forms.Button();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LeftPanel.Controls.Add(this.PluginsCheckboxes);
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(296, 198);
            this.LeftPanel.TabIndex = 0;
            // 
            // PluginsCheckboxes
            // 
            this.PluginsCheckboxes.CheckBoxes = true;
            this.PluginsCheckboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginsCheckboxes.Location = new System.Drawing.Point(0, 0);
            this.PluginsCheckboxes.Name = "PluginsCheckboxes";
            this.PluginsCheckboxes.ShowLines = false;
            this.PluginsCheckboxes.ShowPlusMinus = false;
            this.PluginsCheckboxes.ShowRootLines = false;
            this.PluginsCheckboxes.Size = new System.Drawing.Size(296, 198);
            this.PluginsCheckboxes.TabIndex = 0;
            this.PluginsCheckboxes.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.PluginsCheckboxes_AfterCheck);
            // 
            // RightPanel
            // 
            this.RightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RightPanel.Controls.Add(this.ReloadBtn);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(295, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(156, 198);
            this.RightPanel.TabIndex = 1;
            // 
            // ReloadBtn
            // 
            this.ReloadBtn.Location = new System.Drawing.Point(8, 12);
            this.ReloadBtn.Name = "ReloadBtn";
            this.ReloadBtn.Size = new System.Drawing.Size(135, 30);
            this.ReloadBtn.TabIndex = 0;
            this.ReloadBtn.Text = "Reload configs";
            this.ReloadBtn.UseVisualStyleBackColor = true;
            this.ReloadBtn.Click += new System.EventHandler(this.ReloadBtn_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 198);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.Text = "WCFSharp Plugins Manager";
            this.TopMost = true;
            this.LeftPanel.ResumeLayout(false);
            this.RightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button ReloadBtn;
        internal System.Windows.Forms.TreeView PluginsCheckboxes;
    }
}