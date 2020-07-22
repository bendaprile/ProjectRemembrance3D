using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour
{
    [SerializeField] Color HubButtonResetColor;
    [SerializeField] private GameObject WeaponUIPrefab_Object;
    [SerializeField] private GameObject ConsumableUIPrefab_Object;
    [SerializeField]  private GameObject InventoryPanelContent;

    private GameObject HubInventoryMenu;
    private GameObject ConsumableMenu;
    private GameObject ArmorMenu;
    private GameObject WeaponsMenu;
    private GameObject MiscMenu;

    private GameObject InventoryPanel;
    private Text InventoryPanelWildText;
    private GameObject TypeDependentPanel;

    private Inventory InventoryScript;

    private ItemTypeEnum last_category;

    private bool Ran_once;

    private void Awake()
    {
        Ran_once = false;
    }

    private void OnEnable()
    {
        if (Ran_once)
        {
            SetupLogic();
        }
    }

    private void Start()
    {
        HubInventoryMenu = GameObject.Find("HubInventoryMenu");
        ConsumableMenu = GameObject.Find("ConsumableMenu");
        ArmorMenu = GameObject.Find("ArmorMenu");
        WeaponsMenu = GameObject.Find("WeaponsMenu");
        MiscMenu = GameObject.Find("MiscMenu");

        InventoryPanel = GameObject.Find("InventoryPanel");
        TypeDependentPanel = GameObject.Find("TypeDependentPanel");
        InventoryScript = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        InventoryPanelWildText = InventoryPanel.transform.Find("InventoryWildField0").GetComponent<Text>();
        SetupLogic();
        Ran_once = true;
    }

    private void SetupLogic()
    {
        last_category = ItemTypeEnum.Misc;
        DestoryInventoryPanel();
        MassDisable();
    }



    public void ConsumableEnable()
    {
        MassDisable();
        last_category = ItemTypeEnum.Consumable;
        ConsumableMenu.SetActive(true);
        InventoryPanelWildText.text = "##";
        UpdateInventoryPanel();
        HubInventoryMenu.transform.Find("ConsumableButton").GetComponent<Image>().color = Color.red;
    }

    public void ArmorEnable()
    {
        MassDisable();
        last_category = ItemTypeEnum.Armor;
        ArmorMenu.SetActive(true);
        InventoryPanelWildText.text = "";
        UpdateInventoryPanel();
        HubInventoryMenu.transform.Find("ArmorButton").GetComponent<Image>().color = Color.red;
    }

    public void WeaponsEnable()
    {
        MassDisable();
        last_category = ItemTypeEnum.Weapon;
        WeaponsMenu.SetActive(true);
        InventoryPanelWildText.text = "";
        UpdateInventoryPanel();
        HubInventoryMenu.transform.Find("WeaponButton").GetComponent<Image>().color = Color.red;
    }

    public void MiscEnable()
    {
        MassDisable();
        last_category = ItemTypeEnum.Misc;
        MiscMenu.SetActive(true);
        InventoryPanelWildText.text = "";
        UpdateInventoryPanel();
        HubInventoryMenu.transform.Find("MiscButton").GetComponent<Image>().color = Color.red;
    }

    private void MassDisable()
    {
        ResetColors();
        ConsumableMenu.SetActive(false);
        ArmorMenu.SetActive(false);
        WeaponsMenu.SetActive(false);
        MiscMenu.SetActive(false);
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

    private void PopulateInventoryPanel (ItemTypeEnum type)
    {
        List<(int, GameObject)> items = InventoryScript.ReturnItems(type);

        foreach ((int, GameObject) item in items)
        {
            ItemMaster itemProperties = item.Item2.GetComponent<ItemMaster>();

            GameObject UIFab = null;
            if (type == ItemTypeEnum.Weapon)
            {
                UIFab = Instantiate(WeaponUIPrefab_Object);
                UIFab.GetComponent<WeaponUIPrefab>().Setup((Weapon)itemProperties, item.Item1);
            }
            else if(type == ItemTypeEnum.Consumable)
            {
                UIFab = Instantiate(ConsumableUIPrefab_Object);
                UIFab.GetComponent<ConsumableUIPrefab>().Setup((Consumable)itemProperties, item.Item1);
            }

            UIFab.transform.SetParent(InventoryPanelContent.transform);
        }
    }
}
