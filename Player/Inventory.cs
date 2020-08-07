using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject Axe; //FOR TESTING ONLY TODO
    [SerializeField] GameObject Pistol; //FOR TESTING ONLY TODO
    [SerializeField] GameObject FirstAid; //FOR TESTING ONLY TODO 


    [SerializeField] private int MaxWeight;
    [SerializeField] public int CurrentWeight; //Serialize for debug


    private WeaponController weaponController;
    private QuestsHolder questsHolder;


    private GameObject[] InventoryStorage = new GameObject[1000];


    private int[] EquippedWeapons = new int[2]; //-1 means none equipped //Item NOT enabled, this happens when the weapon is readied
    private int EquippedConsumable; //-1 means none equipped //Item enabled if here
    private int EquippedArmor; //-1 means none equipped //Item enabled if here


    //InventoryStorage////////////////////////////////
    public bool AddItem(GameObject item)
    {
        int newWeight = CurrentWeight + item.GetComponent<ItemMaster>().Weight;
        if(newWeight > MaxWeight)
        {
            Debug.Log("OVER MAX WEIGHT");
            return false;
        }
        CurrentWeight = newWeight;

        bool itemAdded = false;
        for(int i = 0; i < InventoryStorage.Length; i++)
        {
            if(InventoryStorage[i] == null)
            {
                itemAdded = true;
                InventoryStorage[i] = Instantiate(item, transform.Find("Storage"));
                InventoryStorage[i].SetActive(false);
                questsHolder.CheckFetchObjectives(item, true, i);
                break;
            }
        }
 
        return itemAdded;
    }

    public void DeleteItem(int location)
    {
        CurrentWeight -= InventoryStorage[location].GetComponent<ItemMaster>().Weight;
        questsHolder.CheckFetchObjectives(InventoryStorage[location], false, location);
        InventoryStorage[location] = null;
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
                    if (!ItemIsEquppied(i))
                    {
                        TempList.Add((i, InventoryStorage[i]));
                    }
                }
            }
        }
        return TempList;
    }

    private bool ItemIsEquppied(int i)
    {
        if((i == EquippedConsumable) || (i == EquippedArmor) || (i == EquippedWeapons[0]) || (i == EquippedWeapons[1]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //InventoryStorage////////////////////////////////


    //EquipedWeapons//////////////////////////////////
    public void EquipWeapon(int storage_location, int equppied_location)
    {
        EquippedWeapons[equppied_location] = storage_location;
        weaponController.RefreshWeapons();
    }

    public void UnEquipWeapon(int equppied_location)
    {
        EquippedWeapons[equppied_location] = -1;
        weaponController.RefreshWeapons();
    }

    public GameObject ReturnWeapon(int location)
    {
        if(EquippedWeapons[location] == -1)
        {
            return null;
        }
        else
        {
            return InventoryStorage[EquippedWeapons[location]];
        }
    }
    //EquipedWeapons//////////////////////////////////


    //EquipedConsumable//////////////////////////////////
    public void EquipConsumable(int storage_location)
    {
        if(EquippedConsumable != -1)
        {
            InventoryStorage[EquippedConsumable].SetActive(false);
        }
        EquippedConsumable = storage_location;
        InventoryStorage[EquippedConsumable].SetActive(true);
    }

    public void UnEquipConsumable()
    {
        InventoryStorage[EquippedConsumable].SetActive(false);
        EquippedConsumable = -1;
    }

    public GameObject ReturnConsumable()
    {
        if(EquippedConsumable == -1)
        {
            return null;
        }
        else
        {
            return InventoryStorage[EquippedConsumable];
        }
    }

    public void DisposeConsumable()
    {
        InventoryStorage[EquippedConsumable].GetComponent<Consumable>().quantity -= 1;
        if (InventoryStorage[EquippedConsumable].GetComponent<Consumable>().quantity == 0)
        {
            EquippedConsumable = -1;
        }
    }
    //EquipedConsumable//////////////////////////////////

    private void Start() 
    {
        CurrentWeight = 0;
        EquippedWeapons[0] = -1;
        EquippedWeapons[1] = -1;
        EquippedConsumable = -1;
        EquippedArmor = -1;


        weaponController = GameObject.Find("Player").transform.Find("Body").GetComponent<WeaponController>();
        questsHolder = GameObject.Find("Player").transform.Find("QuestsHolder").GetComponent<QuestsHolder>();

        AddItem(Axe); //TESTING ONLY TODO
        AddItem(Axe);
        AddItem(Axe);
        AddItem(Pistol);
        AddItem(FirstAid);
        AddItem(FirstAid);
    }
}
