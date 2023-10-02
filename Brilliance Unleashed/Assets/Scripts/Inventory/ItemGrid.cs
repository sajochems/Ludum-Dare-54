using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 32f;
    public const float tileSizeHeight = 32f;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    [SerializeField] 
    public int gridSizeWidth;
    [SerializeField] 
    public int gridSizeHeight;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Resize(gridSizeWidth, gridSizeHeight);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        CleanGridReference(toReturn);

        return toReturn;
    }

    public void CleanGridReference(InventoryItem item)
    {
        if(item == null) { return; }

        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }
     
    public void Resize(int width, int height)
    {
        InventoryItem[,] old = inventoryItemSlot;
        inventoryItemSlot = new InventoryItem[width, height];
        if (old != null)
        {
            for(int y = 0; y<gridSizeHeight; y++)
            {
                for(int x = 0; x<gridSizeWidth; x++)
                {
                    inventoryItemSlot[x, y] = old[x, y];
                }
            }
        }
        gridSizeWidth = width;
        gridSizeHeight = height;

        Vector2 size = new Vector2(gridSizeWidth * tileSizeWidth, gridSizeHeight * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    public InventoryItem FindRequiredItem(string name)
    {
        InventoryItem current = null;
        //loop over all items in grid and return one 
        for (int y = 0; y < gridSizeHeight; y++)
        {
            for (int x = 0; x < gridSizeWidth; x++)
            {
                current = inventoryItemSlot[x, y];
                if(current == null) { continue; }
                if (current.itemData.nameID.Equals(name))
                {
                    return current;
                }
            }
        }
        return current;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int) (positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;

    }

    //possibly not used for our game
    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.HEIGHT + 1;
        int width = gridSizeWidth - itemToInsert.WIDTH + 1;
        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x,y, itemToInsert.WIDTH, itemToInsert.HEIGHT))
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (!BoundryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT))
        {
            return false;
        }

        if (!OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        //Can be altered for different shapes
        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for(int x = 0; x<width; x++)
        {
            for(int y = 0; y< height; y++)
            {
                if(inventoryItemSlot[posX+x, posY+y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    } else
                    {
                        if(overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                    
                }
            }
        }
        
        return true;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if(!PositionCheck(posX, posY)) { return false; }

        //TODO change for weird items
        posX += width-1;
        posY += height-1;

        if(!PositionCheck(posX, posY)) { return false;  }

        return true;
    }
}
