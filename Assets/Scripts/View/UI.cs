using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Carta[] cartas;

    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button buttonToRestart;

    public void ShowUi(bool isStartGame)
    {
        parent.SetActive(isStartGame);
    }

    public Action OnClickInCard;

    private void Start()
    {
        foreach(var carta in cartas){
            var cartaInstanciada = Instantiate(carta, parent.transform);
            cartaInstanciada.Config();
            cartaInstanciada.onClick = ()=>{
                OnClickInCard?.Invoke();
            };
        }
        buttonToRestart.onClick.AddListener(()=>{
            SceneManager.LoadScene(0);
        });
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }
}
