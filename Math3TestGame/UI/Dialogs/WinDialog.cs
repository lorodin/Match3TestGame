using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.UI.Dialogs
{
    public class WinDialog : ADialog
    {
        private SimpleButton btnRestart;
        private SimpleButton btnMainMenu;

        public WinDialog(int x, int y, int width, int height):base(x, y, width, height)
        {
            Text = "You win!";
            Font = gc.DefaultFont24;
            TextColor = new Color(210, 26, 26);
            TextPosition = new Vector2(gc.Center.X - 55, y + 10);

            btnRestart = new SimpleButton(gc.DefaultFont12, "Restart", -60 + x + width / 2, y + height - gc.RegionHeight - 35, 120, 30, Color.White);
            btnRestart.TextPosition = new Vector2(btnRestart.Region.X + 31, btnRestart.Region.Y + 6);

            btnMainMenu = new SimpleButton(gc.DefaultFont12, "Main menu", -60 + x + width / 2, y + height - gc.RegionHeight, 120, 30, Color.White);
            btnMainMenu.TextPosition = new Vector2(btnMainMenu.Region.X + 15, btnMainMenu.Region.Y + 6);

            Controls.Add(btnRestart);
            Controls.Add(btnMainMenu);
        }

        public override PlayDialogResult MouseClick(int x, int y)
        {
            if (btnRestart.Region.Contains(x, y)) return PlayDialogResult.RESTART;
            if (btnMainMenu.Region.Contains(x, y)) return PlayDialogResult.MAIN_MENU;
            return PlayDialogResult.NONE;
        }

        public override void MouseMove(int x, int y)
        {
            foreach (var control in Controls)
            {
                if (control.Region.Contains(x, y))
                {
                    control.ButtonState = ButtonState.HOVER;
                }
                else
                {
                    control.ButtonState = ButtonState.NONE;
                }
            }
        }
    }
}
