using UnityEngine;
using System.Collections;

public class MovingBlockBehaviour : MonoBehaviour {

	public float speed;
	public bool InMov;
	public LayerMask layerMask;

	private Vector3 getDirectionFromTag(string name){
		Vector3 finalDir = Vector3.zero;

		switch(name){
		case "Moveable-N":
			finalDir = new Vector3 (0.0f, 0.0f, 1.0f);
			break;
		case "Moveable-S":
			finalDir = new Vector3 (0.0f, 0.0f, -1.0f);
			break;
		case "Moveable-E":
			finalDir = new Vector3 (1.0f, 0.0f, 0.0f);
			break;
		case "Moveable-W":
			finalDir = new Vector3 (-1.0f, 0.0f, 0.0f);
			break;
		}

		return finalDir;

	}


	public void InitLerp(float maxDistanceRaycast){
		Vector3 dir = getDirectionFromTag(gameObject.tag);
		if (!Physics.Raycast(transform.position, dir, maxDistanceRaycast, layerMask) && !InMov) {
			InMov = true;
			StartCoroutine (Move (dir));

		}
	}
	
	IEnumerator Move(Vector3 dir){
		Vector3 origin = transform.position;
		while (Vector3.Distance(transform.position, origin + dir) > .1) {
			transform.position = Vector3.Lerp (transform.position, origin + dir, Time.deltaTime * speed); 
			yield return new WaitForSeconds (Time.deltaTime);
		}
		InMov = false;
		transform.position = origin + dir;
        GameManager.instance.totalWallMoved++;
        ChangeType();
        yield return null;
	}

    void ChangeType()
    {
        int randomString = Random.Range(0, 4);

        gameObject.tag = GameManager.instance.fieldScript.moveableTags[randomString];
        gameObject.GetComponent<MeshRenderer>().material.color = GameManager.instance.fieldScript.moveableColors[randomString];
    }

}
