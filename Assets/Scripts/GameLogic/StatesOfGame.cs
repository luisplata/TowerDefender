using UnityEngine;

public class StatesOfGame : MonoBehaviour{
    [SerializeField] private BuildTerrain building;
    [SerializeField] private UI ui;
    [SerializeField] private bool isStartGame;
    TeaTime begingGame, playGame, pauseGame;
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
            if(!isStartGame){
                t.Break();
            }
        }).Add(()=>{
            pauseGame.Play();
        });

        pauseGame = this.tt().Pause().Add(()=>{
            building.PauseGame();
        }).Wait(()=>isStartGame,0.1f).Add(()=>{
            playGame.Restart();
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