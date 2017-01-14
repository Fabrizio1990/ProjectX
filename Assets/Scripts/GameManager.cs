using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public FieldGenerator fieldScript;
    [HideInInspector]
    public InputManager inputController;
    public GameObject Player1;
    public GameObject Player2;

    [HideInInspector]
    public PlayerController Player1Script;
    [HideInInspector]
    public PlayerController Player2Script;

    public int turn;
    public int totalWallMoved;

    private bool cicloEseguito;
    private bool wallEnded;

    void Awake()
    {
        instance = this;
        inputController = GetComponent<InputManager>();
        Player1Script = Player1.GetComponent<PlayerController>();
        Player2Script = Player2.GetComponent<PlayerController>();
        fieldScript = GetComponent<FieldGenerator>();
        totalWallMoved = 0;
        turn = 1;
    }

    void Start()
    {

    }

    void Update()
    {
        if (turn == 1 && Player1Script.availableMove == 0)
        {
            NextTurn();
            Player2Script.resetMove();
        }
        else if (turn == 2 && Player2Script.availableMove == 0)
        {
            NextTurn();
            Player1Script.resetMove();
        }
        else if (turn == 3)
        {


            // Muovo i Muri.
            for (int i = 0; i < fieldScript.moveableWallsArray.Length; i++)
            {
                fieldScript.moveableWallsArray[i].GetComponent<MovingBlockBehaviour>().InitLerp(1.0f);
            }




            NextTurn();

        }
    }

    void NextTurn()
    {
        if (turn == 3)
        {
            turn = 1;
        }
        else
        {
            turn++;
        }
    }
}
