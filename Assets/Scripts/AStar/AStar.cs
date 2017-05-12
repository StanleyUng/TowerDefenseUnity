using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

   public static void GetPath(Point start, Point end) {
      
      if (nodes == null) {
         CreateNodes();
      }

      HashSet<Node> openList = new HashSet<Node>();

      HashSet<Node> closedList = new HashSet<Node>();

      // Stack to hold the final path
      Stack<Node> finalPath = new Stack<Node>();

      // Accessing the node at Point start
      Node currentNode = nodes[start];

      // Add the start to the open list
      openList.Add(currentNode);

      while (openList.Count > 0) {

      // Find all the neighbors of the tile
      for (int x = -1; x <= 1; x++) {
         for (int y = -1; y <= 1; y++) {
            Point neighborPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
            
            if (LevelManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition) {

               int gCost = 0;
               if (Mathf.Abs(x - y) == 1) {
                  gCost = 10;
               } else {
                  gCost = 14;
               }   

               Node neighbor = nodes[neighborPos];

               if (openList.Contains(neighbor)) {
                  if (currentNode.G + gCost < neighbor.G) {
                     neighbor.CalcValues(currentNode, nodes[end], gCost);
                  }
               } else if (!closedList.Contains(neighbor)) {
                  openList.Add(neighbor);
                  neighbor.CalcValues(currentNode, nodes[end], gCost);
               }
            }
         }
      }

      openList.Remove(currentNode);
      closedList.Add(currentNode);

      if (openList.Count > 0) {
         // Sort list by F value, the first of the list is the lowest F value 
         currentNode = openList.OrderBy(n => n.F).First();
      }

      if (currentNode == nodes[end]) {
         while (currentNode.GridPosition != start) {
            finalPath.Push(currentNode);
            currentNode = currentNode.Parent;
         }
         break;
      }

      }// while
      GameObject.Find("AStarDebugger").GetComponent<AStarDebug>().DebugPath(openList, closedList);
   }
}
