using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    [HideInInspector]
    public GameObject currPlayerGameObject;
    [HideInInspector]
    public PlayerController currentPlayer;

	void Start () {
        
	}

	void Update () {

		if (GameManager.instance.gameTurn != 3) {
			currPlayerGameObject = (GameManager.instance.gameTurn == 1) ? GameManager.instance.player1GameObject : GameManager.instance.player2GameObject;
			currentPlayer = GameManager.instance.currentPlayer;
		}

		if (currPlayerGameObject != null) {
			if (!currentPlayer.isMoving && currentPlayer.availableMove > 0) { 

				//Vector3 inputMovement = new Vector3 (Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
				Vector3 inputMovement = new Vector3 (InputToInt(KeyCode.D) - InputToInt(KeyCode.A), 0.0f, InputToInt(KeyCode.W) - InputToInt(KeyCode.S)).normalized;
				float rotation = 0.0f;

				if (inputMovement == Vector3.right || inputMovement == Vector3.left) {
					rotation = (Input.GetAxisRaw ("Horizontal") == 1) ? 90.0f : -90.0f;
					currentPlayer.Move (rotation, inputMovement);

				} else if (inputMovement == Vector3.forward || inputMovement == Vector3.back) {
					rotation = (Input.GetAxisRaw ("Vertical") == 1) ? 0.0f : 180.0f;
					currentPlayer.Move (rotation, inputMovement);
				
				}else if (Input.GetAxisRaw("LookHorizontal") != 0){
					rotation = (Input.GetAxisRaw ("LookHorizontal") == 1) ? 90.0f : -90.0f;
					currentPlayer.Rotate(new Vector3(0.0f, rotation, 0.0f));

	            }else if (Input.GetAxisRaw("LookVertical") != 0){
					rotation = (Input.GetAxisRaw ("LookVertical") == 1) ? 0.0f : 180.0f;
					currentPlayer.Rotate(new Vector3(0.0f, rotation, 0.0f));

	            }else if (Input.GetKeyDown(KeyCode.Space)){
					currentPlayer.shoot();
	            }
			}

		}
    }

	int InputToInt(KeyCode input){
		if (Input.GetKeyDown (input)) {
			return 1;
		} else {
			return 0;
		}
	}

}
