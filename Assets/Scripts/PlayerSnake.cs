using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.Networking;
using Mirror;


public class PlayerSnake : NetworkBehaviour
{

    // Snek variables:
	public GameObject tailPrefab; //assign tail prefab later
	List<Transform> tail = new List<Transform>(); // List of tail pieces
	public float speed;

	bool onTheMove;
	public bool ate; //did snek just ate?
	public bool ateUpdate;
	Vector2 dir = Vector2.right; //starting direction
	Vector2 currentDir;

	AudioSource asWalking;
	AudioSource asFoodPickup;
    //public Rigidbody2D rigidbody2d;

    // Use this for initialization.. When a second player has joined?
	void Start () {
		// call "move" function every speed
		//InvokeRepeating(Move(), speed, speed); old move update
		onTheMove = true;
		speed = PlayerPrefs.GetFloat("speed");
		ate = false;
		ateUpdate = false;
		transform.position = new Vector2(0, 0);
		StartCoroutine(MoveUpdate());
		currentDir = dir;

		//asWalking = GetComponents<AudioSource>()[0];
		//asFoodPickup = GetComponents<AudioSource>()[1];
	}

    // need to use FixedUpdate for rigidbody
    void FixedUpdate()
    {
        // only let the local player control the snake.
        // don't control other player's snakes
        if (isLocalPlayer) {
        Debug.Log("Player is Local");
        if (Input.GetKey(KeyCode.RightArrow) && currentDir != Vector2.left){
			dir = Vector2.right;
		} else if (Input.GetKey(KeyCode.DownArrow) && currentDir != Vector2.up){
			dir = -Vector2.up;
		} else if (Input.GetKey(KeyCode.LeftArrow)&& currentDir != Vector2.right){
			dir = -Vector2.right;
		} else if (Input.GetKey(KeyCode.UpArrow)&& currentDir != Vector2.down){
			dir = Vector2.up;
		}
            //rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
        }
    }

    IEnumerator MoveUpdate() {
		for(;;){
			Move();
			yield return new WaitForSeconds(speed);
			if (!onTheMove){
				break;
			}
			//asWalking.Play(); // Play walking sound
		}
	}

    void Move() {
		// save current pos, (where gap is)
		Vector2 v = transform.position;

		transform.Translate(dir);

		if(ate) {
			GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
			//currentScore += 1;				// 1 point to Snek for eating
			//asFoodPickup.Play(); // Play food pickup sound
			tail.Insert(0, g.transform);
			ate = false;
		} else if (tail.Count > 0) {
			tail.Last().position = v;
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
		currentDir = dir;
	}
}

