using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;


    public void ShowToolTip(ItemData_Equipment _item)
    {
        if(_item == null)
            return;

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.itemType.ToString();
        itemDescription.text = _item.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
