using System.Collections.Generic;

namespace ClockApp
{
    public interface IGateway<TModel, in TPrimaryKey>
        where TModel : Model 
        where TPrimaryKey : IPrimaryKey<TPrimaryKey, TModel>
    {
        TModel Get(TPrimaryKey primaryKey);
        List<TModel> GetAll();
    }

    public interface IOverWriteableGateway<TModel, in TPrimaryKey> : IGateway<TModel, TPrimaryKey>
        where TModel : Model 
        where TPrimaryKey : IPrimaryKey<TPrimaryKey, TModel>
    {
        TModel GetOwner();
        void Save(TModel model);
        void Remove(TModel model);
    }
}