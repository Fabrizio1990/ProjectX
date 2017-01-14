using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public float journeyTime;
    [HideInInspector]
    public GameObject currPlayer;
    [HideInInspector]
    public PlayerController currPlayerScript;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.turn == 1)
        {
            currPlayer = GameManager.instance.Player1;
            currPlayerScript = GameManager.instance.Player1Script;
        }
        if (GameManager.instance.turn == 2)
        {
            currPlayer = GameManager.instance.Player2;
            currPlayerScript = GameManager.instance.Player2Script;
        }
        
        if (!currPlayerScript.isMoving && currPlayerScript.availableMove>0 && currPlayer !=  null) { 
            if (Input.GetKeyDown(KeyCode.W))
            {
                Vector3 destination = currPlayer.transform.position + new Vector3(0, 0, currPlayerScript.moveDistance);
                Vector3 rotation = new Vector3(0, 0, 0);
                currPlayerScript.Rotate(rotation);
                if(currPlayerScript.canMove())
                    StartCoroutine(currPlayerScript.Move(Vector3.up,destination));

            }else if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 destination = currPlayer.transform.position + new Vector3(0, 0, -currPlayerScript.moveDistance);
                Vector3 rotation = new Vector3(0, 180, 0);
                currPlayerScript.Rotate(rotation);
                if (currPlayerScript.canMove())
                    StartCoroutine(currPlayerScript.Move(Vector3.down, destination));
            }else if(Input.GetKeyDown(KeyCode.A))
            {
                Vector3 destination = currPlayer.transform.position + new Vector3(-currPlayerScript.moveDistance, 0 , 0);
                Vector3 rotation = new Vector3(0, 270, 0);
                currPlayerScript.Rotate(rotation);
                if (currPlayerScript.canMove())
                    StartCoroutine(currPlayerScript.Move(Vector3.left,  destination));
            }else if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 destination = currPlayer.transform.position + new Vector3(currPlayerScript.moveDistance, 0, 0);
                Vector3 rotation = new Vector3(0, 90 , 0);
                currPlayerScript.Rotate(rotation);
                if (currPlayerScript.canMove())
                    StartCoroutine(currPlayerScript.Move(Vector3.right,  destination));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                Vector3 rotation = new Vector3(0, 0, 0);
                currPlayerScript.Rotate(rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Vector3 rotation = new Vector3(0, 180, 0);
                currPlayerScript.Rotate(rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Vector3 rotation = new Vector3(0, 270, 0);
                currPlayerScript.Rotate(rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                Vector3 rotation = new Vector3(0, 90, 0);
                currPlayerScript.Rotate(rotation);
            }else if (Input.GetKeyDown(KeyCode.Space))
            {
                currPlayerScript.shoot();
            }
        }
    }


    


}
