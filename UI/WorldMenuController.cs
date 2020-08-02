using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform mapRect;
    private bool First_run = true;

    private void OnEnable()
    {
        if (First_run)
        {
            first_run_func();
        }
        mapRect.localPosition = new Vector3(410f, 0f);
    }

    private void first_run_func()
    {
        First_run = false;
    }

    private void OnDisable()
    {
        mapRect.localPosition = new Vector3(0f, 0f);
    }
}
