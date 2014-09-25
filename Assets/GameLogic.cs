using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*
 * This is the core of the simulation.
 * 
 * It controlls the High Level Logic.
 */
public class GameLogic : MonoBehaviour {
	public GameObject CreaturePrefab;
	public GameObject FoodPrefab;
	public GameObject PosionPrefab;

	/* OnStart we Initilize all our simulation objets */
	void Start () {
		Init_Objects(CreaturePrefab, Constants.NUM_CREATURES);
		Init_Objects(FoodPrefab, Constants.NUM_FOOD);
		Init_Objects(PosionPrefab, Constants.NUM_POSION);

		// We don't want to call this every frame!
		InvokeRepeating("NextGeneration", 0, 5);
	}

	/*
	 * A simple function that will create 'instances' number of
	 * a specified GameObject - read prefab.
	 */
	void Init_Objects(GameObject gameObject, int instances) {
		for(int i = 0; i < instances; i++)
			GameObject.Instantiate(gameObject);
	}

	/*
	 * To be run at the end of the current simulation.
	 */
	public void NextGeneration() {
		if(IsGenerationDone()) {  // Only run this if this simulation run is at the end
			var creatures = SortByFitness();
			foreach(var creature in creatures) {
				// Select a random number of creatures for a random mutation.
				Genetitics.Mutate(creature.Key, Constants.MUTATION_CHANCE);
				if(creature.Value == 0)
					Destroy (creature.Key);  //A creature starved to death.
				else                         //Otherwise we can reset it's fitness for the next generation
					creature.Key.GetComponent<Creature>().Fitness = 0;
			}
			if(creatures.Count() < 2)
				Debug.Log ("There is not enough creatures to propagate...the species has died");
			else
				Genetitics.Breed(creatures.ElementAt(0).Key, creatures.ElementAt(1).Key, CreaturePrefab);

			Init_Objects(FoodPrefab, Constants.NUM_FOOD - CountTag("Food"));
			Init_Objects(PosionPrefab, Constants.NUM_POSION - CountTag("Posion"));
		}
	}

	/*
	 * We set condition for the end of the current generation here.
	 */
	public bool IsGenerationDone() {
		var food  = GameObject.FindGameObjectsWithTag("Food");
		return food.Length == 0;
	}

	public IOrderedEnumerable<KeyValuePair<GameObject, int>> SortByFitness() {
		Dictionary<GameObject, int> creatureDict = new Dictionary<GameObject, int>();
		foreach(var creatureObj in GameObject.FindGameObjectsWithTag("Creature")) {
			if(creatureObj.GetComponent<Creature>() != null) {  //We have to check that it has not been destroyed in the meantime.
				int fitness = creatureObj.GetComponent<Creature>().Fitness;
				creatureDict.Add (creatureObj, fitness);
			}
		}

		return from pair in creatureDict
			   orderby pair.Value descending
			   select pair;
	}

	public int CountTag(string tag) {
		return GameObject.FindGameObjectsWithTag(tag).Length;
	}
}