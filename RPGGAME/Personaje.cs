// See https://aka.ms/new-console-template for more information
public class Personaje{
    private Datos DatosPersonaje;
    private Caracteristicas CaracteristicasPersonaje;

    public Personaje()
    {
    }

    public Personaje(Datos datosPersonaje, Caracteristicas caracteristicasPersonaje)
    {
        DatosPersonaje1 = datosPersonaje;
        CaracteristicasPersonaje1 = caracteristicasPersonaje;
    }

    public Datos DatosPersonaje1 { get => DatosPersonaje; set => DatosPersonaje = value; }
    public Caracteristicas CaracteristicasPersonaje1 { get => CaracteristicasPersonaje; set => CaracteristicasPersonaje = value; }
}

