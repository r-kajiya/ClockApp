using System.Collections.Generic;

namespace ClockApp
{
    public interface IDataStore<TModel, TPrimaryKey> 
        where TModel : IModel
        where TPrimaryKey : IPrimaryKey<TPrimaryKey, TModel>
    {
        Dictionary<TPrimaryKey, TModel> Load();
        void Save(TModel model);
        void Remove(TModel model);
    }
}