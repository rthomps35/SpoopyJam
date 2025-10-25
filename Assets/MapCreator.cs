using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class MapCreator : MonoBehaviour
{
	[SerializeField] Texture2D mapPNG;  //The map texture
	public GameObject[,] MapTiles = new GameObject[0, 0];
	[SerializeField] PrefabManager PM;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

	
	void MapGenerator()
	{
		mapPNG = Resources.Load<Texture2D>("Art/map"); //Grab the provincemap
		MapTiles = new GameObject[mapPNG.width, mapPNG.height];
		int xInc = 0;//sprite chunk
		int yInc = 0;
		for (int y = 0; y < mapPNG.height; y++)
		{
			for (int x = 0; x < mapPNG.width; x++)
			{
				Color thisPixel = mapPNG.GetPixel(x, y);
				GameObject newTile = Instantiate(Tile, new Vector2(x, y), Quaternion.identity, gameMap.transform);
				tileSelector(thisPixel, x, y, newTile);    //make the tile based on the RGB value
				MapTiles[x, y] = newTile;

				if (x == xInc && y == yInc)
				{
					xInc += 4;
					if (xInc > mapPNG.width)
					{
						xInc = 0;
						yInc += 4;
					}

				}
				else
				{
					SpriteRenderer TS = newTile.GetComponent<SpriteRenderer>();
					TS.enabled = false;
				}
				Debug.Log(xInc);

				newTile.name = $"Tile{x}.{y}";
			}
		}
		/*
		activeSummonSpawn = spawns[UnityEngine.Random.Range(0, spawns.Count)];  //pick the first spawn loaction
		GraveGeneration();                                                      //Set up the body locations
		Debug.Log($"Map Initialization ended at:{DateTime.Now}");
		*/
		//gameMap.SetActive(false);       //set the map to inactive
		//probably want the above if the fuccccccccccccccccccccccccccccccccccccccccccck

	}

	
	void tileSelector(Color pixelColor, int x, int y, GameObject tile)
	{
		int r = (int)(pixelColor.r * 255);  //needs to be convereted to 255 color
		int g = (int)(pixelColor.g * 255);
		int b = (int)(pixelColor.b * 255);

		GameObject newGameObject;
		//Tile tileScript = tile.GetComponent<TileScript>();

		if (r == 38 && g == 127 && b == 0)
		{
			newGameObject = Instantiate(PM.BorderObject, new Vector3(x, y), Quaternion.identity, gameMap.transform);   //Border
			//tileScript.Walkable = false;
			//tileScript.ObjectOnTile = newGameObject;
		}
		else if (r == 64 && g == 64 && b == 64)
		{
			newGameObject = Instantiate(PM.GraveObject, new Vector3(x, y), Quaternion.identity, gameMap.transform);    //Graves
			//AllGraves.Add(newGameObject);
			//tileScript.Walkable = true;
			//tileScript.ObjectOnTile = newGameObject;
		}
		else if (r == 255 && g == 0 && b == 0)
		{
			newGameObject = Instantiate(PM.SpawnObject, new Vector3(x, y), Quaternion.identity, gameMap.transform);    //Spwan
			//spawns.Add(newGameObject);
			//tileScript.Walkable = true;
			//tileScript.ObjectOnTile = newGameObject;
		}
		else if (r == 0 && g == 0 && b == 0)
		{
			newGameObject = Instantiate(PM.TombstoneObject, new Vector3(x, y), Quaternion.identity, gameMap.transform);    //Graves
			//tileScript.Walkable = false;
			//tileScript.ObjectOnTile = newGameObject;
			//TileScript ts = MapTiles[x, y - 1].GetComponent<TileScript>();    //link to nearby grave
			//GraveScript gs = ts.ObjectOnTile.GetComponent<GraveScript>();
			//gs.Tombstone = newGameObject;
		}
		else
		{
			//tileScript.Walkable = true;
		}


	}
	
}
