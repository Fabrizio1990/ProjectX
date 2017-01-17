using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float distance;
	public float currentTime;

	private Rigidbody rb;
	private Collider bulletCollider;
	private Vector3 destinaton;
	private Vector3 origin;
	private Vector3 direction;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		bulletCollider = GetComponent<SphereCollider>();
	}

	void Start () {
		SetNewDestination (transform.position, transform.forward.normalized * distance); 
	}

	void FixedUpdate(){

		/*if (Mathf.RoundToInt(Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z), destinaton)) == 0){
			Destroy (this.gameObject);
		}*/

		/*RaycastHit hit;
		if (Physics.Raycast (transform.position, direction, out hit, 1.0f)) {
			if (hit.collider.gameObject.name == "D") {
				bulletCollider.isTrigger = false;
			}
		}*/
	}

	private void SetNewDestination(Vector3 _origin, Vector3 _direction){
		// ottengo l'origine del bullet
		origin = new Vector3(_origin.x, 0.0f, _origin.z);

		// Trovo la sua direzione, la normalizzo e la moltiplico per la distanza
		direction = _direction;

		// Aggiungo la direzione all'origine per ottenere la destinazione
		destinaton = origin + direction;
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "Diagonal1") {
			SetNewDestination(transform.position, rb.velocity.normalized * distance);
		
			if (!bulletCollider.isTrigger) {
				bulletCollider.isTrigger = true;
			}
		}

	}
}
