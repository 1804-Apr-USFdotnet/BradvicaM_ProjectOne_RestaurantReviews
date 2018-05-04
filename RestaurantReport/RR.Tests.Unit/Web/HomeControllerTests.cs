using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.ViewModels;
using RR.Web.Controllers;

namespace RR.Tests.Unit.Web
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexAction_OnCall_ReturnsCorrectView()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void AboutAction_OnCall_ReturnsCorrectView()
        {
            var controller = new HomeController();

            var result = controller.About() as ViewResult;

            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void ContactAction_OnGet_ReturnsCorrectView()
        {
            var controller = new HomeController();

            var result = controller.Contact() as ViewResult;

            Assert.AreEqual("Contact", result.ViewName);
        }

        [TestMethod]
        public void Contact_OnPostWithValidViewModel_ReturnsCorrectView()
        {
            var controller = new HomeController();

            var viewModel = new CreateContactViewModel
            {
                Name = "mike",
                Email = "mike@mike.com",
                Message = "okish",
                Phone = 1234569871
            };

            var result = controller.Contact(viewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Contact_OnPostWithInvalidViewModel_ReturnsToView()
        {
            var controller = new HomeController();

            controller.ModelState.AddModelError("Something bad happened", "Error");

            var result = controller.Contact(new CreateContactViewModel()) as ViewResult;

            Assert.AreEqual("Contact", result.ViewName);
        }

        [TestMethod]
        public void Contact_OnPostModelErrorViewModel_ReturnsCorrectViewModel()
        {
            var controller = new HomeController();

            controller.ModelState.AddModelError("Bad", "Error");

            var result = controller.Contact(new CreateContactViewModel()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CreateContactViewModel));
        }
    }
}
