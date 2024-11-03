using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    public int quantity;
    public Item item;
    public bool selected;
    public string itemName = "";

    public GameObject quantityText;
    public GameObject itemFrame;
    public GameObject border;

    void Start()
    {
        itemName = "";
        quantity = 1;
    }
    void Update()
    {
        if (item != null)
        {
            itemFrame.GetComponent<Image>().sprite = item.icon;

            if (item.maxStackSize == 1)
            {
                quantityText.SetActive(false);
            }

            quantityText.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
            itemName = item.itemName;
        }
       border.GetComponent<Image>().enabled = selected;
    }
}
