using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField]
	private int _health = 100;
	public int health {
		get {
			return _health;
		} set {
			_health = value;
		}
	}
	[SerializeField]
	private int _damage = 15;
	public int damage {
		get {
			return _damage;
		} set {
			_damage = value;
		}
	}

	public UISystem uiSystem;
	public float speed;
	public float min;
	public float max;
	private float inputMove;
	private AudioSource _audi;
	public AudioSource audi {
		get {
			return _audi;
		}
	}

	private void DecreaseHealth () {
		health -= damage;
		print ("P health: " + health);
		if (health <= 0) {
			health = 0;
			print ("you lose");
			uiSystem.SetPanelStart (true);
			uiSystem.SetWin (false);
			uiSystem.SetLose (true);
			audi.Stop ();
			Time.timeScale = 0;
		}
	}

	private void Start () {
		_audi = GetComponent<AudioSource> ();
	}

	private void Update () {
		transform.Translate (inputMove * Time.deltaTime * speed, 0, 0);
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, min, max), transform.position.y, transform.position.z);
		transform.Translate (Input.GetAxis ("HorizontalED") * Time.deltaTime * speed, 0, 0);
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, min, max), transform.position.y, transform.position.z);
		print (inputMove);
	}

	private void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.layer.Equals (10))
			DecreaseHealth ();
	}

	public void Left (bool set) {
		if (set)
			inputMove = -1;
		else if (!set && !inputMove.Equals (1))
			inputMove = 0;
	}

	public void Right (bool set) {
		if (set)
			inputMove = 1;
		else if (!set && !inputMove.Equals (-1))
			inputMove = 0;
	}
}
