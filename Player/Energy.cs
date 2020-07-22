using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    [SerializeField] private float maxNormalEnergy = 0;
    [SerializeField] private float maxExpandedEnergy = 0;
    [SerializeField] private float energyNormal_regen = 0;
    [SerializeField] private float energyExpanded_regen = 0;

    [SerializeField] Image energySlider = null;
    [SerializeField] Image expandedEnergySlider = null;

    private float maxTotalEnergy;
    private float currentEnergy;

    void Start()
    {
        maxTotalEnergy = maxNormalEnergy + maxExpandedEnergy;
        currentEnergy = maxTotalEnergy;

        energySlider.fillAmount = 1;
        expandedEnergySlider.fillAmount = 1;
    }

    public void modifymaxNormalEnergy(float value)
    {
        maxNormalEnergy = value;
    }
    public void modify_maxExpandedEnergy(float value)
    {
        maxExpandedEnergy = value;
    }
    public void modify_energyNormal_regen(float value)
    {
        energyNormal_regen = value;
    }
    public void modify_energyExpanded_regen(float value)
    {
        energyExpanded_regen = value;
    }


    public bool drain_energy(float amount)
    {
        if(currentEnergy >= amount)
        {
            currentEnergy -= amount;
            return true;
        }
        return false;
    }

    public bool drain_energy_greaterThan(float amount, bool NewCommand, float new_required_amount) //Used for some time-based drains. A new command requires the required_amount
    {
        if (currentEnergy >= new_required_amount || (!NewCommand && (currentEnergy >= amount)))
        {
            currentEnergy -= amount;
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if(currentEnergy >= maxTotalEnergy)
        {
            currentEnergy = maxTotalEnergy;
        } 
        else if(currentEnergy >= maxNormalEnergy)
        {
            currentEnergy += energyExpanded_regen;
        }
        else
        {
            currentEnergy += energyNormal_regen;
        }
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        if(currentEnergy > maxNormalEnergy)
        {
            energySlider.fillAmount = 1;
            expandedEnergySlider.fillAmount = (currentEnergy - maxNormalEnergy) / maxExpandedEnergy;
        }
        else
        {
            energySlider.fillAmount = currentEnergy / maxNormalEnergy;
            expandedEnergySlider.fillAmount = 0;
        }

    }
}
