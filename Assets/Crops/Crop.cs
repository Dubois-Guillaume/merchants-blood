using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    public Sprite[] sprites;
    public int stages;
    public int daysPerSprite;
    public int growthTimeInDays;
    public bool isHarvestable;
    public string name;
    public SpriteRenderer renderer;
    //public GameObject loot;
    private int currentStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        CropControler.Grow += growCrop;
    }

    // Update is called once per frame
    void Update()
    {
       
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
        return sprites.Length - 1 == currentStage;
    }

    private void growCrop()
    {
        if (isFinalStage() == true) return;
        
        advanceCropStage();
        changeSprite(sprites[currentStage]);

    }
}
