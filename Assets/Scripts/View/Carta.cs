using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    [SerializeField] private Image render;
    [SerializeField] private Sprite image;

    private void Start()
    {
        render.sprite = image;
    }
}
