using UnityEngine;

namespace PjComponent
{
    public interface IPj
    {
        void Move(Vector3 position, Vector3 positionFinal, float speed);
        Vector3 GetPosition();
        bool IsClose(Vector3 pointA, Vector3 pointB);
    }
}