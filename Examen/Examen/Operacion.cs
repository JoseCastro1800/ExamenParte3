using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Examen
{
    public class Operacion
    {    //lista global 
        public List<Perros> Perros;
        
        public Operacion()
        {   //Lista publica que trabaja con el metodo
            Perros = ObtenerPerros();   
        }
        //Se le llama desde el main
        internal void Principal()
        {
            Console.WriteLine("Bienvenido al programa de perritos");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        //Para poder trabajar mas agusto como tenemos la lista hecha esta estara todo el tiempo 
        public void Menu()
        {
            try
            {   //Despliegue de la lista de objetos
                 ShowPerros();
                Console.WriteLine("\n\nFavor de escoger alguna opcion que desea : \n1.- Detalles del perro.\n2.- Salida.");
                switch (int.Parse(Console.ReadLine()))
                {
                    //Cuando el usuario detalle lo mandara al metodo
                    case 1:
                        Console.Clear();
                        //Se accede al detallado
                        DetallePerro();
                        break;
                    case 2:
                        System.Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("Escoga una opcion correcta man");
                        Console.ReadKey();
                        Console.Clear();
                        Menu();
                        break;
                }

            }
            //Solo para pedir numeros
            catch (Exception ex) 
            {
                Console.Clear();
                Console.WriteLine("oh no de nuevo amigo, escoga una opcion correcta por favor");
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        //Despliegue de la lista
        public void ShowPerros() 
        {
            Console.WriteLine("Estos son los perros de la lista: ");
            //foreach para cada elemento de la lista
            foreach (var item in Perros)
            {
                //Se despliega el id y nombre por que e slo mas importante
                Console.WriteLine("{0}.- {1}", item.Id, item.Nombre); 
            }
        }
        //Se obtiene la informacion del archivo txt
        public List<string> ObtenerLineas(string path)
        {
            //Creacion de su lista
            List<string> lineas = new List<string>();
            //Se busca el file por si existe
            if (File.Exists(path))
            {   //De esta manera se saca el jugo de la informacion
                string[] datos = File.ReadAllLines(path);
                //Busqueda mediante del foreach
                foreach (var item in datos)
                {   //Agregacion a la nueva lista
                    lineas.Add(item);
                }
            }
            else
            {
                Console.WriteLine("El archivo murio");
                return null;
            }
            //Se regresa al metodo
            return lineas;
        }
        public List<Perros> ObtenerPerros()
        {   //Se instancia la clase perro 
            Perros p = new Perros();
            //Optimizacion de lista
            var lineas = ObtenerLineas("Perritos.txt");
            List<Perros> perros = new List<Perros>();
            //Busqueda en la lista de string
            foreach (var item in lineas)
            {   //Por cada uno de la lista se crea un arreglo
                string[] datos = item.Split(',');
                perros.Add(new Perros { Id = int.Parse(datos[0]), Nombre = datos[1], Fecha = datos[2], Raza = datos[3], Genero = datos[4] });//cada que llenes tu arreglo de 5 elementos, los conviertes en atributos del objeto y los agregas a la lista 
            }
            //Se regresa la lista llena 
            return perros;
        }
        //Detalles
        public void DetallePerro() 
        {
            //Por si el compa se equivoca 
            try
            {
                Console.Clear();
                //Mostrar la lista para saber que elegira el usuario
                ShowPerros();
                //Despliegue del objeto
                Perros p = new Perros();
                Console.WriteLine("Seleccione un perrito para poder detallarlo ");
                //Con el id se busca al perro
                int perroid = int.Parse(Console.ReadLine());
                //Busqueda implacable
                foreach (var item in Perros)
                {
                    //Si lo que se busca esta se convierte en un objeto y este lo arrojara
                    if (perroid == item.Id)
                    {
                        p = item;
                    }
                }
                string raza = "";//usamos un int en un atributo para no tener strings variados, asi generalizas los atributos a cosas especificas, como el status del to do list
                switch (p.Raza)//con el switch asignamos a cada valor un string especifico
                {
                    case "1":
                        raza = "Pitbull";
                        break;
                    case "2":
                        raza = "Pug";
                        break;
                    case "3":
                        raza = "Pastor Aleman";
                        break;
                    case "4":
                        raza = "Chihuahua";
                        break;
                    case "5":
                        raza = "Callejero";
                        break;
                }
                Console.Clear();
                //Despliegue de los atributos
                Console.WriteLine("Nombre:  {0}\nFecha:  {1}\nRaza:  {2}\nGenero:  {3}", p.Nombre, p.Fecha, raza, p.Genero);
                Console.WriteLine("\nSi quiere cambiar algo solo de click en 1, si desea volver a lista man de click en 2.");
                //Se le da la opcion de que el usuario quiera editar el objeto y se va a la goma 
                int option = int.Parse(Console.ReadLine());
                //Si se modifica el objeto pues este sera sustituido por una nueva version y este fue enviado al cambio como una tipo sobrecarga
                //CUando este se modifica le llama al metodo para que se actualize el archivo 
                if (option == 1)
                {
                    p = EdicionPerro(p);
                    ActualizacionTxt();
                    Console.Clear();
                    Menu();
                }
                //Por si no quiere editar se va a la goma xd
                else
                {
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
            }
            //En caso de una exepcion se interrumpe la edicion
            catch (Exception e) 
            {
                Console.WriteLine("uy no valio kaka: {0}\nPiquele a algo para volver al principio", e.Message);
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        //Edicion del objeto cuando se acepta
        public Perros EdicionPerro(Perros p) 
        {
            try
            {
                Console.WriteLine("Escoga algun atributo a modificar man:\n1.-Nombre\n2.-Fecha\n3.-Raza\n4.-Genero");
                //Se selecciona el atributo a desear
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Ingrese el nuevo nombre: ");
                        string nombre = Console.ReadLine();
                        p.Nombre = nombre;
                        Console.WriteLine("Se ha modificado el Nombre.");
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la nueva Fecha: ");
                        string fecha = Console.ReadLine();
                        p.Fecha = fecha;
                        Console.WriteLine("Se ha modificado la fecha");
                        break;
                    case 3:
                        Console.WriteLine("Seleccione con un numero la nueva raza:\n1.- Pitbull\n2.- Pug\n3.- Pastor Aleman\n4.- Chihuahua\n5.- Callejero");
                        string raza = Console.ReadLine();
                        p.Raza = raza;
                        Console.WriteLine("Se ha modificado la raza");
                        break;
                    case 4:
                        Console.WriteLine("Ingrese el nuevo genero: ");
                        string genero = Console.ReadLine();
                        p.Genero = genero;
                        Console.WriteLine("Se ha modificado el genero.");
                        break;
                    default: break;
                }
                Console.WriteLine("Presione ENTER para continuar");
                Console.ReadKey();
                return p;
            }
            //Por si hay un error de sintaxis este te devolvera sin ningun cambio jaja
            catch (Exception exe) 
            {
                Console.WriteLine("otra vez man?: {0}\nDe regreso al principio por feo", exe.Message);
                Menu();
                return p;
            }
        }
        //Modificacion de la lista
        //Es cuando se crea la lista en forma global pero de forma contraria en vez de usarse split se uso joim
        public void ActualizacionTxt()
        {//Se hace una lista en la cual se usa una Fusiooon
            List<string> lineas = new List<string>();
            foreach (var perro in Perros) 
            {    // Se llena un vector de string, esto es para que se llenen los atributos
                //Entonces aqui se actualiza la lista por cada nuevo objeto
                string[] Nuevo = new string[5];
                Nuevo[0] = Convert.ToString(perro.Id);
                Nuevo[1] = perro.Nombre;
                Nuevo[2] = perro.Fecha;
                Nuevo[3] = perro.Raza;
                Nuevo[4] = perro.Genero;
                lineas.Add(string.Join(",", Nuevo));
            }
            //Ahora se utiliza otro join ya que se fusionen las listas, se usa el blackash ya que esto hace una division y es un reglon a lo que se refiere
            var joinedstring = string.Join("\n", lineas);
            //Ya por ultimo todo se pasa a la lista Perritos a el archivo txt
            File.WriteAllText("Perritos.txt", joinedstring);
        }
    }
}
