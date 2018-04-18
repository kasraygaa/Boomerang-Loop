using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour {
	public Boomerang[] boomerang;
	public GameObject panelStart;
	public Player player;
	public Text timeText;
	public float time;
	public float timeExpert;
	private float time_temp;
	public Text durabilityText;
	private int durability_temp;
	public GameObject win;
	public GameObject lose;

	public void SetPanelStart (bool set) {
		panelStart.SetActive (set);
	}

	public void SetTimeText (bool set) {
		timeText.gameObject.SetActive (set);
	}

	public void SetDurabilityText (bool set) {
		durabilityText.gameObject.SetActive (set);
	}

	public void SetWin (bool set) {
		win.SetActive (set);
	}

	public void SetLose (bool set) {
		lose.SetActive (set);
	}

	private void SetInitPlay () {
		SetPanelStart (false);
		SetTimeText (true);
		SetDurabilityText (true);
		player.health = durability_temp;
		player.audi.Play ();
		time = time_temp;
		Time.timeScale = 1;
	}

	public void PlayBoomerangNewbie () {
		boomerang [boomerang.Length - 1].gameObject.SetActive (false);
		for (int i = 0; i < boomerang.Length - 1; i++) {
			boomerang [i].Play ();
		}
		SetInitPlay ();
	}

	public void PlayBoomerangExpert () {
		boomerang [boomerang.Length - 1].gameObject.SetActive (true);
		for (int i = 0; i < boomerang.Length; i++) {
			boomerang [i].Play ();
		}
		SetInitPlay ();
		time += timeExpert;
	}

	private void Start () {
		time_temp = time;
		durability_temp = player.health;
		Time.timeScale = 0;
		SetTimeText (false);
		SetDurabilityText (false);
		SetWin (false);
		SetLose (false);
	}

	private void Update () {
		if (time <= 0) {
			SetPanelStart (true);
			player.audi.Stop ();
			Time.timeScale = 0;
			SetWin (true);
			SetLose (false);
		}
		time -= 1 * Time.deltaTime;
		timeText.text = "Time Left: " + (int)time;
		durabilityText.text = "Durability: " + (int)player.health;
	}
}
