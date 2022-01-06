using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassNum : MonoBehaviour
{
    public int GrassNumbers = 18;
    private GameObject showNum;

    void Start() 
    {
        GetComponent<Text>().text = "Grass : "+GrassNumbers;
    }
    public void grassAdd(int num)
    {
        GrassNumbers += num;
        GetComponent<Text>().text = "Grass : "+GrassNumbers;
    }
}
