using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;

    [SerializeField] private Image[] materialImage;

    public void SetupCraftWindow(ItemData_Equipment _data)
    {
        craftButton.onClick.RemoveAllListeners();
        GameObject materialList = null;

        if (_data == null)
        {
            materialList = GetMaterialList(materialList);

            if (materialList != null)
            {
                materialList.SetActive(false);
            }

            itemName.text = "";
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            itemDescription.text = "";
            craftButton.gameObject.SetActive(false);

        }
        else
        {

            for (int i = 0; i < materialImage.Length; i++)
            {
                materialImage[i].color = Color.clear;
                materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
            }

            for (int i = 0; i < _data.craftingMaterials.Count; i++)
            {
                if (_data.craftingMaterials.Count > materialImage.Length)
                {

                }

                materialImage[i].sprite = _data.craftingMaterials[i].data.itemIcon;
                materialImage[i].color = Color.white;

                TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

                materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
                materialSlotText.color = Color.white;
            }

            itemName.text = _data.itemName;

            itemIcon.color = Color.white;
            itemIcon.sprite = _data.itemIcon;

            itemDescription.text = _data.GetDescription();

            materialList = GetMaterialList(materialList);

            if (materialList != null)
            {
                materialList.SetActive(true);
            }
            craftButton.gameObject.SetActive(true);
        }

        craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(_data, _data.craftingMaterials));
    }

    private GameObject GetMaterialList(GameObject materialList)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == "MaterialList")
            {
                materialList = child.gameObject;
                break;
            }
        }

        return materialList;
    }
}
