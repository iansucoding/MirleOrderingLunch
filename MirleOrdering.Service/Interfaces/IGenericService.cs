using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;

namespace MirleOrdering.Service.Interfaces
{
    public interface IGenericService<TEntity, TView, TBase> where TView : TBase
    {
        TView GetById(long id);
        IEnumerable<TView> GetAll();
        IEnumerable<TView> Find(Func<TEntity, bool> predicate);
        ReturnViewModel Create(TBase model);
        ReturnViewModel Update(TView model);
        ReturnViewModel Delete(long id);
    }
}
