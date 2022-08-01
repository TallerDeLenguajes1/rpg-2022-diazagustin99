// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
Console.WriteLine("Hello, World!");
var ListaDeTiposCaracteristicas = new List<Caracteristicas>();
var ListaDeTiposString = new List<string>();
var ListaDePersonajes = new List<Personaje>();
var ListaDeNombres = new List<string>();
var objetoRespuesta = new contents();
var ListaDeGanadores = new List<string>();
var opcionmenu = 0;
Personaje PersonajePropio = null;
System.Console.WriteLine("Cargando todos los datos por defecto...");
ListaDeTiposString.Add("Caballero");
ListaDeTiposString.Add("Arquero");
ListaDeTiposString.Add("Ogro");
ListaDeTiposString.Add("Verdugo");
ListaDeTiposString.Add("Mago");
ListaDeTiposCaracteristicas.Add(new Caracteristicas(10, 5, 6, 1, 5));
ListaDeTiposCaracteristicas.Add(new Caracteristicas(8, 5, 8, 1, 5));
ListaDeTiposCaracteristicas.Add(new Caracteristicas(4, 3, 10, 1, 10));
ListaDeTiposCaracteristicas.Add(new Caracteristicas(7, 3, 10, 1, 6));
ListaDeTiposCaracteristicas.Add(new Caracteristicas(10, 5, 10, 4, 1));
System.Console.WriteLine("Todas las plantillas cargadas...");
ListaDeNombres = GeneradorDeNombres();
foreach (var item in ListaDeNombres)
{
    System.Console.WriteLine(item);
}
do
{
    System.Console.WriteLine("****BIENVENIDO A ROL GAME****");
    System.Console.WriteLine("1- Crear Personaje.");
    System.Console.WriteLine("2- Mostrar todos los rivales.");
    System.Console.WriteLine("3- Combate random.");
    System.Console.WriteLine("4- Campeonato.");
    string opcionmenuString = System.Console.ReadLine();
    opcionmenu = Convert.ToInt32(opcionmenuString);
    switch (opcionmenu)
    {
        case 1:
            var aux = 1;
            var opcion = 0;
            System.Console.WriteLine("****CREADOR DE PERSONAJES****");
            do
            {

                System.Console.WriteLine("¿Que tipo sera su personaje?");
                foreach (var item in ListaDeTiposString)
                {

                    System.Console.WriteLine(aux + "- " + item);
                    aux++;
                }
                System.Console.WriteLine("elija una opcion: ");
                string opcionString = System.Console.ReadLine();
                opcion = Convert.ToInt32(opcionString);
                if (opcion > 5 || opcion < 1)
                {
                    System.Console.WriteLine("Eligio una opcion no valida, por favor elija una correcta.");
                }
            } while (opcion > 5 || opcion < 1);
            System.Console.WriteLine("Escriba el Nombre de su personaje:");
            string Nombre = System.Console.ReadLine();
            System.Console.WriteLine("Escriba su nick:");
            string Nick = System.Console.ReadLine();
            System.Console.WriteLine("Escriba su fecha de nacimiento(dd/mm/nnnn): ");
            string FechaString = System.Console.ReadLine();
            var FechaDiv = FechaString.Split('/');
            int dia = Convert.ToInt32(FechaDiv[0]);
            int mes = Convert.ToInt32(FechaDiv[1]);
            int anio = Convert.ToInt32(FechaDiv[2]);
            DateTime FechaN = new DateTime(anio, mes, dia);
            DateTime DiaAc = DateTime.Today;
            int edad = DiaAc.Year - FechaN.Year;
            PersonajePropio = CrearPersonaje(ListaDeTiposString, ListaDeTiposCaracteristicas, Nombre, Nick, edad, FechaN, opcion);
            System.Console.WriteLine("**** PERSONAJE CREADO CON EXITO***");
            MostrarPersonaje(PersonajePropio);
            break;

        case 2:
            foreach (var item in ListaDePersonajes)
            {
                MostrarPersonaje(item);
                System.Console.WriteLine("//////////////////////////////////////////////////////////");
            }
            break;

        case 3:
            if (PersonajePropio != null)
            {
                int opbots;
                int banderaBots = 1;
                do
                {
                    System.Console.WriteLine("///////////// CREACION DE COMBATE RANDOM /////////////");
                    System.Console.WriteLine("Los rivales:");
                    System.Console.WriteLine("1- Cargarlos desde el archivo json (bots.json).");
                    System.Console.WriteLine("2- Generarlos automaticamente.");
                    System.Console.WriteLine("Seleccione una opcion: ");
                    string opbotsString = System.Console.ReadLine();
                    opbots = Convert.ToInt32(opbotsString);
                    switch (opbots)
                    {
                        case 1:
                            if (File.Exists("bots.Json"))
                            {
                                string jsonString = File.ReadAllText("bots.Json");
                                ListaDePersonajes = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
                                banderaBots = 0;
                            }
                            else
                            {
                                System.Console.WriteLine("El archivo bots.Json no existe, debe generar automaticamente los bots.");
                            }
                            break;

                        case 2:
                            ListaDePersonajes = CreacionDePersonajes(ListaDeTiposString, ListaDeTiposCaracteristicas, ListaDeNombres);
                            break;

                        default:
                            System.Console.WriteLine("Elegiste una opcion no valida, por favor vuelve a elegir una opcion....");
                            break;
                    }
                } while (opbots != 1 || opbots != 2 && banderaBots != 0);
                File.WriteAllText("bots.Json", JsonSerializer.Serialize(ListaDePersonajes));
                int nRival = new Random().Next(0, 31);
                double pv1 = PersonajePropio.DatosPersonaje1.Salud1;
                double pv2 = ListaDePersonajes[nRival].DatosPersonaje1.Salud1;
                Personaje Ganador = Combate(PersonajePropio, ListaDePersonajes[nRival], PersonajePropio);
                PersonajePropio.DatosPersonaje1.Salud1 = pv1;
                ListaDePersonajes[nRival].DatosPersonaje1.Salud1 = pv2;
                System.Console.WriteLine("/////////// TENEMOS GANADOR DEL COMBATE RANDOM \\\\\\\\\\\\");
                MostrarPersonaje(Ganador);
            }
            else
            {
                System.Console.WriteLine("Aun no creaste un personaje... hazlo antes de batallar.");
            }
            break;
        case 4:
            if (PersonajePropio != null)
            {
                int opbots;
                int banderaBots = 1;
                do
                {
                    System.Console.WriteLine("///////////// CREACION DE TORNEO /////////////");
                    System.Console.WriteLine("Los rivales:");
                    System.Console.WriteLine("1- Cargarlos desde el archivo json (bots.json).");
                    System.Console.WriteLine("2- Generarlos automaticamente.");
                    System.Console.WriteLine("Seleccione una opcion: ");
                    string opbotsString = System.Console.ReadLine();
                    opbots = Convert.ToInt32(opbotsString);
                    switch (opbots)
                    {
                        case 1:
                            if (File.Exists("bots.Json"))
                            {
                                string jsonString = File.ReadAllText("bots.Json");
                                ListaDePersonajes = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
                                banderaBots = 0;
                            }
                            else
                            {
                                System.Console.WriteLine("El archivo bots.Json no existe, debe generar automaticamente los bots.");
                            }
                            break;

                        case 2:
                            ListaDePersonajes = CreacionDePersonajes(ListaDeTiposString, ListaDeTiposCaracteristicas, ListaDeNombres);
                            break;

                        default:
                            System.Console.WriteLine("Elegiste una opcion no valida, por favor vuelve a elegir una opcion....");
                            break;
                    }
                } while (opbots != 1 || opbots != 2 && banderaBots != 0);
                File.WriteAllText("bots.Json", JsonSerializer.Serialize(ListaDePersonajes));
                Personaje Campeon = Torneo(2, PersonajePropio, ListaDePersonajes);
                System.Console.WriteLine("/////****TENEMOS CAMPEON DEL TORNEO!****/////");
                MostrarPersonaje(Campeon);
                if (File.Exists("Ganadores.csv") == false)
                {
                    ListaDeGanadores.Add("Nick;Nombre;Tipo;Fecha Que gano");
                    ListaDeGanadores.Add(Campeon.DatosPersonaje1.Apodo1 + ";" + Campeon.DatosPersonaje1.Nombre1 + ";" + Campeon.DatosPersonaje1.Tipo1 + ";" + DateTime.Now.ToShortDateString());
                    File.WriteAllLines("Ganadores.csv", ListaDeGanadores);
                }
                else
                {
                    var reader = new StreamReader(File.OpenRead("Ganadores.csv"));
                    while (!reader.EndOfStream)
                    {
                        ListaDeGanadores.Add(reader.ReadLine());
                    }
                    ListaDeGanadores.Add(Campeon.DatosPersonaje1.Apodo1 + ";" + Campeon.DatosPersonaje1.Nombre1 + ";" + Campeon.DatosPersonaje1.Tipo1 + ";" + DateTime.Now.ToShortDateString());
                    reader.Close();
                    File.WriteAllLines("Ganadores.csv", ListaDeGanadores);
                }
            }
            else
            {
                System.Console.WriteLine("Aun no creaste un personaje... hazlo antes de batallar.");
            }
            break;
        default:
            break;
    }
} while (opcionmenu != 5);




