using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatChecker : MonoBehaviour
{
    public bool enemies_nearby;
    List<Collider> TriggerList;

    private void Start()
    {
        enemies_nearby = false;
        TriggerList = transform.GetComponent<ColliderChild>().TriggerList;
    }

    private void FixedUpdate()
    {
        enemies_nearby = false;

        int potential_enemies_in_range = TriggerList.Count; //Includes recently dead enemies
        for (int i = potential_enemies_in_range - 1; i >= 0; i--)
        {
            Collider col = TriggerList[i];
            if (!col || col.tag != "BasicEnemy")
            {
                TriggerList.Remove(col);
            }
            else
            {
                enemies_nearby = true;
            }

        }
    }

}
