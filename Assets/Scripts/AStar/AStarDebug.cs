using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour {
   
   [SerializeField]
   private TileScript start;
   
   [SerializeField]
   private TileScript end;

   [SerializeField]
   private GameObject arrowPrefab;

   [SerializeField]
   private GameObject debugTilePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      ClickTile();

      if (Input.GetKeyDown(KeyCode.Space)) {
         AStar.GetPath(start.GridPos, end.GridPos);
      }
	}

   private void ClickTile() {
      if (Input.GetMouseButtonDown(1)) {
         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

         if (hit.collider != null) {
            TileScript temp = hit.collider.GetComponent<TileScript>();
            if (start == null) {
               start = temp;
               //start.Debug = true;
               //start.SpriteRenderer.color = Color.cyan;
               CreateDebugTile(start.WorldPosition, Color.red);
            } else if (end == null) {
               end = temp;
               CreateDebugTile(end.WorldPosition, Color.red);
               //end.Debug = true;
               //end.SpriteRenderer.color = Color.cyan;
            }
         }
      }
   }

   // Changes color of nodes in openList 
   public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList) {
      foreach (Node n in openList) {
         if (n.TileRef != start) {
            //n.TileRef.SpriteRenderer.color = Color.red;
            CreateDebugTile(n.TileRef.WorldPosition, Color.cyan, n);
         }

         PointToParent(n, n.TileRef.WorldPosition);
      }

      foreach (Node n in closedList) {
         if (n.TileRef != start && n.TileRef != end) {
            //n.TileRef.SpriteRenderer.color = Color.red;
            CreateDebugTile(n.TileRef.WorldPosition, Color.blue, n);
         }
      }
   }

   private void PointToParent(Node node, Vector2 position) {
      if (node.Parent != null) {
         GameObject arrow = (GameObject)Instantiate(arrowPrefab, position, Quaternion.identity);
         arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;

         // Right
         if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 0);
         } 
         // Top Right
         else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 45);
         }
         // Up
         else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 90);
         }
         // Top Left
         else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 135);
         }
         // Left
         else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 180);
         }
         // Bottom Left
         else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 225);
         }
         // Down
         else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 270);
         }
         // Bottom Right
         else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y)) {
            arrow.transform.eulerAngles = new Vector3(0, 0, 315);
         }
      }
   }

   //Node is an optional parameter, unless i put it in
   private void CreateDebugTile(Vector3 worldPos, Color color, Node n = null) {
      GameObject debugTile = (GameObject)Instantiate(debugTilePrefab, worldPos, Quaternion.identity);
      
      if (n != null) {
         DebugTile temp = debugTile.GetComponent<DebugTile>();
         temp.G.text += n.G;
         temp.H.text += n.H;
         temp.F.text += n.F;
      }

      debugTile.GetComponent<SpriteRenderer>().color = color;
   }
}
