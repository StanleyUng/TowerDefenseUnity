using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {

   // Dictionary of tiles with a Point key and Node value
   private static Dictionary<Point, Node> nodes;
   
   private static void CreateNodes() {
      nodes = new Dictionary<Point, Node>();

      // Iterate through each TileScript in the game and create a node for each
      foreach (TileScript t in LevelManager.Instance.Tiles.Values) {
         nodes.Add(t.GridPos, new Node(t));
      }
   }

   public static void GetPath(Point start) {
      
      if (nodes == null) {
         CreateNodes();
      }

      HashSet<Node> openList = new HashSet<Node>();

      // Accessing the node at Point start
      Node currentNode = nodes[start];

      // Add the start to the open list
      openList.Add(currentNode);

      // Find all the neighbors of the tile
      for (int x = -1; x <= 1; x++) {
         for (int y = -1; y <= 1; y++) {
            Point neighborPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
            
            if (LevelManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition) {
               Node neighbor = nodes[neighborPos];
               neighbor.TileRef.SpriteRenderer.color = Color.black;
            }
         }
      }

      GameObject.Find("AStarDebugger").GetComponent<AStarDebug>().DebugPath(openList);
   }
}
