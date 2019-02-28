using Math3TestGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Texture2D DefaultSpriteMap { get; set; }

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
                new Rectangle(1024, 1380, 256, 74),
                new Rectangle(1280, 1380, 256, 74),
                new Rectangle(1536, 1380, 256, 74),

            });

            textureRegions.Add(SpriteName.BangEffect, new[]
            {
                new Rectangle(0, 2404, 256, 256),
                new Rectangle(256, 2404, 256, 256),
                new Rectangle(512, 2404, 256, 256),
                new Rectangle(768, 2404, 256, 256),
                new Rectangle(1024, 2404, 256, 256),
                new Rectangle(1280, 2404, 256, 256),
                new Rectangle(1536, 2404, 256, 256),
                new Rectangle(1792, 2404, 256, 256)
            });

            textureRegions.Add(SpriteName.LineHEffect, new[]
            {
                new Rectangle(0, 2148, 256, 256),
                new Rectangle(256, 2148, 256, 256),
                new Rectangle(512, 2148, 256, 256),
                new Rectangle(768, 2148, 256, 256),
                new Rectangle(1024, 2148, 256, 256)
            });

            textureRegions.Add(SpriteName.LineVEffect, new[]
            {
                new Rectangle(1280, 2148, 256, 256),
                new Rectangle(1532, 2148, 256, 256),
                new Rectangle(1788, 2148, 256, 256),
                new Rectangle(2048, 2148, 256, 256),
                new Rectangle(2304, 2148, 256, 256)
            });

            textureRegions.Add(SpriteName.ToggleButton, new[]
            {
                new Rectangle(0, 2660, 256, 256),
                new Rectangle(256, 2660, 256, 256),
                new Rectangle(512, 2660, 256, 256),
                new Rectangle(768, 2660, 256, 256),
                new Rectangle(1024, 2660, 256, 256)
            });

            textureRegions.Add(SpriteName.SimpleButton, new[]
            {
                new Rectangle(768, 3172, 256, 72),
                new Rectangle(1024, 3172, 256, 72),
                new Rectangle(1280, 3172, 256, 72),
                new Rectangle(1536, 3172, 256, 72)
            });

            textureRegions.Add(SpriteName.DialogBackground, new[]
            {
                new Rectangle(0, 3172, 768, 512)
            });

            textureRegions.Add(SpriteName.Overllay, new[]
            {
                new Rectangle(0, 3684, 128, 128)
            });

            textureRegions.Add(SpriteName.Check, new[]
            {
                new Rectangle(129, 3684, 128, 128)
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
        None = 0,
        PlayButton = 1,
        GameObject1 = 2,
        GameObject2 = 3,
        GameObject3 = 4,
        GameObject4 = 5,
        GameObject5 = 6,
        Bang = 7,
        LineV = 8,
        LineH = 9,
        FireBall = 10,
        GameOverBG = 11,
        GameOverButton = 12,
        BangEffect = 13,
        LineVEffect = 14,
        LineHEffect = 15,
        ToggleButton = 16,
        SimpleButton = 17,
        DialogBackground = 18,
        Overllay = 19,
        Check = 20
    }
}
