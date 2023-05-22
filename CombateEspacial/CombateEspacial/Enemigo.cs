using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CombateEspacial
{
    public enum TipoEnemigo
    { 
        Normal,Boss,Menu
    }
    internal class Enemigo
    {
        enum Direccion
        { 
            Derecha,Izquierda,Arriba,Abajo
        }


        public bool Vivo { get; set; }
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public Ventana ventana { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoEnemigo tipoEnemigo { get; set; }
        public List<Point> PosicionesEnemigo { get; set; }
        public List<Bala> Balas { get; set; }
        public Nave nave { get; set; }

        private Direccion direccion;
        private DateTime tiempodireccion;
        private float tiempodireccionAleatorio;

        private DateTime tiempoMovimiento;

        private DateTime tiempoDisparo;

        private float tiempoDisparoAleatorio;

        public Enemigo(Point posicion, ConsoleColor color, Ventana ventana, TipoEnemigo tipoEnemigo, Nave nave)
        { 
            Posicion = posicion;
            Color = color;
            this.ventana= ventana;
            this.tipoEnemigo= tipoEnemigo;
            this.nave = nave;
            Vivo = true;    
            Vida = 100;
            direccion = Direccion.Derecha;
            tiempodireccion = DateTime.Now;
            tiempodireccionAleatorio = 1000;
            tiempoMovimiento = DateTime.Now;
            tiempoDisparo = DateTime.Now;
            tiempoDisparoAleatorio = 200;
            PosicionesEnemigo=new List<Point>();
            Balas = new List<Bala>();

        }

        public void Dibujar()
        {
            switch (tipoEnemigo)
            {
                case TipoEnemigo.Normal:
                    DibujoNormal();
                    break;
                case TipoEnemigo.Boss:
                    DibujarBoss();
                    break;
                case TipoEnemigo.Menu:
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
            Console.Write("▀  ▀");

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

            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x + 4, y + 1));
            PosicionesEnemigo.Add(new Point(x + 6, y + 1));
            PosicionesEnemigo.Add(new Point(x + 7, y + 1));

            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 1, y + 2));
            PosicionesEnemigo.Add(new Point(x + 2, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
            PosicionesEnemigo.Add(new Point(x + 4, y + 2));
            PosicionesEnemigo.Add(new Point(x + 5, y + 2));
            PosicionesEnemigo.Add(new Point(x + 6, y + 2));
            PosicionesEnemigo.Add(new Point(x + 7, y + 2));


        }


        public void Muerte()
        {
            if (tipoEnemigo == TipoEnemigo.Normal)
            {
                MuerteNormal();
            }
            if (tipoEnemigo == TipoEnemigo.Boss)
            {
                MuerteBoss();    
            }
        }
        public void MuerteBoss()
        {
            Console.ForegroundColor = Color;
            foreach (Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("▓");
                Thread.Sleep(200);
            }
            foreach (Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("");
                Thread.Sleep(200);
            }
            PosicionesEnemigo.Clear();
            foreach (Bala item in Balas)
            {
                item.Borrar();

            }
            Balas.Clear();
        }

        public void MuerteNormal()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄Zzz");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");
            PosicionesEnemigo.Clear();
            foreach (Bala item in Balas)
            {
                item.Borrar();
            }
            Balas.Clear();

        }

        public void Borrar()
        {
            foreach (Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");

            }
        }

        public void Mover() 
        {
            if (!Vivo)
            {
                Muerte();
                return;
            }
            int tiempo = 30;
            if (tipoEnemigo == TipoEnemigo.Boss)
                tiempo = 20;
            if (DateTime.Now > tiempoMovimiento.AddMilliseconds(tiempo))
            {
                Borrar();
                DireccionAleatoria();
                Point posicionAux = Posicion;
                Movimiento(ref posicionAux);
                Colisiones(posicionAux);
                Dibujar();
                tiempoMovimiento = DateTime.Now;
            }

            if (tipoEnemigo != TipoEnemigo.Menu)
            {
                CrearBalas();
                Disparar();
            }
            
        }

        private void Colisiones(Point posicionAux)
        {
            int ancho = 3;
            if (tipoEnemigo == TipoEnemigo.Boss)
                ancho = 7;

            int limiteInferioe = ventana.LimiteSuperior.Y + 15;

            if (tipoEnemigo == TipoEnemigo.Menu)
            {
                limiteInferioe = ventana.LimiteInferior.Y - 1;
            }

            if (posicionAux.X <= ventana.LimiteSuperior.X)
            {
                direccion = Direccion.Derecha;
                posicionAux.X = ventana.LimiteSuperior.X + 1;
            }
            if (posicionAux.X + ancho >= ventana.LimiteInferior.X)
            { 
                direccion = Direccion.Izquierda;
                posicionAux.X = ventana.LimiteInferior.X - 1-ancho;

            }
            if (posicionAux.Y <= ventana.LimiteSuperior.Y + 1)
            { 
                direccion = Direccion.Abajo;
                posicionAux.Y = ventana.LimiteSuperior.Y + 1;
            }
            if (posicionAux.Y + 2 >= limiteInferioe)
            { 
                direccion = Direccion.Arriba;
                posicionAux.Y = limiteInferioe - 2;
            }

            Posicion = posicionAux;
        }

        public void Movimiento(ref Point posicionAux)
        {
            switch (direccion)
            {
                case Direccion.Derecha:
                    posicionAux.X += 1; 
                    break;
                case Direccion.Izquierda:
                    posicionAux.X -= 1;
                    break;
                case Direccion.Arriba:
                    posicionAux.Y += 1;
                    break;
                case Direccion.Abajo:
                    posicionAux.Y -= 1;
                    break;
            }
        }

        public void DireccionAleatoria()
        { 
            if (DateTime.Now > tiempodireccion.AddMilliseconds(tiempodireccionAleatorio)
                &&(direccion==Direccion.Derecha|| direccion==Direccion.Izquierda))
            {
                Random random = new Random();
                int numAleatorio = random.Next(1, 5);
                switch (numAleatorio)
                {
                    case 1:
                        direccion = Direccion.Derecha;
                        break;
                    case 2:
                        direccion = Direccion.Izquierda;
                        break;
                    case 3:
                        direccion = Direccion.Arriba;
                        break;
                    case 4:
                        direccion = Direccion.Abajo;
                        break;
                }
                tiempodireccion = DateTime.Now;
                tiempodireccionAleatorio=random.Next(1000,2000);
            }

            if (DateTime.Now > tiempodireccion.AddMilliseconds(50)
              && (direccion == Direccion.Arriba || direccion == Direccion.Abajo ))
            {
                Random random = new Random();
                int numAleatorio = random.Next(1, 3);
                switch (numAleatorio)
                {
                    case 1:
                        direccion = Direccion.Derecha;
                        break;
                    case 2:
                        direccion = Direccion.Izquierda;
                        break;
        
                }
                tiempodireccion = DateTime.Now;
            }
        }

        public void CrearBalas()
        {
            if (DateTime.Now > tiempoDisparo.AddMilliseconds(tiempoDisparoAleatorio))
            {
                Random random=new Random();

                if (tipoEnemigo == TipoEnemigo.Normal)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 1, Posicion.Y + 2), Color, TipoBala.Enemigo);
                    Balas.Add(bala);
                    tiempoDisparoAleatorio = random.Next(200, 500);
                }
                if (tipoEnemigo == TipoEnemigo.Boss)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 4, Posicion.Y + 2), Color, TipoBala.Enemigo);
                    Balas.Add(bala);
                    tiempoDisparoAleatorio = random.Next(100, 150);

                }

                tiempoDisparo = DateTime.Now;
            }
        }

        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, ventana.LimiteInferior.Y,nave))
                {
                    Balas.Remove(Balas[i]);
                }

            }
        }

        public void Informacion(int distanciax)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(ventana.LimiteSuperior.X+distanciax, ventana.LimiteSuperior.Y - 1);
            Console.Write("Enemigo: "+(int)Vida+"%");
        }
    }
}
