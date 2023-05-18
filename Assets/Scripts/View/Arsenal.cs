using System;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour {
    //Rango de ataque
    //Poder de ataque
    //Cooldown
    [SerializeField] private SphereCollider collider;
    [SerializeField] private float rangeOfCollider;
    [SerializeField] private float power;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject model3D;
    [SerializeField] private GameObject pointToShoot;
    [SerializeField] private BulletVfx bulletVfx;

    private List<GameObject> targets;
    private GameObject targetForShoot;

    private float deltaTimeLocal;

    private void Start() {
        collider.center = new Vector3(0,0.5f,0);
        collider.radius = rangeOfCollider;
        targets = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<PjFather>().onDestroyMoster += (moster)=>{
                targets.Remove(moster);
            };
            targets.Add(other.gameObject);
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            targets.Remove(other.gameObject);
        }
    }
    private void FixedUpdate() {
        if(targets.Count > 0){
            var mostClose = 100f;
            var closest = targets[0];
            foreach(var target in targets){
                if(target == null) continue;
                var diff = Vector3.Distance(transform.position, target.transform.position);
                if(diff < mostClose){
                    closest = target;
                    mostClose = diff;
                }
            }
            targetForShoot = closest;
            
            //Ask if can shoot
            if(deltaTimeLocal >= cooldown){
                deltaTimeLocal = 0;
                Debug.Log("Shoot");
                ShootBullet();
            }
        }else{
            targetForShoot = null;
        }
        deltaTimeLocal += Time.fixedDeltaTime;
    }

    private void ShootBullet()
    {
        var bulletShooted = Instantiate(bulletVfx);
        bulletShooted.transform.position = pointToShoot.transform.position;
        bulletShooted.transform.rotation = pointToShoot.transform.rotation;
        bulletShooted.Config(targetForShoot, power);
    }

    private void Update() {
        if(targetForShoot != null){
            // Calculate the direction from the current position to the target position
            Vector3 directionToTarget = targetForShoot.transform.position - model3D.transform.position;
            directionToTarget.y = 0; // Set the Y component to zero to only rotate around the Y-axis

            // Rotate the object to look in the calculated direction
            model3D.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }
}