static Personaje Torneo(int participantes, Personaje usuario, List<Personaje> Bots)
{
    Personaje ganador = usuario;
    double pv1;
    double pv2;
    int nSuerte;
    Random rnd = new Random();
    for (int i = 0; i < participantes; i++)
    {
        System.Console.WriteLine("//////COMIENZA EL COMBATE N°{0}\\\\\\", i + 1);
        pv1 = ganador.DatosPersonaje1.Salud1;
        pv2 = Bots[i].DatosPersonaje1.Salud1;
        ganador = Combate(ganador, Bots[i], usuario);
        if (ganador.DatosPersonaje1.Apodo1 == Bots[i].DatosPersonaje1.Apodo1)
        {
            ganador.DatosPersonaje1.Salud1 = pv2;
        }
        else
        {
            ganador.DatosPersonaje1.Salud1 = pv1;
        }
        nSuerte = rnd.Next(1, 4);
        System.Console.WriteLine("TENEMOS GANADOR DE LA BATALLA n°" + (i + 1) + " ES: " + ganador.DatosPersonaje1.Nombre1 + " ({0}) " + " por ganar tendra un atributo sorpresa de premio... ahora sabremos cual...", ganador.DatosPersonaje1.Tipo1);
        switch (nSuerte)
        {
            case 1:
                System.Console.WriteLine("¡Wow! la vida se aumento +500 puntos extras... Sera mas duro de matar al ganador la proxima batalla...");
                ganador.DatosPersonaje1.Salud1 = ganador.DatosPersonaje1.Salud1 + 500;
                break;
            case 2:
                System.Console.WriteLine("¡Wow! El nivel del ganador aumento +1, tus ataques seran mas duros!..");
                ganador.CaracteristicasPersonaje1.Nivel1++;
                break;
            case 3:
                System.Console.WriteLine("¡Wow! Ganar la batalla te hizo mas fuerte +1 de Fuerza...");
                ganador.CaracteristicasPersonaje1.Fuerza1++;
                break;
            case 4:
                System.Console.WriteLine("Le Robaste la armadura a tu rival, ahora estas mas equipado! +1 de Armadura...");
                ganador.CaracteristicasPersonaje1.Armadura1++;
                break;


            default:
                break;
        }
        Thread.Sleep(2000);
    }
    return ganador;
}




