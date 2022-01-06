using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDie : MonoBehaviour
{
    [SerializeField] private GameObject grassDie;
    private float ftime = 0f;
    private bool beenEat = false;

    void FixedUpdate()
    {
        if(beenEat)
        {
            ftime += Time.deltaTime;
            if(ftime >= 0.1f)
            {
                Instantiate(grassDie, this.transform.position, this.transform.rotation);
                beenEat = false;
                ftime = 0;
                Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Sheep")
        {
            if( other.gameObject.GetComponent<SheepControl>().waitTimeEat >= 0.9f )
            {
                beenEat = true;
            }
        }


    }

}


