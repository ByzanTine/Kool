using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// TODO: boundary checks
public class MapManager : MonoBehaviour {
	// cube size default is 1*1, change this factor to get a larger map
	const float tile_size_factor = 2.0f;
	// tile_space changes the space between each tile
	const float tile_space = 0.2f;
	// tile_height is the height of each tile
	const float tile_height = 0.1f;
	// the size of whole map is map_size * map_size
	const int map_size = 20;
	public GameObject tile_prefab;
	
	List<GameObject> border_tiles = new List<GameObject>();
	GameObject[,] tiles = new GameObject[map_size, map_size];
	
	// this function load the map from file Assets/map_data.txt
	// the map in the file should have a size < map_size
	// TODO: add check of the file 

	public void load_map(){
//		string map_file = File.ReadAllText("Assets/map_data.txt");
//		string[] lines = map_file.Split('\n');
//		int x = 0, y = 0;
//		foreach (string s in lines) {
//			s.Trim();
//			for(int i = 0; i < s.Length; i++){
//				if(s[i] == ' '){
//					continue;
//				}
//				string p = s[i].ToString();
//				if(int.Parse(p) == 1){
//					tileAt(x,y);
//				}
//				x ++;
//			}
//			y ++;
//			x = 0;
//		}
	}
	
	// this function remove all the border tiles
	public void removeWholeBorder(){
		updateBorder();
		foreach (GameObject go in border_tiles){
			Destroy (go);
		}
		border_tiles.Clear ();
	}
	
	// this function remove a random tile from the border
	public void removeRandomBorder(){
		if(border_tiles.Count == 0){
			updateBorder();
		}
		int index = Random.Range (0, border_tiles.Count);
		Destroy (border_tiles [index]);
		border_tiles.RemoveAt(index);
		
	}
	
	// this function generate Map around the point,(y axis is always 0).
	// generate size controls the size of the new map
	public void generateMapAround(Vector3 point, int generate_size){
		int ix = (int) (point.x / tile_size_factor);
		int iy = (int) (point.z / tile_size_factor);
		List<Vector2> border = new List<Vector2> ();
//		print (ix + " " + iy);
		tileAround (ix, iy, border);
		for (int i = 0; i < generate_size; i++) {
			List<Vector2> new_border = new List<Vector2> ();
			foreach(Vector2 v in border){
				tileAround((int)v.x, (int)v.y, new_border);
			}
			border = new_border;
		}
		
	}
	
	void tileAt(int x, int y){
		tiles[x,y] = (GameObject)Instantiate(tile_prefab, new Vector3(x * tile_size_factor,0,y * tile_size_factor), Quaternion.identity);
		tiles[x,y].transform.localScale = new Vector3(tile_size_factor - tile_space,
		                                              tile_height,
		                                              tile_size_factor - tile_space);
//		tiles [x, y].transform.parent = this.gameObject.transform;
		tiles [x, y].tag = TagList.GroundBlock;
	}
	
	void updateBorder(){
		border_tiles.Clear ();
		for (int x = 0; x < tiles.GetLength(0); x ++) {
			for(int y = 0; y < tiles.GetLength(1); y ++){
				if(tiles[x,y] != null && isBorder(x,y)){
					border_tiles.Add(tiles[x,y]);
				}
			}
		}
	}
	
	void tileAround(int x, int y, List<Vector2> new_inserted){
		if (tiles [x, y] == null) {
			tileAt(x,y);
		}
		if (x + 1 < map_size && tiles[x + 1,y] == null) {
			if(new_inserted != null){
				new_inserted.Add(new Vector2(x + 1, y));
			}
			tileAt(x + 1,y);
		}
		if (x - 1 >= 0 && tiles[x - 1,y] == null) {
			if(new_inserted != null){
				new_inserted.Add(new Vector2(x - 1, y));
			}
			tileAt(x - 1,y);
		}
		if (y + 1 < map_size && tiles[x,y + 1] == null) {
			if(new_inserted != null){
				new_inserted.Add(new Vector2(x, y + 1));
			}
			tileAt(x,y + 1);
		}
		if (y - 1 >= 0 && tiles[x,y - 1] == null) {
			if(new_inserted != null){
				new_inserted.Add(new Vector2(x, y - 1));
			}
			tileAt(x,y - 1);
		}
	}
	
	bool isBorder(int x, int y){
		if (x + 1 >= map_size || tiles[x + 1,y] == null) {
			return true;
		}
		if (x - 1 < 0 || tiles[x - 1,y] == null) {
			return true;
		}
		if (y + 1 >= map_size || tiles[x,y + 1] == null) {
			return true;
		}
		if (y - 1 < 0 || tiles[x,y - 1] == null) {
			return true;
		}
		return false;
	}
	
	// return true if it is on board
	bool query(float x, float y){
		x = x / tile_size_factor;
		y = y / tile_size_factor;
		int ix = (int)x;
		int iy = (int)y;
		return tiles [ix, iy] != null;
	}
	// Use this for initialization
	void Start () {
		generateMapAround (new Vector3 (20f, 0, 20f), 20);
		//tileAround (10, 10, null);
		//load_map ();
		StartCoroutine (Devastating ());
	}

	IEnumerator Devastating()
	{
		while(true)
		{
			removeRandomBorder();
			yield return new WaitForSeconds(0.15f);
		}
	}
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Q)) {
//			removeWholeBorder();
//		}
//		if (Input.GetKeyDown (KeyCode.W)) {
//			removeRandomBorder();
//		}
	}
}
