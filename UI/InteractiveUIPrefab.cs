using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractiveUIPrefab : MonoBehaviour
{
    private InteractiveObjectMenuUI IOM;
    private Inventory inv;
    private int itemStorageLoc;

    void Start()
    {
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        IOM = GameObject.Find("InteractiveObjectMenu").GetComponent<InteractiveObjectMenuUI>();
    }

    public void Setup(string name, int storage_loc)
    {
        GetComponentInChildren<Text>().text = name;
        itemStorageLoc = storage_loc;
    }

    public void ButtonPressedTransfer()
    {
        IOM.ButtonPressed(itemStorageLoc);
    }
}