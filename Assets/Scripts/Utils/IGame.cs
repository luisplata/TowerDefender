namespace TowerDefender.Assets.Scripts.Utils
{
    public interface IGame
    {
        void GameOver();
        bool IsGameOver();
        void ResetGame();
    }
}