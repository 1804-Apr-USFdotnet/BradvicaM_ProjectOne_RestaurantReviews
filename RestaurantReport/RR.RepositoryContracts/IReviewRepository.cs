using System;
using System.Collections.Generic;
using RR.Models;

namespace RR.RepositoryContracts
{
    public interface IReviewRepository
    {
        Review GetById(Guid reviewId);
        IEnumerable<Review> Get();
        void Add(Review review);
        void Update(Review review);
        void Delete(Review review);
    }
}
