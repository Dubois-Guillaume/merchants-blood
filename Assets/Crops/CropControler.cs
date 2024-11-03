using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropControler : MonoBehaviour
{

    public static Action Grow;
    public Dictionary<Vector3Int, GameObject> placedCrops = new Dictionary<Vector3Int, GameObject>();
    public Tilemap fields;
    public TileBase dryLand;

    private IList<TileBase> tileBases = new List<TileBase>();

    // Start is called before the first frame update
    void Start()
    {
        TimeSystem.Daypassed += growCrop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void growCrop ()
    {
        checkIfSoilWatered();

        Grow.Invoke();

        ResetAllTiles();
    }

    void OnDisable()
    {

    }

    private void checkIfSoilWatered()
    {
        foreach (var plantEntry in placedCrops)
        {
            Vector3Int position = plantEntry.Key;
            GameObject plant = plantEntry.Value;

            // Get the tile at the plant's position
            TileBase groundTile = fields.GetTile(position);

            shouldDeleteCurrentPlant(position, plant);

            // Check if the ground tile matches the watered tile
            if (groundTile.name == "Wetland")
            {
                plant.GetComponent<Crop>().isWatered = true;
            }
            else
            {
                plant.GetComponent<Crop>().isWatered = false;
            }
        }
    }

    // Method to reset all "watered soil" tiles to "dry soil"
    public void ResetAllTiles()
    {
        BoundsInt bounds = fields.cellBounds;

        // Loop through each tile position in the tilemap bounds
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase currentTile = fields.GetTile(position);

                if (currentTile != null && currentTile.name == "Wetland")
                {
                    fields.SetTile(position, dryLand);
                }
            }
        }
    }

    private void shouldDeleteCurrentPlant(Vector3Int pos, GameObject plant)
    {
        if (plant == null)
        {
            placedCrops.Remove(pos);
        }
    }
}


