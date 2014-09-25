using UnityEngine;
using System.Collections;

public static class Constants {
	// This game runs on a 3d system with limits of CORD_MAX and 
	// CORD_MIN for all three planes x,y, and z.
	public const float CORD_MAX = 10.0f;
	public const float CORD_MIN = -10.0f;

	/* Number of objects to spawn at the start of each new generation */
	public const int NUM_CREATURES = 30;
	public const int NUM_POSION    = 10;
	public const int NUM_FOOD      = 20;

	public const double MUTATION_CHANCE = 0.10;
}
