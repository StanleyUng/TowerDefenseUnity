using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point {

   public int X { get; set; }
   public int Y { get; set; }

   public Point(int x, int y) {
      this.X = x;
      this.Y = y;
   }

   public static bool operator ==(Point lhs, Point rhs) {
      return lhs.X == rhs.X && lhs.Y == rhs.Y;
   }

   public static bool operator !=(Point lhs, Point rhs) {
      return lhs.X != rhs.X || lhs.Y != rhs.Y;
   }

   public override int GetHashCode() {
      return X ^ Y;
   }

   public override bool Equals(object obj) {
      if (obj == null)
         return false;

      if (GetType() != obj.GetType())
         return false;

      Point point = (Point)obj;

      if (X != point.X)
         return false;

      return Y == point.Y;
   }
}