static Personaje Combate(Personaje p1, Personaje p2, Personaje usuario)
{
    int nSuerte = 0;
    int nAlAzar = 0;
    double DifNSuerte = 0;
    int eddp1 = 0;
    int eddp2 = 0;
    int nSuertep2 = 0;
    int nAlAzarp2 = 0;
    double DifNSuertep2 = 0;
    Random rnd = new Random();
    for (int i = 0; i < 3; i++)
    {
        if (p1.DatosPersonaje1.Salud1 <= 0 || p2.DatosPersonaje1.Salud1 <= 0)
        {
            if (p1.DatosPersonaje1.Salud1 <= 0)
            {
                System.Console.WriteLine("El jugador: " + p2.DatosPersonaje1.Apodo1 + " Mató en el round " + (i + 1) + " al jugador " + p1.DatosPersonaje1.Apodo1 + " Felicidades!!");
                return p2;
            }
            else
            {
                System.Console.WriteLine("El jugador: " + p1.DatosPersonaje1.Apodo1 + " Mató en el round " + (i + 1) + " al jugador " + p2.DatosPersonaje1.Apodo1 + " Felicidades!!");
                return p1;
            }
        }
        else
        {
            nAlAzar = rnd.Next(1, 5);
            System.Console.WriteLine("/////////////////////// ROUND: " + (i + 1) + " ///////////////////////");
            if (p1.DatosPersonaje1.Apodo1 == usuario.DatosPersonaje1.Apodo1 || p2.DatosPersonaje1.Apodo1 == usuario.DatosPersonaje1.Apodo1)
            {
                System.Console.WriteLine("Elige un numero de la suerte (entre 1 y 5)... si le aciertas tu efectividad de disparo sera 100%:");
                string nSuerteString = System.Console.ReadLine();
                nSuerte = Convert.ToInt32(nSuerteString);
                DifNSuerte = nAlAzar - nSuerte;
            }
            else
            {
                nSuerte = rnd.Next(1, 5);
                nAlAzar = rnd.Next(1, 5);
                DifNSuerte = nAlAzar - nSuerte;
            }
            nSuertep2 = rnd.Next(1, 5);
            nAlAzarp2 = rnd.Next(1, 5);
            DifNSuertep2 = nAlAzarp2 - nSuertep2;
            DifNSuerte = Math.Pow(DifNSuerte, 2);
            DifNSuerte = Math.Pow(DifNSuerte, 0.5);
            DifNSuertep2 = Math.Pow(DifNSuertep2, 2);
            DifNSuertep2 = Math.Pow(DifNSuertep2, 0.5);
            System.Console.WriteLine("el numero al azar es el: " + nAlAzar + " la diferencia es: " + DifNSuerte);
            switch (DifNSuerte)
            {
                case 0:
                    eddp1 = 100;
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " acerto el numero de la suerte! tiene 100% de efectividad!!.");
                    break;
                case 1:
                    eddp1 = 75;
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " estuvo demasiado cerca del numero de la suerte tiene 75% de efectividad!!.");
                    break;
                case 2:
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " cerca del numero de la suerte tiene 50% de efectividad!!.");
                    eddp1 = 50;
                    break;

                case 3:
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " un poco lejos del numero de la suerte tiene 25% de efectividad!!.");
                    eddp1 = 25;
                    break;
                case 4:
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " demasiado lejos del numero de la suerte tiene 10% de efectividad!!.");
                    eddp1 = 10;
                    break;

                default:
                    eddp1 = 1;
                    System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " se paso de listo asi que tiene una efectividad de 1.");
                    break;
            }

            double daniop1 = (((p1.CaracteristicasPersonaje1.Fuerza1 * p1.CaracteristicasPersonaje1.Destreza1 * p1.CaracteristicasPersonaje1.Nivel1 * eddp1 * eddp1) - (p2.CaracteristicasPersonaje1.Armadura1 * p2.CaracteristicasPersonaje1.Velocidad1)) / 50000) * 100;
            Thread.Sleep(2000);
            System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " , hace un daño de: " + daniop1 + " a " + p2.DatosPersonaje1.Apodo1);
            p2.DatosPersonaje1.Salud1 = p2.DatosPersonaje1.Salud1 - daniop1;
            Thread.Sleep(2000);
            System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " Queda con: " + p2.DatosPersonaje1.Salud1 + " puntos de salud luego del ataque.");
            Thread.Sleep(2000);
            if (p2.DatosPersonaje1.Salud1 > 0)
            {
                System.Console.WriteLine("el numero al azar es el: " + nAlAzarp2 + " la diferencia es: " + DifNSuertep2);
                switch (DifNSuertep2)
                {
                    case 0:
                        eddp2 = 100;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " acerto el numero de la suerte! tiene 100% de efectividad!!.");
                        break;
                    case 1:
                        eddp2 = 75;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " estuvo demasiado cerca del numero de la suerte tiene 75% de efectividad!!.");
                        break;
                    case 2:
                        eddp2 = 50;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " cerca del numero de la suerte tiene 50% de efectividad!!.");
                        break;

                    case 3:
                        eddp2 = 25;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " un poco lejos del numero de la suerte tiene 25% de efectividad!!.");
                        break;
                    case 4:
                        eddp2 = 10;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " se paso de listo asi que tiene una efectividad de 1.");
                        break;

                    default:
                        eddp2 = 1;
                        System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " se paso de listo asi que tiene una efectividad de 1.");
                        break;
                }
                double daniop2 = (((p2.CaracteristicasPersonaje1.Fuerza1 * p2.CaracteristicasPersonaje1.Destreza1 * p2.CaracteristicasPersonaje1.Nivel1 * eddp2 * eddp2) - (p1.CaracteristicasPersonaje1.Armadura1 * p1.CaracteristicasPersonaje1.Velocidad1)) / 50000) * 100;
                Thread.Sleep(2000);
                System.Console.WriteLine("el jugador: " + p2.DatosPersonaje1.Apodo1 + " , hace un daño de: " + daniop2 + " a " + p1.DatosPersonaje1.Apodo1);
                p1.DatosPersonaje1.Salud1 = p1.DatosPersonaje1.Salud1 - daniop2;
                Thread.Sleep(2000);
                System.Console.WriteLine("el jugador: " + p1.DatosPersonaje1.Apodo1 + " Queda con: " + p1.DatosPersonaje1.Salud1 + " puntos de salud luego del ataque.");
                Thread.Sleep(2000);

            }
            else
            {
                System.Console.WriteLine("El jugador: " + p1.DatosPersonaje1.Apodo1 + " Mató en el round " + (i + 1) + " al jugador " + p2.DatosPersonaje1.Apodo1 + " Felicidades!!");
                return p1;
            }
        }
    }

    if (p1.DatosPersonaje1.Salud1 < p2.DatosPersonaje1.Salud1)
    {
        System.Console.WriteLine("En una batalla reñida gano el jugador: " + p2.DatosPersonaje1.Apodo1 + " por diferencia de vida" + " al jugador " + p1.DatosPersonaje1.Apodo1);
        return p2;
    }
    else
    {
        System.Console.WriteLine("En una batalla reñida gano el jugador: " + p1.DatosPersonaje1.Apodo1 + " por diferencia de vida" + " al jugador " + p2.DatosPersonaje1.Apodo1);
        return p1;
    }


}




