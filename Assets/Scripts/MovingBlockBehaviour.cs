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
			finalDir = Vector3.forward;
			break;
		case "Moveable-S":
			finalDir = Vector3.back;
			break;
		case "Moveable-E":
			finalDir = Vector3.right;
			break;
		case "Moveable-W":
			finalDir = Vector3.left;
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
		while (Mathf.RoundToInt(Vector3.Distance(transform.position, origin + dir)) > 0) {
			transform.position = Vector3.Lerp (transform.position, origin + dir, Time.deltaTime * 5.0f); 
			yield return null;
		}

		InMov = false;
		transform.position = origin + dir;
        //GameManager.instance.totalWallMoved++;
        ChangeType();
        yield return null;
	}

    void ChangeType()
    {
        int randomString = Random.Range(0, 4);

        gameObject.tag = GameManager.instance.fieldScript.moveableTags[randomString];
        gameObject.GetComponent<MeshRenderer>().material.color = GameManager.instance.fieldScript.moveableColors[randomString];
    }

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Bullet(Clone)") {
			Destroy (other.gameObject);
		}
	}
}
