using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script to create levels
public class LevelManager : MonoBehaviour {

   [SerializeField]
   private GameObject[] tilePrefabs;

   [SerializeField]
   private CameraMovement cameraMovement;

   public float TileSize {
      get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
   }

   public Dictionary<Point, TileScript> Tiles { get; set; }

	// Use this for initialization
	void Start () {
      // Creates the level
      CreateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   private void CreateLevel() {

      Tiles = new Dictionary<Point, TileScript>();
      
      string[] mapData = ReadLevelText();

      // The length of one row of the map
      int mapXSize = mapData[0].ToCharArray().Length;
      // The number of elements in the map array
      int mapYSize = mapData.Length;

      Vector3 maxTile = Vector3.zero;

      // A point of reference according to the camera to start from
      Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

      for (int y = 0; y < mapYSize; y++) {

         char[] newTiles = mapData[y].ToCharArray();

         for (int x = 0; x < mapXSize; x++) {

            // Place the tile
            PlaceTile(newTiles[x].ToString(), x, y, worldStart);
         }
      }

      maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

      cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
   }

   private void PlaceTile(string tileType, int x, int y, Vector3 worldStart) {
      // A string number is passed in and parsed to determine which prefab to use
      int tileIndex = int.Parse(tileType);

      // Create a new tile object with a reference to a specific prefab
      TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

      // Move the tile accordingly on the map
      newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

      Tiles.Add(new Point(x, y), newTile);
   }

   private string[] ReadLevelText() {
      TextAsset bindData = Resources.Load("Level") as TextAsset;

      string data = bindData.text.Replace(Environment.NewLine, String.Empty);

      return data.Split('-');
   }
}
