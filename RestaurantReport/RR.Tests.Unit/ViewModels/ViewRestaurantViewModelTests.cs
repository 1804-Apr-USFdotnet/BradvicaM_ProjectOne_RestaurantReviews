using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.ViewModels;

namespace RR.Tests.Unit.ViewModels
{
    [TestClass]
    public class ViewRestaurantViewModelTests
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ToString_OnCall_ReturnsCorrectInformation()
        {
            var viewModel = new ViewRestaurantViewModel
            {
                AverageRating = 1.0,
                City = "Some City",
                Name = "Some Name",
                PhoneNumber = "1234567890",
                RestaurantPublicId = Guid.Empty,
                State = "SA",
                Street = "123 Some St.",
                Website = "www.testsite.com",
                ZipCode = 12345
            };

            Approvals.Verify(viewModel);
        }
    }
}
