using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float timer;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * 1 * Time.deltaTime);
        timer = timer + Time.deltaTime;


        if (timer > 3f)
        {
            SpawnManager.Instance.SpawnFire(this.transform.position);
            timer = 0;
        }
        
        if (transform.position.y < -7f || ShipController.Instance.isGameOver == true)
        {
            PoolManager.Instance.Recycle("Enemy", this.gameObject);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ShipController.Instance.LostLife(1);
            PoolManager.Instance.Recycle("Enemy", this.gameObject);
        }
    }
}
