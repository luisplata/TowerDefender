using UnityEngine;

public interface ITerrain
{
    ObjetoInteractuable InstantiateObjectInPosition(string name, Vector3 position);
    Vector3 Position();
    void DestroyElement(GameObject gameObject);
    Material GetMaterial(string materialName);
    int Random_Range(int min, int max);
    Vector3 GetVector(int x, float y, int z);
}