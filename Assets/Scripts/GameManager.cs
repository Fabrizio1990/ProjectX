using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;								// Variabile Statica contenente l'istanza del GameManager.

    [HideInInspector]
    public FieldGenerator fieldScript;										// Variabile dell'istanza del generatore di terreno.
    [HideInInspector]
    public InputManager inputController;									// Input.
	[HideInInspector]
	public PlayerController currentPlayer;									// Variabile che contiene il giocatore attuale.
    public GameObject player1GameObject;									// Variabile che contiene il GameObject del Giocatore 1.
	public GameObject player2GameObject;									// Variabile che contiene il GameObject del Giocatore 2.
    public int gameTurn;													// Variabile che segna il turno attuale.
 
	//public int totalWallMoved;											// Non ricordo cazzo.

    private bool wallEnded;													// Non ricordo cazzo - Il ritorno.
	private PlayerController[] players = new PlayerController[2];			// Array dei due giocatori.

    void Awake()
    {
		// Singleton per evitare doppie istanze del GameManager.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}

		// Otteniamo l'input e il fieldGenerator.
		inputController = GetComponent<InputManager>();
		fieldScript = GetComponent<FieldGenerator>();

		// Otteniamo gli script dei due giocatori.
		players [0] = player1GameObject.GetComponent<PlayerController> ();
		players [1] = player2GameObject.GetComponent<PlayerController> ();

		// Il player che inizia è il player1.
		currentPlayer = players [0];

		// Il turno attuale è il turno 1.
		gameTurn = 1;

		//totalWallMoved = 0;

    }

    void Start()
    {

    }

    void Update()
    {
		if (gameTurn != 3) {
			if (players [getPlayerByTurn (gameTurn)].availableMove == 0) {
				players [getPlayerByTurn (gameTurn)].resetMove ();
				NextTurn ();

			}
		} else {
            // Muovo i Muri.
            for (int i = 0; i < fieldScript.moveableWallsArray.Length; i++)
            {
                fieldScript.moveableWallsArray[i].GetComponent<MovingBlockBehaviour>().InitLerp(1.0f);
            }

            NextTurn();
        }
    }

	// Assegno il turno successivo e Cambio il giocatore attuale.
    private void NextTurn(){
		gameTurn = (gameTurn == 3) ? 1 : (gameTurn + 1);
		currentPlayer = players [getPlayerByTurn(gameTurn)];
    }

	// Ottengo l'id del player corrispondente al turno nell'array.
	int getPlayerByTurn(int turn){
		return (turn == 1) ? 0 : 1;
	}
}
