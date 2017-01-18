using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float distance;														// Distanza coperta dal proiettile
	public string diagonalWallTag;												// Variabile che contiene il tag del muro diagonale

	private Rigidbody rb;														// Variabile per ottenere il RigidBody
	private Collider bulletCollider;											// Variabile per ottenere il Collider
	private Vector3 destinaton;													// Destinazione del proiettile
	private Vector3 origin;														// Il suo punto di partenza
	private Vector3 direction;													// La direzione del proiettile

	void Awake(){
		// Ottengo i componenti quali rigidBody e BoxCollider
		rb = GetComponent<Rigidbody> ();
		bulletCollider = GetComponent<SphereCollider>();
	}

	void Start () {
		// Essendo appena stato creato, ottengo la sua posizione e la sua direzione per la distanza.
		// Normalizzando il vettore, ottengo un valore che è privo di fattori esterni come la forza
		// e il delta Time, a questo punto posso moltiplicarlo per la distanza.
		SetNewDestination (transform.position, transform.up.normalized * distance); 
	}

	void FixedUpdate(){

		// Se il proiettile arriva a destinazione, lo cancello.
		if (Mathf.RoundToInt(Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), destinaton)) == 0){
			Destroy (this.gameObject);
		}

		// Se il proiettile vede un muro diagonale, non lo faccio diventare trigger.
		RaycastHit hit;
		if (Physics.Raycast (transform.position, direction, out hit, 1.0f)) {
			if (hit.collider.gameObject.tag == diagonalWallTag) {
				bulletCollider.isTrigger = false;
			}
		}
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == diagonalWallTag) {

			// Se esco dal muro diagonale, setto il suo 
			// trigger a true e potrà di nuovo interagire.
			if (!bulletCollider.isTrigger) 
				bulletCollider.isTrigger = true;

			// Procedimento per ottenere una direzione a 90°, se una coordinata
			// è più grande dell'altra, vuol dire che si dovrà muovere in quella 
			// direzione e per questo motivo la coordinata più bassa dev'essere 
			// azzerata e non deve influire il movimento.
			Vector3 dir = rb.velocity;

			float speed = 1.5f;

			if (Mathf.Abs (dir.x) > Mathf.Abs (dir.z)) {
				dir = new Vector3 (dir.x * speed, Mathf.RoundToInt (dir.y), 0.0f);
			} else {
				dir = new Vector3 (0.0f, Mathf.RoundToInt (dir.y), dir.z * speed);
			}

			rb.velocity = dir;

			// Una volta settato la nuova direzione, cambio di nuovo la destinazione 
			SetNewDestination(transform.position, rb.velocity.normalized * distance);

		}

	}

	// Funzione che mi permette di assegnare facilmente l'origine e la destinazione, partendo dalla 
	// sua direzione.
	private void SetNewDestination(Vector3 _origin, Vector3 _direction){
		// ottengo l'origine del bullet
		origin = new Vector3(_origin.x, _origin.y, _origin.z);

		// Trovo la sua direzione, la normalizzo e la moltiplico per la distanza
		direction = _direction;

		// Aggiungo la direzione all'origine per ottenere la destinazione
		destinaton = origin + direction;
	}

}
