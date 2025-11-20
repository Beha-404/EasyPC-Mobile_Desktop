using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IBaseService<TModel,TSearch,TInsert,TUpdate>
    {
        public PagedResult<TModel> GetAll(TSearch search);
        public TModel? GetById(int id);
        public TModel? Insert(TInsert insertRequest);
        public TModel? Update(int id, TUpdate updateRequest);
        public TModel? Hide(int id);
        public TModel? Edit(int id);
        public TModel? Activate(int id);
        public List<string> AllowedActions(int id);

    }
}
