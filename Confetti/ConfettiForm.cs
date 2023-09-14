using Timer = System.Windows.Forms.Timer;

namespace Confetti;

public sealed class ConfettiForm : Form
{
    private readonly Timer _timer;
    private static int _screenWidth;
    private static int _screenHeight;
    private const int ConfettiCount = 100;
    private readonly Confetti[] _confettiArray;

    public ConfettiForm(Rectangle screenBounds)
    {
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        TransparencyKey = BackColor;
        DoubleBuffered = true;
        TopMost = true;

        _screenWidth = screenBounds.Width;
        _screenHeight = screenBounds.Height;

        var random = new Random();
        _confettiArray = new Confetti[ConfettiCount];
        for (var i = 0; i < ConfettiCount; i++)
        {
            _confettiArray[i] = new Confetti(random, _screenWidth, _screenHeight);
        }

        _timer = new Timer();
        _timer.Interval = 10;
        _timer.Tick += Timer_Tick!;
        _timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        foreach (var confetti in _confettiArray)
        {
            confetti.Update();
        }

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        foreach (var confetti in _confettiArray)
        {
            confetti.Draw(e.Graphics);
        }
    }

    class Confetti
    {
        private float _x, _y;
        private readonly float _velocityX;
        private readonly float _velocityY;
        private readonly Color _color;
        private readonly Random _random;

        public Confetti(Random random, int screenWidth, int screenHeight)
        {
            _random = random;
            _x = random.Next(screenWidth);
            _y = random.Next(screenHeight);
            _velocityX = random.Next(-5, 6);
            _velocityY = random.Next(2, 6);
            _color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        public void Update()
        {
            _x += _velocityX;
            _y += _velocityY;

            if (!(_x < 0) && !(_x > _screenWidth) && !(_y > _screenHeight)) return;
            _x = _random.Next(_screenWidth);
            _y = -10;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(_color), _x, _y, 10, 10);
        }
    }
}