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
    private float randomValue_x;
    private float randomValue_y;
    private float ftime = 0f;
    public float waitTimeEat = 0f;
    private float savedTime = 2.1f;
    private bool findFriend = false;
    private Vector2 memoryWolfPos;
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
        randomValue_x = UnityEngine.Random.Range(-50f,50f);
        randomValue_y = UnityEngine.Random.Range(-50f,50f);
        memoryWolfPos = new Vector2(0,0);
    }
    void FixedUpdate()
    {
        ftime += Time.deltaTime;
        savedTime += Time.deltaTime;
        if(ftime >= 1f && !(findFriend)) //持續扣飢餓度
        {
            ChangeSaturation(-5);
            ftime = 0f;
        }

        if(saturation <= 0 || isAte.GetComponent<SheepVision>().findTarget) //餓死了或被殺死了
        {
            DieImage();
        }



        if(getWolfVision.GetComponent<SheepVision>().findTarget) //如果發現狼 無情開跑
        {
            savedTime = 0f;
            memoryWolfPos = new Vector2( getWolfVision.GetComponent<SheepVision>().targetObject.GetComponent<Transform>().position.x , 
                                         getWolfVision.GetComponent<SheepVision>().targetObject.GetComponent<Transform>().position.y);

            RunAway(getWolfVision);

        }
        else if (savedTime <= 2f) //遠離剛剛狼的地方
        {
            RunOnPosition(memoryWolfPos.x,memoryWolfPos.y);
        }
        else if(getGrassHitBox.GetComponent<SheepVision>().findTarget) //如果站在草上 等待一秒食用
        {
            waitTimeEat += Time.deltaTime;
            if(waitTimeEat >= 1f)
            {
                ChangeSaturation(50);
                waitTimeEat = 0f;
            }
        }

        else if (getAnotherSheep.GetComponent<SheepVision>().findTarget && saturation>=100) //飽的時候發現其他羊
        {
            findFriend = true;
            RunTo(getAnotherSheep);
        }
        else if (getGrassVision.GetComponent<SheepVision>().findTarget) //發現有草
        {
            RunTo(getGrassVision);
        }
        else //亂走
        {
            if (ftime >= 0.25f && ftime<=0.251f || ftime >= 0.75f && ftime<=0.751f)
            {
                randomValue_x = UnityEngine.Random.Range(-50f,50f);
                randomValue_y = UnityEngine.Random.Range(-50f,50f);
            }
            RunOnPosition(randomValue_x,randomValue_y);

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
            GameObject.Find("SheepNumber").GetComponent<SheepNum>().sheepAdd(1);
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

    void RunOnPosition(float x , float y)
    {
        unitVector = new Vector3( GetUnit(0f, x , 0f , y) , GetUnit(0f, y , 0f , x) );

        transform.Translate(unitVector.x*0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25), 
                            unitVector.y*0.0001f*speed*(float)Math.Pow(saturation*0.1,0.25) , 0);
    }

    public void DieImage()
    {
        Instantiate(diedImage, this.transform.position, this.transform.rotation);
        GameObject.Find("SheepNumber").GetComponent<SheepNum>().sheepAdd(-1);
        Destroy(this.gameObject);
    }

}


