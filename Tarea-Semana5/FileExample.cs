using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tarea_Semana5
{
    class FileExample
    {
        static void Main()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = Menu(); 
            }
            Console.ReadKey();
        }

        private static bool Menu()
        {
            
            Console.WriteLine("Por favor seleccione una opcion: ");
            Console.WriteLine("1. Registrar nuevo usuario: ");
            Console.WriteLine("2. Actualizar datos de usuario: ");
            Console.WriteLine("3. Eliminar datos de usuario: ");
            Console.WriteLine("4. Mostrar listado de usuarios: ");
            Console.WriteLine("5. salir: ");
            Console.WriteLine("\nOpcion: ");

                    switch (Console.ReadLine())
            {
                case "1":
                    register(); 
                    return true;
                case "2":
                    updateData(); 
                    Console.ReadKey();
                    return true;
                case "3":
                    return true;
                case "4":
                    Console.WriteLine("Listado de todos los usuarios");
                    foreach (KeyValuePair<object, object> data in readFile())
                    {
                        Console.WriteLine("{0}: {1}", data.Key, data.Value);
                    }
                    Console.ReadKey();
                    return true;
                case "5":
                    return false;
                default:
                    return false;
            }
        }

       
        private static string getPath()
        {
            string path = @"E:\ejemplo\usuarios.txt";
            return path;
        }

     
        private static void register()
        {
            
            Console.WriteLine("Datos del usuario");
            Console.Write("Nombre Completo: ");
            string fullname = Console.ReadLine();
            Console.Write("Años de trabajo en la empresa: ");
            int añosdetrabajo = int.Parse(Console.ReadLine());

            
            using (StreamWriter sw = File.AppendText(getPath()))
            {
                sw.WriteLine("{0}; {1}", fullname, añosdetrabajo);
                sw.Close();
                Console.WriteLine("Se registrado este usuario correctamente ");
            }
        }
        private static Dictionary<object, object> readFile()
        {
            
            Dictionary<object, object> listausuarios = new Dictionary<object, object>();

            
            using (var lector = new StreamReader(getPath()))
            {
                
                string lines;

                while ((lines = lector.ReadLine()) != null) 
                {
                    string[] keyvalue = lines.Split(';');
                    if (keyvalue.Length == 2)
                    {
                        listausuarios.Add(keyvalue[0], keyvalue[1]);
                    }
                }

            }
            return listausuarios;
        }
        private static bool search(string name)
        {
            if (!readFile().ContainsKey(name))
            {
                return false;
            }
            return true;
        }
        private static void updateData()
        {
            Console.Write("Escriba el nombre del usuario que desea actualizar: ");
            var name = Console.ReadLine();

            
            if (search(name))
            {
                Console.WriteLine("El registro existe por favor intente una vez mas");
                Console.Write("Nuevos años de trabajo: ");
                var newjobtyears = Console.ReadLine();

               
                Dictionary<object, object> temp = new Dictionary<object, object>();
                temp = readFile();

                temp[name] = newjobtyears; 
                Console.WriteLine("El registro ha sido actualizado con excito");
                File.Delete(getPath());

                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temp)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                       
                    }
                }

            }
            else
            {
                Console.WriteLine("Lo siento el registro no se logro encontrar por verifique si lo escribio correctamente");
            }
        }
    }
}







