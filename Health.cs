using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 0;
    [SerializeField] private float healthRegen = 0;
    [SerializeField] private int Plating = 0;
    [SerializeField] private int Armor = 0;

    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private Image healthSlider = null;
    [SerializeField] private bool damageTextEnabled = false;

    [SerializeField] private GameObject damageTextPrefab = null;
    [SerializeField] private EnemyTemplateMaster ETmaster = null;

    private Transform FloatingTextParent;

    public bool isDead = false;
    private float health;



    private GameObject regularDoT_number;
    private GameObject elementalDoT_number;
    private GameObject trueDoT_number;

    private void Start()
    {
        FloatingTextParent = GameObject.Find("FloatingTextParent").transform;
        health = maxHealth;

        if (healthSlider)
        {
            healthSlider.fillAmount = 1f;
        }

        regularDoT_number = null;
        elementalDoT_number = null;
        trueDoT_number = null;
    }

    public void modify_maxHealth(float value)
    {
        maxHealth = value;
    }
    public void modify_healthRegen(float value)
    {
        healthRegen = value;
    }
    public void modify_Plating(int value)
    {
        Plating = value;
    }
    public void modify_Armor(int value)
    {
        Armor = value;
    }

    public void take_damage(float damage, bool knockback = false, Vector3 force = new Vector3(), float stun_duration = 0f, DamageType DT = DamageType.Regular, bool isDoT = false)
    {
        //stun_duration is used to guarentee a minimum stun duration
        float ModifiedDamage = HealthCalculation(damage, DT);
        health -= ModifiedDamage;

        if (ETmaster && health > 0)
        {
            ETmaster.AIenabled = true;
            if (stun_duration > 0)
            {
                ETmaster.EnemyStun(stun_duration);
            }
            else if (knockback)
            {
                ETmaster.EnemyKnockback(force); //Dependent on the animation length
            }
        }

        if (healthSlider)
        {
            UpdateHealthBar();
        }

        if (damageTextEnabled)
        {
            DisplayDamageText(ModifiedDamage, DT, isDoT);
        }
    }

    public void heal(float amount)
    {
        if(health < maxHealth)
        {
            health += amount;
        }

        if (healthSlider)
        {
            UpdateHealthBar();
        }
    }


    float HealthCalculation(float damage, DamageType DT)
    {
        if (DT == DamageType.Regular || DT == DamageType.Elemental)
        {
            float resist = (100f / (Armor + 100f));
            damage *= resist;
        }

        if (DT == DamageType.Regular)
        {
            damage -= Plating;
        }

        if(damage < 0)
        {
            damage = 0;
        }

        return damage;
    }

    void UpdateHealthBar()
    {
        healthSlider.fillAmount = health / maxHealth;
    }

    void DisplayDamageText(float incomingDamage, DamageType DT, bool isDoT)
    {
        if (isDoT) //Keeps one number alive for every type of dot
        {
            if(DT == DamageType.Regular)
            {
                if(regularDoT_number == null)
                {
                    GameObject damageGameObject = Instantiate(damageTextPrefab, FloatingTextParent);
                    damageGameObject.GetComponent<FloatingTextScript>().Setup(transform.position, incomingDamage, DT);
                    regularDoT_number = damageGameObject;
                }
                else
                {
                    Vector3 offsetPOS = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    regularDoT_number.GetComponent<FloatingTextScript>().UpdateNumber(offsetPOS, incomingDamage);
                }
            }
            else if(DT == DamageType.Elemental)
            {
                if (elementalDoT_number == null)
                {
                    GameObject damageGameObject = Instantiate(damageTextPrefab, FloatingTextParent);
                    damageGameObject.GetComponent<FloatingTextScript>().Setup(transform.position, incomingDamage, DT);
                    elementalDoT_number = damageGameObject;
                }
                else
                {
                    Vector3 offsetPOS = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                    elementalDoT_number.GetComponent<FloatingTextScript>().UpdateNumber(offsetPOS, incomingDamage);
                }
            }
            else if(DT == DamageType.True)
            {
                if (trueDoT_number == null)
                {
                    GameObject damageGameObject = Instantiate(damageTextPrefab, FloatingTextParent);
                    damageGameObject.GetComponent<FloatingTextScript>().Setup(transform.position, incomingDamage, DT);
                    trueDoT_number = damageGameObject;
                }
                else
                {
                    Vector3 offsetPOS = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                    trueDoT_number.GetComponent<FloatingTextScript>().UpdateNumber(offsetPOS, incomingDamage);
                }
            }
        }
        else
        {
            GameObject damageGameObject = Instantiate(damageTextPrefab, FloatingTextParent);
            damageGameObject.GetComponent<FloatingTextScript>().Setup(transform.position, incomingDamage, DT);
        }
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
        else if(health > maxHealth)
        {
            health = maxHealth;
        }

        heal(healthRegen * Time.deltaTime);
    }
}
