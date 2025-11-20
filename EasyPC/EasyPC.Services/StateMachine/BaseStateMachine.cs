using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine
{
    public abstract class BaseStateMachine<TModel, TInsert, TUpdate, TEntity> : IBaseStateMachine<TModel, TInsert, TUpdate, TEntity> where TEntity : class
    {
        protected DatabaseContext _context;
        protected IMapper _mapper;
        protected IServiceProvider _serviceProvider;
        public BaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public virtual TModel? Insert(TInsert insert)
        {
            throw new NotImplementedException();
        }

        public virtual TModel? Update(int id, TUpdate update)
        {
            throw new NotImplementedException();
        }

        public virtual TModel? Edit(int id)
        {
            throw new NotImplementedException();
        }

        public virtual TModel? Hide(int id)
        {
            throw new NotImplementedException();
        }

        public virtual TModel? Activate(int id)
        {
            throw new NotImplementedException();
        }

        public virtual List<string> AllowedActions()
        {
            throw new NotImplementedException();
        }

        public virtual IBaseStateMachine<TModel, TInsert, TUpdate, TEntity>? NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetService<InitialStateMachine<TModel, TInsert, TUpdate, TEntity>>(),
                "draft" => _serviceProvider.GetService<DraftMachineState<TModel, TInsert, TUpdate, TEntity>>(),
                "active" => _serviceProvider.GetService<ActiveMachineState<TModel, TInsert, TUpdate, TEntity>>(),
                "hidden" => _serviceProvider.GetService<HiddenMachineState<TModel, TInsert, TUpdate, TEntity>>(),
                _ => throw new NotImplementedException(),
            };
        }

        public class StateNames
        {
            public const string Draft = "draft";
            public const string Active = "active";
            public const string Hidden = "hidden";
        }
    }
}

