using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEat : MonoBehaviour
{
    [SerializeField] private string targetObjectTag;
    public bool findTarget = false;
    public GameObject targetObject;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == targetObjectTag)
        {
            findTarget = true;
            targetObject = other.gameObject;
            other.GetComponent<SheepControl>().DieImage();
        }
    }

}