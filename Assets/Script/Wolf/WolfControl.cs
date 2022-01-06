using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WolfControl : MonoBehaviour
{
    private float ftime = 0f;
    private bool findFriend = false;
    [SerializeField] private float speed; 
    [SerializeField] private int saturation = 199;
    [SerializeField] private GameObject diedImage;
    [SerializeField] private GameObject bornedWolf;
    private GameObject getSheepVision;
    private GameObject touchAnotherWolf;
    private GameObject touchSheep;
    private GameObject m_targetObject;
    private GameObject getFriendVision;
    private GameObject wallVision;
    private Vector3 getPosition;
    private Vector3 unitVector;
    private float randomValue_x;
    private float randomValue_y;
    private float runTime = 1f;



    
    void Start()
    {
        getSheepVision = gameObject.transform.GetChild(0).gameObject;
        touchSheep = gameObject.transform.GetChild(1).gameObject;
        getFriendVision = gameObject.transform.GetChild(2).gameObject;
        touchAnotherWolf = gameObject.transform.GetChild(3).gameObject;
        wallVision = gameObject.transform.GetChild(4).gameObject;
        randomValue_x = UnityEngine.Random.Range(-50f,50f);
        randomValue_y = UnityEngine.Random.Range(-50f,50f);
        
    }
    void FixedUpdate()
    {
        ftime += Time.deltaTime;
        if(ftime >= 1f && !(findFriend)) //持續扣飢餓度
        {
            ChangeSaturation(0);
            ftime = 0f;
        }

        if(saturation <= 0) //餓死了
        {
            Instantiate(diedImage, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }



        if(touchSheep.GetComponent<WolfEat>().findTarget) //吃到羊
        {
            ChangeSaturation(100);
            touchSheep.GetComponent<WolfEat>().findTarget = false;
            runTime = 1f;
        }
        else if(getSheepVision.GetComponent<SheepVision>().findTarget) //如果發現羊 無情開跑
        {
            runTime += Time.deltaTime*0f;
            RunToEat(getSheepVision);
        }
        else if(wallVision.GetComponent<SheepVision>().findTarget) //如果發現牆壁
        {
            RunToPovit();
        }
        else //亂走
        {
            if (ftime >= 0.25f && ftime<=0.251f || ftime >= 0.50f && ftime<=0.501f || ftime >= 0.75f && ftime<=0.751f)
            {
                randomValue_x = UnityEngine.Random.Range(-50f,50f);
                randomValue_y = UnityEngine.Random.Range(-50f,50f);
            }
            RunOnPosition(randomValue_x,randomValue_y);

        }



        if (!getFriendVision.GetComponent<SheepVision>().findTarget || saturation<100)
        {
            findFriend = false;
        }
        if(touchAnotherWolf.GetComponent<SheepVision>().findTarget && findFriend)
        {
            saturation /= 2;
            Instantiate(bornedWolf, this.transform.position, this.transform.rotation);
        }


    }

    void ChangeSaturation(int num)
    {
        saturation += num;
        if (saturation > 300) saturation = 300;
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

        transform.Translate(unitVector.x*0.0001f*speed*1.5f, 
                            unitVector.y*0.0001f*speed*1.5f , 0);
    }

    void RunToEat(GameObject tar)
    {
        m_targetObject = tar.GetComponent<SheepVision>().targetObject;
        if(m_targetObject != null)
        {
            getPosition = m_targetObject.GetComponent<Transform>().position;

            unitVector = new Vector3(GetUnit(transform.position.x, getPosition.x , transform.position.y , getPosition.y) , 
                                    GetUnit(transform.position.y, getPosition.y , transform.position.x , getPosition.x) , 0);

            transform.Translate(unitVector.x*-0.0001f*speed*(float)Math.Pow(runTime*3,0.25), 
                                unitVector.y*-0.0001f*speed*(float)Math.Pow(runTime*3,0.25) , 0);
        }
        else
        {
            getSheepVision.GetComponent<SheepVision>().findTarget = false;
        }

    }

    void RunTo(GameObject tar)
    {
        m_targetObject = tar.GetComponent<SheepVision>().targetObject;
        getPosition = m_targetObject.GetComponent<Transform>().position;

        unitVector = new Vector3(GetUnit(transform.position.x, getPosition.x , transform.position.y , getPosition.y) , 
                                 GetUnit(transform.position.y, getPosition.y , transform.position.x , getPosition.x) , 0);

        transform.Translate(unitVector.x*-0.0001f*speed*1.5f, 
                            unitVector.y*-0.0001f*speed*1.5f , 0);
    }

    void RunOnPosition(float x , float y)
    {
        unitVector = new Vector3( GetUnit(0f, x , 0f , y) , GetUnit(0f, y , 0f , x) );

        transform.Translate(unitVector.x*0.0001f*speed, 
                            unitVector.y*0.0001f*speed , 0);
    }

    void RunToPovit()
    {
        unitVector = new Vector3(GetUnit(transform.position.x, 0 , transform.position.y , 0) , 
                                 GetUnit(transform.position.y, 0 , transform.position.x , 0) , 0);

        transform.Translate(unitVector.x*-0.0001f*speed*1.5f, 
                            unitVector.y*-0.0001f*speed*1.5f , 0);
    }

}
