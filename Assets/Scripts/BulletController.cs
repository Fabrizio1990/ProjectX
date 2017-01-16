using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float distance;
	public float currentTime;

	//private Rigidbody rb;
	private Vector3 destinaton;
	private Vector3 origin;
	private Vector3 direction;

	void Awake(){
		//rb = GetComponent<Rigidbody> ();
	}

	void Start () {
		SetNewDestination (transform.position, transform.forward.normalized * distance); 
	}

	void FixedUpdate(){
		if (Mathf.RoundToInt(Vector3.Distance (transform.position, destinaton)) == 0){
			Destroy (this.gameObject);
		}
	}

	private void SetNewDestination(Vector3 _origin, Vector3 _direction){
		// ottengo l'origine del bullet
		origin = _origin;

		// Trovo la sua direzione, la normalizzo e la moltiplico per la distanza
		direction = _direction;

		// Aggiungo la direzione all'origine per ottenere la destinazione
		destinaton = origin + direction;
	}
		
}
