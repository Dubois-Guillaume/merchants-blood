using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class playerControls : MonoBehaviour
{

    public GameObject player;
    public Tilemap tilemap;
    public GameObject crop;
    public Slider energySlider;
    public Slider waterSlider;
    public GameObject cursor;
    public CropControler cropController;

    public Tile WetLand;

    private float energyLevel = 100;
    private float waterLevel = 100;

    private static string FARMLAND = "Farmland";
    private static string WETLAND = "Wetland";
    private enum actions
    {
        HARVEST,
        WATER,
        PLANT,
        REFILL
    }

    private inventorySystem inventorySystem;

    private List<string> plantableTiles = new(new string[] { FARMLAND, WETLAND });

    // Start is called before the first frame update
    void Start()
    {
        inventorySystem = GetComponent<inventorySystem>();
    }

    // Update is called once per frame
    void Update() 
    { 
        if (energyLevel <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            switch(inventorySystem.currentSelectedItem.itemName)
            {
                case "Bucket":
                    waterTile();
                    fillWaterBucket();
                    break;
                case "Hoe":
                    plantCrop();
                    break;
                default:
                    selectCurrentCursorEntity();
                    break;
            }
        };

        updateUi();
    }

    private void updateUi()
    {
        energySlider.value = (energyLevel - 10f) / (100f - 10f);
        waterSlider.value = (waterLevel - 10f) / (100f - 10f);
    }

    private void fillWaterBucket()
    {
        if (getTileNameAtCurrentCursorPosition() == "water")
        {
            waterLevel = 100;
        }
    }

    private void waterTile()
    {
        if (getTileNameAtCurrentCursorPosition() == FARMLAND && waterLevel >= 10)
        {
            setCurrentTile(WETLAND);
            waterLevel -= 10;
            player.GetComponent<Animator>().SetTrigger("watering");
        }
    }

    private void setCurrentTile(string tileName)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(cursor.transform.position);
        tilemap.SetTile(gridPosition, WetLand);
    }

    private string getTileNameAtCurrentCursorPosition()
    {
        // Convertir la position du monde en position de grille
        Vector3Int gridPosition = tilemap.WorldToCell(cursor.transform.position);

        return tilemap.GetTile(gridPosition).name;

    }
    private void plantCrop()
    {

        if (getTileNameAtCurrentCursorPosition() != null && plantableTiles.Contains(getTileNameAtCurrentCursorPosition()))
        {

            // Convertir la position du monde en position de grille
            Vector3Int gridPosition = tilemap.WorldToCell(cursor.transform.position);

            if (cropController.placedCrops.ContainsKey(gridPosition)) return;

            Vector3 tileCenterPos = tilemap.GetCellCenterWorld(gridPosition);

            GameObject newCrop = Instantiate(crop, tileCenterPos, Quaternion.identity);
            cropController.placedCrops[gridPosition] = newCrop;
        }
    }

    private void selectCurrentCursorEntity()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (isCropHarvestable(hit.collider.gameObject) && hit.collider.gameObject.tag.Contains("Crop"))
            {
                Crop crop = hit.collider.gameObject.GetComponent<Crop>();
                harvestCrop(crop); 
            };
        }

    }

    private void harvestCrop(Crop crop)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(cursor.transform.position);
        cropController.placedCrops.Remove(gridPosition);

        crop.deleteSelf();
    }

    private bool isCropHarvestable(GameObject gameObject)
    {
        Crop crop = gameObject.GetComponent<Crop>();
        return crop.isHarvestable;
    }
}
