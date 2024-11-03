using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crop : MonoBehaviour
{

    public Sprite[] sprites;
    public int growthDuration;
    public bool isHarvestable;
    public string name;
    public SpriteRenderer renderer;
    public bool isWatered = false;
    public BoxCollider2D collider2D;
    public Item item;
    public Item itemSeed;

    public static Action<Item> pickUpCrop;

    private double factor;
    private int currentSpriteIndex = 0;
    //public GameObject loot;
    private int currentStage = 0;
    private bool isDoneGrowing;

    // Start is called before the first frame update
    void Start()
    {
        CropControler.Grow += growCrop;
        factor = getGrowthFactor();
        collider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void deleteSelf() 
    { 
        pickUpCrop.Invoke(item);

        Destroy(this.gameObject);
    }

    public int getCurrentStage()
    {
        return currentStage;
    }

    public void setCurrentStage(int newStage)
    {
        currentStage = newStage;
    }

    private void advanceCropStage()
    {
        currentStage++;
    }

    private void changeSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    private bool isFinalStage()
    {
        return sprites.Length == currentSpriteIndex + 1;
    }

    private void growCrop()
    {
        if (isDoneGrowing) return;

        if (isWatered)
        {
            advanceCropStage();
        }

        if (isGrowthFactorReached() == true && isWatered)
        {
            currentSpriteIndex++;
            changeSprite(sprites[currentSpriteIndex]);

            if (isFinalStage() == true) lockCropState();
        }

    }

    private void lockCropState()
    {
        isHarvestable = true;
        isDoneGrowing = true;
        collider2D.enabled = true;
    }

    private double getGrowthFactor()
    {
        double factor = growthDuration / sprites.Length;
        return Math.Floor(factor);
    }

    private bool isGrowthFactorReached()
    {
        return currentStage % factor == 0;
    }
}
