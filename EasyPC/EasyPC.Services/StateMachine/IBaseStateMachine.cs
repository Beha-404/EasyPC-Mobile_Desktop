namespace EasyPC.Services.StateMachine
{
    public interface IBaseStateMachine<TModel,TInsert,TUpdate,TEntity> where TEntity : class
    {
        public TModel? Insert(TInsert insert);
        public TModel? Update(int id,TUpdate update);
        public TModel? Edit(int id);
        public TModel? Hide(int id);
        public TModel? Activate(int id);
        public List<string> AllowedActions();
        public IBaseStateMachine<TModel, TInsert, TUpdate, TEntity> NextState(string state);
    }
}
