using UnityEngine;
using System.Collections.Generic;
public class Layer
{
 
    public int NumNeurons;
    public List<Neuron> vecNeurons = new List<Neuron>();
 
    public Layer(int f_NumNeurons, int numInputsPerNeuron)
    {
 
        for(int i=0; i<f_NumNeurons; i++)
        {
            vecNeurons.Add(new Neuron(numInputsPerNeuron));
        }
        NumNeurons = f_NumNeurons;
    }
 
}