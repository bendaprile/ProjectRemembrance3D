using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject HUD;
    public GameObject Map;

    private float originalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(!Menu.activeSelf);
            if (Menu.activeSelf == true)
            {
                Paused();
            }
            else
            {
                Unpaused();
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Menu.activeSelf)
            {
                Menu.GetComponent<MenuController>().MapEnable();                
            }
            else
            {
                Map.SetActive(!Map.activeSelf);
            }
        }
    }

    void Paused()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
    }

    void Unpaused()
    {
        Time.timeScale = originalTimeScale;
        HUD.SetActive(true);
    }
}
