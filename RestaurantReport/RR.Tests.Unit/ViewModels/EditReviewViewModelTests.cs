using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.ViewModels;

namespace RR.Tests.Unit.ViewModels
{
    [TestClass]
    public class EditReviewViewModelTests
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ToString_OnCall_ReturnsCorrectFormat()
        {
            var viewModel = new EditReviewViewModel
            {
                Comment = "Some Comment",
                Rating = 5.5,
                ReviewerName = "Some Name",
                ReviewPublicId = Guid.Empty
            };

            Approvals.Verify(viewModel);
        }
    }
}
