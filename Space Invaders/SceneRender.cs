using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class SceneRender
    {
        int screenWidth;

        int screenHeight;

        char[,] screenMatrix;

        public SceneRender(GameSettings gameSettings)
        {
            screenWidth = gameSettings.ConsoleWidth;
            screenHeight = gameSettings.ConsoleHeight;
            screenMatrix = new char[gameSettings.ConsoleHeight, gameSettings.ConsoleWidth];

            Console.WindowHeight = gameSettings.ConsoleHeight;
            Console.WindowWidth = gameSettings.ConsoleWidth;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
        }

        public void Render(Scene scene)
        {
            ClearScreen();
            Console.SetCursorPosition(0, 0);
            
            AddListForRendering(scene.swarm);
            AddListForRendering(scene.ground);
            AddListForRendering(scene.playerShipMissile);

            AddGameObjectForRendering(scene.playerShip);

            StringBuilder stringBuilder = new StringBuilder();
                        
            for(int y = 0; y < screenHeight; y++)
            {
                for(int x = 0; x < screenWidth; x++)
                {
                    stringBuilder.Append(screenMatrix[y, x]);
                }
                stringBuilder.Append(Environment.NewLine);
            }

            Console.WriteLine(stringBuilder.ToString());
            Console.SetCursorPosition(0, 0);
        }

        public void AddGameObjectForRendering(GameObject gameObject)
        {
            if(gameObject.GameObjectPlace.YCoordinate < screenMatrix.GetLength(0) && gameObject.GameObjectPlace.XCoordinate < screenMatrix.GetLength(1))
            {
                screenMatrix[gameObject.GameObjectPlace.YCoordinate, gameObject.GameObjectPlace.XCoordinate] = gameObject.Figure;
            }
            else
            {
                screenMatrix[gameObject.GameObjectPlace.YCoordinate, gameObject.GameObjectPlace.XCoordinate] = '*';
            }
        }

        public void AddListForRendering(List<GameObject> gameObjects)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                AddGameObjectForRendering(gameObject);
            }
        }

        public void ClearScreen()
        {
           for(int y = 0; y < screenHeight; y++)
            {
                for(int x = 0; x < screenWidth; x++)
                {
                    screenMatrix[y, x] = ' ';
                }
           }
            Console.SetCursorPosition(0, 0);
        }

        public void RenderGameOver()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Game Over!!!!!!");

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
