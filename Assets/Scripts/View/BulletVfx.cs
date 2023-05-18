using System;
using UnityEngine;

public class BulletVfx : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private ProjectileMover projectileMover;
    private GameObject _targetForShoot;
    private float _damage;

    internal void Config(GameObject targetForShoot, float damage)
    {
        _targetForShoot = targetForShoot;
        _damage = damage;
        //Directional and velocity for shoot the target
        projectileMover.speed = bulletSpeed;
        projectileMover.Configure(_targetForShoot);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            if(other.gameObject.TryGetComponent<PjFather>(out var pj)){
                pj.ApplyDamange(_damage);
            }
            Debug.Log($"Damage: {_damage}");
        }
    }
}