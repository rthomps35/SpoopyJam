using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField] GameManager GM;
	[SerializeField] Rigidbody2D rigidbody2D;
	[SerializeField] Camera gameCamera;
	[SerializeField] BoxCollider2D playerCollider;
	public float speed; //This should be the max speed probably? Right and just work up to it
	[SerializeField] float baseSpeed;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		playerMovementAndControl();
		eatSugar();	//This causes the up and down to lock totally?
		
	}

	void playerMovementAndControl()
	{

		//It reall needs a ramp up to the proper speed
		//Its way too floaty right now but it can be fixed later. It needs to slowly scale the speed up just like it slows down

		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
		{
			rigidbody2D.linearVelocity = new Vector2(-speed, speed);   //left up
		}
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
		{
			rigidbody2D.linearVelocity = new Vector2(speed, speed);   //right up
		}
		else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
		{
			rigidbody2D.linearVelocity = new Vector2(-speed, -speed);   //left down
		}
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
		{
			rigidbody2D.linearVelocity = new Vector2(speed, -speed);   //right down
		}
		else if (Input.GetKey(KeyCode.W))
		{
			//face up
			rigidbody2D.linearVelocity = new Vector2(0.0f, speed);    //up
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rigidbody2D.linearVelocity = new Vector2(0.0f, -speed);   //down
		}
		else if (Input.GetKey(KeyCode.A))
		{
			rigidbody2D.linearVelocity = new Vector2(-speed, 0.0f);   //left
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rigidbody2D.linearVelocity = new Vector2(speed, 0.0f);    //right
		}

		else if (Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.D) != true)
		{
			rigidbody2D.linearVelocity = rigidbody2D.linearVelocity / 1.005f;   //Grabbed thos line from stack overflow and i'm shocked it works. I need to find a way to reverse this without exceeding speed.
		}

		Camera.main.transform.position = new Vector3(rigidbody2D.position.x, rigidbody2D.position.y, -10);
		//Debug.Log(rigidbody2D.linearVelocity);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.CompareTag("Sugar"))
		{
			//Debug.Log("I hit Candy!");
			GM.PlayerSugar += 1;
			GM.updateUICount(0);	//Insert the type of candy
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Choco"))
		{
			//Debug.Log("I hit Candy!");
			GM.PlayerChoco += 1;
			GM.updateUICount(1);    //Insert the type of candy
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Hard"))
		{
			//Debug.Log("I hit Candy!");
			GM.PlayerHard += 1;
			GM.updateUICount(2);    //Insert the type of candy
			Destroy(collision.gameObject);
		}
	}

	void eatSugar()
	{
		if (GM.PlayerSugar != 0 && Input.GetKey(KeyCode.Alpha1)) 
		{
			
			GM.PlayerSugar -= 1;
			StartCoroutine(SpeedBoost());
			GM.updateUICount(0);    //Insert the type of candy
		}
	}

	private IEnumerator SpeedBoost()
	{
		speed = baseSpeed * (float)1.5;
		yield return new WaitForSeconds(5);
		speed = baseSpeed;
	}
}
