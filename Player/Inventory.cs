using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject Axe; //FOR TESTING ONLY TODO
    [SerializeField] GameObject Pistol; //FOR TESTING ONLY TODO
    public int MaxWeight;
    public int CurrentWeight;


    private WeaponController weaponController;


    private GameObject[] InventoryStorage = new GameObject[1000];

    private GameObject[] EquipedWeapons = new GameObject[2];
    private GameObject EquipedConsumable;
    private GameObject EquipedArmor; //TODO EXPAND TO MORE CATEGORIES


    //InventoryStorage////////////////////////////////
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

    public GameObject ReturnSingleItem(int location)
    {
        return InventoryStorage[location];
    }

    public List<(int, GameObject)> ReturnItems(ItemTypeEnum type)
    {
        List<(int, GameObject)> TempList = new List<(int, GameObject)>();
        for (int i = 0; i < InventoryStorage.Length; i++)
        {
            if (InventoryStorage[i] != null)
            {
                if (InventoryStorage[i].GetComponent<ItemMaster>().ItemType == type)
                {
                    TempList.Add((i, InventoryStorage[i]));
                }
            }
        }
        return TempList;
    }
    //InventoryStorage////////////////////////////////


    //EquipedWeapons//////////////////////////////////
    public GameObject EquipWeapon(int location, GameObject Weapon)
    {
        GameObject temp = EquipedWeapons[location];
        EquipedWeapons[location] = Weapon;
        weaponController.RefreshWeapons();
        return temp;
    }

    public GameObject ReturnWeapon(int location)
    {
        return EquipedWeapons[location];
    }
    //EquipedWeapons//////////////////////////////////

    private void Start() //TESTING ONLY TODO
    {
        weaponController = GameObject.Find("Player").transform.Find("Body").GetComponent<WeaponController>();
        AddItem(Axe);
        AddItem(Axe);
        AddItem(Axe);
        AddItem(Pistol);
    }
}
