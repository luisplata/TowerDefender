using TowerDefender.Assets.Scripts.Utils;
using UnityEngine;

public class Tower : ObjetoInteractuable{
    [SerializeField] private GameObject pointToView;
    [SerializeField] private float life;
    
    public Transform GetPointToView()
    {
        return pointToView.transform;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            if(other.gameObject.TryGetComponent<PjFather>(out var pj)){
                life -= pj.Damage;
                pj.DestroyMoster();
                if(life <= 0){
                    Debug.Log($"Game Over");
                    ServiceLocator.Instance.GetService<IGame>().GameOver();
                }
            }
        }
    }
}