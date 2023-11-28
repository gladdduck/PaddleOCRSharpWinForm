using PaddleOCRSharp;
using System.Drawing.Imaging;

namespace PaddldOCRTest2
{
    public partial class Form1 : Form
    {
        // 创建 OCRModelConfig
        private OCRModelConfig ocrConfig;

        // 创建 OCRParameter
        private OCRParameter ocrParameter = new OCRParameter();

        // 创建 PaddleOCREngine
        private PaddleOCREngine ocrEngine;

        //选择的自动区域
        private Rectangle _autoRegionArea { get; set; }

        public Form1()
        {
            InitializeComponent();
            //构建 OCRModelConfig 实例
            ocrEngine = new PaddleOCREngine(ocrConfig, ocrParameter);
        }

        private ResultDto<string> PaddleOCRTransfer(Bitmap screenImage)
        {
            if (screenImage == null)
            {
                return new ResultDto<string>() { Message = "图像区域识别错误!", Success = false };
            }
            // 保存为 PNG 格式到内存流:(MemortBMP->Pnh)
            using (MemoryStream memoryStream = new MemoryStream())
            {
                screenImage.Save(memoryStream, ImageFormat.Png);

                memoryStream.Position = 0;
                // 从内存流中重新读取为 PNG 格式的位图
                screenImage = new Bitmap(memoryStream);
            }
            // 识别图片
            OCRResult ocrResult = ocrEngine.DetectText(screenImage);
            string resultText = "0";
            if (ocrResult != null)
            {
                //解析结果
                resultText = ocrResult.Text;
                return new ResultDto<string>() { Message = resultText, Success = true};
            }

            return new ResultDto<string>() { Message = "图像区域识别错误!", Success = false };
        }

        private async void ManualSelectClick(object sender, EventArgs e)
        {
           
            Bitmap? bitmap;
            ScreenCapture screencapture = new ScreenCapture(autoRegion: false, screenSelect: false);
            if (screencapture.ShowDialog() == DialogResult.OK)
            {
                //value就是关闭的时候传出的
                bitmap = screencapture.OutScreenImage;
                if (bitmap != null)
                {
                    OCRImage.Image = bitmap;
                    //OCR识别
                    var ocrMeasureResult = PaddleOCRTransfer(bitmap);
                    if (!ocrMeasureResult.Success)
                    {
                        return;
                    }
                    //添加测量值
                    OCRResult.Text = ocrMeasureResult.Message;
                    return;
                }
            }
        }

        private void SelectRegionClick(object sender, EventArgs e)
        {
            ScreenCapture screencapture = new ScreenCapture(autoRegion: false, screenSelect: true);
            if (screencapture.ShowDialog() == DialogResult.OK)
            {
                //value就是关闭的时候传出的
                _autoRegionArea = screencapture.OutSelectedRegion;
                 RegionAreaShow.Text = $"自动截图区域选择:\n左上角:({_autoRegionArea.X},{_autoRegionArea.Y})\n宽高为:({_autoRegionArea.Width},{_autoRegionArea.Height}).";
            }
        }

        private void AutoSelectClick(object sender, EventArgs e)
        {
            if (_autoRegionArea.Width == 0 || _autoRegionArea.Height == 0)
            {
                MessageBox.Show("请先选择自动截图区域!");
                return;
            }
            Bitmap? bitmap;
            ScreenCapture screencapture = new ScreenCapture(autoRegion: true, screenSelect: false);
            screencapture.SetAutoRegionArea(_autoRegionArea);
            if (screencapture.ShowDialog() == DialogResult.OK)
            {
                //value就是关闭的时候传出的
                bitmap = screencapture.OutScreenImage;
                if (bitmap != null)
                {
                    OCRImage.Image = bitmap;
                    //OCR识别
                    var ocrMeasureResult = PaddleOCRTransfer(bitmap);
                    if (!ocrMeasureResult.Success)
                    {
                        return;
                    }
                    //添加测量值
                    OCRResult.Text = ocrMeasureResult.Message;
                    return;
                }
            }
        }

        private void CleanPanelClick(object sender, EventArgs e)
        {
            OCRImage.Image = null;
            OCRResult.Text = "";
            _autoRegionArea = new Rectangle();
            RegionAreaShow.Text = $"";
        }
        private void HandleMinimizeRequested(object sender, EventArgs e)
        {
            // 当收到通知时，执行最小化操作
            this.WindowState = FormWindowState.Minimized;
        }
    }

    internal class ResultDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}