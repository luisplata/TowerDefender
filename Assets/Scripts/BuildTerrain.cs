using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildTerrain : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private int countEnemies;
    [SerializeField] private ObjetoInteractuable cube;
    [SerializeField] private ObjetoInteractuable tower;
    [SerializeField] private ObjetoInteractuable portalEnemies;
    [SerializeField] private Camera camera;
    [SerializeField] private TeoriaDeGrafos grafos;
    [SerializeField] private Graph graphFind;
    [SerializeField] private Material materialToRoad;
    private bool hasSpawnTower;
    //private List<ObjetoInteractuable> objetoInteractuable;
    private List<ObjetoInteractuable> portalList;
    private ObjetoInteractuable[,] portalListarray;
    private Dictionary<int, List<int>> espaciosUsados;
    private void Start()
    {
        //objetoInteractuable = new List<ObjetoInteractuable>();
        portalList = new List<ObjetoInteractuable>();
        espaciosUsados = new Dictionary<int, List<int>>();
        portalListarray = new ObjetoInteractuable[size,size];
        GenerateMapAlgoritm();
    }

    public void Restart()
    {
        foreach (var ofObject in portalListarray.Cast<ObjetoInteractuable>().ToList())
        {
            ofObject.DestroyAll();
        }
        //objetoInteractuable = new List<ObjetoInteractuable>();
        portalList = new List<ObjetoInteractuable>();
        espaciosUsados = new Dictionary<int, List<int>>();
        portalListarray = new ObjetoInteractuable[size, size];
        GenerateMapAlgoritm();
    }

    private void GenerateMapAlgoritm()
    {
        var positionLocalInX = size / 2;
        var positionLocalInZ = size / 2;
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var instantiate = Instantiate(cube, transform);
                instantiate.transform.position = transform.position;
                instantiate.transform.position += new Vector3(i - positionLocalInX, transform.position.y, j - positionLocalInZ);
                instantiate.Config(grafos);
                //objetoInteractuable.Add(instantiate);
                portalListarray[i, j] = instantiate;
            }
        }

        var finalDelCamino = SpawnObject(tower);

        //spawn Enemies
        for (int i = 0; i < countEnemies; i++)
        {
            portalList.Add(SpawnObject(portalEnemies));
        }
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j > 0)
                {
                    portalListarray[i, j].aristas.Add(portalListarray[i, j - 1]);
                    portalListarray[i, j].GetNode().connections.Add(portalListarray[i, j - 1].GetNode());
                }

                if (i > 0)
                {
                    portalListarray[i, j].GetNode().connections.Add(portalListarray[i - 1, j].GetNode());
                    portalListarray[i, j].aristas.Add(portalListarray[i - 1, j]);
                }

                if (j + 1 < size)
                {
                    portalListarray[i, j].aristas.Add(portalListarray[i, j + 1]);
                    portalListarray[i, j].GetNode().connections.Add(portalListarray[i, j + 1].GetNode());
                }

                if (i + 1 < size)
                {
                    portalListarray[i, j].GetNode().connections.Add(portalListarray[i + 1, j].GetNode());
                    portalListarray[i, j].aristas.Add(portalListarray[i + 1, j]);
                }
            }
        }

        var nodelist = portalListarray.Cast<ObjetoInteractuable>().ToList().Select(p => p.GetNode()).ToList();
        foreach (var final in portalList)
        {
            var shortestPath = graphFind.GetShortestPath(nodelist, finalDelCamino.GetNode(), final.GetNode());
            final.ListOfPath(shortestPath);
            foreach (var node in shortestPath.nodes)
            {
                node.GetObjetoInteractuable().ChangeMaterial(materialToRoad);
            }

            final.StartSpawn();
        }
        /*
        grafos.Config(portalListarray.Cast<ObjetoInteractuable>().ToList(), materialToRoad);

        foreach (var interactuable in portalList)
        {
            Debug.Log(grafos.FuncionRecursivaParaEncontrarFinal(1,interactuable));
        }*/
    }

    private ObjetoInteractuable SpawnObject(ObjetoInteractuable spawnObject)
    {
        int positionInList1;
        int positionInList2;
        List<int> positionInListList;
        do
        {
            positionInList1 = Random.Range(0, size);
        } while (espaciosUsados.ContainsKey(positionInList1));
        
        if(!espaciosUsados.TryGetValue(positionInList1,out positionInListList))
        {
            positionInListList = new List<int>();
        }
        
        do
        {
            positionInList2 = Random.Range(0, size);
        } while (positionInListList.Contains(positionInList2));
        
        espaciosUsados.Add(positionInList1, positionInListList);
        var listOfObject = portalListarray[positionInList1, positionInList2];
        var objectInstantiate = Instantiate(spawnObject, transform);
        objectInstantiate.transform.position = listOfObject.transform.position;
        portalListarray[positionInList1, positionInList2] = objectInstantiate;
        Destroy(listOfObject.gameObject);
        return objectInstantiate;
    }

    private void Update()
    {
        camera.transform.LookAt(transform.position);
    }
}
