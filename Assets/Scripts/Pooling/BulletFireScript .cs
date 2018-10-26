using UnityEngine;
using System.Collections;
public class BulletFireScript : MonoBehaviour
{
    public float fireTime = 0.05f;
    void Start()
    {
        InvokeRepeating("Fire", fireTime, fireTime);
    }
    void Fire()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null)
        {
            return;
        }
        // Position the bullet
        obj.SetActive(true);
    }
}