namespace WindowsFormsApplication1
{
    partial class Viewer
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.picJbig2 = new System.Windows.Forms.PictureBox();
            this.tbarSize = new System.Windows.Forms.TrackBar();
            this.tooltipSize = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picJbig2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSize)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpen,
            this.tsbtnSave});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(1032, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // tsbtnOpen
            // 
            this.tsbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpen.Image = global::WindowsFormsApplication1.Properties.Resources.Folder;
            this.tsbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpen.Name = "tsbtnOpen";
            this.tsbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpen.Text = "Open";
            this.tsbtnOpen.ToolTipText = "Open file";
            this.tsbtnOpen.Click += new System.EventHandler(this.tsbtnOpen_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::WindowsFormsApplication1.Properties.Resources.save;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "Save";
            this.tsbtnSave.ToolTipText = "Save image as";
            // 
            // picJbig2
            // 
            this.picJbig2.Location = new System.Drawing.Point(0, 25);
            this.picJbig2.Name = "picJbig2";
            this.picJbig2.Size = new System.Drawing.Size(1032, 715);
            this.picJbig2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picJbig2.TabIndex = 1;
            this.picJbig2.TabStop = false;
            // 
            // tbarSize
            // 
            this.tbarSize.AutoSize = false;
            this.tbarSize.LargeChange = 50;
            this.tbarSize.Location = new System.Drawing.Point(92, 0);
            this.tbarSize.Maximum = 100;
            this.tbarSize.Minimum = 10;
            this.tbarSize.Name = "tbarSize";
            this.tbarSize.Size = new System.Drawing.Size(314, 25);
            this.tbarSize.SmallChange = 10;
            this.tbarSize.TabIndex = 2;
            this.tbarSize.TickFrequency = 10;
            this.tbarSize.Value = 100;
            this.tbarSize.Scroll += new System.EventHandler(this.tbarSize_Scroll);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 740);
            this.Controls.Add(this.tbarSize);
            this.Controls.Add(this.picJbig2);
            this.Controls.Add(this.toolStripMain);
            this.Name = "Viewer";
            this.Text = "JBIG2 viewer";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picJbig2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tsbtnOpen;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.PictureBox picJbig2;
        private System.Windows.Forms.TrackBar tbarSize;
        private System.Windows.Forms.ToolTip tooltipSize;
    }
}

