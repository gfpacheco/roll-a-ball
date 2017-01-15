using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Rigidbody player;
	private int pickUpCount;
	private float g;

	public float speed;
	public Text countText;
	public Text winText;
	public Button resetButton;

	void Start () {
		player = GetComponent<Rigidbody> ();
		pickUpCount = 0;
		g = 9.8f;
		UpdateCountText ();
		winText.text = "";
		resetButton.gameObject.SetActive (false);
	}

	void Update () {

	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		Vector3 relativeGravity = new Vector3 (
			                          Input.acceleration.x,
			                          0,
			                          Input.acceleration.y
		                          ) * g;

		player.AddForce (movement * speed);
		player.AddForce (relativeGravity * speed, ForceMode.Acceleration);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive(false);
			pickUpCount++;
			UpdateCountText ();
			if (pickUpCount == 10) {
				winText.text = "You win!";
				resetButton.gameObject.SetActive (true);
			}
		}
	}

	void UpdateCountText() {
		countText.text = "Pick Ups: " + pickUpCount.ToString ();
	}

	public void ResetGame() {
		SceneManager.LoadScene ("Minigame");
	}

}
