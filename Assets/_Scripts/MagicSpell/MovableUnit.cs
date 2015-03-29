using UnityEngine;
using System.Collections;
/// <summary>
/// May need explodeLink to delegate the caster info
/// But no explodeLink is still plausible
/// </summary>
public class MovableUnit : MonoBehaviour {
	public Vector3 destination;
	public GameObject explosion;
	public bool isMoving = true;
	public float speed;
	public Vector3 curSpeed;
	public float damage = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate (){

		// If the object is already there, explode
		if ((transform.position - destination).magnitude < 1.0f && isMoving){
			isMoving = false;
			// Cause Explosion Here
			ExplodeLink explodeLink = GetComponent<ExplodeLink>();
			if (explodeLink) {
				explodeLink.CasterDelegateDestroy(destination);
			}
			else {
				// no caster delegate 
				Instantiate(explosion, destination, Quaternion.identity);
				Destroy(gameObject);
			}
		}

		// Reflect if speed change 
		// change the orientation for effect glitches
		if ((GetComponent<Rigidbody>().velocity - curSpeed).magnitude > 0.1f)
		{
			// Debug.Log("Rotate Angle: " + Vector3.Angle(curSpeed, rigidbody.velocity));
			float angle = Vector3.Angle(curSpeed, GetComponent<Rigidbody>().velocity);
			Vector3 cross = Vector3.Cross(curSpeed, GetComponent<Rigidbody>().velocity);
			if (cross.y < 0) angle = -angle;
			transform.Rotate(new Vector3 (0, angle, 0));

			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * speed; // enfore a constant speed 
			curSpeed = GetComponent<Rigidbody>().velocity;  
		}
	}

	void OnTriggerEnter(Collider other) {
		// avoid collider with self

		ExplodeLink explodeLink = GetComponent<ExplodeLink>();
		if (explodeLink && 
		    other.gameObject.GetInstanceID() == explodeLink.caster.GetInstanceID()) {
			// nothing should happen
			return;
		}
		// if other got a explodelink as well.
		// Then we want to identify whether they are from same guy
		ExplodeLink otherExplodeLink = other.GetComponent<ExplodeLink>();
		if (otherExplodeLink && 
		    otherExplodeLink.caster.GetInstanceID() == explodeLink.caster.GetInstanceID()) {
			// nothing should happen
			return;
		}


		if ((other.gameObject.tag == TagList.Player
		    || other.gameObject.tag == TagList.Fireball) && isMoving){

			isMoving = false;
			// Cause Explosion Here
			// Debug.Log ("Knocked On other, explode now");
			Debug.DrawLine (transform.position,
			                new Vector3(transform.position.x, 30.0f, transform.position.z),Color.red,10.0f);

			if (explodeLink) {
				explodeLink.CasterDelegateDestroy(transform.position);
			}
			else {
				// no caster delegate 
				Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		
	}

	public void MoveTo(Vector3 destination) {
		// TODO HARD CODE
		transform.Rotate (new Vector3 (0, 180, 0));
		this.destination = destination;
		// destination.y = transform.position.y;
		destination.y = 0.5f; // should be a little bit higher to separate from ground
		Vector3 moveDirection = (destination - transform.position).normalized;
		GetComponent<Rigidbody>().velocity = speed * moveDirection;
		curSpeed = GetComponent<Rigidbody>().velocity;

	}
}
