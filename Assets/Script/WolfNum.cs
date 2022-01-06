using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfNum : MonoBehaviour
{
    public int WolfNumbers = 1;
    private GameObject showNum;

    void Start() 
    {
        GetComponent<Text>().text = "Wolf : "+WolfNumbers;
    }
    public void wolfAdd(int num)
    {
        WolfNumbers += num;
        GetComponent<Text>().text = "Wolf : "+WolfNumbers;
    }
}
