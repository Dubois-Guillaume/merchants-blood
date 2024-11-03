using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{

    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform hotbar;

    [SerializeField]
    List<Item> inventoryItemsSO = new List<Item>();

    List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public InventoryItem currentSelectedItem = null;

    private int currentSelectedSlot = 0;


    // Start is called before the first frame update
    void Start()
    {
        InitialiazeInventoryuUI(8);
        fillInventory();
    }

    // Update is called once per frame
    void Update()
    {
        changeSelectedItem();
        Crop.pickUpCrop = item => addItemToInventory(item);
    }

    public void InitialiazeInventoryuUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(hotbar);
            inventoryItems.Add(item);
        }
    }

    private void fillInventory()
    {
        for (int i = 0; i < inventoryItemsSO.Count; i++)
        {
            inventoryItems[i].GetComponent<InventoryItem>().item = inventoryItemsSO[i]; 
        }
    }

    private void UpdateSelectedItem()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (currentSelectedSlot == i)
            {
                currentSelectedItem = inventoryItems[currentSelectedSlot];
                inventoryItems[currentSelectedSlot].GetComponent<InventoryItem>().selected = true;
            } else
            {
                inventoryItems[i].GetComponent<InventoryItem>().selected = false;
            }
        }
    }

    private void changeSelectedItem()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentSelectedSlot++;

            if (currentSelectedSlot >= inventoryItems.Count)
            {
                currentSelectedSlot = 0;
            }

            UpdateSelectedItem();
        }
        else if (scroll < 0f)
        {
            currentSelectedSlot--;

            if (currentSelectedSlot < 0)
            {
                currentSelectedSlot = inventoryItems.Count - 1;
            }

            UpdateSelectedItem();
        }
    }

    private void addItemToInventory(Item item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (item != null)
            {
                if (checkIfInventoryItemExist(i, item.itemName))
                {
                    addItemIntoInventory(i, item);
                    return;
                }

                if (!checkIfInventorySlotIsTaken(i)) 
                {
                    addNewItemIntoInventory(i, item);
                    return;
                }
            }
        }
    }

    private void addItemIntoInventory(int index, Item item)
    {
        inventoryItems[index].quantity += item.dropQuantity;
    }
    private void addNewItemIntoInventory(int index, Item item)
    {
        inventoryItems[index].item = item;
        inventoryItems[index].quantity += item.dropQuantity;
    }

    private bool checkIfInventorySlotIsTaken(int index)
    {
        return inventoryItems[index].item != null;
    }


    private bool checkIfInventoryItemExist(int index, string itemName)
    {
        Item invItem = inventoryItems[index].GetComponent<InventoryItem>().item;
        if (invItem != null)
        {
            return invItem.itemName == itemName; 
        } else
        {
            return false;
        }
    }
}
