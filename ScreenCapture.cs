namespace PaddldOCRTest2
{
    public partial class ScreenCapture : Form
    {
        private int _maxHeight = 0;

        //获取屏幕的宽度和高度
        private int _maxWidth = 0;
        //鼠标是否按下的标记
        private bool _mouseIsDown = false;

        //画矩形的画笔
        private Pen _pen = new Pen(Color.Red, 2.0f);

        //按钮点击的起始坐标
        private Point _pressPoint = new Point();

        //一个空白的位图,用来展示空白的背景
        private Bitmap emptyBackgroundImage;

        //截屏之前,用于存储整个屏幕截图的原始位图。
        private Bitmap orginScreenImage;
        //用于在鼠标移动期间临时存储图像的位图副本。
        private Bitmap tempScreenImage;

        private bool needChangeScreen = false;

        //初始化窗体
        public ScreenCapture(bool autoRegion = false, bool screenSelect = false)
        {
            InitializeComponent();
            //设置窗体为无边框样式
            this.FormBorderStyle = FormBorderStyle.None;
            //最大化窗体
            this.WindowState = FormWindowState.Maximized;

            //获取所有屏幕的工作区域大小
            Rectangle virtualScreenBounds = Rectangle.Empty;
            foreach (Screen screen in Screen.AllScreens)
            {
                virtualScreenBounds = Rectangle.Union(virtualScreenBounds, screen.Bounds);
                //这个地方屏幕的属性是系统中标识的显示器12的顺序
                //screen中有是否主屏幕的区分,workarea是工作区域
            }
            // 这个地方可以在两块屏幕和一块屏幕之间切换
            //virtualScreenBounds = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            // 设置窗体的位置和大小覆盖所有屏幕
            this.Bounds = virtualScreenBounds;
            //设置窗体置顶
            this.TopMost = true;
            //加载窗体响应事件
            this.Load += CaptureScreenLoad;
            if (autoRegion)
            {
                this.Shown += AutoRegionShown;
            }
            _isSelectedRegion = screenSelect;
            //获取当前所有屏幕的最大宽高
            _maxHeight = virtualScreenBounds.Height;
            _maxWidth = virtualScreenBounds.Width;
            //监听键盘esc事件
            this.KeyDown += KeyboardDown;

            // 设置窗体里面的画布大小为虚拟屏幕的大小
            this.pictureBox.ClientSize = new Size(_maxWidth, _maxHeight);
            //在画布中监听鼠标的事件
            this.pictureBox.MouseMove += PictureBoxMouseMove;
            this.pictureBox.MouseDown += PictureBoxMouseDown;
            this.pictureBox.MouseUp += PictureBoxMouseUp;
        }

        //关闭窗体时,返回截取的屏幕图片
        public Bitmap OutScreenImage { get; set; }

        //返回选择区域的坐标
        public Rectangle OutSelectedRegion { get; set; }

        //自动选取区域的区域对象
        private Rectangle _autoRegionArea { get; set; }

        //记录是否是选取区域
        private bool _isSelectedRegion { get; set; }
        public void SetAutoRegionArea(Rectangle rectangle)
        {
            _autoRegionArea = rectangle;
        }

        private void AutoRegionShown(object sender, EventArgs e)
        {
            //把emptyBackgroundImage的图片给tempScreenImage,在tempScreenImage上临时画矩形
            tempScreenImage = (Bitmap)emptyBackgroundImage.Clone();
            var pic = Graphics.FromImage(tempScreenImage);
            //把自动选取的区域画出来
            var region = _autoRegionArea;
            pic.DrawRectangle(_pen, region);
            //如果选中区域的宽度和高度都不为0
            if (region.Width != 0 && region.Height != 0)
            {
                //将选中的区域从orginScreenImage保存到part
                var part = orginScreenImage.Clone(region, System.Drawing.Imaging.PixelFormat.Undefined);
                //记录选中的区域,窗体关闭的时候返回
                this.OutScreenImage = part;
                //在tempScreenImage上画选中的区域(即原图,其他区域是半透明的)
                pic.DrawImage(part, region);
            }
            //将tempScreenImage内容显示在pictureBox上
            pictureBox.Image = tempScreenImage;
            //等待几秒钟
            Task.Delay(1000).Wait();
            this.DialogResult = DialogResult.OK;
            //关闭窗体
            this.Close();
        }

        public event EventHandler MinimizeRequest;
        //窗体加载监听事件
        private async void CaptureScreenLoad(object sender, EventArgs e)
        {

            //截图窗体加载的时候,触发最小化事件
            MinimizeRequest.Invoke(this, EventArgs.Empty);

            //设置this.Visible = false 的意思是截图的时候不会把窗体截进去
            this.Visible = false;
            //将整个屏幕保存到orgin
            orginScreenImage = new Bitmap(_maxWidth, _maxHeight);
            Graphics gg = Graphics.FromImage(orginScreenImage);

            int MinX = Screen.AllScreens.ToList().Min(x => x.Bounds.X);
            int MinY = Screen.AllScreens.ToList().Min(x => x.Bounds.Y);

            //把当前屏幕的内容复制到orginScreenImage中
            foreach (Screen screen in Screen.AllScreens)
            {
                Point scrPoint = screen.Bounds.Location;
                Point desPoint = new Point(scrPoint.X - MinX, scrPoint.Y - MinY);
                gg.CopyFromScreen(scrPoint, desPoint, screen.Bounds.Size);
            }
            orginScreenImage.Save("d:\\全屏.png");
            this.Visible = true;
            //设置窗体为正常状态(不能设置为最大化,否则只会占据一个窗体)
            this.WindowState = FormWindowState.Normal;
            //设置emptyBackgroundImage为全屏大小的空白图片
            emptyBackgroundImage = new Bitmap(_maxWidth, _maxHeight);
            //展示空白图片
            pictureBox.Image = emptyBackgroundImage;
        }
        //获取两点之间的矩形区域
        private Rectangle GetRegion(Point A, Point B)
        {
            return new Rectangle(new Point(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y)), new Size(Math.Abs(B.X - A.X), Math.Abs(B.Y - A.Y)));
        }

        //监听键盘事件
        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            //ESC 键
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                //关闭窗体
                this.Close();
            }
        }

        //鼠标按下的监听事件
        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            _mouseIsDown = true;
            _pressPoint = e.Location;
            //点击的时候把创建的按钮给清空
            pictureBox.Controls.Clear();
        }

        //鼠标移动的监听事件
        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            //如果鼠标已经按下
            if (_mouseIsDown)
            {
                //把emptyBackgroundImage的图片给tempScreenImage,在tempScreenImage上临时画矩形
                tempScreenImage = (Bitmap)emptyBackgroundImage.Clone();
                var pic = Graphics.FromImage(tempScreenImage);
                var region = GetRegion(_pressPoint, e.Location);
                pic.DrawRectangle(_pen, region);
                //如果选中区域的宽度和高度都不为0
                if (region.Width != 0 && region.Height != 0)
                {
                    //将选中的区域从orginScreenImage保存到part
                    var part = orginScreenImage.Clone(region, System.Drawing.Imaging.PixelFormat.Undefined);
                    part.Save("d:\\截图.png");
                    //记录选中的区域,窗体关闭的时候返回
                    if (_isSelectedRegion)
                    {
                        this.OutSelectedRegion = region;
                    }
                    else
                    {
                        this.OutScreenImage = part;
                    }
                    //在tempScreenImage上画选中的区域(即原图,其他区域是半透明的)
                    pic.DrawImage(part, region);
                }
                //将tempScreenImage内容显示在pictureBox上
                pictureBox.Image = tempScreenImage;
            }
        }
        //鼠标松开的监听事件
        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            _mouseIsDown = false;
            //鼠标松开的时候创建一个确定按钮
            Button Confirm = new Button()
            {
                Text = "确认",
                Width = 65,
                Height = 30,
                ForeColor = Color.Blue,
                BackColor = Color.Gray
            };
            Confirm.Click += (x, y) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            Confirm.Location = new Point(e.X + 10, e.Y + 10);
            pictureBox.Controls.Add(Confirm);
            //鼠标松开的时候创建一个取消按钮
            Button Cancel = new Button()
            {
                Text = "取消",
                Width = 65,
                Height = 30,
                ForeColor = Color.MediumVioletRed,
                BackColor = Color.Gray
            };
            Cancel.Click += (x, y) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            Cancel.Location = new Point(e.X + 80, e.Y + 10);
            pictureBox.Controls.Add(Cancel);
        }
    }
}