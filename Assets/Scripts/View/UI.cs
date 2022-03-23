using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> points;

    [SerializeField] private Carta carta;

    [SerializeField] private GameObject parent;
    //[SerializeField] private FactoryOfCarts factoryCarts;

    private void Start()
    {
        foreach (var point in points)
        {
            var cartaInstanciada = Instantiate(carta, parent.transform);
            cartaInstanciada.transform.position = point.transform.position;
        }
    }
}
