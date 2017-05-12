using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
   
   // A point position of the tile
   public Point GridPosition { get; private set; }

   // A reference to the Tile's TileScript
   public TileScript TileRef { get; private set; }

   public Node Parent { get; private set; }

   public int G { get; set; }

   public int H { get; set; }

   public int F { get; set; }

   // Node constructor
   public Node(TileScript t) {
      this.TileRef = t;
      this.GridPosition = t.GridPos;
   }

   public void CalcValues(Node parent, Node end, int gCost) {
      this.Parent = parent;
      this.G = parent.G + gCost;
      this.H = (Mathf.Abs(GridPosition.X - end.GridPosition.X) + Mathf.Abs(end.GridPosition.Y - GridPosition.Y)) * 10;
      this.F = G + H;
   }
}
