using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
   
   // A point position of the tile
   public Point GridPosition { get; private set; }

   // A reference to the Tile's TileScript
   public TileScript TileRef { get; private set; }

   // Node constructor
   public Node(TileScript t) {
      this.TileRef = t;
      this.GridPosition = t.GridPos;
   }
}
