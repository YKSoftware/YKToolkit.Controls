namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Controls;

    public class KeyboardHookViewModel : ViewModelBase
    {
        private KeyboardHook _keyboardHook = new KeyboardHook();

        private bool _isKeyboardHook;
        public bool IsKeyboardHook
        {
            get { return this._isKeyboardHook; }
            set
            {
                if (SetProperty(ref this._isKeyboardHook, value))
                {
                    if (this._isKeyboardHook) Hook();
                    else UnHook();
                }
            }
        }

        private string _message;
        public string Message
        {
            get { return this._message; }
            set { SetProperty(ref this._message, value); }
        }

        private void Hook()
        {
            this._keyboardHook.KeyDown += OnKeyDown;
            this._keyboardHook.KeyUp += OnKeyUp;
            this._keyboardHook.Hook();
        }

        private void UnHook()
        {
            this._keyboardHook.KeyDown -= OnKeyDown;
            this._keyboardHook.KeyUp -= OnKeyUp;
            this._keyboardHook.UnHook();
        }

        private void OnKeyDown(User32.VKs key)
        {
            this.Message = key.ToString() + " Down";
        }

        private void OnKeyUp(User32.VKs key)
        {
            this.Message = key.ToString() + " Up";
        }
    }
}