static List<Personaje> CreacionDePersonajes(List<string> Tipos, List<Caracteristicas> caracteristicas, List<string> Nombres)
{
    var Personajes = new List<Personaje>();
    Random rnd = new Random();
    for (int i = 0; i < 32; i++)
    {
        Personajes.Add(CrearPersonaje(Tipos, caracteristicas, Nombres[rnd.Next(0, 31)], "Personaje" + i, rnd.Next(0, 300), new DateTime(rnd.Next(1500, 1950), rnd.Next(1, 12), rnd.Next(1, 31)), rnd.Next(0, 4)));
    }


    return Personajes;
}

static void MostrarPersonaje(Personaje personaje)
{
    System.Console.WriteLine("**** PERSONAJE DE NOMBRE: " + personaje.DatosPersonaje1.Nombre1 + " ****");
    System.Console.WriteLine("Apodo: " + personaje.DatosPersonaje1.Apodo1 + " / Tipo: " + personaje.DatosPersonaje1.Tipo1 + " / Fecha de Nacimiento: " + personaje.DatosPersonaje1.FechadeNacimiento1.ToShortDateString());
    System.Console.WriteLine("CARACTERISTICAS: ");
    System.Console.WriteLine("Nivel: " + personaje.CaracteristicasPersonaje1.Nivel1);
    System.Console.WriteLine("vida: " + personaje.DatosPersonaje1.Salud1);
    System.Console.WriteLine("armadura: " + personaje.CaracteristicasPersonaje1.Armadura1);
    System.Console.WriteLine("destreza: " + personaje.CaracteristicasPersonaje1.Destreza1);
    System.Console.WriteLine("Fuerza: " + personaje.CaracteristicasPersonaje1.Fuerza1);
}


