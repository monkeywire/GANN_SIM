using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Genetitics {

	private static System.Random rGen = new System.Random();  //TODO Check out unities Random!

	/*
	 * Breed two creatures together to create a baby
	 */
	public static void Breed(GameObject father, GameObject mother, GameObject prefab) {
		double[] gF = father.GetComponent<Creature>().Brain.GetGeneticCode();
		double[] gM = mother.GetComponent<Creature>().Brain.GetGeneticCode();

		List<double> gB = new List<double>();
		for(int i = 0; i < gF.Length; i++) {  //TODO Make this more random selection???
			double g = i % 2 == 1 ? gF[i] : gM[i];
			gB.Add(g);
		}

		GameObject baby = (GameObject)MonoBehaviour.Instantiate(prefab);
		baby.GetComponent<Creature>().Brain.SetGeneticCode(gB.ToArray());
		Debug.Log ("A baby was born!");
	}

	/*
	 * Have 'mutationChance' to Mutate a random gene 
	 */
	public static void Mutate(GameObject creature, double mutationChance) {
		if(RandomDouble (0.0, 1.0) < mutationChance) {
			Debug.Log ("A creature got a random mutation!");
			double[] gC = creature.GetComponent<Creature>().Brain.GetGeneticCode();
			int geneIndex = (int)RandomDouble (0.0, 1.0) * gC.Length;
			gC[geneIndex] = RandomDouble(-1.0, 1.0);
			creature.GetComponent<Creature>().Brain.SetGeneticCode(gC);
		}
	}

	public static double RandomDouble(double min, double max)
	{ return rGen.NextDouble() * (max - min) + min; }

	public static double[] RandomDoubleArray(int size, double min, double max) 
	{
		List<double> l = new List<double>();
		for(int i = 0; i < size; i++) {
			l.Add (RandomDouble(min, max));
		}
		return l.ToArray();
	}
}
