using UnityEngine;

[CreateAssetMenu(fileName = "ListOfEnemies", menuName = "ScriptableObjects/ListOfEnemies")]
public class ListOfEnemies : ScriptableObject
{
    public PjFather[] listOfEnemies;

    internal PjFather GetMoster()
    {
        return listOfEnemies[Random.Range(0, listOfEnemies.Length)];
    }
}