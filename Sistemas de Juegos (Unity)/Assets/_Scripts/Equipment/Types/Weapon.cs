using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable
{
    public void Equip()
    {
        Debug.Log("Equipped Weapon!");
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Unequip()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == false)
        {
            Equip();
        }

        else
        {
            Unequip();
        }
    }
}
