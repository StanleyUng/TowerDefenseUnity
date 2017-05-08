using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

   [SerializeField]
   private int price;
   public int Price {
      get {
         return price;
      }

      set {
         price = value;
      }
   }

   [SerializeField]
   private Text priceText;

   private void Start() {
      // Set price at start
      priceText.text = "$" + Price;
   }
}
