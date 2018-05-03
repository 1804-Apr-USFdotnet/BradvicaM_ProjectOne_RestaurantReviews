using System;
using System.Collections.Generic;
using System.Linq;
using RR.Models;
using RR.RepositoryContracts;

namespace RR.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IContext _context;

        public ReviewRepository(IContext context)
        {
            _context = context;
        }

        public Review GetById(Guid reviewId)
        {
            return _context.Reviews.First(x => x.ReviewId == reviewId);
        }

        public IEnumerable<Review> Get()
        {
            return _context.Reviews;
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void Update(Review review)
        {
            var entity = _context.Reviews.Find(review.Id);
            _context.Entry(entity).CurrentValues.SetValues(review);
            _context.SaveChanges();
        }

        public void Delete(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
