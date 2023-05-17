using UnityEngine;

public class Tower : ObjetoInteractuable{
    [SerializeField] private GameObject pointToView;

    public override void ChangeMaterial(Material materialToRoad)
    {

    }
    
    public Transform GetPointToView()
    {
        return pointToView.transform;
    }
}