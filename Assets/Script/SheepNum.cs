using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepNum : MonoBehaviour
{
    public int SheepNumbers = 6;
    private GameObject showNum;

    void Start() 
    {
        GetComponent<Text>().text = "Sheep : "+(SheepNumbers);
    }
    public void sheepAdd(int num)
    {
        SheepNumbers += num;
        GetComponent<Text>().text = "Sheep : "+(SheepNumbers);
    }
}
