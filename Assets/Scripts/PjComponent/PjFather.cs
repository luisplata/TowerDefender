using System;
using PjComponent;
using UnityEngine;

public class PjFather : MonoBehaviour, IPj
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceMax;
    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private Animator anim;
    public Action<GameObject> onDestroyMoster;
    public float Damage => damage;
    private Pj _pj;

    public void Config(Path shortestPath)
    {
        _pj = new Pj(shortestPath, speed, this, life);
    }

    private void Update()
    {
        if(!_pj.CanMove()) return;
        _pj.Action();
    }

    public void Move(Vector3 position, Vector3 positionFinal, float speed)
    {
        transform.position = Vector3.MoveTowards(position, positionFinal, speed * Time.deltaTime);
        Vector3 direction = positionFinal - transform.position;
        direction.y = 0f; // Set the Y-component to 0 to ignore rotation on other axes
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim?.SetBool("move", true);
        anim?.SetFloat("velocity", speed);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsClose(Vector3 pointA, Vector3 pointB)
    {
        return Vector3.Distance(pointA, pointB) <= distanceMax;
    }

    public void ApplyDamange(float damage)
    {
        _pj.ApplyDamange(damage);
    }

    public void DestroyMoster()
    {
        anim?.SetBool("move", false);
        anim?.SetTrigger("died");
        onDestroyMoster?.Invoke(gameObject);
        Destroy(gameObject, 1);
    }
}