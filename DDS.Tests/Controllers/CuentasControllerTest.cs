using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDS.Controllers;
using System.Web;

namespace DDS.Tests.Controllers
{
    [TestClass]
    public class CuentasControllerTest
    {
        [TestMethod]
        public void ImportarCuentasView()
        {

            // Arrange
            CuentasController controller = new CuentasController();

            // Act
            ViewResult result = controller.Importar() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VisualizarCuentasView()
        {

            // Arrange
            CuentasController controller = new CuentasController();

            // Act
            ViewResult result = controller.Visualizar() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
