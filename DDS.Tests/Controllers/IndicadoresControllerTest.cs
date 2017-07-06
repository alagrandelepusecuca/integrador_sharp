using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDS.Controllers;
using System.Web;

namespace DDS.Tests.Controllers
{
    [TestClass]
    public class IndicadoresControllerTest
    {
        [TestMethod]
        public void AgregarIndicadoresView()
        {

            // Arrange
            IndicadoresController controller = new IndicadoresController();

            // Act
            ViewResult result = controller.Agregar() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VisualizarIndicadoresView()
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
