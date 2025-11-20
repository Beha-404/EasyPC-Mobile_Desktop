using EasyPC.Model.Requests.RatingRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace EasyPC.Services
{
    public class RatingService : IRatingService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RatingService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Model.Rating? Insert(RatingInsertRequest insert)
        {
            if (insert == null)
            {
                throw new ArgumentException("Insert model is null");
            }

            var existing = _context.Ratings
                .FirstOrDefault(r => r.UserId == insert.UserId && r.PcId == insert.PcId);

            if (existing != null)
            {
                // If rating already exists, update it instead
                existing.RatingValue = insert.RatingValue;
                _context.SaveChanges();
                CalculateRating(insert.PcId);
                return _mapper.Map<Model.Rating>(existing);
            }

            var entity = _mapper.Map<Database.Rating>(insert);

            _context.Ratings.Add(entity);
            _context.SaveChanges();

            CalculateRating(insert.PcId);

            return _mapper.Map<Model.Rating>(entity);
        }

        public Model.Rating? Update(int id, RatingUpdateRequest updateRequest)
        {
            var entity = _context.Ratings.Find(id);
            if (entity == null)
            {
                throw new Exception("Rating not found");
            }

            var pcId = entity.PcId;

            entity.RatingValue = updateRequest.RatingValue;
            _context.SaveChanges();
            CalculateRating(pcId);

            return _mapper.Map<Model.Rating>(entity);
        }

        public Model.Rating? GetById(int id)
        {
            var entity = _context.Ratings
                .Include(r => r.User)
                .Include(r => r.PC)
                .FirstOrDefault(r => r.Id == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<Model.Rating>(entity);
        }

        public List<Model.Rating> GetAll(RatingSearchObject searchObject)
        {
            var query = _context.Ratings
                .Include(r => r.User)
                .Include(r => r.PC)
                .AsQueryable();

                if(searchObject.PcId.HasValue)
                {
                    query = query.Where(r => r.PcId == searchObject.PcId.Value);
                }
                if (searchObject.UserId.HasValue)
                {
                    query = query.Where(r => r.UserId == searchObject.UserId.Value);
                }
                if (searchObject.RatingValue.HasValue)
                {
                    query = query.Where(r => r.RatingValue == searchObject.RatingValue.Value);
                }

            return _mapper.Map<List<Model.Rating>>(query.ToList());
        }

        public bool Delete(int id)
        {
            var entity = _context.Ratings.Find(id);
            if (entity == null)
            {
                return false;
            }

            var pcId = entity.PcId;
            _context.Ratings.Remove(entity);
            _context.SaveChanges();

            CalculateRating(pcId);

            return true;
        }

        private void CalculateRating(int pcId)
        {
            var entityRatings = _context.Ratings
                .Where(r => r.PcId == pcId)
                .ToList();

            var pc = _context.PCs.Find(pcId);
            if (pc == null)
            {
                return;
            }

            if (entityRatings.Any())
            {
                var averageRating = entityRatings.Average(r => r.RatingValue);
                pc.AverageRating = (int)Math.Round(averageRating);
                pc.RatingCount = entityRatings.Count;
            }
            else
            {
                pc.AverageRating = null;
                pc.RatingCount = 0;
            }

            _context.SaveChanges();
        }
    }
}