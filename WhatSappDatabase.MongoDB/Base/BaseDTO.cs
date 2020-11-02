using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatSappDatabase.MongoDB.Base
{
    public abstract class BaseDTO<T, K, E> : InnerBaseDTO<T> where T : class where K : InnerBaseDTO<T>
    {
        public BaseDTO()
        {

        }

        public BaseDTO(T model) : base(model)
        {
        }

        public abstract T Modelo(IMongoCollection<E> sessao);
    }
}
