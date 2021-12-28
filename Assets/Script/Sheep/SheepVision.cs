using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepVision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    public bool findTarget = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject == targetObject)
        {
            findTarget = true;
            Debug.Log(targetObject.name+" Detected");
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject == targetObject)
        {
            findTarget = false;
            Debug.Log(targetObject.name+" Exited");
        }
    }
}
