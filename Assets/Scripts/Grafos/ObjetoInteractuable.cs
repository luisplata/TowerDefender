﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoInteractuable : MonoBehaviour
{
    [SerializeField] private bool isFinal;
    [SerializeField] private bool isPortal;
    [SerializeField] private PjFather pj;
    
    private List<ObjetoInteractuable> aristas;
    public int distancia;
    private bool visitado;
    private List<PjFather> pjs;
    private Path _shortestPath;

    public bool IsFinal => isFinal;
    public bool IsPortal => isPortal;

    public List<ObjetoInteractuable> Aristas => aristas;

    public void Config()
    {
        aristas = new List<ObjetoInteractuable>();
    }

    public void ChangeMaterial(Material materialToRoad)
    {
        if(isFinal || IsPortal) return;
        gameObject.GetComponent<MeshRenderer>().material = materialToRoad;
    }

    public Node GetNode()
    {
        return GetComponent<Node>();
    }

    public void ListOfPath(Path shortestPath)
    {
        _shortestPath = shortestPath;
        pjs = new List<PjFather>();
    }

    public void StartSpawn()
    {
        var pjFather = Instantiate(pj);
        var positionInPj = transform.position;
        positionInPj.y += 1;
        pjFather.transform.position = positionInPj;
        pjFather.Config(_shortestPath);
        pjs.Add(pjFather);
    }

    public void DestroyAll()
    {
        if(IsPortal)
        {
            foreach (var pjFather in pjs)
            {
                Destroy(pjFather.gameObject);
            } 
        }
        Destroy(gameObject);
    }
}