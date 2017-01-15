using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    [HideInInspector]
    public GameObject currPlayer;
    [HideInInspector]
    public PlayerController currPlayerScript;

	void Start () {
        
	}

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
        
		if (currPlayer != null) {
			if (!currPlayerScript.isMoving && currPlayerScript.availableMove > 0) { 

				Vector3 inputMovement = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
				float rotation = 0.0f;

				if (inputMovement == Vector3.right || inputMovement == Vector3.left) {
					rotation = (Input.GetAxisRaw ("Horizontal") == 1) ? 90.0f : -90.0f;
					currPlayerScript.Move (rotation, inputMovement);

				} else if (inputMovement == Vector3.forward || inputMovement == Vector3.back) {
					rotation = (Input.GetAxisRaw ("Vertical") == 1) ? 0.0f : 180.0f;
					currPlayerScript.Move (rotation, inputMovement);
				
				}else if (Input.GetAxisRaw("LookHorizontal") != 0){
					rotation = (Input.GetAxisRaw ("LookHorizontal") == 1) ? 90.0f : -90.0f;
					currPlayerScript.Rotate(new Vector3(0.0f, rotation, 0.0f));

	            }else if (Input.GetAxisRaw("LookVertical") != 0){
					rotation = (Input.GetAxisRaw ("LookVertical") == 1) ? 0.0f : 180.0f;
					currPlayerScript.Rotate(new Vector3(0.0f, rotation, 0.0f));

	            }else if (Input.GetKeyDown(KeyCode.Space)){
	                currPlayerScript.shoot();
	            }
			}
		}
    }


    


}
