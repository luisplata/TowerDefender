using UnityEngine;

public class BuildTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private int size;
    [SerializeField] private int countEnemies;
    [SerializeField] private ObjetoInteractuable cube;
    [SerializeField] private ObjetoInteractuable tower;
    [SerializeField] private ObjetoInteractuable portalEnemies;
    [SerializeField] private Material materialToRoad;
    
    private Terrain _terrain;
    
    private void Start()
    {
        _terrain = new Terrain(this,size, countEnemies);
        _terrain.StartGame();
    }

    public void Restart()
    {
        _terrain.Restart();
    }

    public ObjetoInteractuable InstantiateObjectInPosition(string name, Vector3 position)
    {
        var instantiate = Instantiate(FactoryOfTerrain(name), transform);
        instantiate.transform.position = transform.position;
        instantiate.transform.position += position;
        return instantiate;
    }

    public Vector3 Position()
    {
        return transform.position;
    }

    public void DestroyElement(GameObject element)
    {
        Destroy(element);
    }

    public Material GetMaterial(string materialName)
    {
        return materialToRoad;
    }

    public int Random_Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public Vector3 GetVector(int x, float y, int z)
    {
        return new Vector3(x, y, z);
    }

    private ObjetoInteractuable FactoryOfTerrain(string name)
    {
        return name switch
        {
            "portalEnemies" => portalEnemies,
            "tower" => tower,
            _ => cube
        };
    }
}