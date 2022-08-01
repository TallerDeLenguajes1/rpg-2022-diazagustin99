// See https://aka.ms/new-console-template for more information
public class Caracteristicas{
private int Velocidad;// 1 a 10
private int Destreza; //1 a 5
private int Fuerza;//1 a 10
private int Nivel; //1 a 10
private int Armadura; //1 a 10

    public Caracteristicas(int velocidad, int destreza, int fuerza, int nivel, int armadura)
    {
        Velocidad1 = velocidad;
        Destreza1 = destreza;
        Fuerza1 = fuerza;
        Nivel1 = nivel;
        Armadura1 = armadura;
    }

        public Caracteristicas()
    {
    }

    public int Velocidad1 { get => Velocidad; set => Velocidad = value; }
    public int Destreza1 { get => Destreza; set => Destreza = value; }
    public int Fuerza1 { get => Fuerza; set => Fuerza = value; }
    public int Nivel1 { get => Nivel; set => Nivel = value; }
    public int Armadura1 { get => Armadura; set => Armadura = value; }
}