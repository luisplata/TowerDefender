using TowerDefender.Assets.Scripts.Utils;
using UnityEngine;

public class StatesOfGame : MonoBehaviour{
    [SerializeField] private BuildTerrain building;
    [SerializeField] private UI ui;
    [SerializeField] private bool isStartGame;
    TeaTime begingGame, playGame, pauseGame, gameOver;
    private void Start() {
        //PauseGame
        begingGame = this.tt().Pause().Add(()=>{

        }).Wait(()=>isStartGame, 0.1f).Add(()=>{
            building.StartSpawn();
        }).Add(()=>{
            playGame.Play();
        });

        playGame = this.tt().Pause().Add(()=>{
            building.PlayGame();
        }).Loop((TeaHandler t)=>{
            t.Wait(0.2f);
            if(!isStartGame || ServiceLocator.Instance.GetService<IGame>().IsGameOver()){
                t.Break();
            }
        }).Add(()=>{
            if(ServiceLocator.Instance.GetService<IGame>().IsGameOver()){
                gameOver.Play();
            }else{
                pauseGame.Play();
            }
        });

        pauseGame = this.tt().Pause().Add(()=>{
            building.PauseGame();
        }).Wait(()=>isStartGame,0.1f).Add(()=>{
            playGame.Restart();
        });

        gameOver = this.tt().Pause().Add(()=>{
            ui.GameOver();
            ServiceLocator.Instance.GetService<IGame>().ResetGame();
        });

        begingGame.Play();

        ui.OnClickInCard += ()=>{
            building.ShowPlaceEnables();
        };
    }
    public void StartGame(bool start){
        isStartGame = start;
        ui.ShowUi(!isStartGame);
    }
}