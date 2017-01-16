using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveDistance;
    public bool isMoving;
    public GameObject weapon;
    public GameObject bulletPrefab;
	public float bulletSpeed;
    public float journeyTime = .3f;
    public int defAvailableMove;
    public int availableMove;
    // Use this for initialization
    void Start () {
        isMoving = false;
		bulletSpeed = 500.0f;
        moveDistance = 1;
        resetMove();
    }
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1Bullet" && this.gameObject.tag == "Player2")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }else if(other.gameObject.tag == "Player2Bullet" && this.gameObject.tag == "Player1")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (GameManager.instance.turn == 3)
        {
            switch (other.gameObject.tag) { 
                case "Moveable-N":
                case "Moveable-E":
                case "Moveable-S":
                case "Moveable-W":
                    Destroy(this.gameObject);
                    break;
                default:
                    break;

            }
           
        }
    }

	public void Move(float _rotation, Vector3 _dir){
		// Ruoto il giocatore nella direzione
		Rotate(new Vector3(0.0f, _rotation, 0.0f));
		
		// Moltiplico la direzione per la velocità
		_dir *= moveDistance;

		// Controllo con un raycast se posso effettivamente muovermi
		if (!Physics.Raycast (transform.position, transform.forward, 1.0f)) {
			isMoving = true;
			StartCoroutine(MoveInDirection (_dir));
		}
	}

    public IEnumerator MoveInDirection(Vector3 destination)
    {
        float approximation = .5f;
		destination = transform.position + destination;

		while (Vector3.Distance(transform.position, destination) > approximation && this.gameObject != null){
        	transform.position = Vector3.Lerp(transform.position, destination, journeyTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
        availableMove--;

		yield return null;
    }

    public void Rotate(Vector3 direction)
    {
        transform.rotation = Quaternion.Euler(direction);
    }

    public void resetMove()
    {
        availableMove = defAvailableMove;
    }

    public void shoot()
    {
		GameObject bullet = Instantiate(bulletPrefab, weapon.transform.position , weapon.transform.rotation) as GameObject;
		Rigidbody rbBullet = bullet.GetComponent<Rigidbody> ();
		bullet.tag = this.gameObject.tag + "Bullet";
		rbBullet.AddForce (weapon.transform.forward * Time.fixedDeltaTime * bulletSpeed, ForceMode.Impulse);
        availableMove--;
    }


}
