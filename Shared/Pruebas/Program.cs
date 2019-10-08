using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using BussinessLogicLayer.BL;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            BLAgencia ag = new BLAgencia();

            List<SAgencia> agencias = ag.getAll();

            agencias.ForEach(x =>
            {
                Console.WriteLine(x.Nombre);
            });

            Console.WriteLine("esperemos que esto ande");
            Console.ReadLine();
        }
    }
}
