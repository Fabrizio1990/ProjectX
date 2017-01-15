using UnityEngine;
using System.Collections;

public class BullletManager : MonoBehaviour {

	public float speed;
	public float distance;

	public Vector3 direction;
	private float actualDistance;

	void Start () {
		//direction = transform.forward;
		actualDistance = distance;
     
	}
		
	void Update () {
		if (actualDistance > 0) {
			//transform.Translate (direction * Time.deltaTime * speed);
			transform.position += transform.up * Time.deltaTime * speed;
			actualDistance -= Time.deltaTime * speed;
        }else
        {
            Destroy(this.gameObject);
        }
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "PlaneDevSX") {
			if (direction == Vector3.forward || direction == Vector3.back) {
				direction = Vector3.zero;
				direction = Vector3.up; 
			} else if (direction == Vector3.up || direction == Vector3.down) {
				direction = Vector3.zero;
				direction = Vector3.back; 
			}

			actualDistance = distance;

		} else if (other.gameObject.tag == "PlaneDevDX") {
			if (direction == Vector3.forward || direction == Vector3.back) {
				direction = Vector3.zero;
				direction = Vector3.down; 
			} else if (direction == Vector3.up || direction == Vector3.down) {
				
				direction = Vector3.zero;
				direction = Vector3.forward; 
			}
		}

		actualDistance = distance;
	}
}
