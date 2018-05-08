using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.ViewModels;

namespace RR.Tests.Unit.ViewModels
{
    [TestClass]
    public class EditRestaurantViewModelTests
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ToString_OnCall_ReturnsCorrectInformation()
        {
            var viewModel = new EditRestaurantViewModel
            {
                Name = "Some Name",
                City = "Some City",
                PhoneNumber = "1234567890",
                RestaurantPublicId = Guid.Empty,
                State = "CA",
                Street = "123 Whatever Dt.",
                Website = "www.blah.com",
                ZipCode = 12345
            };

            Approvals.Verify(viewModel);
        }
    }
}
