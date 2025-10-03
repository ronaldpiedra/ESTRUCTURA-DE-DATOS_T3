using System;

namespace FigurasGeometricas
{
    // La clase Circulo representa un círculo con su radio como atributo privado.
    class Circulo
    {
        // Atributo privado: radio del círculo
        private double radio;

        // Constructor de la clase Circulo
        public Circulo(double radio)
        {
            this.radio = radio;
        }

        // Método para calcular el área del círculo.
        // Usa la fórmula: PI * radio^2
        public double CalcularArea()
        {
            return Math.PI * Math.Pow(radio, 2);
        }

        // Método para calcular el perímetro (circunferencia).
        // Fórmula: 2 * PI * radio
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }
    }

    // La clase Rectangulo representa un rectángulo con base y altura como atributos privados.
    class Rectangulo
    {
        private double baseRectangulo;
        private double altura;

        // Constructor de la clase Rectangulo
        public Rectangulo(double baseRectangulo, double altura)
        {
            this.baseRectangulo = baseRectangulo;
            this.altura = altura;
        }

        // Método para calcular el área del rectángulo.
        // Fórmula: base * altura
        public double CalcularArea()
        {
            return baseRectangulo * altura;
        }

        // Método para calcular el perímetro.
        // Fórmula: 2 * (base + altura)
        public double CalcularPerimetro()
        {
            return 2 * (baseRectangulo + altura);
        }
    }

    // Clase principal que prueba las figuras
    class ProgramaPrincipal
    {
        static void Main(string[] args)
        {
            // Crear instancia de un círculo de radio 5
            Circulo miCirculo = new Circulo(5);
            Console.WriteLine("Área del Círculo: " + miCirculo.CalcularArea());
            Console.WriteLine("Perímetro del Círculo: " + miCirculo.CalcularPerimetro());

            // Crear instancia de un rectángulo de base 8 y altura 3
            Rectangulo miRectangulo = new Rectangulo(8, 3);
            Console.WriteLine("Área del Rectángulo: " + miRectangulo.CalcularArea());
            Console.WriteLine("Perímetro del Rectángulo: " + miRectangulo.CalcularPerimetro());

            // Esperar entrada para que no se cierre la consola de inmediato
            Console.ReadKey();
        }
    }
}
