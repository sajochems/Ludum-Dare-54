using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DepletionManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap depletionMap;

    [SerializeField]
    private float maxDepletion, depletionAmount, depletionIntervall = 1f;

    [SerializeField]
    private float depletionFallOff;

   

    private Dictionary<Vector3Int, float> depletionTiles = new Dictionary<Vector3Int, float>();

    public void AddDepletion(Vector2 worldPosition, float depletionAmount, int radius)
    {
        Vector3Int gridPosition = depletionMap.WorldToCell(worldPosition);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if(distanceFromCenter <= radius)
                {
                    Vector3Int nextTilePosition = new Vector3Int(gridPosition.x + x, gridPosition.y + y, 0);
                    ChangeDepletion(nextTilePosition, depletionAmount - (distanceFromCenter * depletionFallOff * depletionAmount));
                }
            }
        }

        ChangeDepletion(gridPosition, depletionAmount);
    }

    private void ChangeDepletion(Vector3Int gridPosition, float changeBy)
    {
        float newValue = depletionTiles[gridPosition] + changeBy;

        depletionTiles[gridPosition] = Mathf.Clamp(newValue, 0f, maxDepletion);
    }

    private void Start()
    {
        depletionMap.CompressBounds();
        BoundsInt bounds = depletionMap.cellBounds;
        int counter = 0;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int z = bounds.min.z; z < bounds.max.z; z++)
                {
                    Vector3Int gridPosition = new Vector3Int(x, y, z);

                    if (!depletionTiles.ContainsKey(gridPosition))
                    {
                        depletionTiles.Add(gridPosition, 0f);
                        counter++;
                    }
                }
            }

        }

        Debug.Log(counter + " tiles");
        StartCoroutine(DepletionRoutine());
    }

    private IEnumerator DepletionRoutine()
    {
        while (true)
        {
            Dictionary<Vector3Int, float> depletionTilesCopy = new Dictionary<Vector3Int, float>(depletionTiles);

            foreach (var depletionTile in depletionTilesCopy)
            {
                ChangeDepletion(depletionTile.Key, Random.Range(0, depletionAmount));
            }
                
            yield return new WaitForSeconds(depletionIntervall);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPosition = depletionMap.WorldToCell(mousePosition);

            Debug.Log("Depletion value: " + GetDepletionValue(gridPosition));
        }
    }

    private void RemoveDepletion(Vector3Int gridPosition)
    {
        depletionTiles[gridPosition] = 0f;
    }

    private float GetDepletionValue(Vector3Int gridPosition)
    {
        if(depletionTiles.ContainsKey(gridPosition))
            return depletionTiles[gridPosition];
        else
            return 0f;
    }

}
