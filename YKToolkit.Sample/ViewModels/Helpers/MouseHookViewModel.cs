namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Controls;

    public class MouseHookViewModel : ViewModelBase
    {
        private MouseHook _mouseHook = new MouseHook();

        private bool _isMouseHook;
        public bool IsMouseHook
        {
            get { return this._isMouseHook; }
            set
            {
                if (SetProperty(ref this._isMouseHook, value))
                {
                    if (this._isMouseHook) Hook();
                    else UnHook();
                }
            }
        }

        private int _x;
        public int X
        {
            get { return this._x; }
            set { SetProperty(ref this._x, value); }
        }

        private int _y;
        public int Y
        {
            get { return this._y; }
            set { SetProperty(ref this._y, value); }
        }

        private string _message;
        public string Message
        {
            get { return this._message; }
            set { SetProperty(ref this._message, value); }
        }

        private void Hook()
        {
            this._mouseHook.MouseMove += OnMouseMove;
            this._mouseHook.MouseLeftButtonDown += OnMouseLeftButtonDown;
            this._mouseHook.MouseLeftButtonUp += OnMouseLeftButtonUp;
            this._mouseHook.MouseRightButtonDown += OnMouseRightButtonDown;
            this._mouseHook.MouseRightButtonUp += OnMouseRightButtonUp;
            this._mouseHook.MouseMiddleButtonDown += OnMouseMiddleButtonDown;
            this._mouseHook.MouseMiddleButtonUp += OnMouseMiddleButtonUp;
            this._mouseHook.MouseWheel += OnMouseWheel;
            this._mouseHook.Hook();
        }

        private void UnHook()
        {
            this._mouseHook.MouseMove -= OnMouseMove;
            this._mouseHook.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            this._mouseHook.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            this._mouseHook.MouseRightButtonDown -= OnMouseRightButtonDown;
            this._mouseHook.MouseRightButtonUp -= OnMouseRightButtonUp;
            this._mouseHook.MouseMiddleButtonDown -= OnMouseMiddleButtonDown;
            this._mouseHook.MouseMiddleButtonUp -= OnMouseMiddleButtonUp;
            this._mouseHook.MouseWheel -= OnMouseWheel;
            this._mouseHook.UnHook();
        }

        private void OnMouseMove(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.X = e.Point.X;
            this.Y = e.Point.Y;
        }

        private void OnMouseLeftButtonDown(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseLeftButtonDown";
        }

        private void OnMouseLeftButtonUp(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseLeftButtonUp";
        }

        private void OnMouseRightButtonDown(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseRightButtonDown";
        }

        private void OnMouseRightButtonUp(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseRightButtonUp";
        }

        private void OnMouseMiddleButtonDown(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseMiddleButtonDown";
        }

        private void OnMouseMiddleButtonUp(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseMiddleButtonUp";
        }

        private void OnMouseWheel(MouseHook.MSLLHOOKSTRUCT e)
        {
            this.Message = "OnMouseWheel";
        }
    }
}
