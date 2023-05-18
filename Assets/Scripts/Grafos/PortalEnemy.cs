using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnemy : ObjetoInteractuable{
    [SerializeField] private ListOfEnemies list;
    [SerializeField] private float timeSpawnCooldown;
    private bool isCanBreak = true;
    private Path _shortestPath;
    private bool isCanSpawn;
    private float deltaTimeLocal;
    private List<PjFather> pjs;

    public override void Config(){
        base.Config();
    }    
    
    public void StartSpawn()
    {
        isCanSpawn = true;
        StartCoroutine(Spawn());
    }

    
    private IEnumerator Spawn(){
        while(isCanBreak){
            yield return new WaitForSeconds(timeSpawnCooldown);
            if(!isCanSpawn)continue;
            var pjFather = Instantiate(list.GetMoster());
            var positionInPj = transform.position;
            positionInPj.y += 1;
            pjFather.transform.position = positionInPj;
            pjFather.Config(_shortestPath);
            pjs.Add(pjFather);
        }
    }
    
    internal void PauseSpawn()
    {
        isCanSpawn = false;
    }

    internal void PlaySpawn()
    {
        isCanSpawn = true;
    }

    private void OnDisable() {
        isCanBreak = false;
    }

    public override void DestroyAll(){
        foreach (var pjFather in pjs)
        {
            Destroy(pjFather.gameObject);
        }
        base.DestroyAll(); 
    }
    public void ListOfPath(Path shortestPath)
    {
        _shortestPath = shortestPath;
        pjs = new List<PjFather>();
    }
}