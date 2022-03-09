using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PjFather : MonoBehaviour
{
    private Queue<Node> path;
    private Node concurrentNode;
    private bool hasUse;
    private int count;
    [SerializeField] private float speed;
    private Path _shortestPath;

    public void Config(Path shortestPath)
    {
        _shortestPath = shortestPath;
        path = new Queue<Node>();
        foreach (var no in _shortestPath.nodes)
        {
            path.Enqueue(no);
        }

        count = _shortestPath.nodes.Count - 1;
        concurrentNode = _shortestPath.nodes[count];
        hasUse = true;
    }

    private void Update()
    {
        if (!hasUse) return;
        var positionFinal = concurrentNode.gameObject.transform.position;
        positionFinal.y += 1;
        transform.position = Vector3.MoveTowards(transform.position, positionFinal, speed * Time.deltaTime);
        //Debug.Log(Vector3.Distance(transform.position, concurrentNode.gameObject.transform.position));
        if (Vector3.Distance(transform.position, concurrentNode.gameObject.transform.position) <= 1.01 && count >0)
        {
            count--;
            concurrentNode = _shortestPath.nodes[count];
        }
    }
}
