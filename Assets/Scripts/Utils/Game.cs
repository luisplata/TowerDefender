using UnityEngine;

namespace TowerDefender.Assets.Scripts.Utils
{
    public class Game : MonoBehaviour, IGame
    {
        private bool isGameOver;

        public void GameOver()
        {
            isGameOver = true;
        }

        public bool IsGameOver()
        {
            return isGameOver;
        }

        public void ResetGame()
        {
            isGameOver = false;
        }
    }
}