using System;
using System.Drawing;

namespace NeuralNetwork2020
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*
            NeuralNetwork nn = new NeuralNetwork(new int[] { 784, 40, 10 }, new NeuralNetwork.Activation[] { NeuralNetwork.Activation.ReLU, NeuralNetwork.Activation.ReLU, NeuralNetwork.Activation.Sigmoid }, 0.9f);
            {
                DigitImage[] training = DigitImage.CreateTraining();

                float[] output = new float[10];

                int count = 0;
                float error;

                for (int j = 0; j < 1; j++)
                {
                    foreach (DigitImage image in training)
                    {
                        nn.FeedForward(image.image);

                        for (int i = 0; i < 10; i++)
                        {
                            output[i] = 0;
                        }

                        output[image.label] = 1;

                        nn.BackPropagate(output, .1f, out error);

                        count++;
                        if (count == 1000)
                        {
                            count = 0;
                            Console.WriteLine(error);
                        }

                    }
                }
            }

            NeuralNetwork nn = NeuralNetwork.Load(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\saves\mnist.nn");

            int correct = 0;
            int size;
            {
                DigitImage[] testing = DigitImage.CreateTesting();
                size = testing.Length;
                foreach (DigitImage image in testing)
                {
                    float[] outputt = nn.FeedForward(image.image);
                    int highest = 0;
                    float highestValue = float.MinValue;
                    for (int i = 0; i < outputt.Length; i++)
                    {
                        if (outputt[i] > highestValue)
                        {
                            highest = i;
                            highestValue = outputt[i];
                        }
                    }

                    if (highest == image.label)
                    {
                        correct++;
                    }

                }
            }

            Console.WriteLine((((float)correct / size) * 100f) + " % ");

            //nn.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\saves\mnist.nn");

            Console.Read();
            */

            /*
            NeuralNetwork nn = new NeuralNetwork(new int[] { 2, 40, 40, 40, 1 }, new NeuralNetwork.Activation[] { NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid, NeuralNetwork.Activation.Sigmoid }, .0f);

            Random random = new Random();

            int count = 0;

            for (int i = 0; i < 1000000; i++)
            {
                int num = random.Next(2);
                int num1 = random.Next(2);

                float[] inputs = new float[] { num, num1 };
                nn.FeedForward(inputs);
                nn.BackPropagate(new float[] { (num == 1) ^ (num1 == 1) ? 1 : -1 }, 0.1f, out float error);

                count++;
                if (count == 1000)
                {
                    Console.WriteLine(error);
                    count = 0;
                }
            }

            int success = 0;

            for (int i = 0; i < 1000000; i++)
            {
                int num = random.Next(2);
                int num1 = random.Next(2);

                float[] inputs = new float[] { num, num1 };
                float[] outputt = nn.FeedForward(inputs);

                bool ideal = (num == 1) ^ (num1 == 1);

                if (outputt[0] > 0f)
                {
                    if (ideal)
                    {
                        success++;
                    }
                }
                else if (!ideal)
                {
                    success++;
                }

            }

            Console.WriteLine(success / 1000000f);

            //nn.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\saves\xor.nn");

            return;
            */

            /*
            int length = 384 * 216;
            NeuralNetwork nnR = new NeuralNetwork(new int[] { length * 2, 500, length });
            NeuralNetwork nnG = new NeuralNetwork(new int[] { length * 2, 500, length });
            NeuralNetwork nnB = new NeuralNetwork(new int[] { length * 2, 500, length });

            Console.WriteLine("START");

            float[] input = new float[length * 2];
            float[] output = new float[length];

            for (int epoch = 0; epoch < 1; epoch++)
            {
                for (int i = 1; i < 535; i+=2)
                {
                    string inputExtention = "";
                    if (i > 99)
                    {
                        inputExtention = i.ToString();
                    }
                    else if (i > 9)
                    {
                        inputExtention = "0" + i;
                    }
                    else
                    {
                        inputExtention = "00" + i;
                    }

                    Bitmap inputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\0" + inputExtention + ".jpg");

                    if ((i + 2) > 99)
                    {
                        inputExtention = (i + 2).ToString();
                    }
                    else if ((i + 2) > 9)
                    {
                        inputExtention = "0" + (i + 2);
                    }
                    else
                    {
                        inputExtention = "00" + (i + 2);
                    }

                    Bitmap input2Image = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\0" + inputExtention + ".jpg");

                    string outputExtention = "";
                    if ((i + 1) > 99)
                    {
                        outputExtention = (i + 1).ToString();
                    }
                    else if ((i + 1) > 9)
                    {
                        outputExtention = "0" + (i + 1);
                    }
                    else
                    {
                        outputExtention = "00" + (i + 1);
                    }

                    Bitmap outputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\0" + outputExtention + ".jpg");



                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[j * 216 + k] = inputImage.GetPixel(j, k).R / 255f;
                            output[j * 216 + k] = outputImage.GetPixel(j, k).R / 255f;
                        }
                    }

                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).R / 255f;
                        }
                    }

                    float[] outputR = nnR.FeedForward(input);
                    nnR.BackPropagate(output, 0.01f, out float errorR);



                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[j * 216 + k] = inputImage.GetPixel(j, k).G / 255f;
                            output[j * 216 + k] = outputImage.GetPixel(j, k).G / 255f;
                        }
                    }

                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).G / 255f;
                        }
                    }

                    float[] outputG = nnG.FeedForward(input);
                    nnG.BackPropagate(output, 0.01f, out float errorG);


                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[j * 216 + k] = inputImage.GetPixel(j, k).B / 255f;
                            output[j * 216 + k] = outputImage.GetPixel(j, k).B / 255f;
                        }
                    }

                    for (int j = 0; j < 384; j++)
                    {
                        for (int k = 0; k < 216; k++)
                        {
                            input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).B / 255f;
                        }
                    }

                    float[] outputB = nnB.FeedForward(input);
                    nnB.BackPropagate(output, 0.01f, out float errorB);

                    Bitmap nnOutputImage = new Bitmap(384, 216);

                    for (int x = 0; x < 384; x++)
                    {
                        for (int y = 0; y < 216; y++)
                        {
                            float r = MathF.Min(MathF.Max(0, outputR[x * 216 + y]), 1);
                            float g = MathF.Min(MathF.Max(0, outputG[x * 216 + y]), 1);
                            float b = MathF.Min(MathF.Max(0, outputB[x * 216 + y]), 1);

                            nnOutputImage.SetPixel(x, y, Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)));
                        }
                    }

                    nnOutputImage.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\interpolatedtrain\" + outputExtention + ".jpg");

                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("TEST");

            for (int i = 1; i <= 537; i++)
            {
                string inputExtention = "";
                if (i > 99)
                {
                    inputExtention = i.ToString();
                }
                else if (i > 9)
                {
                    inputExtention = "0" + i;
                }
                else
                {
                    inputExtention = "00" + i;
                }

                Bitmap inputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\0" + inputExtention + ".jpg");

                string inputExtention2 = "";

                if ((i + 1) > 99)
                {
                    inputExtention2 = (i + 1).ToString();
                }
                else if ((i + 1) > 9)
                {
                    inputExtention2 = "0" + (i + 1);
                }
                else
                {
                    inputExtention2 = "00" + (i + 1);
                }

                Bitmap input2Image = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\0" + inputExtention2 + ".jpg");


                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[j * 216 + k] = inputImage.GetPixel(j, k).R / 255f;
                    }
                }

                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).R / 255f;
                    }
                }

                float[] outputR = nnR.FeedForward(input);

                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[j * 216 + k] = inputImage.GetPixel(j, k).G / 255f;
                    }
                }

                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).G / 255f;
                    }
                }

                float[] outputG = nnG.FeedForward(input);


                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[j * 216 + k] = inputImage.GetPixel(j, k).B / 255f;
                    }
                }

                for (int j = 0; j < 384; j++)
                {
                    for (int k = 0; k < 216; k++)
                    {
                        input[length + j * 216 + k - 1] = input2Image.GetPixel(j, k).B / 255f;
                    }
                }

                float[] outputB = nnB.FeedForward(input);

                Bitmap nnOutputImage = new Bitmap(384, 216);

                for (int x = 0; x < 384; x++)
                {
                    for (int y = 0; y < 216; y++)
                    {
                        float r = MathF.Min(MathF.Max(0, outputR[x * 216 + y]), 1);
                        float g = MathF.Min(MathF.Max(0, outputG[x * 216 + y]), 1);
                        float b = MathF.Min(MathF.Max(0, outputB[x * 216 + y]), 1);

                        nnOutputImage.SetPixel(x, y, Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)));
                    }
                }

                nnOutputImage.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\video\interpolatedtest\" + inputExtention + "5.jpg");

                Console.WriteLine(i);
            }
            */

            
            int imageWidth = 512;
            int nnPerWidth = 16;
            int nnOutputWidth = imageWidth / nnPerWidth;
            int bufferPixels = 4;
            int nnInputWidth = nnOutputWidth + bufferPixels * 2;

            NeuralNetwork[,] web = new NeuralNetwork[nnPerWidth, nnPerWidth];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    web[i, j] = new NeuralNetwork(new int[] { nnInputWidth * nnInputWidth * 3 * 2, 20, 40, 40, 40, 20, nnOutputWidth * nnOutputWidth * 3 }, 
                        new NeuralNetwork.Activation[] { NeuralNetwork.Activation.Linear, NeuralNetwork.Activation.PreLU, NeuralNetwork.Activation.PreLU, NeuralNetwork.Activation.PreLU, NeuralNetwork.Activation.PreLU, NeuralNetwork.Activation.PreLU, NeuralNetwork.Activation.Linear },
                        0.9f);
                }
            }

            Console.WriteLine("START");

            float[] input = new float[nnInputWidth * nnInputWidth * 3 * 2];
            float[] output = new float[nnOutputWidth * nnOutputWidth * 3];

            for (int epoch = 0; epoch < 1; epoch++)
            {
                for (int i = 1; i < 535; i += 2)
                {
                    string inputExtention = "";
                    if (i > 99)
                    {
                        inputExtention = i.ToString();
                    }
                    else if (i > 9)
                    {
                        inputExtention = "0" + i;
                    }
                    else
                    {
                        inputExtention = "00" + i;
                    }

                    Bitmap inputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\poweroftwovideo0" + inputExtention + ".jpg");

                    if ((i + 2) > 99)
                    {
                        inputExtention = (i + 2).ToString();
                    }
                    else if ((i + 2) > 9)
                    {
                        inputExtention = "0" + (i + 2);
                    }
                    else
                    {
                        inputExtention = "00" + (i + 2);
                    }

                    Bitmap inputImage2 = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\poweroftwovideo0" + inputExtention + ".jpg");

                    string desiredOutputExtention = "";
                    if ((i + 1) > 99)
                    {
                        desiredOutputExtention = (i + 1).ToString();
                    }
                    else if ((i + 1) > 9)
                    {
                        desiredOutputExtention = "0" + (i + 1);
                    }
                    else
                    {
                        desiredOutputExtention = "00" + (i + 1);
                    }

                    Bitmap desiredOutputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\poweroftwovideo0" + desiredOutputExtention + ".jpg");


                    Bitmap nnOutputImage = new Bitmap(512, 512);
                    int outputPixels = nnOutputWidth * nnOutputWidth;

                    //float learningRate = 5f / (1.5f * i + 5f);

                    for (int j = 0; j < nnPerWidth; j++)
                    {
                        for (int k = 0; k < nnPerWidth; k++)
                        {
                            for (int x = 0; x < nnOutputWidth; x++)
                            {
                                for (int y = 0; y < nnOutputWidth; y++)
                                {
                                    int index = x + y * nnOutputWidth;
                                    int xCoord = x + nnOutputWidth * j;
                                    int yCoord = y + nnOutputWidth * k;
                                    input[index] = inputImage.GetPixel(xCoord, yCoord).R / 255f;
                                    input[index + outputPixels] = inputImage.GetPixel(xCoord, yCoord).G / 255f;
                                    input[index + outputPixels * 2] = inputImage.GetPixel(xCoord, yCoord).B / 255f;

                                    input[index + outputPixels * 3] = inputImage2.GetPixel(xCoord, yCoord).R / 255f;
                                    input[index + outputPixels * 4] = inputImage2.GetPixel(xCoord, yCoord).G / 255f;
                                    input[index + outputPixels * 5] = inputImage2.GetPixel(xCoord, yCoord).B / 255f;

                                    output[index] = desiredOutputImage.GetPixel(xCoord, yCoord).R / 255f;
                                    output[index + outputPixels] = desiredOutputImage.GetPixel(xCoord, yCoord).G / 255f;
                                    output[index + outputPixels * 2] = desiredOutputImage.GetPixel(xCoord, yCoord).B / 255f;
                                }
                            }

                            int extraIndex = 0;
                            for (int x = 0; x < nnOutputWidth; x++)
                            {
                                int xCoord = x + nnOutputWidth * j;
                                int regularYCoord = nnOutputWidth * k;

                                for (int y = 1; y <= bufferPixels; y++)
                                {
                                    int lowerYCoord = regularYCoord - y;
                                    int upperYCoord = regularYCoord + nnOutputWidth + y;

                                    if (lowerYCoord >= 0)
                                    {
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).B / 255f;

                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).B / 255f;
                                    }
                                    else
                                    {
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;

                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                    }

                                    if (upperYCoord < nnOutputWidth)
                                    {
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).B / 255f;

                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).B / 255f;
                                    }
                                    else
                                    {
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;

                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                    }
                                }
                            }


                            for (int y = 0; y < nnOutputWidth; y++)
                            {
                                int yCoord = y + nnOutputWidth * k;
                                int regularXCoord = nnOutputWidth * j;

                                for (int x = 1; x <= bufferPixels; x++)
                                {
                                    int lowerXCoord = regularXCoord - x;
                                    int upperXCoord = regularXCoord + nnOutputWidth + x;

                                    if (lowerXCoord >= 0)
                                    {
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).B / 255f;

                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).B / 255f;
                                    }
                                    else
                                    {
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;

                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                    }

                                    if (upperXCoord < nnOutputWidth)
                                    {
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).B / 255f;

                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).R / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).G / 255f;
                                        input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).B / 255f;
                                    }
                                    else
                                    {
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;

                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                        input[outputPixels * 6 + extraIndex++] = 0;
                                    }
                                }
                            }
                        

                            float[] nnOutput = web[j, k].FeedForward(input);
                            web[j, k].BackPropagate(output, 0.075f, out float error);

                            for (int x = 0; x < 32; x++)
                            {
                                for (int y = 0; y < 32; y++)
                                {
                                    int index = x + y * nnOutputWidth;
                                    int xCoord = x + nnOutputWidth * j;
                                    int yCoord = y + nnOutputWidth * k;

                                    float r = MathF.Min(MathF.Max(0, nnOutput[index]), 1);
                                    float g = MathF.Min(MathF.Max(0, nnOutput[index + outputPixels]), 1);
                                    float b = MathF.Min(MathF.Max(0, nnOutput[index + outputPixels * 2]), 1);

                                    nnOutputImage.SetPixel(xCoord, yCoord, Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)));
                                }
                            }
                        }
                    }

                    nnOutputImage.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\trainoutput\0" + desiredOutputExtention + ".jpg");
                    Console.WriteLine(i);
                }
            }

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    web[x,y].Save($@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\saves\interpolation\{x}-{y}.nn");
                }
            }

            Console.WriteLine("TEST");

            for (int i = 1; i < 535; i += 2)
            {
                string inputExtention = "";
                if (i > 99)
                {
                    inputExtention = i.ToString();
                }
                else if (i > 9)
                {
                    inputExtention = "0" + i;
                }
                else
                {
                    inputExtention = "00" + i;
                }

                Bitmap inputImage = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\poweroftwovideo0" + inputExtention + ".jpg");

                if ((i + 2) > 99)
                {
                    inputExtention = (i + 1).ToString();
                }
                else if ((i + 2) > 9)
                {
                    inputExtention = "0" + (i + 1);
                }
                else
                {
                    inputExtention = "00" + (i + 1);
                }

                Bitmap inputImage2 = new Bitmap(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\poweroftwovideo0" + inputExtention + ".jpg");


                Bitmap nnOutputImage = new Bitmap(512, 512);
                int outputPixels = nnOutputWidth * nnOutputWidth;

                for (int j = 0; j < nnPerWidth; j++)
                {
                    for (int k = 0; k < nnPerWidth; k++)
                    {
                        for (int x = 0; x < nnOutputWidth; x++)
                        {
                            for (int y = 0; y < nnOutputWidth; y++)
                            {
                                int index = x + y * nnOutputWidth;
                                int xCoord = x + nnOutputWidth * j;
                                int yCoord = y + nnOutputWidth * k;
                                input[index] = inputImage.GetPixel(xCoord, yCoord).R / 255f;
                                input[index + outputPixels] = inputImage.GetPixel(xCoord, yCoord).G / 255f;
                                input[index + outputPixels * 2] = inputImage.GetPixel(xCoord, yCoord).B / 255f;

                                input[index + outputPixels * 3] = inputImage2.GetPixel(xCoord, yCoord).R / 255f;
                                input[index + outputPixels * 4] = inputImage2.GetPixel(xCoord, yCoord).G / 255f;
                                input[index + outputPixels * 5] = inputImage2.GetPixel(xCoord, yCoord).B / 255f;
                            }
                        }

                        int extraIndex = 0;
                        for (int x = 0; x < nnOutputWidth; x++)
                        {
                            int xCoord = x + nnOutputWidth * j;
                            int regularYCoord = nnOutputWidth * k;

                            for (int y = 1; y <= bufferPixels; y++)
                            {
                                int lowerYCoord = regularYCoord - y;
                                int upperYCoord = regularYCoord + nnOutputWidth + y;

                                if (lowerYCoord >= 0)
                                {
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, lowerYCoord).B / 255f;

                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, lowerYCoord).B / 255f;
                                }
                                else
                                {
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;

                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                }

                                if (upperYCoord < nnOutputWidth)
                                {
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(xCoord, upperYCoord).B / 255f;

                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(xCoord, upperYCoord).B / 255f;
                                }
                                else
                                {
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;

                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                }
                            }
                        }


                        for (int y = 0; y < nnOutputWidth; y++)
                        {
                            int yCoord = y + nnOutputWidth * k;
                            int regularXCoord = nnOutputWidth * j;

                            for (int x = 1; x <= bufferPixels; x++)
                            {
                                int lowerXCoord = regularXCoord - x;
                                int upperXCoord = regularXCoord + nnOutputWidth + x;

                                if (lowerXCoord >= 0)
                                {
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(lowerXCoord, yCoord).B / 255f;

                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(lowerXCoord, yCoord).B / 255f;
                                }
                                else
                                {
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;

                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                }

                                if (upperXCoord < nnOutputWidth)
                                {
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage.GetPixel(upperXCoord, yCoord).B / 255f;

                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).R / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).G / 255f;
                                    input[outputPixels * 6 + extraIndex++] = inputImage2.GetPixel(upperXCoord, yCoord).B / 255f;
                                }
                                else
                                {
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;

                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                    input[outputPixels * 6 + extraIndex++] = 0;
                                }
                            }
                        }


                        float[] nnOutput = web[j, k].FeedForward(input);

                        for (int x = 0; x < 32; x++)
                        {
                            for (int y = 0; y < 32; y++)
                            {
                                int index = x + y * nnOutputWidth;
                                int xCoord = x + nnOutputWidth * j;
                                int yCoord = y + nnOutputWidth * k;

                                float r = MathF.Min(MathF.Max(0, (nnOutput[index] + 1) / 2), 1);
                                float g = MathF.Min(MathF.Max(0, (nnOutput[index + outputPixels] + 1) / 2), 1);
                                float b = MathF.Min(MathF.Max(0, (nnOutput[index + outputPixels * 2] + 1) / 2), 1);

                                nnOutputImage.SetPixel(xCoord, yCoord, Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)));
                            }
                        }
                    }
                }

                nnOutputImage.Save(@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\poweroftwo\testoutput\0" + inputExtention + "5.jpg");
                Console.WriteLine(i);
            }
            /*
            Bitmap r;
            Bitmap g;
            Bitmap b;

            Bitmap combined = new Bitmap(384, 216);

            for (int i = 0; i < 98; i++)
            {
                r = new Bitmap($@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\res\frame{i}.jpg");
                g = new Bitmap($@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\res\frameG{i}.jpg");
                b = new Bitmap($@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\res\frameB{i}.jpg");

                for (int x = 0; x < 384; x++)
                {
                    for (int y = 0; y < 216; y++)
                    {
                        combined.SetPixel(x, y, Color.FromArgb(r.GetPixel(x, y).R, g.GetPixel(x, y).G, b.GetPixel(x, y).B));
                    }
                }

                combined.Save($@"E:\Users\Stephen\Applications\MyProgram\NeuralNetwork2020\NeuralNetwork2020\res\Finished\finished{i}.jpg");

                Console.WriteLine(i);
            }*/
        }
    }
}
