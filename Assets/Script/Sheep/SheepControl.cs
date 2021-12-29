using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SheepControl : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private int saturation = 99;
    [SerializeField] private GameObject diedImage;
    [SerializeField] private GameObject bornedSheep;
    private float ftime = 0f;
    private float waitTimeEat = 0f;
    private bool findFriend = false;
    private GameObject getWolfVision;
    private GameObject getGrassHitBox;
    private GameObject getGrassVision;
    private GameObject isAte;
    private GameObject getAnotherSheep;
    private GameObject touchAnotherSheep;
    private Vector3 getPosition;
    private Vector3 unitVector;
    private GameObject m_targetObject;
    
    void Start()
    {
        getWolfVision = gameObject.transform.GetChild(0).gameObject;
        getGrassHitBox = gameObject.transform.GetChild(1).gameObject;
        getGrassVision = gameObject.transform.GetChild(2).gameObject;
        isAte = gameObject.transform.GetChild(3).gameObject;
        getAnotherSheep = gameObject.transform.GetChild(4).gameObject;
        touchAnotherSheep = gameObject.transform.GetChild(5).gameObject;
    }
    void Update()
    {
        ftime += Time.deltaTime;
        if(ftime >= 1f && !(findFriend)) //持續扣飢餓度
        {
            ChangeSaturation(-5);
            ftime = 0f;
        }

        if(saturation <= 0 || isAte.GetComponent<SheepVision>().findTarget) //餓死了或被殺死了
        {
            Instantiate(diedImage, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }



        if(getWolfVision.GetComponent<SheepVision>().findTarget) //如果發現狼 無情開跑
        {
            Debug.Log(this.gameObject.name + " found a Wolf");
            RunAway(getWolfVision);

        }
        else if(getGrassHitBox.GetComponent<SheepVision>().findTarget) //如果站在草上 等待一秒食用
        {
            Debug.Log(this.gameObject.name + " have to eat a Grass");
            waitTimeEat += Time.deltaTime;
            if(waitTimeEat >= 1f)
            {
                ChangeSaturation(50);
                Debug.Log(this.gameObject.name + " had ate the Grass");
                waitTimeEat = 0f;
            }
        }
        else if (getAnotherSheep.GetComponent<SheepVision>().findTarget && saturation>=100) //飽的時候發現其他羊
        {
            findFriend = true;
            Debug.Log(this.gameObject.name + " found Sheep friend");
            RunTo(getAnotherSheep);
        }
        else if (getGrassVision.GetComponent<SheepVision>().findTarget) //發現有草
        {
            Debug.Log(this.gameObject.name + " found the Grass");
            RunTo(getGrassVision);

        }
        




        if(!getGrassHitBox.GetComponent<SheepVision>().findTarget && waitTimeEat > 0f) //如果吃到一半開跑 那食用時間歸0
        {
                waitTimeEat = 0f;
        }
        if (!getAnotherSheep.GetComponent<SheepVision>().findTarget || saturation<100)
        {
            findFriend = false;
        }
        if(touchAnotherSheep.GetComponent<SheepVision>().findTarget && findFriend)
        {
            saturation /= 2;
            Instantiate(bornedSheep, this.transform.position, this.transform.rotation);
        }


    }

    void ChangeSaturation(int num)
    {
        saturation += num;
        if (saturation > 150) saturation = 150;
        else if (saturation < 0) saturation = 0;
    }

    float GetUnit(float a1 , float a2 ,float b1 , float b2)
    {
        return (a1-a2)/ (float)(Math.Pow( (Math.Pow((a1-a2),2))+(Math.Pow((b1-b2),2)), 0.5));
    }

    void RunAway(GameObject tar)
    {
        m_targetObject = tar.GetComponent<SheepVision>().targetObject;
        getPosition = m_targetObject.GetComponent<Transform>().position;

        unitVector = new Vector3(GetUnit(transform.position.x, getPosition.x , transform.position.y , getPosition.y) , 
                                 GetUnit(transform.position.y, getPosition.y , transform.position.x , getPosition.x) , 0);

        transform.Translate(unitVector.x*0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25), 
                            unitVector.y*0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25) , 0);
    }

    void RunTo(GameObject tar)
    {
        m_targetObject = tar.GetComponent<SheepVision>().targetObject;
        getPosition = m_targetObject.GetComponent<Transform>().position;

        unitVector = new Vector3(GetUnit(transform.position.x, getPosition.x , transform.position.y , getPosition.y) , 
                                 GetUnit(transform.position.y, getPosition.y , transform.position.x , getPosition.x) , 0);

        transform.Translate(unitVector.x*-0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25), 
                            unitVector.y*-0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25) , 0);
    }

}


