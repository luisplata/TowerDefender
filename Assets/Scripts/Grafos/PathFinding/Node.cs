using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Node.
/// </summary>
public class Node : MonoBehaviour, INodeCustom
{

    /// <summary>
    /// The connections (neighbors).
    /// </summary>
    [SerializeField]
    protected List<INodeCustom> m_Connections = new List<INodeCustom>();

    /// <summary>
    /// Gets the connections (neighbors).
    /// </summary>
    /// <value>The connections.</value>
    public virtual List<INodeCustom> connections
    {
        get
        {
            return m_Connections;
        }
    }

    public INodeCustom this[int index]
    {
        get
        {
            return m_Connections[index];
        }
    }

    public List<INodeCustom> GetConnections()
    {
        return connections;
    }

    public Vector3 GetGameObjectPosition()
    {
        return gameObject.transform.position;
    }

    public string GetGameObjectName()
    {
        return gameObject.name;
    }

    public GameObject GetGameObjectObjectInteractuable()
    {
        return gameObject;
    }
}