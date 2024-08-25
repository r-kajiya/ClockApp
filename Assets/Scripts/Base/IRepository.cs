using System;
using System.Collections.Generic;
using System.Linq;

namespace ClockApp
{
    public interface IRepository<TModel, in TPrimaryKey>
        where TModel : Model 
        where TPrimaryKey : IPrimaryKey<TPrimaryKey, TModel>
    {
        void Load();
        TModel Get(TPrimaryKey primaryKey);
        TModel GetOwner();
        List<TModel> GetAll();
        void Save(TModel model);
        void Remove(TModel model);
    }
    
    public class Repository<TModel, TDataStore, TPrimaryKey> : IRepository<TModel, TPrimaryKey>
        where TModel : Model
        where TDataStore : IDataStore<TModel, TPrimaryKey>
        where TPrimaryKey : IPrimaryKey<TPrimaryKey, TModel>
    {
        Dictionary<TPrimaryKey, TModel> _map;
        List<TModel> _list;
        TDataStore _dataStore;
        bool _isDirty;
        
        protected TDataStore DataStore => _dataStore;

        public Repository(TDataStore dataStore)
        {
            _dataStore = dataStore;
            Load();
        }

        public void Load()
        {
            _map = _dataStore.Load();
            _list = new List<TModel>(_map.Values);
        }

        public void Save(TModel model)
        {
            _isDirty = true;
            _dataStore.Save(model);
        }
        
        public void Remove(TModel model)
        {
            _isDirty = true;
            _dataStore.Remove(model);
        }

        public TModel Get(TPrimaryKey primaryKey)
        {
            ReLoad();

            if (Contains(primaryKey))
            {
                return _map[primaryKey];
            }

            return default;
        }
        
        public TModel GetOwner()
        {
            ReLoad();

            if (_map.Count == 0)
            {
                return null;
            }

            return _map.First().Value;
        }

        public List<TModel> GetAll()
        {
            ReLoad();

            return _list;
        }
        
        void ReLoad()
        {
            if (_isDirty)
            {
                _isDirty = false;
                Load();
            }
        }

        bool Contains(TPrimaryKey primaryKey)
        {
            return _map.ContainsKey(primaryKey);
        }
    }
}
