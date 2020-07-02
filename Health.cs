using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] public float health = 0;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private Image healthSlider = null;
    [SerializeField] private EnemyTemplateMaster ETmaster = null;

    public bool isDead = false;
    private float maxHealth;

    public enum DamageType
    {
        Basic,
    }

    private void Start()
    {
        maxHealth = health;

        if (healthSlider)
        {
            healthSlider.fillAmount = health / maxHealth;
        }
    }

    public void take_damage(float damage, bool stun = false, Vector3 force = new Vector3(), DamageType DT = DamageType.Basic)
    {
        health -= damage;

        if (ETmaster && health > 0)
        {
            ETmaster.AIenabled = true;
            if (stun)
            {
                ETmaster.TakeDamageEnemy(force);
            }
        }

        if (healthSlider)
        {
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.fillAmount = health / maxHealth;
    }

    void Update()
    {
        if (health <= 0 && destroyOnDeath)
        {
            isDead = true;
            Destroy(gameObject);
        }
        else if (health <= 0)
        {
            isDead = true;
        }
    }
}
