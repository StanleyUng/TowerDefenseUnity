using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

   public Point GridPos { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

   public void Setup(Point p, Vector3 worldPos) {
      this.GridPos = p;
      transform.position = worldPos;
   }
}
