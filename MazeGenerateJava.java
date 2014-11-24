package generation;
import java.util.Random;

public class MazeGenerate {
	public boolean fin = false;
	public static void main(String[] args) throws InterruptedException{
		int size = 30;
		boolean east[][] = new boolean[size][size];
		boolean south[][] = new boolean[size][size];
		boolean visited[][] = new boolean[size][size];
		for(int i = 0; i<size; i++){
			for(int q = 0; q<size; q++){
				east[i][q] = true;
				south[i][q] = true;
				visited[i][q] = false;
			}
		}
		visited[0][0] = true;
		int cx = 0;
		int cy = 0;
		while(checkVisited(visited, size)){
			int[] n = getNeighbors(size, cx, cy);
			double r = Math.random();
			if(n[0] == -2 && n[1] == -2 && n[2] == -2 && n[3] == -2){
				break;
			}
			else if (r >= 0.5 && r < 0.75 && n[2] != -2 && visited[n[2]][cy] == false){
				east[cx][cy] = false;
				cx = n[2];
				visited[n[2]][cy] = true;
				
			}
			else if (r >= 0.75 && r < 1.00 && n[3] != -2 && visited[n[3]][cy] == false){
				cx = n[3];
				east[cx][cy] = false;
				visited[n[3]][cy] = true;
			}
			else if (r >= .25 && r < 0.5 && n[1] != -2 && visited[cx][n[1]] == false){
				cy = n[1];
				visited[cx][cy] = true;
				south[cx][n[1]] = false;
			}
			else if (r > 0.25 && n[0] != -2 && visited[cx][n[0]] == false){
				south[cx][cy] = false;
				cy = n[0];
				visited[cx][cy] = true;
			}
			else if(voidOrVisited(visited, n, cx, cy)){
				int mcx = getRand(size);
				int mcy = getRand(size);
				if(east[mcx][mcy] == false || south[mcx][mcy] == false){
					cy = mcy;
					cx = mcx;
				}
			}
		}
		for(int d = 0; d<size; d++){
			System.out.print(" _");
		}
		System.out.println("");
		for(int a = 0; a < size; a++){
			System.out.print("|");
			for(int b = 0; b < size; b++){
				if(east[b][a] == false && south[b][a] == true){
					System.out.print("_ ");
				}
				if(east[b][a] == true && south[b][a] == true){
					System.out.print("_|");
				}
				if(east[b][a] == false && south[b][a] == false){
					System.out.print("  ");
				}
				if(east[b][a] == true && south[b][a] == false){
					System.out.print(" |");
				}
				
			}
			System.out.println("");
		}
	}
	public static int[] getNeighbors(int s,int x, int y){
		int neighbors[] = new int[4];
		for(int q = 0; q<4; q++){
			neighbors[q] = -2;
		}
		if(x > 0){
			neighbors[3] = x - 1;  //y cord is same, WEST
		}
		if(x < s-1){
			neighbors[2] = x + 1;  //y cord is same, EAST
		}
		if(y > 0){
			neighbors[1] = y - 1;  //x cord is same, SOUTH
		}
		if(y < s-1){
			neighbors[0] = y + 1;  //x cord is same, NORTH
		}
		return neighbors;
	}
	public static boolean checkVisited(boolean[][] v, int s){
		for(int i = 0; i<s; i++){
			for(int q = 0; q<s; q++){
				if(v[i][q] == false){
					return true;
				}
			}
		}
		return false;
	}
	public static int getRand(int size){
			Random gen = new Random();
			int r = gen.nextInt(size);
			return r;
		}
	public static boolean voidOrVisited(boolean[][] v, int[] n, int cx, int cy){
		int r;
		boolean[] b = new boolean[4];
		for(int i = 0; i < 4; i++){
			r = n[i];
			if(n[i] == cx - 1 && v[r][cy] == true){
				b[i] = true;
				} 
			else if(n[i] == cx + 1 && v[r][cy] == true){
				b[i] = true;
				} 
			else if(n[i] == cy + 1 && v[cx][r] == true){
				b[i] = true;
				} 
			else if(n[i] == cy - 1 && v[cx][r] == true){
				b[i] = true;
				}
			else if(n[i] == -2){ 
				b[i] = true;
			}
		} 
		for(int j = 0; j < 4; j++){
			if(b[j] == false){ 
				return false;
			}
		} 
		return true;
	}
}