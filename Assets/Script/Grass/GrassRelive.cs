using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRelive : MonoBehaviour
{
    private float ftime = 0f;
    [SerializeField] private GameObject grassBorned;

    void FixedUpdate()
    {
        ftime += Time.deltaTime;
        if(ftime >= 5f) 
        {
            Instantiate(grassBorned, this.transform.position, this.transform.rotation);
            ftime = 0f;
            Destroy(this.gameObject);
            
        }
    }
}
