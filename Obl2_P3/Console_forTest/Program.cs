﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Repositorios;

namespace Console_forTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Barrio barrioX = new Barrio() { nombre_barrio = "Cerrito", descripcion = "Casas sobre un cerrito" };

            RepoVivienda repoB = new RepoVivienda();


            try
            {
                repoB.import();
                Console.WriteLine("Gracias marcelo");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
           
        }
    }
}
