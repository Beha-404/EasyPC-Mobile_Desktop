using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine
{
    public abstract class ActiveMachineState<TModel, TInsert, TUpdate, TEntity> : BaseStateMachine<TModel, TInsert, TUpdate, TEntity> where TEntity : class
    {
        public ActiveMachineState(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override TModel? Hide(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity == null)
                return default;
            entity.GetType().GetProperty("StateMachine")?.SetValue(entity, StateNames.Hidden);
            _context.SaveChanges();
            return _mapper.Map<TModel>(entity);
        }

        public override List<string> AllowedActions()
        {
            return new List<string>() { nameof(Hide) };
        }

    }
}
