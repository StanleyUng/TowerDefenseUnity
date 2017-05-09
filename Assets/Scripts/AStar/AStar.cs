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

}
