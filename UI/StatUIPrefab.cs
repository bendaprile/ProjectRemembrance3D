using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatUIPrefab : MonoBehaviour
{
    Text StatName;
    Text StatValue;
    StatMenuController STC;

    string FinalValueHolder;
    List<(string, float)> AddDerivHolder;
    List<(string, float)> MultDerivHolder;
    (string, string) DescHolder;

    public void Setup(string finalValue, List<(string, float)> Add_in, List<(string, float)> Mult_in, (string, string) desc)
    {
        STC = GetComponentInParent<StatMenuController>();
        StatName = transform.Find("StatName").GetComponent<Text>();
        StatValue = transform.Find("StatValue").GetComponent<Text>();

        StatName.text = desc.Item1;
        StatValue.text = finalValue;

        FinalValueHolder = finalValue;
        AddDerivHolder = Add_in;
        MultDerivHolder = Mult_in;
        DescHolder = desc;
    }

    public void buttonPress()
    {
        STC.UpdateStatsInfo(FinalValueHolder, AddDerivHolder, MultDerivHolder, DescHolder);
    }
}
