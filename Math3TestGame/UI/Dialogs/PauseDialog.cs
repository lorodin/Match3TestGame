using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.UI.Dialogs
{
    public class PauseDialog : ADialog
    {
        private SimpleButton btnSound;

        private SimpleButton btnResume;
        private SimpleButton btnRestart;
        private SimpleButton btnMainMenu;
        private SimpleButton btnExit;

        private CheckBox chbPlayMusic; 

        public PauseDialog(int x, int y, int width, int height):base(x, y, width, height)
        {
            Text = "PAUSE";
            Font = gc.DefaultFont24;
            TextColor = new Color(36, 77, 17);
            TextPosition = new Vector2(gc.Center.X - 55, y + 10);


            chbPlayMusic = new CheckBox(gc.DefaultFont12, "Sound: ", -55 + x + width / 2, y + height - gc.RegionHeight - 140, 30, 30, new Color(36, 77, 17));

            btnResume = new SimpleButton(gc.DefaultFont12, "Resume", -60 + x + width / 2, y + height - gc.RegionHeight - 105, 120, 30, Color.White);
            btnResume.TextPosition = new Vector2(btnResume.Region.X + 30, btnResume.Region.Y + 6);
            
            btnRestart = new SimpleButton(gc.DefaultFont12, "Restart", -60 + x + width / 2, y + height - gc.RegionHeight - 70, 120, 30, Color.White);
            btnRestart.TextPosition = new Vector2(btnRestart.Region.X + 31, btnRestart.Region.Y + 6);

            btnMainMenu = new SimpleButton(gc.DefaultFont12, "Main menu", -60 + x + width / 2, y + height - gc.RegionHeight - 35, 120, 30, Color.White);
            btnMainMenu.TextPosition = new Vector2(btnMainMenu.Region.X + 15, btnMainMenu.Region.Y + 6);

            btnExit = new SimpleButton(gc.DefaultFont12, "Exit", -60 + x + width / 2, y + height - gc.RegionHeight, 120, 30, Color.White);
            btnExit.TextPosition = new Vector2(btnExit.Region.X + 46, btnExit.Region.Y + 6);


            Controls.Add(chbPlayMusic);
            Controls.Add(btnResume);
            Controls.Add(btnExit);
            Controls.Add(btnRestart);
            Controls.Add(btnMainMenu);
        }

        public override PlayDialogResult MouseClick(int x, int y)
        {
            if (btnRestart.Region.Contains(x, y)) return PlayDialogResult.RESTART;
            if (btnResume.Region.Contains(x, y)) return PlayDialogResult.RESUME;
            if (btnMainMenu.Region.Contains(x, y)) return PlayDialogResult.MAIN_MENU;
            if (btnExit.Region.Contains(x, y)) return PlayDialogResult.EXIT;
            if(chbPlayMusic.Region.Contains(x, y))
            {
                gc.SoundOn = !gc.SoundOn;
                chbPlayMusic.Image = gc.SoundOn ? Tools.SpriteName.Check : Tools.SpriteName.None;
            }
            return PlayDialogResult.NONE;
        }

        public override void MouseMove(int x, int y)
        {
            foreach(var control in Controls)
            {
                if(control.Region.Contains(x, y))
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
