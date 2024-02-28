using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeuralNetwork2020
{
    [Serializable]
    public class NeuralNetwork
    {
        public enum Activation
        {
            Linear,
            Sigmoid, 
            Tanh,
            ReLU,
            PreLU,
            SoftMax
        }

        private int[] layers;
        private Activation[] layerActivations;
        private float[][] neurons;
        private float[][] sums;

        private float[][] biases;
        private float[][][] weights;


        //0 = no momentum 1 = max momentum 
        private float momentum = 0;
        private float[][] lastBiasDelta;
        private float[][][] lastWeightDelta;

        //private bool batch = false;
        //private float[][] biasDelta = null;
        //private float[][][] weightDelta = null;
        //private int currentCount = 0;
        //private int batchSize = -1;

        public static NeuralNetwork Load(string filepath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filepath, FileMode.Open))
            {
                return (NeuralNetwork)formatter.Deserialize(stream);
            }
        }

        public void Save(string filepath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filepath, FileMode.Create))
            {
                formatter.Serialize(stream, this);
            }
        }

        public NeuralNetwork(int[] layers, Activation[] layerActivations, float momentum)
        {
            this.layerActivations = layerActivations;
            this.momentum = momentum;
            this.layers = layers;
            //batch = false;
            InitialiseNetwork();
        }

        public NeuralNetwork(int[] layers, Activation[] layerActivations, int batchSize, float momentum)
        {
            //MOMENTUM NOT IMPLEMENTED FOR BATCH LEARNING
            //this.momentum = momentum;
            this.layerActivations = layerActivations;
            //this.batchSize = batchSize;
            //batch = true;
            this.layers = layers;
            InitialiseNetwork();
        }

        private void InitialiseNetwork()
        {
            Random random = new Random();

            neurons = new float[layers.Length][];
            sums = new float[layers.Length][];
            biases = new float[layers.Length][];

            lastBiasDelta = new float[layers.Length][];

            for (int i = 0; i < layers.Length; i++)
            {
                neurons[i] = new float[layers[i]];
                biases[i] = new float[layers[i]];
                sums[i] = new float[layers[i]];
                lastBiasDelta[i] = new float[layers[i]];

                for (int j = 0; j < layers[i]; j++)
                {
                    biases[i][j] = 0;
                }
            }
            /*
            if (batch)
            {
                biasDelta = new float[layers.Length][];

                for (int i = 0; i < layers.Length; i++)
                {
                    biasDelta[i] = new float[layers[i]];
                }
            }*/

            weights = new float[layers.Length - 1][][];

            lastWeightDelta = new float[layers.Length - 1][][];

            for (int i = 0; i < layers.Length - 1; i++)
            {
                weights[i] = new float[layers[i + 1]][];
                lastWeightDelta[i] = new float[layers[i + 1]][];

                for (int j = 0; j < layers[i + 1]; j++)
                {
                    weights[i][j] = new float[layers[i]];
                    lastWeightDelta[i][j] = new float[layers[i]];

                    for (int k = 0; k < layers[i]; k++)
                    {
                        float b = MathF.Sqrt(6) / (layers[i] + layers[i + 1]);
                        weights[i][j][k] = ((float)random.NextDouble() - 0.5f) * 2 * b;
                    }
                }
            }

            /*
            if (batch)
            {
                weightDelta = new float[layers.Length - 1][][];

                for (int i = 0; i < layers.Length - 1; i++)
                {
                    weightDelta[i] = new float[layers[i + 1]][];
                    for (int j = 0; j < layers[i + 1]; j++)
                    {
                        weightDelta[i][j] = new float[layers[i]];
                    }
                }
            }*/
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }

            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < layers[i]; j++)
                {
                    float weightedSum = 0f;
                    for (int k = 0; k < layers[layer]; k++)
                    {
                        weightedSum += neurons[layer][k] * weights[layer][j][k];
                    }

                    weightedSum += biases[i][j];
                    sums[i][j] = weightedSum;
                    neurons[i][j] = Activate(layerActivations[i], weightedSum);
                }
            }

            if (layerActivations[neurons.Length - 1] == Activation.SoftMax)
            {
                float sum = 0;
                for (int i = 0; i < layers[neurons.Length - 1]; i++)
                {
                    sum += neurons[neurons.Length - 1][i]; 
                }

                for (int i = 0; i < layers[neurons.Length - 1]; i++)
                {
                    neurons[neurons.Length - 1][i] /= sum;
                }
            }

            return neurons[neurons.Length - 1];
        }

        public void BackPropagate(float[] correctOutput, float learningRate, out float totalError)
        {
            //batch
            if (false)
            {
                //totalError = OfflineBackPropagation(correctOutput, learningRate);
                totalError = -1;
            }
            else
            { 
                totalError = OnlineBackPropagation(correctOutput, learningRate);
            }
        }

        private float OnlineBackPropagation(float[] correctOutput, float learningRate)
        {
            float totalError = 0;
            float[] lastErrorDiv = new float[layers[layers.Length - 1]];
            for (int i = 0; i < lastErrorDiv.Length; i++)
            {
                float e = 0.5f * MathF.Pow(neurons[neurons.Length - 1][i] - correctOutput[i], 2);
                lastErrorDiv[i] = (neurons[neurons.Length - 1][i] - correctOutput[i]) * ActivateDiv(layerActivations[neurons.Length - 1], sums[sums.Length - 1][i]);

                float dBias = (momentum * lastBiasDelta[biases.Length - 1][i] + (1 - momentum) * lastErrorDiv[i]);
                biases[biases.Length - 1][i] -= dBias * learningRate;

                lastBiasDelta[biases.Length - 1][i] = dBias;
                totalError += e;
            }

            for (int i = layers.Length - 2; i >= 0; i--)
            {
                float[] newErrorDiv = new float[layers[i]];

                for (int j = 0; j < layers[i]; j++)
                {
                    float errorDiv = 0;
                    for (int k = 0; k < layers[i + 1]; k++)
                    {
                        errorDiv += lastErrorDiv[k] * weights[i][k][j] * ActivateDiv(layerActivations[i], sums[i][j]);

                        float dWeight = lastErrorDiv[k] * neurons[i][j] * (1 - momentum) + momentum * lastWeightDelta[i][k][j];
                        weights[i][k][j] -= dWeight * learningRate;
                        lastWeightDelta[i][k][j] = dWeight;
                    }

                    float dBias = (momentum * lastBiasDelta[i][j] + (1 - momentum) * errorDiv);
                    biases[i][j] -= dBias * learningRate;

                    lastBiasDelta[i][j] = dBias;
                    newErrorDiv[j] = errorDiv;
                }

                lastErrorDiv = new float[layers[i]];

                for (int j = 0; j < layers[i]; j++)
                {
                    lastErrorDiv[j] = newErrorDiv[j];
                }
            }
            

            return totalError;
        }

        /*private float OfflineBackPropagation(float[] correctOutput, float learningRate)
        {
            float totalError = 0f;
            float[] lastErrorDiv = new float[layers[layers.Length - 1]];
            for (int i = 0; i < lastErrorDiv.Length; i++)
            {
                float e = 0.5f * MathF.Pow(neurons[neurons.Length - 1][i] - correctOutput[i], 2);
                lastErrorDiv[i] = (neurons[neurons.Length - 1][i] - correctOutput[i]) * ActivateDiv(layerActivations[neurons.Length - 1], sums[sums.Length - 1][i]);
                biasDelta[biases.Length - 1][i] += lastErrorDiv[i];
                totalError += e;
            }


            for (int i = layers.Length - 2; i >= 0; i--)
            {
                float[] newErrorDiv = new float[layers[i]];

                for (int j = 0; j < layers[i]; j++)
                {
                    float errorDiv = 0;
                    for (int k = 0; k < layers[i + 1]; k++)
                    {
                        errorDiv += lastErrorDiv[k] * weights[i][k][j] * ActivateDiv(layerActivations[i], sums[i][j]);
                        weightDelta[i][k][j] += lastErrorDiv[k] * neurons[i][j];
                    }
                    biasDelta[i][j] += errorDiv;
                    newErrorDiv[j] = errorDiv;
                }

                lastErrorDiv = new float[layers[i]];

                for (int j = 0; j < layers[i]; j++)
                {
                    lastErrorDiv[j] = newErrorDiv[j];
                }
            }
            currentCount++;

            if (currentCount == batchSize)
            {
                currentCount = 0;
                BatchGradientDescent(learningRate);
            }

            return totalError;
        }

        public void BatchGradientDescent(float learningRate)
        {
            for (int i = 0; i < layers.Length - 1; i++)
            {
                for (int j = 0; j < layers[i]; j++)
                {
                    for (int k = 0; k < layers[i + 1]; k++)
                    {
                        weights[i][k][j] -= weightDelta[i][k][j] * learningRate / batchSize;
                        weightDelta[i][k][j] = 0;
                    }

                    biases[i][j] -= biasDelta[i][j] * learningRate / batchSize;
                    biasDelta[i][j] = 0;
                }
            }
        }*/

        private float Activate(Activation activation, float weightedSum)
        {
            switch (activation)
            {
                case Activation.Linear:
                    return weightedSum;
                case Activation.Sigmoid:
                    return Sigmoid(weightedSum);
                case Activation.Tanh:
                    return Tanh(weightedSum);
                case Activation.ReLU:
                    return ReLU(weightedSum);
                case Activation.PreLU:
                    return PReLU(weightedSum, 0.1f);
                case Activation.SoftMax:
                    return SoftMax(weightedSum);
                default:
                    throw new Exception("How did you fuck this up?");
            }

        }

        private float ActivateDiv(Activation activation, float weightedSum)
        {
            switch (activation)
            {
                case Activation.Linear:
                    return 1;
                case Activation.Sigmoid:
                    return SigmoidDiv(weightedSum);
                case Activation.Tanh:
                    return TanhDiv(weightedSum);
                case Activation.ReLU:
                    return ReLUDiv(weightedSum);
                case Activation.PreLU:
                    return PReLUDiv(weightedSum, 0.1f);
                default:
                    throw new Exception("How did you fuck this up?");
            }
        }

        private float SoftMax(float x)
        {
            return MathF.Exp(x);
        }

        private float Tanh(float x)
        {
            return MathF.Tanh(x);
        }

        private float TanhDiv(float x)
        {
            return 1 - MathF.Pow(Tanh(x), 2);
        }

        private float ReLU(float x)
        {
            return x >= 0 ? x : 0;
        }

        private float ReLUDiv(float x)
        {
            return x >= 0 ? 1 : 0;
        }

        private float PReLU(float x, float a)
        {
            return x >= 0 ? x : a * x;
        }

        private float PReLUDiv(float x, float a)
        {
            return x >= 0 ? 1 : -a;
        }

        private float Sigmoid(float x)
        {
            return 1 / (1 + MathF.Pow(MathF.E, -x));
        }

        private float SigmoidDiv(float x)
        {
            float sig = Sigmoid(x);

            return sig * (1 - sig);
        }

    }

    public struct DigitImage
    {
        public float[] image;
        public byte label;

        public DigitImage(float[] image, byte label)
        {
            this.image = image;
            this.label = label;
        }

        public static DigitImage[] CreateTraining()
        {
            DigitImage[] images;
            using (FileStream imageStream = new FileStream(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\train\train-images.idx3-ubyte", FileMode.Open))
            {
                using (BinaryReader imageReader = new BinaryReader(imageStream))
                {
                    using (FileStream labelStream = new FileStream(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\train\train-labels.idx1-ubyte", FileMode.Open))
                    {
                        using (BinaryReader labelReader = new BinaryReader(labelStream))
                        {
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();

                            labelReader.ReadInt32();
                            labelReader.ReadInt32();

                            images = new DigitImage[60000];

                            for (int i = 0; i < 60000; i++)
                            {
                                float[] image = new float[784];


                                for (int j = 0; j < 784; j++)
                                {
                                    image[j] = imageReader.ReadByte() / 255f;
                                }

                                images[i] = new DigitImage() { image = image, label = labelReader.ReadByte() };
                            }
                        }
                    }
                }
            }

            Console.WriteLine("FINISHED!");

            return images;
        }

        public static DigitImage[] CreateTesting()
        {
            DigitImage[] images;
            using (FileStream imageStream = new FileStream(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\test\t10k-images.idx3-ubyte", FileMode.Open))
            {
                using (BinaryReader imageReader = new BinaryReader(imageStream))
                {
                    using (FileStream labelStream = new FileStream(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\test\t10k-labels.idx1-ubyte", FileMode.Open))
                    {
                        using (BinaryReader labelReader = new BinaryReader(labelStream))
                        {
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();
                            imageReader.ReadInt32();

                            labelReader.ReadInt32();
                            labelReader.ReadInt32();

                            images = new DigitImage[10000];

                            for (int i = 0; i < 10000; i++)
                            {
                                float[] image = new float[784];


                                for (int j = 0; j < 784; j++)
                                {
                                    image[j] = imageReader.ReadByte() / 255f;
                                }

                                images[i] = new DigitImage() { image = image, label = labelReader.ReadByte() };
                            }
                        }
                    }
                }
            }

            Console.WriteLine("FINISHED!");

            return images;
        }
    }
}
