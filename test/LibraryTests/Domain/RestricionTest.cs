using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;
using Library;
namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Restricion))]
public class RestricionTest
{
    public Restricion mireestricion;

    [Test]
    public void SetUp()
    { 
        mireestricion = new Restricion();
    }

    [Test]
    public void ReestriccionPokemonTest()
    {
        string mensaje = mireestricion.ReestriccionPokemon("Abra");
        Assert.That(mensaje, Is.EqualTo("Se agrego el pokemon Abra a la lista de reestricciones. "));
        mensaje = mireestricion.ReestriccionPokemon("Abra");
        Assert.That(mensaje, Is.EqualTo("Pokemon ya agregado, elija otro"));
        mensaje = mireestricion.ReestriccionPokemon("Kabooom");
        Assert.That(mensaje, Is.EqualTo("Ingrese un pokemon valido"));
    }
    [Test]
    public void ReestriccionitemTest()
    {
        string mensaje = mireestricion.ReestriccionItem("Superpocion");
        Assert.That(mensaje, Is.EqualTo($"El item se Superpocion agrego a la lista de reestricciones. "));
        mensaje = mireestricion.ReestriccionPokemon("Superpocion");
        Assert.That(mensaje, Is.EqualTo("Item ya agregado, elija otro"));
        mensaje = mireestricion.ReestriccionPokemon("Superpocionesss");
        Assert.That(mensaje, Is.EqualTo("Ingrese un item valido"));
    }
    [Test]
    public void ReestriccionTiposTest()
    {
        string mensaje = mireestricion.ReestriccionTipos("Lucha");
        Assert.That(mensaje, Is.EqualTo($"Se agrego el tipo Lucha a la lista de reestricciones. "));
        mensaje = mireestricion.ReestriccionTipos("Lucha");
        Assert.That(mensaje, Is.EqualTo("Tipo ya agregado, elija otro"));
        mensaje = mireestricion.ReestriccionTipos("Fuerte");
        Assert.That(mensaje, Is.EqualTo("Ingrese un tipo valido"));
    }
    [Test]
    public void ReestriccionTest()
    {
        mireestricion.ReestriccionTipos("Lucha");
        mireestricion.ReestriccionItem("Superpocion");
        mireestricion.ReestriccionPokemon("Abra");
        string mensaje = mireestricion.Reestricciones();
        Assert.That(mensaje, Is.EqualTo("Caracteristicas reestringidas esta batalla: \n" + "Items: Superpocion- \n" + "Tipos: Lucha- \n" + "\n Pokemones Abra- ."));
    }
    [Test]
    public void EstePokemonTest()
    {
        mireestricion.ReestriccionTipos("Lucha");
        mireestricion.ReestriccionPokemon("Abra");
        bool mensaje = mireestricion.EstePokemon(7);
        Assert.That(mensaje, Is.False);
        mensaje = mireestricion.EstePokemon(10);
        Assert.That(mensaje, Is.False);
        mensaje = mireestricion.EstePokemon(2);
        Assert.That(mensaje, Is.True);
    }
    [Test]
    public void EsteItemTest()
    {
        mireestricion.ReestriccionItem("Superpocion");
        bool mensaje = mireestricion.EsteItem("Superpocion");
        Assert.That(mensaje, Is.False); 
        mireestricion.EsteItem("Revivir");
        Assert.That(mensaje, Is.True);
    }
    // No entiendo el porque da error el test, creo que no llegue a finalizar porque no puede ver el error donde estaba
    // me falto algo de tiempo para terminarlo,
}