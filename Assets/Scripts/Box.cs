using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Box : MonoBehaviour {

    // If the box is reversed, or upside down
    public bool reverse;
    // The location of the tile below the box position
    public Vector3Int belowBoxTilePos;
    // The tilemap of the level
    public Tilemap levelMap;
    // The color of the tile below the player
    public Color belowBoxColor;
    // Reference red tile color
    public Color redTile;
	
	// Update is called once per frame
	void Update () {
        // Runs the method to check the tile below
        CheckTileBelow();
        // If a tilemap is availalbe
        if (levelMap != null)
        {
            // And the below box color is equal to the red tile reference color
            if (belowBoxColor == redTile)
            {
                // And if it is not already reversed
                if (!reverse)
                {
                    // Reverse
                    // Set gravity scale to negative of what it defaults to
                    GetComponent<Rigidbody2D>().gravityScale = -30;
                    // Delay the feedback of being reversed
                    StartCoroutine(DelayReverse());
                }
                // And if it is already reversed
                if (reverse)
                {
                    // Unreverse
                    // Set gravity scale back to origin default
                    GetComponent<Rigidbody2D>().gravityScale = 30;
                    // Delay the feedback of being unreversed/
                    StartCoroutine(DelayUnReverse());
                }
            }
        }
	}

    // Delay methods exist so that if in the past few seconds it has already used the red box tile below it to reverse, it will not use that box more than once time and get caught in a loop
    // Delay the reverse
    public IEnumerator DelayReverse()
    {
        // Wait a little while
        yield return new WaitForSeconds(.1f);
        // Set reverse to true
        reverse = true;
    }

    // Delay the unreverse
    public IEnumerator DelayUnReverse()
    {
        // Wait a little while
        yield return new WaitForSeconds(.1f);
        // Set reverse to false
        reverse = false;
    }

    // Check the tile below the box
    public void CheckTileBelow()
    {
        // If a levelmap is available
        if(levelMap != null)
        {
            // And not reversed
            if (!reverse)
            {
                // Get the location of the gameobject but 3/4 units below it and check for a tile cell
                belowBoxTilePos = levelMap.LocalToCell(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .75f, 0));
            }
            // And if reversed
            if (reverse)
            {
                // Get the location of the gameobject but 3/4 units above it and check for a tile cell
                belowBoxTilePos = levelMap.LocalToCell(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, 0));
            }
            // The color of the tile cell located
            belowBoxColor = levelMap.GetColor(belowBoxTilePos);
        }

    }
}
