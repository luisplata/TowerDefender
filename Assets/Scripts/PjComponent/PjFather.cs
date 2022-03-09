using PjComponent;
using UnityEngine;

public class PjFather : MonoBehaviour, IPj
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceMax;
    private Pj _pj;

    public void Config(Path shortestPath)
    {
        _pj = new Pj(shortestPath, speed, this);
    }

    private void Update()
    {
        _pj.Action();
    }

    public void Move(Vector3 position, Vector3 positionFinal, float speed)
    {
        transform.position = Vector3.MoveTowards(position, positionFinal, speed * Time.deltaTime);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsClose(Vector3 pointA, Vector3 pointB)
    {
        return Vector3.Distance(pointA, pointB) <= distanceMax;
    }
}