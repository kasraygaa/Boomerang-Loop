using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Graph : MonoBehaviour {
	[System.Serializable]
	public struct NodesUp {
		public Node node;
		public Node[] upLinkAdditional;
		public GameObject upLinkParent;
	}
	[System.Serializable]
	public struct NodesDown {
		public Node node;
		public Node[] downLinkAdditional;
		public GameObject downLinkParent;
	}
	public NodesUp[] graphUp;
	public NodesDown[] graphDown;

	private void SetLinks (NodesUp [] graph) {
		for (int i = 0; i < graph.Length; i++) {
//			print (i);
			int length1 = 0;
			int length2 = 0;
			try {
				if (!graph [i].upLinkAdditional.Length.Equals (0))
					length1 = graph [i].upLinkAdditional.Length;
				else 
					length1 = 0;
				
				if (!graph [i].upLinkParent.transform.childCount.Equals (0))
					length2 = graph [i].upLinkParent.transform.childCount;
			} catch (NullReferenceException) {
				length2 = 0;
			}
//			print (length);
			graph [i].node.linksUp =  new Node[length1 + length2];
			
			if (!graph [i].upLinkAdditional.Length.Equals (0)) {
				for (int j = 0; j < length1; j++) {
					graph [i].node.linksUp [j] = graph [i].upLinkAdditional [j].GetComponent<Node> ();
				}
			}

			try {
				for (int j = length1; j < length2; j++) {
					graph [i].node.linksUp [j] = graph [i].upLinkParent.transform.GetChild (j).GetComponent<Node> ();
				}
			} catch (NullReferenceException) {
				
			}
		}
	}

	private void SetLinks (NodesDown [] graph) {
		for (int i = 0; i < graph.Length; i++) {
			//			print (i);
			int length1 = 0;
			int length2 = 0;
			try {
				if (!graph [i].downLinkAdditional.Length.Equals (0))
					length1 = graph [i].downLinkAdditional.Length;
				else 
					length1 = 0;

				if (!graph [i].downLinkParent.transform.childCount.Equals (0))
					length2 = graph [i].downLinkParent.transform.childCount;
			} catch (NullReferenceException) {
				length2 = 0;
			}
			//			print (length);
			graph [i].node.linksDown =  new Node[length1 + length2];

			if (!graph [i].downLinkAdditional.Length.Equals (0)) {
				for (int j = 0; j < length1; j++) {
					graph [i].node.linksDown [j] = graph [i].downLinkAdditional [j].GetComponent<Node> ();
				}
			}

			try {
				for (int j = length1; j < length2; j++) {
					graph [i].node.linksDown [j] = graph [i].downLinkParent.transform.GetChild (j).GetComponent<Node> ();
				}
			} catch (NullReferenceException) {

			}
		}
	}

	private void GraphMapping (NodesUp[] graph) {
		for (int i = 0; i < graph.Length; i++) {
			print (graph [i].node.name + ": " + graph [i].node.linksUp.Length);
			for (int j = 0; j < graph [i].node.linksUp.Length; j++) {
				print (graph [i].node.name + "--->" + graph [i].node.linksUp [j].name);
			}
		}
	}

	private void GraphMapping (NodesDown[] graph) {
		for (int i = 0; i < graph.Length; i++) {
			for (int j = 0; j < graph [i].node.linksDown.Length; j++) {
				print (graph [i].node.name + "--->" + graph [i].node.linksDown [j].name);
			}
		}
	}

	public void PrintPath(List<Node> path) {
		string str = "";
		for (int i = 0; i < path.Count; i++) {
			str += path [i].name + ", ";
		}
		print ("path: " + str);
	}

	public void ClearCache (NodesUp [] graph) {
		for (int i = 0; i < graph.Length; i++) {
			for (int j = 0; j < graph [i].node.linksUp.Length; j++) {
				graph [i].node.linksUp [j].visited = false;
			}
		}
	}

	public void ClearCache (NodesDown [] graph) {
		for (int i = 0; i < graph.Length; i++) {
			for (int j = 0; j < graph [i].node.linksDown.Length; j++) {
				graph [i].node.linksDown [j].visited = false;
			}
		}
	}

	private bool IsVisited (Node node) {
		if (!node.visited) {
			node.visited = true;
			return false;
		}
		return true;
	}

	public List<Node> DepthFSUP (Node init, Node end) {
		
		print ("Using DepthFS");
		print ("init: " + init.name + "\tend: " + end.name);
		ClearCache (graphUp);
		ClearCache (graphDown);
		List<Node> path = new List<Node> ();
		LinkedList<Node> listDFS = new LinkedList<Node> ();
		listDFS.AddLast (init);
		do {
			Node last = listDFS.Last.Value;
			if (!IsVisited (last)) {
				path.Add (last);
				if (last.Equals (end)) {
					return path;
				}
				try {
					listDFS.AddLast (last.linksUp [UnityEngine.Random.Range (0, last.linksUp.Length)]);
				} catch (NullReferenceException) {
					
				} catch (IndexOutOfRangeException) {
					listDFS.AddLast (last.linksDown [UnityEngine.Random.Range (0, last.linksDown.Length - 1)]);
				}
			}
			listDFS.Remove (last);
		} while (!listDFS.Count.Equals (0));
		return path;
	}

	public List<Node> DepthFSDown (Node init, Node end) {
		print ("Using DepthFS");
		print ("init: " + init.name + "\tend: " + end.name);
		ClearCache (graphUp);
		ClearCache (graphDown);
		List<Node> path = new List<Node> ();
		LinkedList<Node> listDFS = new LinkedList<Node> ();
		listDFS.AddLast (init);
		do {
			Node last = listDFS.Last.Value;
			if (!IsVisited (last)) {
				path.Add (last);
				if (last.Equals (end)) {
					return path;
				}
				try {
					listDFS.AddLast (last.linksDown [UnityEngine.Random.Range (0, last.linksDown.Length)]);
				} catch (NullReferenceException) {

				} catch (IndexOutOfRangeException) {
					listDFS.AddLast (last.linksDown [UnityEngine.Random.Range (0, last.linksDown.Length - 1)]);
				}
			}
			listDFS.Remove (last);
		} while (!listDFS.Count.Equals (0));
		return path;
	}

	private void Awake () {
		SetLinks (graphUp);
		SetLinks (graphDown);
	}
}
 