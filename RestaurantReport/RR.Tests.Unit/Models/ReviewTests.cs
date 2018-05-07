using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Models;

namespace RR.Tests.Unit.Models
{
    [TestClass]
    public class ReviewTests
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void ToString_OnCall_DisplayProperly()
        {
            var review = new Review
            {
                Comment = "It was kinda meh.",
                Rating = 4.45,
                ReviewPublicId = Guid.Empty
            };

            Approvals.Verify(review);
        }
    }
}
