using Math3TestGame.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class TextureHelper
    {
        private static TextureHelper instance;

        private Dictionary<SpriteName, Rectangle[]> textureRegions;

        public static TextureHelper GetInstance()
        {
            if (instance == null) instance = new TextureHelper();
            return instance;
        }

        private TextureHelper()
        {
            textureRegions = new Dictionary<SpriteName, Rectangle[]>();

            textureRegions.Add(SpriteName.PlayButton, new[] {
                new Rectangle(0, 0, 410, 100),
                new Rectangle(408, 0, 410, 100),
                new Rectangle(816, 0, 410, 100),
                new Rectangle(1224, 0, 410, 100),
                new Rectangle(1632, 0, 410, 100)
            });

            textureRegions.Add(SpriteName.GameObject1, new[]
            {
                new Rectangle(0, 100, 256, 256),
                new Rectangle(256, 100, 256, 256),
                new Rectangle(512, 100, 256, 256),
                new Rectangle(768, 100, 256, 256),
                new Rectangle(1024, 100, 256, 256),
                new Rectangle(1280, 100, 256, 256),
                new Rectangle(1536, 100, 256, 256),
                new Rectangle(2048, 100, 256, 256),
                new Rectangle(2304, 100, 256, 256)
            });

            textureRegions.Add(SpriteName.GameObject2, new[]
                        {
                    new Rectangle(0, 356, 256, 256),
                    new Rectangle(256, 356, 256, 256),
                    new Rectangle(512, 356, 256, 256),
                    new Rectangle(768, 356, 256, 256),
                    new Rectangle(1024, 356, 256, 256),
                    new Rectangle(1280, 356, 256, 256),
                    new Rectangle(1536, 356, 256, 256),
                    new Rectangle(2048, 356, 256, 256),
                    new Rectangle(2304, 356, 256, 256)
            });

            textureRegions.Add(SpriteName.GameObject3, new[]
                        {
                    new Rectangle(0, 612, 256, 256),
                    new Rectangle(256, 612, 256, 256),
                    new Rectangle(512, 612, 256, 256),
                    new Rectangle(768, 612, 256, 256),
                    new Rectangle(1024, 612, 256, 256),
                    new Rectangle(1280, 612, 256, 256),
                    new Rectangle(1536, 612, 256, 256),
                    new Rectangle(2048, 612, 256, 256),
                    new Rectangle(2304, 612, 256, 256)
            });

            textureRegions.Add(SpriteName.GameObject4, new[]
                        {
                    new Rectangle(0, 868, 256, 256),
                    new Rectangle(256, 868, 256, 256),
                    new Rectangle(512, 868, 256, 256),
                    new Rectangle(768, 868, 256, 256),
                    new Rectangle(1024, 868, 256, 256),
                    new Rectangle(1280, 868, 256, 256),
                    new Rectangle(1536, 868, 256, 256),
                    new Rectangle(2048, 868, 256, 256),
                    new Rectangle(2304, 868, 256, 256)
            });

            textureRegions.Add(SpriteName.GameObject5, new[]
            {
                    new Rectangle(0, 1124, 256, 256),
                    new Rectangle(256, 1124, 256, 256),
                    new Rectangle(512, 1124, 256, 256),
                    new Rectangle(768, 1124, 256, 256),
                    new Rectangle(1024, 1124, 256, 256),
                    new Rectangle(1280, 1124, 256, 256),
                    new Rectangle(1536, 1124, 256, 256),
                    new Rectangle(2048, 1124, 256, 256),
                    new Rectangle(2304, 1124, 256, 256)
            });

            textureRegions.Add(SpriteName.Bang, new[]
            {
                new Rectangle(0, 1893, 256, 256)
            });

            textureRegions.Add(SpriteName.LineV, new[]
            {
                new Rectangle(0, 1636, 256, 256)
            });

            textureRegions.Add(SpriteName.LineH, new[]
            {
                new Rectangle(0, 1380, 256, 256)
            });

            textureRegions.Add(SpriteName.FireBall, new[]
            {
                new Rectangle(0, 2148, 256, 256),
                new Rectangle(256, 2148, 256, 256),
                new Rectangle(516, 2148, 256, 256),
                new Rectangle(768, 2148, 256, 256),
                new Rectangle(1024, 2148, 256, 256),
                new Rectangle(1280, 2148, 256, 256),
            });

            textureRegions.Add(SpriteName.GameOverBG, new[]
            {
                new Rectangle(256, 1380, 768, 512)
            });

            textureRegions.Add(SpriteName.GameOverButton, new[]
            {
                new Rectangle(1024, 1380, 256, 74)
            });
        }



        public Rectangle GetTextureRegion(SpriteName name, int step)
        {
            if (!textureRegions.ContainsKey(name)) throw new ArgumentException();
            if (textureRegions[name].Length < step || step < 0) throw new ArgumentException();
            return textureRegions[name][step];
        }
    }

    public enum SpriteName
    {
        PlayButton = 0,
        GameObject1 = 1,
        GameObject2 = 2,
        GameObject3 = 3,
        GameObject4 = 4,
        GameObject5 = 5,
        Bang = 6,
        LineV = 7,
        LineH = 8,
        FireBall = 9,
        GameOverBG = 10,
        GameOverButton = 11
    }
}
