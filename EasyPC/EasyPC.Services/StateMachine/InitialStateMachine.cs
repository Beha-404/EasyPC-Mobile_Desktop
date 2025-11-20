using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine
{
    public abstract class InitialStateMachine<TModel, TInsert, TUpdate, TEntity> : BaseStateMachine<TModel, TInsert, TUpdate, TEntity> where TEntity : class
    {
        public InitialStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override TModel? Insert(TInsert insert)
        {
            var entity = _mapper.Map<TEntity>(insert!);
            _context.Set<TEntity>().Add(entity);
            entity.GetType().GetProperty("StateMachine")?.SetValue(entity, StateNames.Draft);
            _context.SaveChanges();
            return _mapper.Map<TModel>(entity);
        }

        public override List<string> AllowedActions()
        {
            return new List<string>() { nameof(Insert) };
        }
    }
}
