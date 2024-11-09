using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunctions : MonoBehaviour
{
    public void Empty() { }

    public void BeReborn()
    {
        Instantiate(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
