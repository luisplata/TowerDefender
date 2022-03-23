using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private int size;
    [SerializeField] private int countEnemies;
    [SerializeField] private ObjetoInteractuable cube;
    [SerializeField] private ObjetoInteractuable road;
    [SerializeField] private ObjetoInteractuable tower;
    [SerializeField] private ObjetoInteractuable portalEnemies;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private int minView, maxView;
    [SerializeField] private float deltaCrement;
    private float indexToMouse = 0;
    
    private Terrain _terrain;
    
    private void Start()
    {
        _terrain = new Terrain(this,size, countEnemies);
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

    public int Random_Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public Vector3 GetVector(int x, float y, int z)
    {
        return new Vector3(x, y, z);
    }

    public ObjetoInteractuable FactoryOfTerrain(string name)
    {
        return name switch
        {
            "portalEnemies" => portalEnemies,
            "tower" => tower,
            "road" => road,
            _ => cube
        };
    }

    public void AddingToCameraAtPlayer(ObjetoInteractuable player)
    {
        camera.Follow = player.GetPointToView();
        camera.LookAt = player.GetPointToView();
    }
    
    public void Scroll(InputAction.CallbackContext context)
    {
        var crement = context.ReadValue<Vector2>().y;
        if (crement > 0)
        {
            //ChangeVioport();
            indexToMouse -= deltaCrement;
        }

        if (crement < 0)
        {
            indexToMouse += deltaCrement;
        }
        if (indexToMouse <= 0)
        {
            indexToMouse = 0;
        }

        if (indexToMouse >= 1)
        {
            indexToMouse = 1;
        }
        Debug.Log($"indexToMouse {indexToMouse}");
        ChangeVioport(indexToMouse);
    }

    public void ChangeVioport(float index)
    {
        if (index <= 0)
        {
            index = 0;
        }

        if (index >= 1)
        {
            index = 1;
        }
        Debug.Log($"index {index}");
        var total = maxView - minView;

        var view = (int)(index * total) + minView;
        Debug.Log($"view {view}");
        camera.m_Lens.FieldOfView = view;
    }
}