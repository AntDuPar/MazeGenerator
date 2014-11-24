using UnityEngine;
using System.Collections;

public class MazeGenerate : MonoBehaviour {
	
	public int size;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		bool[ , ] east = new bool[size , size];
		bool[ , ] south = new bool[size , size];
		bool[ , ] visited = new bool[size , size];
		for (int i = 0; i<size; i++) {
			for (int q = 0; q<size; q++) {
				east[i,q] = true;
				south[i,q] = true;
				visited[i,q] = false;
			}
		}
		visited [0,0] = true;
		int cx = 0;
		int cy = 0;
		int px = 0;
		int py = 0;
		int toLong = 0;
		while (checkVisited(visited, size) && toLong < (1000 * size)) {
			toLong++;
			int[] n = getNeighbors (size, cx, cy);
			double r = Random.Range (0, 4);
			if (n [0] == -2 && n [1] == -2 && n [3] == -2 && n [4] == -2) {
				break;
			}
			else if (r == 2 && n [2] != -2 && visited [n [2], cy] == false) {
				east [cx, cy] = false;
				px = cx;
				py = cy;
				cx = n [2];
				visited [n [2], cy] = true;
			}
			else if (r == 3 && n [3] != -2 && visited [n [3], cy] == false) {
				px = cx;
				py = cy;
				cx = n[3];
				east [cx, cy] = false;
				visited [n [3], cy] = true;
			}
			else if (r == 1 && n [1] != -2 && visited [cx, n [1]] == false) {
				px = cx;
				py = cy;
				cy = n [1];
				visited [cx, cy] = true;
				south [cx, n [1]] = false;
			}
			else if (r == 0 && n [0] != -2 && visited [cx, n [0]] == false) {
				px = cx;
				py = cy;
				south [cx, cy] = false;
				cy = n [0];
				visited [cx, cy] = true;
			}
			if(voidOrVisited (visited, n, cx, cy)) {
				int mcx = Random.Range (0, size-1);
				int mcy = Random.Range (0, size-1);
				
				if (visited[mcx, mcy] == true) {
					cy = mcy;
					cx = mcx;
				}
			}
		}
		Vector3 vec = new Vector3();
		for(int d = 0; d < size; d++){
			//	System.Console.Write(" _");
			Instantiate(wall, vec, Quaternion.Euler(0, 90, 0));
			vec.z += 10;
		}
		vec.z = 0;
		//System.Console.WriteLine("");
		for(int a = 0; a < size; a++){
			//System.Console.Write ("|");
			vec.x += 5;
			vec.y = 0;
			vec.z = -5;
			Instantiate(wall, vec, Quaternion.Euler (0, 0, 0));
			vec.z = 0;
			vec.x += 5;
			for(int b = 0; b < size; b++){
				if(east[b,a] == false && south[b,a] == true){
					//System.Console.Write("_ ");
					Instantiate(wall, vec, Quaternion.Euler(0,90,0));
					vec.z += 10;
				}
				else if(east[b,a] == true && south[b,a] == true){
					//System.Console.Write("_|");
					Instantiate(wall, vec, Quaternion.Euler(0,90,0));
					vec.x = vec.x - 5;
					vec.z += 5;
					Instantiate(wall, vec, Quaternion.Euler(0, 0, 0));
					vec.x = vec.x + 5;
					vec.z += 5;
				}
				else if(east[b,a] == false && south[b,a] == false){
					//System.Console.Write("  ");
					vec.z += 10;
				}
				else if(east[b,a] == true && south[b,a] == false){
					vec.x = vec.x - 5;
					vec.z += 5;
					Instantiate(wall, vec, Quaternion.Euler(0, 0, 0));
					vec.z += 5;
					vec.x =  vec.x + 5;
					//System.Console.Write(" |");
				}
			}
			System.Console.WriteLine("");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	bool checkVisited(bool[,] v, int s){
		for (int i = 0; i<s; i++) {
			for (int q = 0; q<s; q++) {
				if (v [i,q] == false) {
					return true;
				}
			}
		}
		return false;
	}
	
	int[] getNeighbors(int s, int x, int y){
		int[] neighbors = new int[4];
		for (int q = 0; q < 4; q++) {
			neighbors[q] = -2;
		}
		if (x > 0) {
			neighbors[3] = x - 1;
		}
		if (x < s - 1) {
			neighbors[2] = x + 1;
		}
		if (y > 0) {
			neighbors[1] = y - 1;
		}
		if (y < s - 1) {
			neighbors[0] = y + 1;
		}
		return neighbors;
	}
	
	bool voidOrVisited(bool[,] v, int[] n, int cx, int cy){
		int r;
		bool[] b = new bool[4];
		for (int i = 0; i < 4; i++) {
			r = n [i];
			if (n [i] == cx - 1 && v [r, cy] == true) {
				b [i] = true;
			} else if (n [i] == cx + 1 && v [r, cy] == true) {
				b [i] = true;
			} else if (n [i] == cy + 1 && v [cx, r] == true) {
				b [i] = true;
			} else if (n [i] == cy - 1 && v [cx, r] == true) {
				b [i] = true;
			} else if (n [i] == -2) {
				b [i] = true;
			}
		}
		for (int j = 0; j < 4; j++) {
			if(b[j] == false){
				return false;
			}
		}
		return true;
	}
}
