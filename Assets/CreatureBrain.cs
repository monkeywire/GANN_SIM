using UnityEngine;
using System.Collections;
using Encog.Neural.Networks;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks.Structure;
using Encog.Neural.Networks.Layers;
using Encog.MathUtil.Randomize;

public class CreatureBrain {
	/*Neural Networks Makeing up the Brain*/
	private BasicNetwork IsEdible;

	public CreatureBrain() {
		/* Init all the networks in the brain */
		Init_IsEdible();
	}

	public void Init_IsEdible() {
		IsEdible = new BasicNetwork();
		IsEdible.AddLayer(new BasicLayer(1));
		IsEdible.AddLayer(new BasicLayer(2));
		IsEdible.AddLayer(new BasicLayer(2));
		IsEdible.AddLayer(new BasicLayer(1));
		IsEdible.Structure.FinalizeStructure();
		SetGeneticCode(Genetitics.RandomDoubleArray(GetGeneticCode().Length, -1.0, 1.0));
		// (new RangeRandomizer(-1,1)).Randomize(IsEdible); When creating quickly, this can use the same timestamp so above is more random.
	}

	public bool IsThisFood(GameObject obj) {
		double[] input = new double[1];
		input[0] = obj.tag == "Food" ? 1 : 0;
		var output = IsEdible.Compute(new BasicNeuralData(input));
		return output[0] > 0.0f ? true : false;		
	}

	public double[] GetGeneticCode() {
		return NetworkCODEC.NetworkToArray(IsEdible);
	}

	public void SetGeneticCode(double[] geneticCode) {
		NetworkCODEC.ArrayToNetwork(geneticCode, IsEdible);
	}
}
