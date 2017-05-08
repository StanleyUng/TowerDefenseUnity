using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover> {

   private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
      this.spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
      FollowMouse();
	}

   private void FollowMouse() {
      if (spriteRenderer.enabled) {
         // Take the mouse position
         transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         transform.position = new Vector3(transform.position.x, transform.position.y, 0);
      }
   }

   // When we click on a button, the sprite shows up on the arrow like we are dragging it
   public void Activate(Sprite s) {
      this.spriteRenderer.sprite = s;
      spriteRenderer.enabled = true;
   }

   // Deactivate the sprite renderer after we place a tower, so it looks like we aren't carrying another tower
   public void Deactivate() {
      spriteRenderer.enabled = false;
      GameManager.Instance.ClickedButton = null;
   }
}
