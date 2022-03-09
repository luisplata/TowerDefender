using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeoriaDeGrafos : MonoBehaviour
{
    [Header("Vertice Final")]
    [ContextMenuItem("Saca de los vertices actuales uno al azar", "GenerarVerticeFinal")]
    public ObjetoInteractuable verticeFinal;
    public List<ObjetoInteractuable> vertices;
    private List<ObjetoInteractuable> verticesVisitados;
    [Header("Distancias de los vertices")]
    [ContextMenuItem("Ejecutara una funcion recursiva para crear las distancias entre los verices", "GenerarMapaDeDialogosDelGrafo")]
    public ObjetoInteractuable distanciaDesdeAqui;
    private Dictionary<ObjetoInteractuable, int> distanciasPorVertice;
    [SerializeField]
    private int distanciaMaxima;

    private Material materialToRoad;

    private List<ObjetoInteractuable> interactuableObject;

    //Generamos el verice final a partir de los vertices seleccionados

    public void GenerarVerticeFinal()
    {
        foreach (var interactuable in interactuableObject)
        {
            if (interactuable.IsFinal)
            {
                verticeFinal = interactuable;
                break;
            }
        }
    }

    public void Config(List<ObjetoInteractuable> interactuables, Material materialToRoad)
    {
        this.materialToRoad = materialToRoad;
        interactuableObject = interactuables;
        GenerarVerticeFinal();
    }

    public List<string> DialogosDeEsteObjeto(ObjetoInteractuable origen)
    {
        var caminos = new Dictionary<ObjetoInteractuable, int>();
        foreach(ObjetoInteractuable o in origen.aristas)
        {
            caminos.Add(o, BFS(o));
        }
        //decidir cual es el mas corto y el largo de los caminos para sacar sus dialogos
        var myList = caminos.ToList();
        
        myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        
        var dialogos = new List<string>();
        //TODO se debe colocar random el orden de la escritura de dialogos, porque siempre el primero es el correcto.
        foreach(KeyValuePair<ObjetoInteractuable, int> l in myList)
        {
            if (l.Value.Equals(myList[0].Value))
            {
                dialogos.Add(l.Key.dialogoBueno);
            }
            else
            {
                dialogos.Add(l.Key.dialogoMalo);
            }
        }
        return dialogos;
    }

    /// <summary>
    /// Deprecada
    /// </summary>
    /// <param name="distancia"></param>
    /// <param name="verticeIndividual"></param>
    /// <returns></returns>
    public int FuncionRecursivaParaEncontrarFinal(int distancia, ObjetoInteractuable verticeIndividual)
    {
        if (verticeIndividual.Equals(verticeFinal))
        {
            if(distancia < distanciaMaxima)
            {
                distanciaMaxima = distancia;
            }
            else
            {
                //Debug.LogWarning(">>>>>> esporque es el final");
                return distancia;
            }
        }

        if (verticesVisitados.Contains(verticeIndividual) && verticeIndividual.distancia < distancia)
        {
            //Debug.LogWarning("Es porque ya a sido visitado "+ verticeIndividual.gameObject.name +" pero distancia recorrida hasta el fue de: "+verticeIndividual.distancia+" y la recorrida hsata ahora es de "+distancia );
            return distancia;
        }
        else
        {
            verticesVisitados.Add(verticeIndividual);
            verticeIndividual.distancia = distancia;
            ////Todo esta bien
            distancia++;
            foreach (ObjetoInteractuable arista in verticeIndividual.aristas)
            {
                //Debug.LogWarning("El objeto "+verticeIndividual.gameObject.name+" visita a: " + arista.gameObject.name + " distancia hasta aqui es: " + distancia);
                distancia = FuncionRecursivaParaEncontrarFinal(distancia, arista);
            }
            return distancia;
        }
    }


    public int BFS(ObjetoInteractuable origen)
    {
        var cola = new Queue<ObjetoInteractuable>();
        cola.Enqueue(origen);
        origen.visitado = true;
        var i = 0;
        while (cola.Count > 0)
        {
            var v = cola.Dequeue();
            //si es el final return
            if (v == verticeFinal)
            {
                break;
            }
            i++;
            foreach (var w in v.aristas)
            {
                if (!w.visitado)
                {
                    w.visitado = true;
                    w.distancia = i;
                    cola.Enqueue(w);
                }
                else if (w.distancia < i)
                {
                    i = w.distancia;
                }
                else if (w == verticeFinal)
                {
                    i = w.distancia;
                }
                else
                {
                    break;
                }
            }
        }
        return i;
    }
}
