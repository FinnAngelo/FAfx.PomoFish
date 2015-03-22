namespace FinnAngelo.PomoFish
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtxtRestPeriodInMinutes = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtWorkingPeriodInMinutes = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtSnapshotPeriodInSeconds = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxPlaySound = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxTakeSnapshots = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.iSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mtxtRestPeriodInMinutes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mtxtWorkingPeriodInMinutes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // mtxtRestPeriodInMinutes
            // 
            this.mtxtRestPeriodInMinutes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.iSettingsBindingSource, "RestPeriodInMinutes", true));
            this.mtxtRestPeriodInMinutes.Location = new System.Drawing.Point(120, 35);
            this.mtxtRestPeriodInMinutes.Mask = "000";
            this.mtxtRestPeriodInMinutes.Name = "mtxtRestPeriodInMinutes";
            this.mtxtRestPeriodInMinutes.Size = new System.Drawing.Size(103, 20);
            this.mtxtRestPeriodInMinutes.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 38);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rest Period (min)";
            // 
            // mtxtWorkingPeriodInMinutes
            // 
            this.mtxtWorkingPeriodInMinutes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.iSettingsBindingSource, "WorkingPeriodInMinutes", true));
            this.mtxtWorkingPeriodInMinutes.Location = new System.Drawing.Point(120, 9);
            this.mtxtWorkingPeriodInMinutes.Mask = "000";
            this.mtxtWorkingPeriodInMinutes.Name = "mtxtWorkingPeriodInMinutes";
            this.mtxtWorkingPeriodInMinutes.Size = new System.Drawing.Size(103, 20);
            this.mtxtWorkingPeriodInMinutes.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Working Period (min)";
            // 
            // mtxtSnapshotPeriodInSeconds
            // 
            this.mtxtSnapshotPeriodInSeconds.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.iSettingsBindingSource, "SnapshotPeriodInSeconds", true));
            this.mtxtSnapshotPeriodInSeconds.Location = new System.Drawing.Point(120, 34);
            this.mtxtSnapshotPeriodInSeconds.Mask = "00000";
            this.mtxtSnapshotPeriodInSeconds.Name = "mtxtSnapshotPeriodInSeconds";
            this.mtxtSnapshotPeriodInSeconds.Size = new System.Drawing.Size(103, 20);
            this.mtxtSnapshotPeriodInSeconds.TabIndex = 3;
            this.mtxtSnapshotPeriodInSeconds.ValidatingType = typeof(int);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbxPlaySound);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 40);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Play Sound";
            // 
            // cbxPlaySound
            // 
            this.cbxPlaySound.AutoSize = true;
            this.cbxPlaySound.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxPlaySound.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.iSettingsBindingSource, "PlaySound", true));
            this.cbxPlaySound.Location = new System.Drawing.Point(120, 15);
            this.cbxPlaySound.Name = "cbxPlaySound";
            this.cbxPlaySound.Size = new System.Drawing.Size(15, 14);
            this.cbxPlaySound.TabIndex = 0;
            this.cbxPlaySound.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.mtxtSnapshotPeriodInSeconds);
            this.groupBox3.Controls.Add(this.cbxTakeSnapshots);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 63);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Snapshot Period (sec)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Take Snapshots";
            // 
            // cbxTakeSnapshots
            // 
            this.cbxTakeSnapshots.AutoSize = true;
            this.cbxTakeSnapshots.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxTakeSnapshots.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.iSettingsBindingSource, "TakeSnapshots", true));
            this.cbxTakeSnapshots.Location = new System.Drawing.Point(120, 14);
            this.cbxTakeSnapshots.Name = "cbxTakeSnapshots";
            this.cbxTakeSnapshots.Size = new System.Drawing.Size(15, 14);
            this.cbxTakeSnapshots.TabIndex = 1;
            this.cbxTakeSnapshots.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(148, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // iSettingsBindingSource
            // 
            this.iSettingsBindingSource.DataSource = typeof(FinnAngelo.PomoFish.Properties.ISettings);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(231, 203);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox mtxtSnapshotPeriodInSeconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtxtWorkingPeriodInMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxPlaySound;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbxTakeSnapshots;
        private System.Windows.Forms.MaskedTextBox mtxtRestPeriodInMinutes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource iSettingsBindingSource;

    }
}