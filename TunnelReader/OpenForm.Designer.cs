namespace TunnelReader
{
    partial class OpenForm
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
            this.browseButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.headingsCheckBox = new System.Windows.Forms.CheckBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.indexLabel = new System.Windows.Forms.Label();
            this.indexTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(297, 12);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(24, 20);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // openButton
            // 
            this.openButton.Enabled = false;
            this.openButton.Location = new System.Drawing.Point(246, 98);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Открыть";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Enabled = false;
            this.pathTextBox.Location = new System.Drawing.Point(12, 12);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(277, 20);
            this.pathTextBox.TabIndex = 2;
            this.pathTextBox.Text = "Нажмите на ...  чтобы выбрать файл";
            // 
            // headingsCheckBox
            // 
            this.headingsCheckBox.AutoSize = true;
            this.headingsCheckBox.Checked = true;
            this.headingsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.headingsCheckBox.Location = new System.Drawing.Point(12, 104);
            this.headingsCheckBox.Name = "headingsCheckBox";
            this.headingsCheckBox.Size = new System.Drawing.Size(149, 17);
            this.headingsCheckBox.TabIndex = 3;
            this.headingsCheckBox.Text = "Открыть с заголовками";
            this.headingsCheckBox.UseVisualStyleBackColor = true;
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(9, 69);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(250, 13);
            this.countLabel.TabIndex = 4;
            this.countLabel.Text = "Количество отображаемых в сетке элементов: ";
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(265, 66);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(56, 20);
            this.countTextBox.TabIndex = 5;
            this.countTextBox.Text = "0";
            // 
            // openFileDialog
            // 
            this.openFileDialog.AddExtension = false;
            this.openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Открыть файл";
            // 
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Location = new System.Drawing.Point(9, 45);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(146, 13);
            this.indexLabel.TabIndex = 6;
            this.indexLabel.Text = "Начальный индекс данных:";
            // 
            // indexTextBox
            // 
            this.indexTextBox.Location = new System.Drawing.Point(265, 42);
            this.indexTextBox.Name = "indexTextBox";
            this.indexTextBox.Size = new System.Drawing.Size(56, 20);
            this.indexTextBox.TabIndex = 7;
            this.indexTextBox.Text = "1";
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 141);
            this.Controls.Add(this.indexTextBox);
            this.Controls.Add(this.indexLabel);
            this.Controls.Add(this.countTextBox);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.headingsCheckBox);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.browseButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 180);
            this.Name = "OpenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Открыть файл";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.CheckBox headingsCheckBox;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.TextBox indexTextBox;
    }
}