using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script to handle the game
// Time, Currency
public class GameManager : Singleton<GameManager> {

   public TowerButton ClickedButton { get; set; }

   private int gold;

   [SerializeField]
   private Text goldText;
   public int Gold {
      get {
         return gold;
      }

      set {
         this.gold = value;
         this.goldText.text = "<color=lime>$</color>" + value.ToString();
      }
   }

	// Use this for initialization
	void Start () {
      Gold = 10;
	}
	
	// Update is called once per frame
	void Update () {
      HandleEscape();
	}

   public void PickTower(TowerButton towerBtn) {

      if (Gold >= towerBtn.Price) {
         // Reference to which button we pressed
         this.ClickedButton = towerBtn;

         // Activate icon
         Hover.Instance.Activate(towerBtn.Sprite);
      }
   }

   public void BuyTower() {
      if (Gold >= ClickedButton.Price) {
         Gold -= ClickedButton.Price;
         Hover.Instance.Deactivate();
      }
   }

   // Deselect a tower
   private void HandleEscape() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
         Hover.Instance.Deactivate();
      }
   }
}
