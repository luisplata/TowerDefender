using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private int size;
    [SerializeField] private int countEnemies;
    [SerializeField] private ObjetoInteractuable[] cubes;
    [SerializeField] private ObjetoInteractuable road;
    [SerializeField] private ObjetoInteractuable tower;
    [SerializeField] private ObjetoInteractuable portalEnemies;
    [SerializeField] private GameObject vayoneta;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private int minView, maxView;
    [SerializeField] private float deltaCrement;
    private float indexToMouse = 0;
    private Vector2 mousePosition;
    
    private Terrain _terrain;
    private bool isCanClickInTerrain;
    
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
        var instantiate = FactoryOfTerrain(name);
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
        var instantiateObject = cubes[0];
        switch (name)
        {
            case "portalEnemies":
                // Additional code for "portalEnemies" case
                instantiateObject = Instantiate(portalEnemies, transform);
                // Additional code
                // ...
                break; // Optional: Use 'break' if you want to exit the switch statement

            case "tower":
                // Additional code for "tower" case
                instantiateObject = Instantiate(tower, transform);
                // Additional code
                // ...
                break; // Optional: Use 'break' if you want to exit the switch statement

            case "road":
                // Additional code for "road" case
                // Assign 'road' value
                instantiateObject = Instantiate(road, transform);
                // Additional code
                // ...
                break; // Optional: Use 'break' if you want to exit the switch statement

            default:
                // Additional code for the default case
                var randomIndex = Random.Range(0, cubes.Length);
                instantiateObject = Instantiate(cubes[randomIndex], transform);
                // Additional code
                // ...
                break; // Optional: Use 'break' if you want to exit the switch statement
        }

        // var instantiateObject = name switch
        // {
        //     "portalEnemies" => Instantiate(portalEnemies, transform),
        //     "tower" => Instantiate(tower, transform),
        //     "road" => road,
        //     _ => cubes[Random.Range(0,cubes.Length)]
        // };
        
        return instantiateObject;
    }

    public void AddingToCameraAtPlayer(Tower player)
    {
        camera.Follow = player.GetPointToView();
        camera.LookAt = player.GetPointToView();
    }
    
    public void Scroll(InputAction.CallbackContext context)
    {
        var crement = context.ReadValue<Vector2>().y;
        if (crement > 0)
        {
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

    public void Point(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.TryGetComponent<NormalPlace>(out var objetoInteractuable))
            {
                if(isCanClickInTerrain){
                    Debug.Log($"el objeto {hit.transform.name} interactuable {objetoInteractuable.IsClickeable}");
                    isCanClickInTerrain = false;
                    HidePlaceEnables();
                    var vayonetaInstanciada = FactoryOfArsenal("Vayoneta");
                    vayonetaInstanciada.transform.SetParent(objetoInteractuable.transform);
                    vayonetaInstanciada.transform.localPosition = Vector3.zero;
                    objetoInteractuable.HaveArsenal();
                }
            }
        }
    }

    private GameObject FactoryOfArsenal(string name)
    {
        GameObject instantiate = null;
        switch (name)
        {
            case "Vayoneta":
                instantiate = Instantiate(vayoneta, transform);
                break;
        }
        return instantiate;
    }

    private void HidePlaceEnables()
    {
        _terrain.HidePlaceEnables();
    }

    public void StartSpawn()
    {
        _terrain.StartGame();
    }

    public void PauseGame()
    {
        _terrain.PauseGame();
    }

    public void PlayGame()
    {
        _terrain.PlayGame();
    }

    public void ShowPlaceEnables()
    {
        isCanClickInTerrain = true;
        _terrain.ShowPlaceEnables();
    }
}