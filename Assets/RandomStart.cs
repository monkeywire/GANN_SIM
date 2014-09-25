using UnityEngine;
using System.Collections;

public class RandomStart : MonoBehaviour {

	void Start () {
		transform.position = RandomVector3();
	}

	private Vector3 RandomVector3() {
		float randX = RandomFloat ();
		float randY = RandomFloat ();
		float randZ = RandomFloat ();

		return new Vector3(randX, randY, randZ);
	}

	private float RandomFloat() {
		return Random.Range (Constants.CORD_MIN, Constants.CORD_MAX);
	}
}
