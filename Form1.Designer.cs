namespace PaddldOCRTest2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ManualSelect = new Button();
            OCRResultLabel = new Label();
            AutoSelect = new Button();
            SelectRegion = new Button();
            OCRResult = new RichTextBox();
            label2 = new Label();
            RegionAreaShow = new Button();
            OCRImageLabel = new Label();
            OCRImage = new PictureBox();
            CleanPanel = new Button();
            ((System.ComponentModel.ISupportInitialize)OCRImage).BeginInit();
            SuspendLayout();
            // 
            // ManualSelect
            // 
            ManualSelect.Location = new Point(32, 351);
            ManualSelect.Name = "ManualSelect";
            ManualSelect.Size = new Size(116, 34);
            ManualSelect.TabIndex = 0;
            ManualSelect.Text = "手动选择截图";
            ManualSelect.UseVisualStyleBackColor = true;
            ManualSelect.Click += ManualSelectClick;
            // 
            // OCRResultLabel
            // 
            OCRResultLabel.AutoSize = true;
            OCRResultLabel.Location = new Point(421, 22);
            OCRResultLabel.Name = "OCRResultLabel";
            OCRResultLabel.Size = new Size(56, 17);
            OCRResultLabel.TabIndex = 1;
            OCRResultLabel.Text = "检测结果";
            // 
            // AutoSelect
            // 
            AutoSelect.Location = new Point(171, 351);
            AutoSelect.Name = "AutoSelect";
            AutoSelect.Size = new Size(116, 34);
            AutoSelect.TabIndex = 2;
            AutoSelect.Text = "自动选择截图";
            AutoSelect.UseVisualStyleBackColor = true;
            AutoSelect.Click += AutoSelectClick;
            // 
            // SelectRegion
            // 
            SelectRegion.Location = new Point(32, 31);
            SelectRegion.Name = "SelectRegion";
            SelectRegion.Size = new Size(116, 34);
            SelectRegion.TabIndex = 3;
            SelectRegion.Text = "自动截图区域选择";
            SelectRegion.UseVisualStyleBackColor = true;
            SelectRegion.Click += SelectRegionClick;
            // 
            // OCRResult
            // 
            OCRResult.Location = new Point(421, 48);
            OCRResult.Name = "OCRResult";
            OCRResult.Size = new Size(287, 132);
            OCRResult.TabIndex = 4;
            OCRResult.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 84);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 5;
            label2.Text = "自动区域坐标";
            // 
            // RegionAreaShow
            // 
            RegionAreaShow.Location = new Point(32, 115);
            RegionAreaShow.Name = "RegionAreaShow";
            RegionAreaShow.Size = new Size(174, 83);
            RegionAreaShow.TabIndex = 6;
            RegionAreaShow.Text = "未选择区域";
            RegionAreaShow.UseVisualStyleBackColor = true;
            // 
            // OCRImageLabel
            // 
            OCRImageLabel.AutoSize = true;
            OCRImageLabel.Location = new Point(421, 203);
            OCRImageLabel.Name = "OCRImageLabel";
            OCRImageLabel.Size = new Size(56, 17);
            OCRImageLabel.TabIndex = 7;
            OCRImageLabel.Text = "检测图像";
            // 
            // OCRImage
            // 
            OCRImage.Location = new Point(421, 239);
            OCRImage.Name = "OCRImage";
            OCRImage.Size = new Size(287, 99);
            OCRImage.TabIndex = 8;
            OCRImage.TabStop = false;
            // 
            // CleanPanel
            // 
            CleanPanel.Location = new Point(308, 351);
            CleanPanel.Name = "CleanPanel";
            CleanPanel.Size = new Size(116, 34);
            CleanPanel.TabIndex = 9;
            CleanPanel.Text = "清空面板";
            CleanPanel.UseVisualStyleBackColor = true;
            CleanPanel.Click += CleanPanelClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CleanPanel);
            Controls.Add(OCRImage);
            Controls.Add(OCRImageLabel);
            Controls.Add(RegionAreaShow);
            Controls.Add(label2);
            Controls.Add(OCRResult);
            Controls.Add(SelectRegion);
            Controls.Add(AutoSelect);
            Controls.Add(OCRResultLabel);
            Controls.Add(ManualSelect);
            Name = "OCRMain";
            Text = "截屏文字识别";
            ((System.ComponentModel.ISupportInitialize)OCRImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ManualSelect;
        private Label OCRResultLabel;
        private Button AutoSelect;
        private Button SelectRegion;
        private RichTextBox OCRResult;
        private Label label2;
        private Button RegionAreaShow;
        private Label OCRImageLabel;
        private PictureBox OCRImage;
        private Button button1;
        private Button CleanPanel;
    }
}
