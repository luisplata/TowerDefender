using System;
using UnityEngine;
public class NormalPlace : ObjetoInteractuable{
    [SerializeField] private bool isClickeable;
    [SerializeField] private GameObject vfx;
    public bool IsClickeable => isClickeable;

    internal void ShowPlace()
    {
        vfx.SetActive(true);
    }

    internal void HidePlace()
    {
        vfx.SetActive(false);
    }

    internal void HaveArsenal()
    {
        isClickeable = false;
    }
}