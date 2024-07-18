using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData soulDrop;

    public void GenerateDrop()
    {
        DropSoul(soulDrop);
    }

    private void DropSoul(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 17));

        newDrop.GetComponent<SoulObject>().SetupSoul(_itemData, randomVelocity);
    }
}
