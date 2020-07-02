using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour
{
    [SerializeField] Color HubButtonResetColor;
    [SerializeField] private GameObject ItemUIPrefab;
    [SerializeField]  private GameObject InventoryPanelContent;

    private GameObject HubInventoryMenu;
    private GameObject ConsumableMenu;
    private GameObject ArmorMenu;
    private GameObject WeaponsMenu;
    private GameObject MiscMenu;

    private GameObject InventoryPanel;
    private GameObject TypeDependentPanel;

    private Inventory InventoryScript;

    private string last_category;


    private void OnEnable()
    {
        last_category = "";
        HubInventoryMenu = GameObject.Find("HubInventoryMenu");
        ConsumableMenu = GameObject.Find("ConsumableMenu");
        ArmorMenu = GameObject.Find("ArmorMenu");
        WeaponsMenu = GameObject.Find("WeaponsMenu");
        MiscMenu = GameObject.Find("MiscMenu");

        InventoryPanel = GameObject.Find("InventoryPanel");
        TypeDependentPanel = GameObject.Find("TypeDependentPanel");

        InventoryScript = GameObject.Find("Player").GetComponent<Inventory>();
        DestoryInventoryPanel();
        MassDisable();
    }

    public void ConsumableEnable()
    {
        MassDisable();
        DestoryInventoryPanel();
        PopulateInventoryPanel("Consumable");
        last_category = "Consumable";
        ConsumableMenu.SetActive(true);
        HubInventoryMenu.transform.Find("ConsumableButton").GetComponent<Image>().color = Color.red;
    }

    public void ArmorEnable()
    {
        MassDisable();
        DestoryInventoryPanel();
        PopulateInventoryPanel("Armor");
        last_category = "Armor";
        ArmorMenu.SetActive(true);
        HubInventoryMenu.transform.Find("ArmorButton").GetComponent<Image>().color = Color.red;
    }

    public void WeaponsEnable()
    {
        MassDisable();
        DestoryInventoryPanel();
        PopulateInventoryPanel("Weapon");
        last_category = "Weapon";
        WeaponsMenu.SetActive(true);
        HubInventoryMenu.transform.Find("WeaponButton").GetComponent<Image>().color = Color.red;
    }

    public void MiscEnable()
    {
        MassDisable();
        DestoryInventoryPanel();
        PopulateInventoryPanel("Misc");
        last_category = "Misc";
        MiscMenu.SetActive(true);
        HubInventoryMenu.transform.Find("MiscButton").GetComponent<Image>().color = Color.red;
    }

    private void MassDisable()
    {
        ResetColors();
    }

    private void ResetColors()
    {
        foreach (Transform child in HubInventoryMenu.transform)
        {
            child.GetComponent<Image>().color = HubButtonResetColor;
        }
    }

    public void UpdateInventoryPanel()
    {
        DestoryInventoryPanel();
        PopulateInventoryPanel(last_category);
    }


    private void DestoryInventoryPanel()
    {
        foreach (Transform item in InventoryPanelContent.transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void PopulateInventoryPanel (string type)
    {
        List<(int, GameObject)> items = InventoryScript.ReturnItems(type);

        foreach ((int, GameObject) item in items)
        {
            ItemMaster itemProperties = item.Item2.GetComponent<ItemMaster>();

            GameObject UIFab = Instantiate(ItemUIPrefab);
            UIFab.GetComponent<ItemUISetup>().Setup(itemProperties.name, itemProperties.Cost, item.Item1);
            UIFab.transform.SetParent(InventoryPanelContent.transform);
        }
    }
}
