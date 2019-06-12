﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Clases;
using Dominio.Contexto_DB;
using Dominio.Interfaces;
using System.IO;
using System.Diagnostics;

namespace Dominio.Repositorios
{
    public class RepoBarrio : IRepoBarrio, IRepoImport
    {
        
        public bool add(Barrio b)
        {
            Contexto db = new Contexto();
            if (b == null) return false;

            try
            {
                db.barrios.Add(b);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return false;
            }
            finally
            {
                db.Dispose();
            }

        }

        public bool delete(Barrio b)
        {
            Contexto db = new Contexto();

            if (b == null) return false;

            try
            {
                db.barrios.Remove(b);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Barrio> findAll()
        {
            Contexto db = new Contexto();

            List<Barrio> listaBarrios = null;

            if (db.barrios.Count() > 0)
            {
                listaBarrios = (from Barrio b in db.barrios select b).ToList();
                db.Dispose();
            }

            return listaBarrios;
        }

        public Barrio findById(int id)
        {
            Contexto db = new Contexto();

            try
            {
                Barrio barrioBuscado = db.barrios.Find(id);
                db.Dispose();
                return barrioBuscado;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Barrio findByName(string bName)
        {
            Contexto db = new Contexto();

            try
            {
                Barrio barrioBuscado = db.barrios.Find(bName);
                db.Dispose();
                return barrioBuscado;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public bool import()
        {
            Contexto db = new Contexto();

            List<Barrio> barrios_a_importar = new List<Barrio>();
            List<string> errores = new List<string>();

            bool imported = true;

            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Barrios.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');
                    barrios_a_importar.Add(new Barrio
                    {
                        nombre_barrio = s[0],
                        descripcion = s[1]
                    });
                }

                file.Close();
            }

            try
            {

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Errores.txt", string.Empty);

                foreach (Barrio b in barrios_a_importar)
                {
                    if (! b.esValido())
                    {
                        // Mandarlo a errores.txt
                        errores.Add("Nombre o descripcion no válida#" + b.ToString());
                        imported = false;
                    } else
                    {
                        if (db.barrios.Where(bd => bd.nombre_barrio == b.nombre_barrio).FirstOrDefault() != null)
                        {
                            // Mandar a errores.txt
                            errores.Add("Barrio duplicado#" + b.ToString());
                            imported = false;

                        }
                        else
                        {
                            db.barrios.Add(b);
                        }
                    }
                }

                db.SaveChanges();

                using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Errores.txt"))
                {
                    foreach (string b in errores)
                    {
                        file.WriteLineAsync(b);
                    }

                    file.Close();
                }


            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return imported;
        }

        public bool update(Barrio b)
        {
            Contexto db = new Contexto();

            try
            { 
                Barrio barrioBuscado = db.barrios.Find(b.nombre_barrio);
                if (barrioBuscado != null)
                {
                    barrioBuscado.descripcion = b.descripcion;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

