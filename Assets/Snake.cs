using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Snake : MonoBehaviour {

	Vector2 dir = Vector2.right; //starting direction
	Vector2 currentDir;

	List<Transform> tail = new List<Transform>(); // List of tail pieces

	public bool ate = false; //did snek just ate?
	public bool ateUpdate = false;

	public GameObject tailPrefab; //assign tail prefab later
	public Text Score;				// Displays "Score: x"
	private int currentScore = 0;	// Keeps track

	public float speed = 0.2f;

	// Use this for initialization
	void Start () {
		// call "move" function every speed
		//InvokeRepeating(Move(), speed, speed); old move update
		StartCoroutine(MoveUpdate());
		currentDir = dir;

		Score.text = "Score: " + currentScore; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow) && currentDir != Vector2.left){
			dir = Vector2.right;
		} else if (Input.GetKey(KeyCode.DownArrow) && currentDir != Vector2.up){
			dir = -Vector2.up;
		} else if (Input.GetKey(KeyCode.LeftArrow)&& currentDir != Vector2.right){
			dir = -Vector2.right;
		} else if (Input.GetKey(KeyCode.UpArrow)&& currentDir != Vector2.down){
			dir = Vector2.up;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			speed/=5;
		} else if(Input.GetKeyUp(KeyCode.LeftShift)){
			speed*=5;
		}

		Score.text = "Score: " + currentScore; 
	}

	void Move() {
		// save current pos, (where gap is)
		Vector2 v = transform.position;

		transform.Translate(dir);

		if(ate) {
			GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
			currentScore += 1;				// 1 point to Snek for eating
			tail.Insert(0, g.transform);
			ate = false;
		}

		if (tail.Count > 0) {
			tail.Last().position = v;

			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
		currentDir = dir;
	}

	IEnumerator MoveUpdate() {
		for(;;){
			Move();
			yield return new WaitForSeconds(speed);
		}

	}

	void OnTriggerEnter2D(Collider2D coll){
		//food?
		if (coll.name.StartsWith("FoodPrefab")) {
			ate = true;
			ateUpdate = true;
			Destroy(coll.gameObject);
		} else {
			//TODO (lose screen)
		}
	}
}
