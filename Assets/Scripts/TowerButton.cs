using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

   [SerializeField]
   private GameObject towerPrefab;
   public GameObject TowerPrefab {
      get {
         return towerPrefab;
      }
   }

   [SerializeField]
   private Sprite sprite;
   public Sprite Sprite {
      get {
         return sprite;
      }
   }
}
