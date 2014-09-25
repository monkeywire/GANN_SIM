using UnityEngine;
using System.Collections;


public class Creature : MonoBehaviour {

	public int        Fitness = 0;
	public int        Speed;
	public int        RotationSpeed;
	public string     TargetTag;

	public Transform     Target;
	public CreatureBrain Brain;
	//priavte CreatureBody  Body;

	// We have to do this to other wise we can't access these compotents on creation
	// untill the next frame executes.  Awake is called almost directly have Initiation of a new object.
	void Awake () {
		Brain = new CreatureBrain();
		//Body  = new CreatureBody();
	}

	void Start () {
		InvokeRepeating("TargetNearestFoodSource", 0, 0.05f);
	}

	void Update () {
		MoveTowardsTarget();
	}

	public void MoveTowardsTarget() {
		if(Target != null) {
			transform.position = Vector3.MoveTowards(transform.position,
			                                         Target.position,
			                                         Time.deltaTime * Speed);
			
			Vector3 direction = (transform.position - Target.position).normalized;
			direction.y = transform.position.y;
			var rotation  = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation,
			                                      rotation,
			                                      Time.deltaTime * Speed);
		}
	}

	/*
	 * We find the nearest Target and make the determination if it is food or not
	 */
	public void TargetNearestFoodSource() {
		GameObject nearestObj = null;
		float nearestDistance = Mathf.Infinity;
		
		var taggedObjects = GameObject.FindGameObjectsWithTag(TargetTag);
		foreach(GameObject obj in taggedObjects) {
			Vector3 objPosition = obj.transform.position;
			float distanceSqr = (objPosition - transform.position).sqrMagnitude;
			GameObject parentObj = obj.transform.parent.gameObject;
			if(distanceSqr < nearestDistance && Brain.IsThisFood(parentObj)) {
				nearestDistance = distanceSqr;
				nearestObj      = obj;
			}
		}
		if(nearestObj != null)
			Target = nearestObj.transform;
	}

	/*
	 * Handle Collisions.
	 */
	public void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "Food" && Brain.IsThisFood(collider.gameObject)) {
			Destroy (collider.gameObject);
			Fitness++;
		}

		if(collider.gameObject.tag == "Posion" && Brain.IsThisFood(collider.gameObject)) {
			Debug.Log ("A creature eat posion and died!");
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
