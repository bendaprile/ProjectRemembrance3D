using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractiveObjectMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;

    private Inventory inv;
    private GameObject Panel;
    private GameObject StatsPanel;
    private List<GameObject> DisplayedItems;
    private GameObject PreviousContainer;
    private Transform SelectedStats;

    void Awake()
    {
        StatsPanel = transform.Find("StatsPanel").gameObject;
        Panel = transform.Find("Panel").gameObject;
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        SelectedStats = StatsPanel.transform.Find("SelectedStats");
        PreviousContainer = null;
    }

    public void Refresh(GameObject container)
    {
        PreviousContainer = container;
        foreach (Transform child in Panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in SelectedStats)
        {
            child.GetComponent<Text>().text = "";
        }

        List<GameObject> items = container.GetComponent<InteractiveBox>().Items;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp = Instantiate(ButtonPrefab);
            temp.GetComponent<InteractiveUIPrefab>().Setup(items[i].GetComponent<ItemMaster>(), i);
            temp.transform.SetParent(Panel.transform);
        }
    }

    public void TransferButtonPressed(int i)
    {
        if (inv.AddItem(PreviousContainer.GetComponent<InteractiveBox>().Items[i]))
        {
            PreviousContainer.GetComponent<InteractiveBox>().Items.RemoveAt(i);
            Refresh(PreviousContainer);
        }
    }

    public void StatsButtonPressed(ItemMaster item_in)
    {
        foreach (Transform child in SelectedStats)
        {
            child.GetComponent<Text>().text = "";
        }

        if (item_in.ItemType == ItemTypeEnum.Weapon)
        {
            if (((Weapon)item_in).weaponType == WeaponType.Melee_1H)
            {
                Melee1H temp = (Melee1H)item_in;

                SelectedStats.Find("A_Name").GetComponent<Text>().text = "Basic Attack Damage: ";
                SelectedStats.Find("A_Value").GetComponent<Text>().text = temp.basicAttackDamage.ToString();

                SelectedStats.Find("B_Name").GetComponent<Text>().text = "Heavy Attack Damage: ";
                SelectedStats.Find("B_Value").GetComponent<Text>().text = temp.heavyAttackDamage.ToString();

                SelectedStats.Find("C_Name").GetComponent<Text>().text = "Energy Cost: ";
                SelectedStats.Find("C_Value").GetComponent<Text>().text = temp.energyCost.ToString();

                SelectedStats.Find("D_Name").GetComponent<Text>().text = "Damage Type: ";
                SelectedStats.Find("D_Value").GetComponent<Text>().text = temp.DT.ToString();
            }
        }
            
            

    }
}