using System;
using System.Collections.Generic;
using RR.Models;

namespace RR.RepositoryContracts
{
    public interface IReviewRepository
    {
        Review GetById(Guid reviewId);
        IEnumerable<Review> Get();
        void Update(Review review);
        void Delete(Review review);
        void Add(Review review);
    }
}
