using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {
   
   // A position of the tile (X, Y)
   public Point GridPos { get; private set; }

   // Is there a tower on the tile?
   public bool IsEmpty { get; private set; }

   public Vector2 WorldPosition { 
      get {
         return new Vector2(
            transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2),
            transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
      } 
   }
   
   //public SpriteRenderer SpriteRenderer { get; set; }

   private SpriteRenderer spriteRenderer;

   // Is there a structure in the way?
   public bool Walkable { get; set; }

   private Color occupiedColor = Color.red;
   private Color emptyColor = Color.green;

   // Temp
   public bool Debug { get; set; }

	// Use this for initialization
	void Start () {
      //SpriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

   public void Setup(Point p, Vector3 worldPos, Transform parent) {
      IsEmpty = true;
      Walkable = true;
      this.GridPos = p;
      transform.position = worldPos;

      // Set this tiles parent into the Map for organizing
      transform.SetParent(parent);

      LevelManager.Instance.Tiles.Add(p, this);

      //lm.Tiles.Add(p, this);
   }

   // When I hover my mouse cursor over a a tile, this will execute
   private void OnMouseOver() {
      //Debug.Log(GridPos.X + ", " + GridPos.Y);
 
      // This will execute if the mouse is currently not over a game object
      // Only place a tower if the mouse is not on top of the UI buttons
      if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null) {
         
         if (IsEmpty && !Debug) {
            ColorTile(emptyColor);
         }
         
         if (!IsEmpty && !Debug) {
            ColorTile(occupiedColor);
         } else if (Input.GetMouseButtonDown(0)) {
            PlaceTower();
         }

      }
   }

   private void OnMouseExit() {
    
      if (!Debug) {
         ColorTile(Color.white);
      }

      //ColorTile(Color.white);
   }

   // Place a tower on a tile
   private void PlaceTower() {
      // Instantiate Tower and create a reference to it
      GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerPrefab, transform.position, Quaternion.identity);
      
      // Set the sorting layer to the Y value position of the Tile so towers won't overlap incorrectly
      tower.GetComponent<SpriteRenderer>().sortingOrder = GridPos.Y;

      // The tower is set as a child of the tile it is on
      tower.transform.SetParent(transform);

      // A tower was placed down so it is not empty
      IsEmpty = false;

      // Reset color after placing tower
      ColorTile(Color.white);

      GameManager.Instance.BuyTower();

      // A tile is not walkable since we bought a tower and put it there
      Walkable = false;
   }

   // Change the color of the tile
   private void ColorTile(Color c) {
      spriteRenderer.color = c;
   }
}
