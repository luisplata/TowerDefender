using System;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    [SerializeField] private Image render;
    [SerializeField] private Sprite image;
    [SerializeField] private Button button;
    public Action onClick;

    internal void Config()
    {
        button.onClick.AddListener(()=>{
            onClick?.Invoke();
        });
    }

    private void Start()
    {
        render.sprite = image;
    }
}
