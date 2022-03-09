using System.Collections.Generic;
using UnityEngine;

public interface INodeCustom
{
    List<INodeCustom> GetConnections();
    Vector3 GetGameObjectPosition();
    string GetGameObjectName();
    GameObject GetGameObjectObjectInteractuable();
}