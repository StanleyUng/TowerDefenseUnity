using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour {
   
   [SerializeField]
   private TileScript start;
   
   [SerializeField]
   private TileScript end;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      ClickTile();

      if (Input.GetKeyDown(KeyCode.Space)) {
         AStar.GetPath(start.GridPos);
      }
	}

   private void ClickTile() {
      if (Input.GetMouseButtonDown(1)) {
         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

         if (hit.collider != null) {
            TileScript temp = hit.collider.GetComponent<TileScript>();
            if (start == null) {
               start = temp;
               start.Debug = true;
               start.SpriteRenderer.color = Color.cyan;
            } else if (end == null) {
               end = temp;
               end.Debug = true;
               end.SpriteRenderer.color = Color.cyan;
            }
         }
      }
   }

   // Changes color of nodes in openList 
   public void DebugPath(HashSet<Node> openList) {
      foreach (Node n in openList) {
         n.TileRef.SpriteRenderer.color = Color.red;
      }
   }
}
