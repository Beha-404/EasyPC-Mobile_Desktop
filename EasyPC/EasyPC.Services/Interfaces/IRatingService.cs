using EasyPC.Model.Requests.RatingRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces
{
    public interface IRatingService
    {
        public List<Model.Rating> GetAll(RatingSearchObject searchObject);
        public Model.Rating? Insert(RatingInsertRequest insert);
        public Model.Rating? Update(int id, RatingUpdateRequest updateRequest);
        public bool Delete(int id);
        public Model.Rating? GetById(int id);
    }
}
