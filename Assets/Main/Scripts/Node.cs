using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour {
	private Node[] _linksUp;
	public Node[] linksUp {
		get {
			return _linksUp;
		} set {
			_linksUp = value;
		}
	}

	private Node[] _linksDown;
	public Node[] linksDown {
		get {
			return _linksDown;
		} set {
			_linksDown = value;
		}
	}

	private bool _visited;
	public bool visited {
		get {
			return _visited;
		} set {
			_visited = value;
		}
	}

	private void Start () {

	}

	private void OnTriggerEnter (Collider coll) {
		if (CompareTag ("NPC") && (GetBoomerang (coll).toNode + 1).Equals (GetBoomerang (coll).path.Count)) {
			GetBoomerang (coll).toNode = 1;
			if (name.Equals ("NPC_Down"))
				GetBoomerang (coll).GeneratePathUP (GetBoomerang (coll).NPC_Down, GetBoomerang (coll).NPC_Up);
			else
				GetBoomerang (coll).GeneratePathDown (GetBoomerang (coll).NPC_Up, GetBoomerang (coll).NPC_Down);
			GetBoomerang (coll).SetAgentDestination ();
		}
		if (coll.CompareTag ("Boomerang") && GetBoomerang (coll).CheckNode (GetNode ())) {
			GetBoomerang (coll).toNode++;
			GetBoomerang (coll).SetAgentDestination ();
		}
	}

	private Boomerang GetBoomerang (Collider coll) {
		return coll.GetComponent<Boomerang> ();
	}

	private Node GetNode () {
		return GetComponent<Node> ();
	}
}
