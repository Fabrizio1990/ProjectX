using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;								// Variabile Statica contenente l'istanza del GameManager.

    public GameObject[] playersPrefab;                                      // Array che contiene i prefab dei player

    [HideInInspector]
    public GameObject[] players;                                            // Array che contiene le istanze dei player 
    [HideInInspector]
    public GameObject currentPlayer;									    // Variabile che contiene il giocatore attuale.
    [HideInInspector]
    public PlayerController currentPlayerScript;                             // Variabile che contiene lo script del giocatore attuale.

    public int numberOfPlayer;                                              // Numero di giocatori che si vogliono avere in scena
    [HideInInspector]
    public int numberOfTurns;                                               // Numero di turni ( numero giocatori +1)

    [HideInInspector]
    public FieldGenerator fieldScript;										// Variabile dell'istanza del generatore di terreno.
    [HideInInspector]
    public InputManager inputController;									// Input.
	

    public int gameTurn;													// Variabile che segna il turno attuale.
 
	//public int totalWallMoved;											// Non ricordo cazzo.

    private bool wallEnded;		                                             // Non ricordo cazzo - Il ritorno.
	private PlayerController[] playersScript;			                     // Array contenenti gli script dei  giocatori.

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
		fieldScript     = GetComponent<FieldGenerator>();

        //setto il numero dei turni (numero player +1 "il turno dei muri")
        numberOfTurns   = numberOfPlayer + 1;
        // Inizializzo gli array dei player e degli script dei player
        players         = new GameObject[numberOfPlayer];
        playersScript   = new PlayerController[numberOfPlayer];

        // in base a quanti giocatori si è deciso di essere instanzio i player e ne recupero gli script
        for (int i = 0; i < numberOfPlayer; i++)
        {
            GameObject playerPrefab = playersPrefab[i];
            GameObject playerInstance = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation) as GameObject;
            playerInstance.name = "Player" + (i+1);
            players[i] = playerInstance;
            playersScript[i] = playerInstance.GetComponent<PlayerController>();
        }


        // Il player che inizia è il player1.
        currentPlayer = players[0];
        currentPlayerScript = playersScript[0];

		// Il turno attuale è il turno 1.
		gameTurn = 1;

		//totalWallMoved = 0;

    }

    void Start()
    {
        // Generazione del terreno
        fieldScript.Generate();
    }

    void Update()
    {
		if (gameTurn < numberOfTurns) {
			if (currentPlayerScript.endTurn == true) {
                currentPlayerScript.resetMove ();
				NextTurn();

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
        //aumento il turno o lo riporto a 1 se sono all ultimo turno (quello dei muri)
		gameTurn = (gameTurn == numberOfTurns) ? 1 : (gameTurn + 1);

        // se è il turno dei muri non assegno il currentPlayer e currentPlayerScript
        if(gameTurn < numberOfTurns) {
            currentPlayer = players[gameTurn-1];
            currentPlayerScript = playersScript[gameTurn-1];
        }
        else{
            currentPlayer = null;
            currentPlayerScript = null;
        }
    }


}
