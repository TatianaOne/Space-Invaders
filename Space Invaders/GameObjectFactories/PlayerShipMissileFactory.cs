using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjectFactories
{
    class PlayerShipMissileFactory : GameObjectFactory
    {
        public PlayerShipMissileFactory(GameSettings gameSettings) : base(gameSettings)
        {

        }

        public override GameObject GetGameObject(GameObjectPlace objectPlace)
        {
            GameObject missilePlace = new PlayerShipMissile() { Figure = GameSettings.PlayerMissile, GameObjectPlace = objectPlace, GameObjectType = GameObjectType.PlayerShipMissile };

            return missilePlace;
        }
    }
}
