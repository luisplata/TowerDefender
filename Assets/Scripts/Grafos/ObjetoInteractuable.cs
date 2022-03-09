using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoInteractuable : MonoBehaviour
{
    [TextArea]
    [Tooltip("A string using the TextArea attribute")]
    public string dialogoBueno, dialogoMalo, dialogoFinal;
    public List<ObjetoInteractuable> aristas;
    private TeoriaDeGrafos padre;
    private Button boton;
    public int distancia;
    public bool visitado = false;
    public GameObject botonMapaGeneral;
    [SerializeField] private bool isFinal;
    [SerializeField] private bool isPortal;
    [SerializeField] private PjFather pj;
    private List<PjFather> pjs;
    private Path _shortestPath;

    public bool IsFinal => isFinal;
    public bool IsPortal => isPortal;

    public void Config(TeoriaDeGrafos father)
    {
        padre = father;
        aristas = new List<ObjetoInteractuable>();
    }
    //preguntar si es el objeto final
    //si no mostar dialogos
    public void GenerarDialogosDeAristas()
    {
        List<string> dialogos = new List<string>();
        //Debug.Log(padre.verticeFinal.gameObject.name + " - " + this.name+ " son iguales -> "+ padre.verticeFinal.Equals(this));
        if (padre.verticeFinal.Equals(this))
        {
            if (PlayerPrefs.GetString("escenaSeleccionada").Equals(ConstantesDelProyecto.ESCENA_MAPA_IGLESIA))
            {
                //dialogos.Add(padre.dialogoSegunElTiempoTranscurrido());
            }
            else
            {
                dialogos.Add(dialogoFinal);
            }
            botonMapaGeneral.SetActive(true);
            
        }
        else
        {
            //dialogos = padre.DialogosDeEsteObjeto(this);   
        }
        TextMeshProUGUI texto = GameObject.Find("CajaDeDialogos").GetComponent<TextMeshProUGUI>();
        texto.text = string.Empty;
        foreach(string s in dialogos)
        {
            texto.text += s + "\n";
        }
    }

    public void ChangeMaterial(Material materialToRoad)
    {
        if(isFinal || IsPortal) return;
        gameObject.GetComponent<MeshRenderer>().material = materialToRoad;
    }

    public Node GetNode()
    {
        return GetComponent<Node>();
    }

    public void ListOfPath(Path shortestPath)
    {
        _shortestPath = shortestPath;
        pjs = new List<PjFather>();
    }

    public void StartSpawn()
    {
        var pjFather = Instantiate(pj);
        var positionInPj = transform.position;
        positionInPj.y += 1;
        pjFather.transform.position = positionInPj;
        pjFather.Config(_shortestPath);
        pjs.Add(pjFather);
    }

    public void DestroyAll()
    {
        if(IsPortal)
        {
            foreach (var pjFather in pjs)
            {
                Destroy(pjFather.gameObject);
            } 
        }
        Destroy(gameObject);
    }
}
