using EasyPC.Model;
using EasyPC.Model.Requests.UserRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using Mapster;
using MapsterMapper;

namespace EasyPC.Services
{
    public class UserService : IUserService
    {
        protected DatabaseContext _context;
        protected IMapper _mapper;
        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Model.User? Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            user.IsDeleted = true;
            _context.SaveChanges();
            return _mapper.Map<Model.User>(user);
        }

        public List<Model.User>? Get(UserSearchObject? userSearchObject)
        {
            var query = _context.Users.AsQueryable();
            if (userSearchObject == null)
            {
                return _mapper.Map<List<Model.User>>(query.ToList());
            }

            query = ApplyFilter(query, userSearchObject);
            if (userSearchObject.Page.HasValue && userSearchObject.PageSize.HasValue)
            {
                var skip = (userSearchObject.Page.Value - 1) * userSearchObject.PageSize.Value;
                var take = userSearchObject.PageSize.Value;
                query = query.Skip(skip).Take(take);
            }
            return _mapper.Map<List<Model.User>>(query.ToList());

        }

        public Model.User? GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            return _mapper.Map<Model.User>(user);
        }

        public Model.User? Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                return null;
            return _mapper.Map<Model.User>(user);
        }

        public Model.User? Register(string username, string email, string password)
        {
            if(username == null || email == null || password == null)
                return null;
            var newUser = new Database.User
            {
                Username = username,
                Email = email,
                Password = password,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return _mapper.Map<Model.User>(newUser);
        }

        public Model.User? Restore(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            user.IsDeleted = false;
            _context.SaveChanges();
            return _mapper.Map<Model.User>(user);
        }

        public Model.User? Update(int id, UserUpdateRequest updateRequest)
        {
            var entity = _context.Users.FirstOrDefault(u => u.Id == id);
            if (entity == null)
                return null;

            var cfg = new TypeAdapterConfig();
            cfg.ForType<UserUpdateRequest, Database.User>()
                .IgnoreNullValues(true)
                .IgnoreIf(
                    (src, dest) => src.profilePicture == null || src.profilePicture.Length == 0,
                    dest => dest.profilePicture!
                );

            updateRequest.Adapt(entity, cfg);
            _context.SaveChanges();
            return _mapper.Map<Model.User>(entity);
        }

        public Model.User? UpdateRole(UpdateRoleRequest updateRoleRequest)
        {
            var entity = _context.Users.FirstOrDefault(u => u.Id == updateRoleRequest.UserId);
            if (entity == null)
                return null;

            entity.Role = (Database.UserRole)updateRoleRequest.NewRole;
            _context.SaveChanges();
            return _mapper.Map<Model.User>(entity);
        }

        protected IQueryable<Database.User> ApplyFilter(IQueryable<Database.User> query, UserSearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Username))
            {
                query = query.Where(u => u.Username!.Contains(searchObject.Username));
            }
            if (!string.IsNullOrEmpty(searchObject.Email))
            {
                query = query.Where(u => u.Email!.Contains(searchObject.Email));
            }
            if (!string.IsNullOrEmpty(searchObject.FirstName))
            {
                query = query.Where(u => u.FirstName!.Contains(searchObject.FirstName));
            }
            if (!string.IsNullOrEmpty(searchObject.LastName))
            {
                query = query.Where(u => u.LastName!.Contains(searchObject.LastName));
            }
            return query;
        }
    }
}
