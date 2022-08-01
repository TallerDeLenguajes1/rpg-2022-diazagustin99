// See https://aka.ms/new-console-template for more information
public class Datos{
private String Tipo; //entre 4
private String Nombre; //modificable
private String Apodo; //modificable
private DateTime FechadeNacimiento; //modificable
private int Edad; //entre 0 a 300 modificable
private double Salud; // depende del tipo

    public Datos()
    {
    }

    public Datos(String tipo, String nombre, String apodo, DateTime fechadeNacimiento, int edad, double salud)
    {
        Tipo1 = tipo;
        Nombre1 = nombre;
        Apodo1 = apodo;
        FechadeNacimiento1 = fechadeNacimiento;
        Edad1 = edad;
        Salud1 = salud;
    }

    public String Tipo1 { get => Tipo; set => Tipo = value; }
    public String Nombre1 { get => Nombre; set => Nombre = value; }
    public String Apodo1 { get => Apodo; set => Apodo = value; }
    public DateTime FechadeNacimiento1 { get => FechadeNacimiento; set => FechadeNacimiento = value; }
    public int Edad1 { get => Edad; set => Edad = value; }
    public double Salud1 { get => Salud; set => Salud = value; }
}