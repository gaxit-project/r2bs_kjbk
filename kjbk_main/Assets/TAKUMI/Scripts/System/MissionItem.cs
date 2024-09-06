using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissionItem : MonoBehaviour
{
    public static bool ItemGet = false;

    public ItemTake ItemTakeSc;

    public int ItemNum;



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemGet = true;
            ItemTakeSc.ItemFlagSet(ItemGet, ItemNum);
            Debug.Log("ItemGet ; " + ItemGet);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemGet = false;
            ItemTakeSc.ItemFlagSet(ItemGet, ItemNum);
            Debug.Log("ItemGet ; " + ItemGet);
        }
    }
}
