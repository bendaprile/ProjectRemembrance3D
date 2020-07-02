using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected Material[] materials;

    protected MeshRenderer mesh;
    protected bool isCursorOverhead;

    protected virtual void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public virtual void CursorOverObject()
    {
        isCursorOverhead = true;
        set_item_material(1);
    }

    public virtual void CursorLeftObject()
    {
        isCursorOverhead = false;
        set_item_material(0);
    }



    protected virtual void set_item_material(int i)
    {
        mesh.material = materials[i];
    }


}
