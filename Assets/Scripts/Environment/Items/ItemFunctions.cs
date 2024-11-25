using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunctions : MonoBehaviour
{
    public GameObject playerPrefab;

    public void Empty() { }

    public void BeReborn()
    {
        Instantiate(playerPrefab).transform.position = transform.parent.position;
        Destroy(transform.parent.gameObject);
    }
}
