using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombateEspacial
{
    public enum TipoEnemigo
    { 
        Normal,Boss
    }
    internal class Enemigo
    {
        public bool Vivo { get; set; }
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public Ventana ventana { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoEnemigo tipoEnemigo { get; set; }
        public List<Point> PosicionesEnemigo { get; set; }

        public Enemigo(Point posicion, ConsoleColor color, Ventana ventana, TipoEnemigo tipoEnemigo)
        { 
            Posicion = posicion;
            Color = color;
            this.ventana= ventana;
            this.tipoEnemigo= tipoEnemigo;
            Vivo = true;    
            Vida = 100;
            PosicionesEnemigo=new List<Point>();
        }

        public void Dibujar()
        {
            switch (tipoEnemigo)
            {
                case TipoEnemigo.Normal:
                    DibujoNormal();
                    break;
            }
        }

        public void DibujoNormal()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x+1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y+1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀   ▀");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x+1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x, y+1));
            PosicionesEnemigo.Add(new Point(x + 1, y+1));
            PosicionesEnemigo.Add(new Point(x + 2, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));

        }

        public void DibujarBoss()
        { 
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y=Posicion.Y;
            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y+1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");


            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x + 3, y));
            PosicionesEnemigo.Add(new Point(x + 4, y));
            PosicionesEnemigo.Add(new Point(x + 5, y));
            PosicionesEnemigo.Add(new Point(x + 6, y));

            PosicionesEnemigo.Add(new Point(x, y+1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x + 4, y + 1));
            PosicionesEnemigo.Add(new Point(x + 5, y + 1));
            PosicionesEnemigo.Add(new Point(x + 6, y + 1));

            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 1, y + 2));
            PosicionesEnemigo.Add(new Point(x + 2, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
            PosicionesEnemigo.Add(new Point(x + 4, y + 2));
            PosicionesEnemigo.Add(new Point(x + 5, y + 2));
            PosicionesEnemigo.Add(new Point(x + 6, y + 2));
            PosicionesEnemigo.Add(new Point(x + 7, y + 2));


        }



    }
}
