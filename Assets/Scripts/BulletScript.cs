using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{ 
	#region PUBLIC VARIABLES
	// The bullet's speed in Unity units.
	public float speed = 7f;
	#endregion


	#region PRIVATE VARIABLES
	private Camera mainCamera;
	#endregion


	#region MONOBEHAVIOUR METHODS
	void Start()
	{
		mainCamera = Camera.main;
	}


	void Update()
	{
		Vector3 newPosition = transform.position + transform.up * speed * Time.deltaTime;
		newPosition.z = 0f;
		transform.position = newPosition;


		if (transform.position.y > 6f )
        {
            PoolManager.Instance.Recycle("Bullet", this.gameObject);
        }
	}
	#endregion


	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 8)
		{			
			ShipController.Instance.UpdateScore(10);
			PoolManager.Instance.Recycle("Enemy", collision.gameObject);
			PoolManager.Instance.Recycle("Bullet", this.gameObject);
		}
		if (collision.gameObject.layer == 9)
		{			
			PoolManager.Instance.Recycle("EnemyBullet", collision.gameObject);
			PoolManager.Instance.Recycle("Bullet", this.gameObject);
		}
		Debug.Log("colliding");
	}




    #region PUBLIC METHODS
    // Set the position of the bullet.
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    #endregion

}
