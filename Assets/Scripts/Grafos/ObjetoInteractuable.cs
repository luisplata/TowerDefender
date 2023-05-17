using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{   
    private List<ObjetoInteractuable> aristas;
    private bool visitado;
    public List<ObjetoInteractuable> Aristas => aristas;

    private void Start() {
        
    }

    public virtual void Config()
    {
        aristas = new List<ObjetoInteractuable>();
    }

    public virtual void ChangeMaterial(Material materialToRoad)
    {
        gameObject.GetComponent<MeshRenderer>().material = materialToRoad;
    }

    public INodeCustom GetNode()
    {
        return GetComponent<INodeCustom>();
    }


    public virtual void DestroyAll()
    {
        Destroy(gameObject);
    }

}
