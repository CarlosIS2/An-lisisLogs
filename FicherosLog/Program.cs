using System;
using System.Collections;
using System.Globalization;
using System.IO;


namespace FicherosLog
{
    class Program
    {


        static void Main(string[] args)
        {
            String[] files = Directory.GetFiles(@"C:\Users\Carlos\Desktop\Central");
            foreach (String s in files)
            {
                StreamReader file = new StreamReader(s);
            }

            //obtieneFicherosExistentes(files[files.Length-2]);
            //ficherosDescargados(files);
            obtieneFicherosDescargados(files[files.Length - 2]);
        }

        static void obtieneFicherosDescargados(String path) //Obtiene todos los archivos descargados de un fichero determinado
        {
            String line = "";
            int counter = 0;
            try
            {
                StreamReader file = new StreamReader(path);
                ArrayList fileLines = new ArrayList();
                while (line != null)
                {
                    line = file.ReadLine();
                    if (line != null && (line.Contains("CONTENIDO_DEL_FICHERO_INCORRECTO") || line.Contains("OK") || line.Contains("FICHERO_YA_EXISTE")))
                    {


                        if (line[41] == 'C')
                        {
                            int posicion = line.IndexOf('.') - 13;
                            String hora = line.Substring(posicion, 13);
                            DateTime theTime = DateTime.ParseExact(hora,
                                        "yyyyMMdd_HHmm",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
                            //Console.WriteLine(theTime.ToString());     
                            if (theTime > DateTime.Now.AddDays(-7))
                            {
                                counter++;
                                fileLines.Add(line);
                            }
                        }
                        else if (line[41] == 'V')
                        {
                            int posicion = line.IndexOf('$') - 13;
                            String hora = line.Substring(posicion, 13);
                            DateTime theTime = DateTime.ParseExact(hora,
                                        "yyyyMMdd_HHmm",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
                            if (theTime > DateTime.Now.AddDays(-7))
                            {
                                fileLines.Add(line);
                                counter++;
                            }
                        }
                    }
                }
                foreach (String e in fileLines)
                {
                    Console.WriteLine(e);
                }
                file.Close();
                line = "";
                Console.WriteLine($"Se descargaron {counter} ficheros los últimos 7 días");
            }
            catch (Exception e)
            {
                Console.WriteLine("No se encontró el archivo");
            }

        }

        static void ficherosDescargados(String[] files)
        {
            String line = "";
            int counter = 0;

            foreach (String s in files)
            {
                if (File.GetLastWriteTime(s) > DateTime.Now.AddDays(-7))
                {
                    try
                    {
                        StreamReader file = new StreamReader(s);
                        ArrayList fileLines = new ArrayList();
                        Console.WriteLine(File.GetLastWriteTime(s));
                        while (line != null)
                        {
                            line = file.ReadLine();
                            if (line != null && (line.Contains("CONTENIDO_DEL_FICHERO_INCORRECTO") || line.Contains("OK") || line.Contains("FICHERO_YA_EXISTE")))
                            {
                                fileLines.Add(line);
                                counter++;
                            }
                        }
                        foreach (String e in fileLines)
                        {
                            Console.WriteLine(e);
                        }
                        fileLines.Clear();
                        line = "";
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No se encontró el archivo");
                    }

                }
            }
            Console.WriteLine($"Se encontraron un total de {counter} ficheros descargados previo a los últimos 7 días");
        }

        static void ficherosNoDescargados(String[] files)
        {
            String line = "";
            int counter = 0;

            foreach (String s in files)
            {
                if (File.GetLastWriteTime(s) < DateTime.Now.AddDays(-7))
                {
                    try
                    {
                        StreamReader file = new StreamReader(s);
                        ArrayList fileLines = new ArrayList();
                        Console.WriteLine(File.GetLastWriteTime(s));
                        while (line != null)
                        {
                            line = file.ReadLine();
                            if (line != null && (line.Contains("CONTENIDO_DEL_FICHERO_INCORRECTO") || line.Contains("OK") || line.Contains("FICHERO_YA_EXISTE")))
                            {
                                fileLines.Add(line);
                                counter++;
                            }
                        }
                        foreach (String e in fileLines)
                        {
                            Console.WriteLine(e);
                        }
                        fileLines.Clear();
                        line = "";
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No se encontró el archivo");
                    }

                }
            }
            Console.WriteLine($"Se encontraron un total de {counter} ficheros descargados los últimos 7 días");
        }

        static void obtieneFicherosIncorrectos(String[] files)   //Obtiene todos los ficheros incorrectos de los archivos anteriores a los 7 días
        {
            String line = "";
            int counter = 0;

            foreach (String s in files)
            {
                if (File.GetLastWriteTime(s) > DateTime.Now.AddDays(-7))
                {
                    try
                    {
                        StreamReader file = new StreamReader(s);
                        ArrayList fileLines = new ArrayList();
                        Console.WriteLine(File.GetLastWriteTime(s));
                        while (line != null)
                        {
                            line = file.ReadLine();
                            if (line != null && line.Contains("CONTENIDO_DEL_FICHERO_INCORRECTO"))
                            {
                                fileLines.Add(line);
                                counter++;
                            }
                        }
                        foreach (String e in fileLines)
                        {
                            Console.WriteLine(e);
                        }
                        file.Close();
                        Console.WriteLine($"Se encontraron {counter} ficheros incorrectos");
                        fileLines.Clear();
                        line = "";
                        counter = 0;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No se encontró el archivo");
                    }
                }

            }
        }

        static void obtieneFicherosExistentes(String[] files)   //Obtiene todos los ficheros existentes de los archivos anteriores a los 7 días
        {
            String line = "";
            int counter = 0;

            foreach (String s in files)
            {
                if (File.GetLastWriteTime(s) > DateTime.Now.AddDays(-7))
                {
                    try
                    {
                        StreamReader file = new StreamReader(s);
                        ArrayList fileLines = new ArrayList();
                        Console.WriteLine(File.GetLastWriteTime(s));
                        while (line != null)
                        {
                            line = file.ReadLine();
                            if (line != null && line.Contains("FICHERO_YA_EXISTE"))
                            {
                                fileLines.Add(line);
                                counter++;
                            }
                        }
                        foreach (String e in fileLines)
                        {
                            Console.WriteLine(e);
                        }
                        file.Close();
                        Console.WriteLine($"Se encontraron {counter} ficheros incorrectos");
                        fileLines.Clear();
                        line = "";
                        counter = 0;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No se encontró el archivo");
                    }
                }

            }
        }


        static void obtieneFicherosIncorrectos(String path) //Obtiene todos los archivos incorrectos de un fichero determinado
        {
            String line = "";
            int counter = 0;
            try
            {
                StreamReader file = new StreamReader(path);
                ArrayList fileLines = new ArrayList();
                Console.WriteLine(File.GetLastWriteTime(path));
                while (line != null)
                {
                    line = file.ReadLine();
                    if (line != null && line.Contains("CONTENIDO_DEL_FICHERO_INCORRECTO"))
                    {
                        fileLines.Add(line);
                        counter++;
                    }
                }
                foreach (String e in fileLines)
                {
                    Console.WriteLine(e);
                }
                file.Close();
                line = "";
                counter = 0;
                Console.WriteLine($"Se encontraron {counter} ficheros incorrectos");
            }
            catch (Exception e)
            {
                Console.WriteLine("No se encontró el archivo");
            }

        }

        static void obtieneFicherosExistentes(String path)                      //Obtiene todos los archivos existentes de un fichero determinado
        {
            String line = "";
            int counter = 0;
            try
            {
                StreamReader file = new StreamReader(path);
                ArrayList fileLines = new ArrayList();
                Console.WriteLine(File.GetLastWriteTime(path));
                if (File.GetLastWriteTime(path) > DateTime.Now.AddDays(-7))
                {
                    while (line != null)
                    {
                        line = file.ReadLine();
                        if (line != null && line.Contains("FICHERO_YA_EXISTE"))
                        {
                            fileLines.Add(line);
                            counter++;
                        }
                    }
                    foreach (String e in fileLines)
                    {
                        Console.WriteLine(e);
                    }
                    file.Close();
                    line = "";
                    counter = 0;
                    Console.WriteLine($"Se encontraron {counter} ficheros existentes");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se encontró el archivo");
            }

        }
    }
}
