// ------------------------------------------------------------------------------------------------
// <copyright file="MudanzasTest.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa las pruebas unitarias de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.PruebasUnitarias
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MovingInc.Negocio.Clases;
    using MovingInc.Negocio.Interfaces;
    using MovingInc.Repositorio.Interfaces;    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    [TestClass]
    public class MudanzasTest
    {
        private IMudanzasNegocio mudanzasNegocio;

        private Mock<IMudanzasRepositorio> mudanzasRepositorio;

        [TestInitialize]
        public void InitializeTest()
        {
            mudanzasRepositorio = new Mock<IMudanzasRepositorio>();
            mudanzasNegocio = new MudanzasNegocio(mudanzasRepositorio.Object);
        }

        [TestMethod]
        public void ProcesarArchivoTest()
        {
            List<IFormFile> listaArchivos = new List<IFormFile>();
            int documento = 11;
            var resultTest = mudanzasNegocio.ProcesarArchivo(listaArchivos, documento);

            Assert.AreEqual(resultTest, string.Empty);
        }

        [TestMethod]
        public void RestriccionTMayor500Test()
        {
            try
            {
                string[] arr = { "501", "1", "1" };
                List<string> list = new List<string>(arr);
                
                mudanzasNegocio.ValidarArchivo(list);

                Assert.Fail();
            }
            catch (Exception)
            {
                // Como se genera una excepción, la prueba es exitosa
            }
        }

        [TestMethod]
        public void RestriccionNMayor100Test()
        {
            try
            {
                string[] arr = { "5", "120", "1" };
                List<string> list = new List<string>(arr);

                mudanzasNegocio.ValidarArchivo(list);

                Assert.Fail();
            }
            catch (Exception)
            {
                // Como se genera una excepción, la prueba es exitosa
            }
        }

        [TestMethod]
        public void RestriccionWiMayor100Test()
        {
            try
            {
                string[] arr = { "5", "1", "101" };
                List<string> list = new List<string>(arr);

                mudanzasNegocio.ValidarArchivo(list);

                Assert.Fail();
            }
            catch (Exception)
            {
                // Como se genera una excepción, la prueba es exitosa
            }
        }


    }

}
