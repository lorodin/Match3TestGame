using Math3TestGame.Models.GameModels;
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
            var matrix = new GameMatrix();
            Console.WriteLine(matrix.ToString());
            
           

            matrix.Next();

            Console.WriteLine("Get horizontal matrix");
            Console.WriteLine(matrix.GetHMatrix());

            Console.WriteLine("Get vertiacal matrix");
            Console.WriteLine(matrix.GetVMatrix());

            Console.WriteLine("Test killed items");
            Console.WriteLine(matrix.GetKilledItems());

            matrix.TestSwap();
            Console.WriteLine("Test swap:");
            Console.WriteLine(matrix.ToString());

            matrix.Next();
             
            Console.WriteLine("Test drop down items");
            Console.WriteLine(matrix.ToString());

            matrix.Next();

            Console.WriteLine("Create new items");
            Console.WriteLine(matrix.ToString());

            Console.WriteLine("Test matrix state");
            Console.WriteLine(matrix.State);
            Console.WriteLine();

            while(matrix.State != MatrixState.NONE)
            {
                matrix.Next();
                Console.WriteLine("Test matrix state");
                Console.WriteLine(matrix.State);
            }

            Console.WriteLine("Matrix");
            Console.WriteLine(matrix.ToString());

            Console.WriteLine();

            foreach (var go in matrix)
            {
                Console.Write((int)go.SpriteName + " ");
            }

            Console.ReadLine();
        }
    }
}
