using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepControl : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private int saturation = 100;
    [SerializeField] private GameObject diedImage;
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
        }

        if(saturation <= 0)
        {
            Debug.Log(this.gameObject.name + " died because of hungry.");
            Instantiate(diedImage, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }



    }

    void ChangeSaturation(int num)
    {
        saturation += num;
        if (num > 150) num = 150;
        else if (num < 0) num = 0;
    }
}


