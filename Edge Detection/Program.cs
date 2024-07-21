using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Edge_Detection
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileSave;
            string filePath;
            Console.WriteLine("Enter image load path:");
            filePath = Console.ReadLine();
            Console.WriteLine("Enter save path:");
            fileSave = Console.ReadLine();
            Console.WriteLine("Flip shading? y/n");
            string flip = Console.ReadLine();
            Image image = new Image(filePath);
            bool flipB = (flip == "y") ? true : false;
            image.edgeDetection(flipB).Save(fileSave);
            
        }
    }
}
