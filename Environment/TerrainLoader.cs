using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    [SerializeField] float terrain_size = 0;
    [SerializeField] int terrain_amount = 0;

    private float half_terrain_size;

    private GameObject[,] terrains;


    private (bool, float, float)[] activeterrains;

    private Transform PlayerTransform;

    private int PrevX;
    private int PrevZ;

    private void Start()
    {
        half_terrain_size = terrain_size / 2;

        terrains = new GameObject[terrain_amount, terrain_amount];

        activeterrains = new (bool, float,float)[4];

        PlayerTransform = GameObject.Find("Player").transform;


        GameObject obj;
        for (int i = 0; i < terrain_amount; i++)
        {
            for (int j = 0; j < terrain_amount; j++)
            {
                string terrain_string = "Terrain_(" + i + "," + j + ")";
                obj = GameObject.Find(terrain_string);
                obj.SetActive(false);
                terrains[i, j] = obj;
            }
        }

        PrevX = 0;
        PrevZ = 0;
    }

    private void FixedUpdate()
    {
        UpdateTerrain();
    }

    private void UpdateTerrain()
    {
        int currentX = (int)(PlayerTransform.position.x / terrain_size);
        int currentZ = (int)((terrain_size - PlayerTransform.position.z) / terrain_size);

        int subX = (int)((PlayerTransform.position.x % terrain_size) / half_terrain_size);
        int subZ = (int)(((terrain_size - PlayerTransform.position.z) % terrain_size) / half_terrain_size);

        subX = subX * 2 - 1; //-1 or 1 
        subZ = subZ * 2 - 1; //-1 or 1

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                int modX = currentX + subX*i;//enabled or disabled ( because i is either 0 or 1)
                int modZ = currentZ + subZ*j; //enabled or disabled
                if (inbounds(modX, modZ))
                {
                    activeterrains[i + (j * 2)] = (true, modX, modZ);
                }
                else
                {
                    activeterrains[i + (j * 2)] = (false, 0, 0);
                }

            }
        }

        for (int i = 0; i < terrain_amount; i++)
        {
            for (int j = 0; j < terrain_amount; j++)
            {

                bool should_enable = false;
                for(int k = 0; k < 4; k++)
                {
                    if (activeterrains[k] == (true, i, j))
                    {
                        should_enable = true;
                    }
                }

                if(should_enable)
                {
                    terrains[i, j].SetActive(true);
                }
                else
                {
                    terrains[i, j].SetActive(false);
                }

            }
        }

        PrevX = currentX;
        PrevZ = currentZ;
    }

    bool inbounds(float x, float z)
    {
        if(x < terrain_amount && x >= 0 && z < terrain_amount && z >= 0)
        {
            return true;
        }
        return false;
    }

}