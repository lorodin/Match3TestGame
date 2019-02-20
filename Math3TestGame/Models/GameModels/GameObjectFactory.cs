using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.GameModels
{
    public class GameObjectFactory
    {
        private static GameObjectFactory instance;

        private Random rnd;

        public static GameObjectFactory GetInstance()
        {
            if (instance == null) instance = new GameObjectFactory();
            return instance;
        }

        private GameObjectFactory()
        {
            rnd = new Random();
        }

        public GameObject GetGameObject(GameObject gameObject)
        {
            return new GameObject(gameObject, RandomSpriteName());
        }

        public GameObject GetGameObject(int x, int y)
        {
            return new GameObject(new Rectangle(x, y, 256, 256), RandomSpriteName());
        }

        public GameObject GetGameObject(Rectangle region, GameObject left = null, GameObject right = null, GameObject top = null, GameObject bottom = null)
        {
            return new GameObject(region, RandomSpriteName(), left, right, top, bottom);
        }
        

        public SpriteName RandomSpriteName()
        {
            switch (rnd.Next(1, 6))
            {
                case 1: return SpriteName.GameObject1;
                case 2: return SpriteName.GameObject2;
                case 3: return SpriteName.GameObject3;
                //case 4: return SpriteName.GameObject4;
            }

            return SpriteName.GameObject5;
        }
    }
}
