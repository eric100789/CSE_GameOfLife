using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepControl : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private int saturation = 100;
    private float ftime = 0f;
    private GameObject getWolfVision;


    void Start()
    {
        getWolfVision = gameObject.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        ftime += Time.deltaTime;
        if(ftime >= 1f)
        {
            ChangeSaturation(-5);
            ftime = 0f;
            Debug.Log(getWolfVision.GetComponent<SheepVision>().findTarget);
        }
    }

    void ChangeSaturation(int num)
    {
        saturation += num;
        if (num > 150) num = 150;
        else if (num < 0) num = 0;
    }
}


