using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject AXE; //FOR TESTING ONLY TODO
    public int MaxWeight;
    public int CurrentWeight;



    private GameObject[] InventoryStorage = new GameObject[1000];

    public GameObject[] EquipedWeapons = new GameObject[2];
    private GameObject EquipedConsumable;
    private GameObject EquipedArmor; //TODO EXPAND TO MORE CATEGORIES



    public bool AddItem(GameObject item)
    {
        bool itemAdded = false;
        for(int i = 0; i < InventoryStorage.Length; i++)
        {
            if(InventoryStorage[i] == null)
            {
                itemAdded = true;
                InventoryStorage[i] = item;
                break;
            }
        }
        return itemAdded;
    }

    public void DeleteItem(int location)
    {
        InventoryStorage[location] = null;
    }

    public List<(int, GameObject)> ReturnItems(string type)
    {
        List<(int, GameObject)> TempList = new List<(int, GameObject)>();
        for (int i = 0; i < InventoryStorage.Length; i++)
        {
            if (InventoryStorage[i] != null)
            {
                if (InventoryStorage[i].GetComponent<ItemMaster>().ThisItemsType == type)
                {
                    TempList.Add((i, InventoryStorage[i]));
                }
            }
        }
        return TempList;
    }



    private void Start() //TESTING ONLY TODO
    {
        AddItem(AXE);
        AddItem(AXE);
        AddItem(AXE);
    }
}
