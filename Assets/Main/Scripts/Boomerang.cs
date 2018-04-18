using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boomerang : MonoBehaviour {
	public Graph graph;
	public Node NPC_Up;
	public Node NPC_Down;
	private Node _onNode;
	public Node onNode {
		get {
			return _onNode;
		} set {
			_onNode = value;
		}
	}

	private List<Node> _path;
	public List<Node> path {
		get {
			return _path;
		} set {
			_path = value;
		}
	}

	private int _toNode = 1;
	public int toNode {
		get {
			return _toNode;
		} set {
			_toNode = value;
		}
	}

	private NavMeshAgent _agent;
	public NavMeshAgent agent {
		get {
			return _agent;
		}
	}

	public enum Choose
	{
		UP, DOWN
	};

	public Choose choose;
	public int choosed {
		get {
			return (int)choose;
		}
	}

	public void GeneratePathUP (Node init, Node end) {
		path = graph.DepthFSUP (init, end);
		graph.PrintPath (path);
	}

	public void GeneratePathDown (Node init, Node end) {
		path = graph.DepthFSDown (init, end);
		graph.PrintPath (path);
	}

	public bool CheckNode (Node tempNode) {
		return path [toNode].Equals (tempNode);
	}

	public void SetAgentDestination () {
		agent.SetDestination (path [toNode].transform.position);
	}

	private void Start () {
		_agent = GetComponent<NavMeshAgent> ();
	}
	
	public void Play () {
		if (choosed.Equals (0))
			GeneratePathUP (NPC_Down, NPC_Up);
		else
			GeneratePathDown (NPC_Up, NPC_Down);
		transform.position = path [0].transform.position;
		SetAgentDestination ();
	}
}
