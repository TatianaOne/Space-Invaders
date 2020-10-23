using SpaceInvaders.GameObjectFactories;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class GameEngine
    {
        private bool isNotOver;

        private static GameEngine gameEngine;

        private SceneRender sceneRender;

        private GameSettings gameSettings;

        private Scene scene;

        private GameEngine()
        {

        }

        public static GameEngine GetGameEngine(GameSettings gameSettings)
        {
            if (gameEngine == null)
            {
                gameEngine = new GameEngine(gameSettings);
            }
            return gameEngine;
        }

        private GameEngine(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
            isNotOver = true;
            scene = Scene.GetScene(gameSettings);
            sceneRender = new SceneRender(gameSettings);
        }

        public void Run()
        {
            int swarmMoveCounter = 0;
            int playerMissileCounter = 0;
            do
            {
                sceneRender.Render(scene);
                Thread.Sleep(gameSettings.GameSpeed);
                sceneRender.ClearScreen();

                if (swarmMoveCounter == gameSettings.SwarmSpeed)
                {
                    CalculateSwarmMove();
                    swarmMoveCounter = 0;
                }
                swarmMoveCounter++;

                if (playerMissileCounter == gameSettings.PlayerMissileSpeed)
                {
                    CalculateMissileMove();
                    playerMissileCounter = 0;
                }
                playerMissileCounter++;

            } while (isNotOver);

            Console.ForegroundColor = ConsoleColor.Red;
            sceneRender.RenderGameOver();
        }

        public void CalculateMovePlayerShipLeft()
        {
            if (scene.playerShip.GameObjectPlace.XCoordinate > 1)
            {
                scene.playerShip.GameObjectPlace.XCoordinate--;
            }
        }

        public void CalculateMovePlayerShipRight()
        {
            if (scene.playerShip.GameObjectPlace.YCoordinate < gameSettings.ConsoleWidth)
            {
                scene.playerShip.GameObjectPlace.XCoordinate++;
            }
        }

        public void CalculateSwarmMove()
        {
            for (int i = 0; i < scene.swarm.Count; i++)
            {
                GameObject alienShip = scene.swarm[i];

                alienShip.GameObjectPlace.YCoordinate++;

                if (alienShip.GameObjectPlace.YCoordinate == scene.playerShip.GameObjectPlace.YCoordinate)
                {
                    isNotOver = false;
                }
            }
        }

        public void Shot()
        {
            PlayerShipMissileFactory missileFactory = new PlayerShipMissileFactory(gameSettings);

            GameObject missile = missileFactory.GetGameObject(scene.playerShip.GameObjectPlace);


            scene.playerShipMissile.Add(missile);

            Console.Beep(1000, 200);
        }

        public void CalculateMissileMove()
        {
            if (scene.playerShipMissile.Count == 0)
            {
                return;
            }


            for (int x = 0; x < scene.playerShipMissile.Count; x++)
            {
                GameObject missile = scene.playerShipMissile[x];

                if (missile.GameObjectPlace.YCoordinate == 1)
                {
                    scene.playerShipMissile.RemoveAt(x);
                }


                missile.GameObjectPlace.YCoordinate--;

                for (int i = 0; i < scene.swarm.Count; i++)
                {

                    GameObject alienSip = scene.swarm[i];
                    if (missile.GameObjectPlace.Equals(alienSip.GameObjectPlace))
                    {
                        scene.swarm.RemoveAt(i);

                        scene.playerShipMissile.RemoveAt(x);

                        break;
                    }
                }
            }
        }
    }
}
