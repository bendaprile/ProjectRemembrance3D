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
    private List<GameObject> DisplayedItems;
    private GameObject PreviousContainer;

    void Awake()
    {
        Panel = transform.Find("Panel").gameObject;
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        PreviousContainer = null;

    }

    public void Refresh(GameObject container)
    {
        PreviousContainer = container;
        foreach (Transform child in Panel.transform)
        {
            Destroy(child.gameObject);
        }

        List<GameObject> items = container.GetComponent<InteractiveBox>().Items;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp = Instantiate(ButtonPrefab);
            temp.GetComponent<InteractiveUIPrefab>().Setup(items[i].GetComponent<ItemMaster>().name, i);
            temp.transform.SetParent(Panel.transform);
        }
    }

    public void ButtonPressed(int i)
    {
        inv.AddItem(PreviousContainer.GetComponent<InteractiveBox>().Items[i]);
        PreviousContainer.GetComponent<InteractiveBox>().Items.RemoveAt(i);
        Refresh(PreviousContainer);
    }
}