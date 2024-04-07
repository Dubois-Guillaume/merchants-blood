using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crop : MonoBehaviour
{
    public int timeToGrow = 10;
    public int count = 0;

    public List<Sprite> sprites;
    public List<int> growthStageTime;

    private double currentTime = 0;
    public SpriteRenderer renderer;

    // Start is called before the first frame update

    /* 
        Create the crop 
        Update tick
            if the current time is higher than growth stage then + 1
        changeSprite method
    */

    void Start()
    {
        currentTime = 0;
    }

    void tick()
    {
        currentTime += 0.01;

        if (currentTime >= growthStageTime[count] && count < sprites.Count)
        {
            count++;
            changeSprite();
        }
    }

    void changeSprite()
    {
        renderer.sprite = sprites[count];
    }

    // Update is called once per frame
    void Update()
    {
        tick();
    }
}
