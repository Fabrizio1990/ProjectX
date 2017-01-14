using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveDistance;
    public bool isMoving;
    public GameObject weapon;
    public GameObject bulletPrefab;
    public float journeyTime = .3f;
    public int defAvailableMove;
    public int availableMove;
    // Use this for initialization
    void Start () {
        isMoving = false;
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
        //if (GameManager.instance.turn == 3)
        //{
            switch (other.gameObject.tag) { 
                case "Moveable-N":
                case "Moveable-E":
                case "Moveable-S":
                case "Moveable-W":
                    Destroy(this.gameObject);
                    break;
                default:
                    break;

            //}
           
        }
    }

    public IEnumerator Move(Vector3 direction, Vector3 destination)
    {
        Debug.Log("sto muovendo il " + this.gameObject.name);
        isMoving = true;
        
        float approximation = .5f;
        if (canMove()) { 
            while (Vector3.Distance(transform.position, destination) > approximation && this.gameObject != null)
            {
                transform.position = Vector3.Lerp(transform.position, destination, journeyTime);
                yield return null;
            }

        transform.position = destination;
        isMoving = false;
        availableMove--;
        }

        yield return null;
    }

    public bool canMove()
    {
        bool ret = true;
        //RaycastHit hit = new RaycastHit();
       Debug.DrawRay(transform.position, transform.forward, Color.black, 4f);
        if (Physics.Raycast(transform.position, transform.forward, 1.0f))
        {
            ret = false;
        }
        Debug.Log(ret);

        return ret;
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
        bullet.tag = this.gameObject.tag + "Bullet";
        availableMove--;
    }


}
