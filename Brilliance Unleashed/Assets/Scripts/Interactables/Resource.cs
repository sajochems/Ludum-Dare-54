using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable
{

    [SerializeField]
    ItemGrid itemGrid;

    InventoryItem selectedItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    [SerializeField] AudioSource audioSource;

    private float time = 0.0f;
    public float itemSpawnCooldown;

    bool active = false;

    public void Start()
    {
        itemGrid.gameObject.SetActive(active);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time >= itemSpawnCooldown)
        {
            time = 0;
            InsertRandomItem();
        }
    }

    
    protected override void Interact()
    {
        itemGrid.gameObject.SetActive(!active);
        active = !active;

        if(audioSource != null) audioSource.PlayOneShot(audioSource.clip, 1);
    }

    protected override void LeaveSpace()
    {
        itemGrid.gameObject.SetActive(false);
        active = !active;
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void InsertRandomItem()
    {
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        bool result = InsertItem(itemToInsert);
        if (!result)
        {
            Destroy(itemToInsert.gameObject);
        }
    }

    private bool InsertItem(InventoryItem itemToInsert)
    {
        if (itemGrid == null) { return false; }

        Vector2Int? posOnGrid = itemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null) { return false; }

        itemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);

        return true;
    }


}
