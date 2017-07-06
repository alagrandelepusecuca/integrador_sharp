using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDS.Controllers;
using System.Web;

namespace DDS.Tests.Controllers
{
    [TestClass]
    public class MetodologiaControllerTest
    {
        [TestMethod]
        public void AgregarMetodologiaView()
        {

            // Arrange
            MetodologíasController controller = new MetodologíasController();

            // Act
            ViewResult result = controller.Agregar() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VisualizarMetodologiaView()
        {

            // Arrange
            IndicadoresController controller = new IndicadoresController();

            // Act
            ViewResult result = controller.Visualizar() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
