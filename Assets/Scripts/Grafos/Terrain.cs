using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Terrain
{
    private readonly ITerrain _terrain;
    private readonly int _size;
    private bool hasSpawnTower;
    private List<PortalEnemy> portalList;
    private ObjetoInteractuable[,] portalListarray;
    private Dictionary<int, List<int>> espaciosUsados;
    private int countEnemies;
    private readonly Graph graphFind;

    public Terrain(ITerrain terrain,int size, int i)
    {
        _terrain = terrain;
        _size = size;
        countEnemies = i;
        //materialToRoad = mat;
        portalList = new List<PortalEnemy>();
        espaciosUsados = new Dictionary<int, List<int>>();
        portalListarray = new ObjetoInteractuable[size,size];
        graphFind = new Graph();
        GenerateMapAlgoritm();
        
        //StartGame();
    }
    
    private void GenerateMapAlgoritm()
    {
        var positionLocalInX = _size / 2;
        var positionLocalInZ = _size / 2;
        
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                var instantiate = _terrain.InstantiateObjectInPosition("cube",_terrain.GetVector(i - positionLocalInX, _terrain.Position().y, j - positionLocalInZ));
                instantiate.Config();
                portalListarray[i, j] = instantiate;
            }
        }

        var finalDelCamino = (Tower)SpawnObject("tower");

        _terrain.AddingToCameraAtPlayer(finalDelCamino);

        //spawn portal Enemies
        for (int i = 0; i < countEnemies; i++)
        {
            portalList.Add((PortalEnemy)SpawnObject("portalEnemies"));
        }
        
        //adding conextions in nodes
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                if (j > 0)
                {
                    portalListarray[i, j].Aristas.Add(portalListarray[i, j - 1]);
                    portalListarray[i, j].GetNode().GetConnections().Add(portalListarray[i, j - 1].GetNode());
                }

                if (i > 0)
                {
                    portalListarray[i, j].GetNode().GetConnections().Add(portalListarray[i - 1, j].GetNode());
                    portalListarray[i, j].Aristas.Add(portalListarray[i - 1, j]);
                }

                if (j + 1 < _size)
                {
                    portalListarray[i, j].Aristas.Add(portalListarray[i, j + 1]);
                    portalListarray[i, j].GetNode().GetConnections().Add(portalListarray[i, j + 1].GetNode());
                }

                if (i + 1 < _size)
                {
                    portalListarray[i, j].GetNode().GetConnections().Add(portalListarray[i + 1, j].GetNode());
                    portalListarray[i, j].Aristas.Add(portalListarray[i + 1, j]);
                }
            }
        }

        List<INodeCustom> nodelist = portalListarray.Cast<ObjetoInteractuable>().ToList().Select(p => p.GetNode()).ToList();
        foreach (var final in portalList)
        {
            var shortestPath = graphFind.GetShortestPath(nodelist, finalDelCamino.GetNode(), final.GetNode());
            final.ListOfPath(shortestPath);
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    foreach (var node in shortestPath.Nodes())
                    {
                        if (portalListarray[i, j].GetNode() == node &&  portalListarray[i, j].GetType() != typeof(Tower) && portalListarray[i, j].GetType()!=typeof(PortalEnemy))
                        {
                            portalListarray[i, j].gameObject.SetActive(false);
                            //Debug.Log($"This portalListarray[i, j] is equals to node {portalListarray[i, j].GetNode() == node}");
                            var instantiate = _terrain.InstantiateObjectInPosition("road",_terrain.GetVector(i - positionLocalInX, _terrain.Position().y, j - positionLocalInZ));
                            instantiate.Config();
                            portalListarray[i, j] = instantiate;
                        }
                    }
                }
            }
        }
    }
    
    public void Restart()
    {
        foreach (var ofObject in portalListarray.Cast<ObjetoInteractuable>().ToList())
        {
            ofObject.DestroyAll();
        }
        portalList = new List<PortalEnemy>();
        espaciosUsados = new Dictionary<int, List<int>>();
        portalListarray = new ObjetoInteractuable[_size, _size];
        GenerateMapAlgoritm();
        //StartGame();
    }
    
    private ObjetoInteractuable SpawnObject(string name)
    {
        int positionInList1;
        int positionInList2;
        List<int> positionInListList;
        do
        {
            positionInList1 = _terrain.Random_Range(0, _size);
        } while (espaciosUsados.ContainsKey(positionInList1));
        
        if(!espaciosUsados.TryGetValue(positionInList1,out positionInListList))
        {
            positionInListList = new List<int>();
        }
        
        do
        {
            positionInList2 = _terrain.Random_Range(0, _size);
        } while (positionInListList.Contains(positionInList2));
        
        espaciosUsados.Add(positionInList1, positionInListList);
        var listOfObject = portalListarray[positionInList1, positionInList2];
        var objectInstantiate = _terrain.InstantiateObjectInPosition(name, listOfObject.transform.position);
        objectInstantiate.Config();
        portalListarray[positionInList1, positionInList2] = objectInstantiate;
        _terrain.DestroyElement(listOfObject.gameObject);
        return objectInstantiate;
    }

    public void StartGame()
    {
        foreach (var final in portalList)
        {
            final.StartSpawn();
        }
    }

    internal void PauseGame()
    {
        foreach (var final in portalList)
        {
            final.PauseSpawn();
        }
    }

    internal void PlayGame()
    {
        foreach (var final in portalList)
        {
            final.PlaySpawn();
        }
    }
}