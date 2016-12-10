﻿namespace MasssDeffect.Data.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Security.Cryptography.X509Certificates;

    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Find(int id);

        TEntity First(Expression<Func<TEntity, bool>> expression);

        TEntity First();

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);

        int Count();

        int Count(Expression<Func<TEntity, bool>> expression);
    }
}
