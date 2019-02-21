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
        

        public AGameObject GetGameObject(AGameObject cloned, LineType type)
        {
            return new LineGameObject(cloned, type);
        }

        public AGameObject GetGameObject(AGameObject gameObject, BonusEffect bonus = BonusEffect.NONE)
        {
            if (bonus == BonusEffect.BANG) return new BangGameObject(gameObject);
            return new SimpleGameObject(gameObject, RandomSpriteName());
        }

        public AGameObject GetGameObject(int x, int y, GameMatrix parent)
        {
            return new SimpleGameObject(new Rectangle(x, y, 256, 256), RandomSpriteName(), parent);
        }

        public AGameObject GetGameObject(Rectangle region, GameMatrix parent, AGameObject left = null, AGameObject right = null, AGameObject top = null, AGameObject bottom = null)
        {
            return new SimpleGameObject(region, RandomSpriteName(), parent, left, right, top, bottom);
        }
        

        public SpriteName RandomSpriteName()
        {
            switch (rnd.Next(1, 6))
            {
                case 1: return SpriteName.GameObject1;
                case 2: return SpriteName.GameObject2;
                case 3: return SpriteName.GameObject3;
                case 4: return SpriteName.GameObject4;
            }

            return SpriteName.GameObject5;
        }
    }
}
