using UnityEngine;
using System.Collections;

public class FieldGenerator : MonoBehaviour
{
	public int width = 21;
	public int height = 21;
	public int numberOfPlanes = 1;
	public int fixedWalls;
	public int moveableWalls;
	public int diagonalWalls1 = 5;
	public int diagonalWalls2 = 5;
	public GameObject wallPrefab;
	public GameObject FloorGroup;
	public GameObject terrainPrefab;
	public GameObject diagonalWall1Prefab;
	public GameObject diagonalWall2Prefab;
	public GameObject fieldSpawnPoint;
    public GameObject player1;
    public GameObject player2;
    public GameObject[] moveableWallsArray;
    public string[] moveableTags = { "Moveable-N", "Moveable-S", "Moveable-W", "Moveable-E" };
    public Color[] moveableColors = { Color.red, Color.magenta, Color.blue, Color.cyan };

    private char[] playerChars = { 'A', 'B', 'C', 'D' };
    //private Color[] playerColors = { Color.yellow, Color.green, Color.black, Color.grey };
    private char[,] field;
    private const char nWall = 'N';
	private const char sWall = 'S';
	private const char wWall = 'W';
	private const char eWall = 'E';
	private const char fWall = 'F';
	private const char d1Wall = 'D';
	private const char d2Wall = 'd';
	private const string moveableNWall = "Moveable-N";
	private const string moveableSWall = "Moveable-S";
	private const string moveableEWall = "Moveable-E";
	private const string moveableWWall = "Moveable-W";
	private const string fixedWall = "Fixed";
	private const string diagonalWall1 = "Diagonal1";
	private const string diagonalWall2 = "Diagonal2";

    void Awake()
    {
        moveableWallsArray = new GameObject[moveableWalls];
    }

	// Use this for initialization
	void Start ()
	{
       
	}

	public void Generate ()
	{
		GameObject[] fixedWallsArray;
		GameObject[] diagonal1WallsArray;
		GameObject[] diagonal2WallsArray;

		field = new char[height, width];
		GenerateFieldFrame ();
		fixedWallsArray = GenerateInnerWalls (wallPrefab, moveableWalls, new string[]{ fixedWall }, new char[] { fWall });
		moveableWallsArray = GenerateInnerWalls (wallPrefab, fixedWalls, new string[] {
			moveableNWall,
			moveableSWall,
			moveableEWall,
			moveableWWall
		}, new char[] {
			nWall,
			sWall,
			eWall,
			wWall
		});
		diagonal1WallsArray = GenerateInnerWalls (diagonalWall1Prefab, diagonalWalls1, new string[]{ diagonalWall1 }, new char[] { d1Wall });
		diagonal2WallsArray = GenerateInnerWalls (diagonalWall2Prefab, diagonalWalls1, new string[]{ diagonalWall2 }, new char[] { d2Wall });
		DebugWalls (fixedWallsArray, Color.gray, Color.red, Color.magenta, Color.blue, Color.cyan); //Color nWallColor, Color sWallColor, Color wWallColor, Color eWallColor
        DebugWalls (moveableWallsArray, Color.gray, Color.red, Color.magenta, Color.blue, Color.cyan);

        for(int i = 0; i< GameManager.instance.numberOfPlayer; i++) {
		    GeneratePlayer (GameManager.instance.players[i], playerChars[i]);
            //DebugPlayer(GameManager.instance.players[i], playerColors[i]);
        }
	}

	private void GenerateFieldFrame ()
	{
		int row;
		int col;

		// North and south frame
		for (col = 0; col < width; col++) {
			GenerateGameObject (wallPrefab, 0, col, moveableNWall, nWall);
			GenerateGameObject (wallPrefab, height - 1, col, moveableSWall, sWall);
		}
		// West and east frame
		for (row = 1; row < height - 1; row++) {
			GenerateGameObject (wallPrefab, row, 0, moveableWWall, wWall);
			GenerateGameObject (wallPrefab, row, width - 1, moveableEWall, eWall);
		}
		// Generate Terrain
		for(int i = 0; i < width; i ++){
			for(int j = 0; j < height; j ++){
				GameObject c = Instantiate (terrainPrefab, new Vector3 (fieldSpawnPoint.transform.position.x + i, 0.0f, fieldSpawnPoint.transform.position.z + j), Quaternion.identity) as GameObject;
				c.transform.SetParent (FloorGroup.transform);
			}
		}
	}

	private GameObject[] GenerateInnerWalls (GameObject currWallPrefab, int numberOfWalls, string[] wallTags, char[] wallChars)
	{
		GameObject[] generatedWalls;
		string wallTag;
		char wallChar;
		int row;
		int col;
		int wallIndex;
		int index;
		bool ok;

		generatedWalls = new GameObject[numberOfWalls];
		for (wallIndex = 0; wallIndex < numberOfWalls; wallIndex++) {
			ok = false;
			while (!ok) {
				row = Random.Range (1, height - 1);
				col = Random.Range (1, width - 1);
				if (field [row, col] == '\0') {
					index = Random.Range (0, wallTags.Length);
					wallTag = wallTags [index];
					wallChar = wallChars [index];
					generatedWalls [wallIndex] = GenerateGameObject (currWallPrefab, row, col, wallTag, wallChar);
					ok = true;
				}
			}
		}
		return generatedWalls;
	}

	private void GeneratePlayer (GameObject player, char playerChar)
	{
		int row;
		int col;
		bool ok;

		ok = false;
		while (!ok) {
			row = Random.Range (1, height - 1);
			col = Random.Range (1, width - 1);
			if (field [row, col] == '\0') {
				player.transform.position = new Vector3 (col, 0.5f, row);
				field [row, col] = playerChar;
				ok = true;
			}
		}
	}

	private GameObject GenerateGameObject (GameObject prefab, int row, int col, string gameObjectTag, char gameObjectChar)
	{
		GameObject newGameObject;
		Vector3 gameObjectPosition;

		gameObjectPosition = new Vector3 (col, 0.5f, row);
		newGameObject = Instantiate (prefab) as GameObject;
		newGameObject.transform.position = gameObjectPosition;
		newGameObject.tag = gameObjectTag;
		newGameObject.name = gameObjectChar.ToString();
		newGameObject.transform.SetParent (fieldSpawnPoint.transform);
		field [row, col] = gameObjectChar;
		return newGameObject;
	}

	private void DebugWalls (GameObject[] walls, Color fWallColor, Color nWallColor, Color sWallColor, Color wWallColor, Color eWallColor)
	{
		MeshRenderer wallMesh;
		Color wallColor;
		int index;

		for (index = 0; index < walls.Length; index++) {
			wallMesh = walls [index].GetComponent<MeshRenderer> ();
			wallColor = Color.black;
			if (walls [index].tag.Equals (moveableNWall)) {
				wallColor = nWallColor;
			} else if (walls [index].tag.Equals (moveableSWall)) {
				wallColor = sWallColor;
			} else if (walls [index].tag.Equals (moveableWWall)) {
				wallColor = wWallColor;
			} else if (walls [index].tag.Equals (moveableEWall)) {
				wallColor = eWallColor;
			} else if (walls [index].tag.Equals (fixedWall)) {
				wallColor = fWallColor;
			}
			wallMesh.material.color = wallColor;
		}
	}

	private void DebugPlayer (GameObject player, Color playerColor)
	{
		MeshRenderer wallMesh;

		wallMesh = player.GetComponent<MeshRenderer> ();
		wallMesh.material.color = playerColor;
	}
}
