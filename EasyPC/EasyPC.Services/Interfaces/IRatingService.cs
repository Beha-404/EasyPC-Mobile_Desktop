using EasyPC.Model;
using EasyPC.Model.Requests.RatingRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IRatingService
{
    public List<Rating> GetAll(RatingSearchObject searchObject);
    public Rating? Insert(RatingInsertRequest insert);
    public Rating? Update(int id, RatingUpdateRequest updateRequest);
    public bool Delete(int id);
    public Rating? GetById(int id);
}
