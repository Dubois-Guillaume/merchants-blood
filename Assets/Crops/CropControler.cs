using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropControler : MonoBehaviour
{

    public static event Action Grow;

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
        Grow.Invoke();
    }

    void OnDisable()
    {

    }
}
