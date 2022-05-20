using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * 2 * Time.deltaTime);
        if (transform.position.y < -7f || ShipController.Instance.isGameOver == true)
        {
            PoolManager.Instance.Recycle("EnemyBullet", this.gameObject);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ShipController.Instance.LostLife(1);
            PoolManager.Instance.Recycle("EnemyBullet", this.gameObject);
        }
    }
}
