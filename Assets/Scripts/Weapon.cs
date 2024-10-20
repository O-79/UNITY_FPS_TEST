using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject BULLET_PREFAB;
    public Transform BULLET_SPAWN;
    public float BULLET_SPEED = 48f;
    public float BULLET_LIFETIME = 4f;

    // Update is called once per frame
    void Update()
    {
        // shoot on left mouse click
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SHOOT();
        }    
    }

    private void SHOOT()
    {
        // instiantiate bullet
        GameObject BULLET = Instantiate(BULLET_PREFAB, BULLET_SPAWN.position, Quaternion.identity);

        // add force to (shoot) bullet
        BULLET.GetComponent<Rigidbody>().AddForce(BULLET_SPAWN.forward.normalized * BULLET_SPEED, ForceMode.VelocityChange);

        // destroy bullet eventually
        StartCoroutine(STOP(BULLET, BULLET_LIFETIME));
    }

    private IEnumerator STOP(GameObject BULLET, float BULLET_LIFETIME)
    {
        yield return new WaitForSeconds(BULLET_LIFETIME);

        Destroy(BULLET);
    }
}
