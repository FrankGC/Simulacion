using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Clases
{
    class PruebaFrecuencia
    {
        List<NumeroAleatorio> listaAleatorios;
        List<Rango> rangos;
        int nRangos;
        public PruebaFrecuencia(List<NumeroAleatorio> NumerosAleatorios,int NumeroDeRangos)
        {
            listaAleatorios = NumerosAleatorios;
            nRangos = NumeroDeRangos;
        }


        public List<Rango> CalcularRangos()
        {
            try
            {
                rangos = new List<Rango>();
                double intervalo = Math.Round((double)1 / nRangos, 2);
                double incremento = intervalo;
                int FrecuenciaE = listaAleatorios.Count / nRangos;
                rangos.Add(new Rango { posicion = 1, LimiteInferior = 0, LimiteSuperior = intervalo, FrecuenciaEsperada = FrecuenciaE });
                for (int i = 1; i < nRangos; i++)
                {
                    if (i == nRangos - 1)
                        rangos.Add(new Rango { posicion = i + 1, LimiteInferior = intervalo, LimiteSuperior = Math.Round(intervalo + incremento), FrecuenciaEsperada = FrecuenciaE });
                    else
                    {
                        rangos.Add(new Rango { posicion = i + 1, LimiteInferior = intervalo, LimiteSuperior = intervalo + incremento, FrecuenciaEsperada = FrecuenciaE });
                        intervalo += incremento;
                    }
                }
                CalcularFrecuenciaObtenida();
            }
            catch { }
           
            return rangos;          
        }

        private void CalcularFrecuenciaObtenida()
        {
            int NumerosEnRango = 0;
            foreach (Rango rango in rangos)
            {
                NumerosEnRango = 0;
                foreach (NumeroAleatorio Aleatorio in listaAleatorios)
                {
                    if (double.Parse(Aleatorio.numero) < rango.LimiteSuperior && double.Parse(Aleatorio.numero) > rango.LimiteInferior)
                        NumerosEnRango++;
                }
                if (NumerosEnRango != 0)
                    rango.FrecuenciaObtenida = NumerosEnRango;
            }
        }


    }
}
