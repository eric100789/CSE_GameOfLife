using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepVision : MonoBehaviour
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
            Debug.Log(other.gameObject.name+" Detected");
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == targetObjectTag)
        {
            findTarget = false;
            Debug.Log(other.gameObject.name+" Exited");
        }
    }
}
