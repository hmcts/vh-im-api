﻿using System.Threading.Tasks;

namespace InstantMessagingAPI.DAL.Queries.Core
{
    public interface IQueryHandler
    {
        Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery:IQuery where TResult: class;
    }

    public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery where TResult : class 
    {
        Task<TResult> Handle(TQuery query);
    }
}