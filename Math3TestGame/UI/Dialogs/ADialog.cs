using Math3TestGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.UI.Dialogs
{
    public abstract class ADialog : AUIControl
    {
        public List<AUIControl> Controls { get; private set; }
        public DialogState DialogState { get; private set; }

        public ADialog(int x, int y, int width, int height):base(x, y, width, height)
        {
            Controls = new List<AUIControl>();
            Background = SpriteName.DialogBackground;
            DialogState = DialogState.HIDE;
        }

        public void Show()
        {
            DialogState = DialogState.SHOW;
        }

        public void Hide()
        {
            DialogState = DialogState.HIDE;
        }

        public abstract void MouseMove(int x, int y);
        public abstract PlayDialogResult MouseClick(int x, int y);

        public override void Update(int dt)
        {
            foreach (var control in Controls) control.Update(dt);
        }
    }
    
    public enum DialogState
    {
        HIDE = 0,
        SHOW = 1,
    }

    public enum PlayDialogResult
    {
        NONE = 0,
        RESUME = 1,
        RESTART = 2,
        EXIT = 3,
        MAIN_MENU = 4
    }
}
