using Math3TestGame;
using Math3TestGame.Models.GameModels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            Rectangle[,] gObjects = new Rectangle[8, 8];

            GameConfigs.GetInstance().RegionWidth = 1;
            GameConfigs.GetInstance().RegionHeight = 1;


            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    gObjects[i, j] = new Rectangle(j, i, 256, 256);
                }
            }

            var matrix = new GameMatrix(gObjects, 8, 8);

            Console.WriteLine("Input matrix");
            Console.WriteLine(matrix.ToString());
            Console.WriteLine();
            Console.WriteLine("Matrix items positions");
            Console.WriteLine(matrix.GetItemsPositions());
            Console.WriteLine();

            bool busy = true;

            while(busy || matrix.State != MatrixState.NONE) { 
            while (busy) {
                busy = false;
                foreach (var go in matrix)
                {
                    go.Update(100);
                    busy |= go.IsBusy();
                }
            }

            matrix.Next();

            busy = true;

            while (busy)
            {
                busy = false;
                foreach (var go in matrix)
                {
                    go.Update(100);
                    busy |= go.IsBusy();
                }
            }


            Console.WriteLine("Test killed items:");
            Console.WriteLine(matrix.ToString());

            Console.WriteLine();
            Console.WriteLine("Matrix items positions");
            Console.WriteLine(matrix.GetItemsPositions());
            Console.WriteLine();

            busy = true;

            while (busy)
            {
                busy = false;
                foreach (var go in matrix)
                {
                    go.Update(100);
                    busy |= go.IsBusy();
                }
            }

            
            matrix.Next();
             
            Console.WriteLine("Test drop down items");
            Console.WriteLine(matrix.ToString());
            Console.WriteLine();
            Console.WriteLine("Matrix items positions");
            Console.WriteLine(matrix.GetItemsPositions());
            Console.WriteLine();

            busy = true;

            while (busy)
            {
                busy = false;
                foreach (var go in matrix)
                {
                    go.Update(100);
                    busy |= go.IsBusy();
                }
            }

            matrix.Next();

            Console.WriteLine("Create new items");
            Console.WriteLine(matrix.ToString());
            Console.WriteLine();

            Console.WriteLine("Test matrix state");
            Console.WriteLine(matrix.State);
            Console.WriteLine();
            Console.WriteLine("Matrix items positions");
            Console.WriteLine(matrix.GetItemsPositions());
            Console.WriteLine();

                Console.WriteLine("+++++++++++++++++++++++");
            }
            /*while(matrix.State != MatrixState.NONE)
            {
                matrix.Next();
                Console.WriteLine("Test matrix state");
                Console.WriteLine(matrix.State);
            }


            Console.WriteLine("Matrix");
            Console.WriteLine(matrix.ToString());

            Console.WriteLine();

            
            Console.WriteLine();
            Console.WriteLine("Test h swap");
            Console.WriteLine(matrix.ToString());
            matrix.TestHSwap();
            Console.WriteLine(matrix.ToString());*/

            Console.ReadLine();
        }
    }
}
