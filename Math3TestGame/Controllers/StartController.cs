using Math3TestGame.Models;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Renders;
using Math3TestGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Controllers
{
    public class StartController:Controller
    {
        private PlayButton playButton;
        
        private GameConfigs gc;

        private List<ToggleButton> tbtns = new List<ToggleButton>();

        private List<AUIControl> controls = new List<AUIControl>();

        public StartController() : base(ControllerNames.Start)
        {
            gc = GameConfigs.GetInstance();

            playButton = new PlayButton(gc.Center.X - 101, gc.Center.Y + gc.Center.Y / 2);
            
            tbtns.Add(new ToggleButton(GameType.G8x8, gc.Center.X - 7 * gc.RegionWidth / 2, gc.Center.Y + 00, gc.RegionWidth, gc.RegionHeight));
            tbtns.Add(new ToggleButton(GameType.G6x6, gc.Center.X - 3 * gc.RegionWidth / 2, gc.Center.Y + 00, gc.RegionWidth, gc.RegionHeight));
            tbtns.Add(new ToggleButton(GameType.G8x9, gc.Center.X + 1 * gc.RegionWidth / 2, gc.Center.Y + 00, gc.RegionWidth, gc.RegionHeight));
            tbtns.Add(new ToggleButton(GameType.G6x8, gc.Center.X + 5 * gc.RegionWidth / 2, gc.Center.Y + 00, gc.RegionWidth, gc.RegionHeight));

            foreach (var t in tbtns)
                if (gc.GameType == t.GameType)
                    t.ToggleState = ToggleButtonState.ON;


            List<Label> labels = new List<Label>();

            controls.Add(new Label(gc.DefaultFont18, 
                                    "Field type", 
                                    gc.Center.X - 2 * gc.RegionWidth / 2, 
                                    gc.Center.Y - 45));

            controls.Add(new Label(gc.DefaultFont24, "MATCH 3 Test game", 
                                    -10 + gc.Center.X - 3 * gc.RegionWidth, 
                                    gc.Center.Y - gc.Center.Y + 10, 
                                    new Color(36, 77, 17)));

            controls.Add(new Label(gc.DefaultFont12, 
                                        "You must score 500 points by moving adjacent \r\n          pieces.You have one minute. Let's go!",
                                        5 + gc.Center.X - 4 * gc.RegionWidth, gc.Center.Y - 150));



            controls.Add(playButton);

            controls.AddRange(tbtns);



            renderer = new StartRenderer(controls);
        }
        
        public override void MouseClick(int x, int y)
        {
            if (playButton.Region.Contains(x, y))
            {
                ChangeController(ControllerNames.Play);
                return;
            }

            foreach(var toggle in tbtns)
            {
                if (!toggle.Region.Contains(x, y)) continue;

                tbtns.Find(t => t.ToggleState == ToggleButtonState.ON).ToggleState = ToggleButtonState.OFF;
                toggle.ToggleState = ToggleButtonState.ON;
                gc.GameType = toggle.GameType;

                break;
            }
        }

        public override void MouseMove(int x, int y)
        {
            if(playButton.Region.Contains(x, y))
            {
                playButton.PlayButtonState = ButtonState.HOVER;
            }
            else
            {
                playButton.PlayButtonState = ButtonState.NONE;
            }

            foreach(var toggle in tbtns)
            {
                if(toggle.Region.Contains(x, y))
                {
                    toggle.ButtonState = ButtonState.HOVER;
                }
                else
                {
                    toggle.ButtonState = ButtonState.NONE;
                }
            }
        }

        public override void Update(int dt)
        {
            foreach (var control in controls) control.Update(dt);
        }
    }
}
