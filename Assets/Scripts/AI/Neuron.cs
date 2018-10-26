using System.Collections.Generic;
using UnityEngine;
public class Neuron
{
 
    public int numInputs;
    public List<double> vecWeights = new List<double>();
 
    public Neuron(int f_NumInputs)
    {
        for(int i = 0; i <f_NumInputs + 1; i++)
        {
            vecWeights.Add(Random.Range(-1f,1f));
        }
        numInputs = f_NumInputs;
    }
 
}