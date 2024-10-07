using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crop : MonoBehaviour
{
    public int timeToGrow = 600;
    public int count = 0;

    public List<Sprite> sprites;
    public List<int> growthStageTime;

    private double currentTime = 0;
    public SpriteRenderer renderer;

    void Start()
    {
        currentTime = 0;
    }

    void tick()
    {
        currentTime += 0.01;

        if (currentTime > growthStageTime[count] && count < sprites.Count)
        {
            count++;
            changeSprite();
        }
    }

    void changeSprite()
    {
        renderer.sprite = sprites[count];
    }

    void Update()
    {
        if (currentTime < timeToGrow) {
        tick();
        }
    }
}

// Une graine de plante peut �tre planter