static Personaje CrearPersonaje(List<string> Tipos, List<Caracteristicas> caracteristicas, string Nombre, string Nick, int edad, DateTime FechaN, int opcion)
{
    Personaje PersonajeCreado;
    switch (opcion)
    {
        case 1:
            PersonajeCreado = new Personaje(new Datos(Tipos[0], Nombre, Nick, FechaN, edad, 8500), caracteristicas[0]);
            break;
        case 2:
            PersonajeCreado = new Personaje(new Datos(Tipos[1], Nombre, Nick, FechaN, edad, 6000), caracteristicas[1]);
            break;
        case 3:
            PersonajeCreado = new Personaje(new Datos(Tipos[2], Nombre, Nick, FechaN, edad, 9000), caracteristicas[2]);
            break;
        case 4:
            PersonajeCreado = new Personaje(new Datos(Tipos[3], Nombre, Nick, FechaN, edad, 10000), caracteristicas[3]);
            break;
        case 5:
            PersonajeCreado = new Personaje(new Datos(Tipos[4], Nombre, Nick, FechaN, edad, 4000), caracteristicas[4]);
            break;

        default:
            PersonajeCreado = new Personaje(new Datos(Tipos[4], Nombre, Nick, FechaN, edad, 4000), caracteristicas[4]);
            break;
    }

    return PersonajeCreado;
}


static List<string> GeneradorDeNombres()
{
    string url = @"https://api.fungenerators.com/name/generate.json?category=elf&limit=32";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";
    request.ContentType = "application/json";
    request.Accept = "application/json";
    var objetoRespuesta = new respuesta();
    int bandera = 0;
    int bandera2 = 0;
    do
    {
        try
        {
            if (bandera2 == 1)
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";
            }
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            objetoRespuesta = JsonSerializer.Deserialize<respuesta>(responseBody);
                        }
                    }
                }
            }
            bandera = 0;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            Thread.Sleep(2000);
            bandera = 1;
            bandera2 = 1;
        }
    } while (bandera == 1);

    return objetoRespuesta.contenido.Names;
}


public class contents
{
    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("variation")]
    public string Variation { get; set; }

    [JsonPropertyName("names")]
    public List<string> Names { get; set; }
}

public class respuesta
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("contents")]
    public contents contenido { get; set; }


    [JsonPropertyName("copyright")]
    public string Copyright { get; set; }
}



