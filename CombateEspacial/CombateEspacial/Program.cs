using CombateEspacial;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

Ventana ventana;
Nave nave;
Enemigo enemigo1;
Enemigo enemigo2;
Enemigo enemigoBoss1;

bool jugar = false;
bool bossfinal=false;
bool ejeucion = true;

void Iniciar() 
{
    ventana = new Ventana(170, 45, ConsoleColor.Black, new Point(5, 3), new Point(165, 43));
    ventana.DibujarMarco();
    nave = new Nave(new Point(80,30),ConsoleColor.White,ventana);
  
    enemigo1 = new Enemigo(new Point(50,10),ConsoleColor.Cyan,ventana,TipoEnemigo.Normal,nave);
    enemigo2 = new Enemigo(new Point(20, 12), ConsoleColor.Red, ventana, TipoEnemigo.Normal,nave);
    enemigoBoss1 = new Enemigo(new Point(100, 10), ConsoleColor.Magenta, ventana, TipoEnemigo.Boss, nave);
    nave.Enemigos.Add(enemigo1);
    nave.Enemigos.Add(enemigo2);
    nave.Enemigos.Add(enemigoBoss1);
}

void Reiniciar()
{
    Console.Clear();
    ventana.DibujarMarco();

    nave.Vida = 100;
    nave.SobreCarga = 0;
    nave.BalaEspecial = 0;
    nave.Balas.Clear();

    enemigo1.Vida= 100;
    enemigo1.Vivo = true;

    enemigo2.Vida = 100;
    enemigo2.Vivo = true;

    enemigoBoss1.Vida = 100;
    enemigoBoss1.Vivo = true;

    enemigoBoss1.PosicionesEnemigo.Clear();

    bossfinal = false;
}
void Game()
{
    while (ejeucion)
    {
        ventana.Menu();
        ventana.Teclado(ref ejeucion, ref jugar);
        while (jugar)
        {
            if (!enemigo1.Vivo && !enemigo2.Vivo && !bossfinal)
            {
                bossfinal = true;
                ventana.Peligro();
            }
            if (bossfinal)
            {
                enemigoBoss1.Dibujar();
                enemigoBoss1.Mover();
                enemigoBoss1.Informacion(140);
            }
            else
            {
                enemigo1.Mover();
                enemigo1.Informacion(100);
                enemigo2.Mover();
                enemigo2.Informacion(120);
            }

            nave.Mover(2);
            nave.Disparar();
            if (nave.Vida <= 0)
            {
                jugar = false;
                nave.Muerte();
                Reiniciar();
            }
            if (!enemigoBoss1.Vivo)
            {
                jugar = false;
                Reiniciar();
            }
        }
    }
   
}

Iniciar();
Game();

