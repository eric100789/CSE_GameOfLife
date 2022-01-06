using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject spawnedWolf;
    [SerializeField] private GameObject spawnedSheep;
    [SerializeField] private GameObject spawnedGrass;
    public void WolfSpawn()
    {
        GameObject.Find("WolfNumber").GetComponent<WolfNum>().wolfAdd(1);
        Instantiate(spawnedWolf, new Vector3(0,0,0), this.transform.rotation);
    }
    public void SheepSpawn()
    {
        GameObject.Find("SheepNumber").GetComponent<SheepNum>().sheepAdd(1);
        Instantiate(spawnedSheep, new Vector3(0,0,0), this.transform.rotation);
    }
    public void GrassSpawn()
    {
        GameObject.Find("GrassNumber").GetComponent<GrassNum>().grassAdd(1);
        Instantiate(spawnedGrass, new Vector3(UnityEngine.Random.Range(-10f,10f),UnityEngine.Random.Range(-4f,4f),0), this.transform.rotation);
    }
}
