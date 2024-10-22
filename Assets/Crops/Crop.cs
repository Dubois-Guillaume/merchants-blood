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
    private double factor;
    private int currentSpriteIndex = 0;
    //public GameObject loot;
    private int currentStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        CropControler.Grow += growCrop;
        factor = getGrowthFactor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(sprites.Length);
        }
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
        isHarvestable = true;       
        return sprites.Length - 1 == currentSpriteIndex;
    }

    private void growCrop()
    {
        if (isFinalStage() == true) return;
        advanceCropStage();
        if (isGrowthFactorReached() == true)
        {
            currentSpriteIndex++;
            changeSprite(sprites[currentSpriteIndex]);
        }

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
