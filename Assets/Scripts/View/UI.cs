using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Carta[] cartas;

    [SerializeField] private GameObject parent;

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
    }
}
