using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Math3TestGame
{
    public class GameConfigs
    {
        private static GameConfigs instance;

        public SpriteFont DefaultFont12 { get; set; }
        public SpriteFont DefaultFont18 { get; set; }
        public SpriteFont DefaultFont24 { get; set; }

        public Point Center { get; set; }

        private int width = Int32.MaxValue;
        private int height = Int32.MaxValue;
        private int min = Int32.MaxValue;

        private int regionCounts = 10;
        
        public int RegionWidth { get; set; }
        public int RegionHeight { get; set; }

        public int MaxPoints { get; set; } = 1200;

        public int ADTime { get; } = 10;

        public int BOMB_TIME { get; } = 250;

        public float DefaultSpeed = 0.5f;

        public int GameTime { get; private set; } = 1200;

        public GameType GameType { get; set; } = GameType.G8x8;
        
        public Game CurrentGame { get; set; }

        public bool SoundOn
        {
            get
            {
                return MediaPlayer.IsMuted;
            }
            set
            {
                MediaPlayer.IsMuted = value;
            }
        }

        public int Width {
            get {
                return width;
            }
            set
            {
                width = value;
                min = Math.Min(width, height);
                RegionWidth = min / regionCounts;
                RegionHeight = min / regionCounts;
            }
        }
        public int Height {
            get
            {
                return height;
            }
            set
            {
                height = value;
                min = Math.Min(width, height);
                RegionWidth = min / regionCounts;
                RegionHeight = min / regionCounts;
            }
        }

        public static GameConfigs GetInstance()
        {
            if (instance == null) instance = new GameConfigs();
            return instance;
        }

        public Point GetIndexes(int x, int y)
        {
            int i = (x - Center.X + min / 2) / RegionWidth;
            int j = (y - Center.Y + min / 2) / RegionHeight;
            return new Point(i , j);
        }

        public Point GetRealPoint(float i, float j)
        {
            int x = (int)Math.Round((Center.X - min / 2) + i * RegionWidth);
            int y = (int)Math.Round((Center.Y - min / 2) + j * RegionHeight);
            return new Point(x, y);
        }

        private GameConfigs()
        {
            SoundOn = false;
        }
    }

    public enum GameType
    {
        G8x8 = 0,
        G6x6 = 1,
        G8x9 = 2,
        G6x8 = 3
    }
}
