using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Composite
    {
        static void Main()
        {
            // Create a tree structure
            Composite root = new Composite("root");

            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));

            Composite comp = new Composite("Composite X");

            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            root.Add(comp);
            root.Add(new Leaf("Leaf C"));

            // Add and remove a leaf
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);

            // Recursively display tree
            root.Display(1);

            // Wait for user
            Console.Read();
        }
    }

    /// <summary>
    /// Component - компонент
    /// </summary>
    /// <li>
    /// <lu>объявляет интерфейс для компонуемых объектов;</lu>
    /// <lu>предоставляет подходящую реализацию операций по умолчанию,
    /// общую для всех классов;</lu>
    /// <lu>объявляет интерфейс для доступа к потомкам и управлению ими;</lu>
    /// <lu>определяет интерфейс доступа к родителю компонента в рекурсивной структуре
    /// и при необходимости реализует его. Описанная возможность необязательна;</lu>
    /// </li>
    abstract class Component
    {
        protected string name;

        // Constructor
        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Display(int depth);
    }

    
}
