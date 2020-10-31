// ------------------------------------------------------------------------------------------------
// <copyright file="Mudanzas.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa la lógica de negocio de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.Negocio.Clases
{
    using Microsoft.AspNetCore.Http;
    using MovingInc.Entidades.Modelos;
    using MovingInc.Negocio.Interfaces;
    using MovingInc.Repositorio.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class MudanzasNegocio : IMudanzasNegocio
    {
        private readonly IMudanzasRepositorio mudanzasRepositorio;

        public MudanzasNegocio(IMudanzasRepositorio mudanzasRepositorio)
        {
            this.mudanzasRepositorio = mudanzasRepositorio;
        }

        public string ProcesarArchivo(List<IFormFile> archivos, int documento)
        {
            string resultadoFinal=string.Empty;
            var archivoEntrada = new StringBuilder();

            foreach (var archivo in archivos)
            {
                using (var reader = new StreamReader(archivo.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        archivoEntrada.AppendLine(reader.ReadLine());

                    }

                    resultadoFinal = archivoEntrada.ToString().Replace("\r", "");
                    List<string> listado = resultadoFinal.Split(new[] { "\n" },
                    StringSplitOptions.RemoveEmptyEntries).ToList();
                    ValidarArchivo(listado);
                    resultadoFinal = ProcesarDiasDeTrabajo(listado);

                    GuardarLog(archivoEntrada.ToString().ToString(), resultadoFinal, documento);
                }           
           
            }
            return resultadoFinal;
        }

        public void ValidarArchivo(List<string> lista)
        {           
            if (lista.Any())
            {
                List<int> listadoInicial = lista.Select(x => Convert.ToInt32(x)).ToList();
                if (!(listadoInicial.First()>1 && listadoInicial.First()<500))
                {
                    //Restricción (1 ≤​ T​ ≤ 500 )
                    throw new System.ArgumentException(String.Format("El número de días debe estar entre 1 y 500. Valor actual: {0}", listadoInicial.First()), "T");
                }
                int dias = 0;
                int indice;

                string resultado = string.Empty;

                for (int posicion = 1; posicion < listadoInicial.Count; posicion++)
                {
                    dias++;
                    var numeroObjetos = listadoInicial[posicion];
                    if (numeroObjetos < 1 || numeroObjetos >100)
                    {
                        // Restriccion (1 ≤ ​N​ ≤ 100)
                        throw new System.ArgumentException(String.Format("El número de elementos debe estar entre 1 y 100. Valor actual: {0}", numeroObjetos), "N");
                    }

                    List<int> listaPesoDia = new List<int>();

                    for (indice = posicion + 1; indice <= (posicion + numeroObjetos); indice++)
                    {
                        if (listadoInicial[indice] < 1 || listadoInicial[indice] > 100)
                        {
                            // Restriccion (1 ≤ ​Wi​ ≤ 100)
                            throw new System.ArgumentException(String.Format("El peso del elemento debe estar entre 1 y 100. Valor actual: {0}", listadoInicial[indice]), "Wi");
                        }
                        listaPesoDia.Add(listadoInicial[indice]);
                    }                   

                    posicion = indice - 1;
                }

            }
            else
            {
                throw new System.ArgumentException("El archivo no contiene elementos");
            }
            
        }

        private void GuardarLog(string archivoEntrada, string archivoSalida, int documento)
        {
            //LOG
            LogMudanza log = new LogMudanza();
            log.IdLog = new Guid();
            log.DocumentoUsuario = documento;
            log.EntradaArchivo = archivoEntrada;
            log.SalidaArchivo = archivoSalida;
            log.FechaRegistro = DateTime.Now;

            mudanzasRepositorio.GuardarLog(log);

        }

        private string ProcesarDiasDeTrabajo(List<string> lista)
        {
            List<int> listadoInicial = new List<int>();

            listadoInicial = lista.Select(x => Convert.ToInt32(x)).ToList();
            int dias = 0;
            int indice;

            string resultado = string.Empty;

            for (int posicion = 1; posicion < listadoInicial.Count; posicion++)
            {
                dias++;
                var numeroObjetos = listadoInicial[posicion];
                List<int> listaPesoDia = new List<int>();

                for (indice = posicion + 1; indice <= (posicion + numeroObjetos); indice++)
                {
                    listaPesoDia.Add(listadoInicial[indice]);
                }

                var resultadoxDia = "Case #" + dias + ": " + CalcularNumeroViajes(listaPesoDia);

                resultado = string.Concat(resultado, resultadoxDia, Environment.NewLine);               

                posicion = indice - 1;
            }

            return resultado;
        }

        public static int CalcularNumeroViajes(List<int> elementos)
        {
            var pivote = elementos.Max();
            elementos.Remove(pivote);

            var peso = 0;
            var indice = 1;
            var viajes = 0;

            while (peso < 50 && pivote < 50)
            {
                if (elementos.Count == 0)
                    return 0;

                var menor = elementos.Min();
                elementos.Remove(menor);
                indice++;
                peso = pivote * indice;
            }

            viajes++;

            if (elementos.Count > 0)
                viajes += CalcularNumeroViajes(elementos);

            return viajes;
        }

    }
}
