using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoul_Trigger : MonoBehaviour
{
    private SoulObject myItemObject => GetComponentInParent<SoulObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<PlayerStats>().isDead)
                return;
            myItemObject.PickupSoul();
        }
    }

}
