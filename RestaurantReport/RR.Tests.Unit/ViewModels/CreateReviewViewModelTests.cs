using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.ViewModels;

namespace RR.Tests.Unit.ViewModels
{
    [TestClass]
    public class CreateReviewViewModelTests
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ToString_OnCall_ReturnsCorrectInformation()
        {
            var viewModel = new CreateReviewViewModel
            {
                Comment = "Some Comment",
                Rating = 1.0,
                ReviewerName = "Some Reviewer",
                RestaurantPublicId = Guid.Empty
            };

            Approvals.Verify(viewModel);
        }
    }
}
