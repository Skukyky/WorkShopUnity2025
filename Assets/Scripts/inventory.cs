using System;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public int items;
    Canvas _canvas;


    void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void Energize()
    {
        if (items < 2)
        {
            items++;
            return;
        } 
        print("Inventory full");
    }
}
