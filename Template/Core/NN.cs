using Raylib_cs;
using System;

namespace Template.Core
{
    class NN
    {
        private int[] _networkShape;
        private Layer[] _layers;

        public NN(int[] networkShape)
        {
            _networkShape = networkShape;

            _layers = new Layer[_networkShape.Length - 1];

            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i] = new Layer(_networkShape[i], _networkShape[i + 1]);
            }
        }

        public float[] Brain(float[] inputs)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                if (i == 0)
                {
                    _layers[i].Forward(inputs);
                    _layers[i].Activation();
                }
                else if (i == _layers.Length - 1)
                {
                    _layers[i].Forward(_layers[i - 1].Nodes);
                }
                else
                {
                    _layers[i].Forward(_layers[i - 1].Nodes);
                    _layers[i].Activation();
                }
            }

            return _layers[_layers.Length - 1].Nodes;
        }

        public Layer[] copyLayers()
        {
            var tmpLayers = new Layer[_networkShape.Length - 1];

            for (int i = 0; i < _layers.Length; i++)
            {
                tmpLayers[i] = new Layer(_networkShape[i], _networkShape[i + 1]);

                Array.Copy(_layers[i].Weights, tmpLayers[i].Weights, _layers[i].Weights.GetLength(0) * _layers[i].Weights.GetLength(1));
                Array.Copy(_layers[i].Biases, tmpLayers[i].Biases, _layers[i].Biases.GetLength(0));
            }

            return tmpLayers;
        }

        public void MutateNetwork(float mutationChance, float mutationAmount)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i].MutateLayer(mutationChance, mutationAmount);
            }
        }

        public class Layer
        {
            public float[,] Weights;
            public float[] Biases;
            public float[] Nodes;

            public int InputsCount;
            public int NeuronsCount;

            public Layer(int inputsCount, int neuronsCount)
            {
                InputsCount = inputsCount;
                NeuronsCount = neuronsCount;

                Weights = new float[neuronsCount, inputsCount];
                Biases = new float[neuronsCount];
            }

            public void Forward(float[] inputsArray)
            {
                Nodes = new float[NeuronsCount];

                for (int i = 0; i < NeuronsCount; i++)
                {
                    for (int j = 0; j < InputsCount; j++)
                    {
                        Nodes[i] += inputsArray[j] * Weights[i, j];
                    }

                    Nodes[i] += Biases[i];
                }
            }

            public void Activation()
            {
                for (int i = 0; i < Nodes.Length; i++)
                {
                    Nodes[i] = (float)Math.Tanh(Nodes[i]);
                }
            }

            public void MutateLayer(float mutationChance, float mutationAmount)
            {
                for (int i = 0; i < NeuronsCount; i++)
                {
                    for (int j = 0; j < InputsCount; j++)
                    {
                        if (Raylib.GetRandomValue(0, 100) / 100f < mutationChance)
                        {
                            Weights[i, j] += Raylib.GetRandomValue(-100, 100) / 100f * mutationAmount;
                        }
                    }

                    if (Raylib.GetRandomValue(0, 100) / 100f < mutationChance)
                    {
                        Biases[i] += Raylib.GetRandomValue(-100, 100) / 100f * mutationAmount;
                    }
                }
            }
        }
    }
}
