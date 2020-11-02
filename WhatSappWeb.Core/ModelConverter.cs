using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Core
{
    public static class ModelConverter
    {
        public static K ToDTO<T, K, E>(this T registro) where K : BaseDTO<T, K, E> where T : class
        {
            try
            {
                if (registro == null)
                {
                    return null;
                }
                var c = typeof(K).GetConstructor(new Type[] { typeof(T) });
                K retorno = c.Invoke(new object[] { registro }) as K;

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T ToModel<T, K, E>(this K registro, IMongoCollection<E> sessao) where K : BaseDTO<T, K, E> where T : class
        {
            try
            {
                if (registro == null)
                {
                    return null;
                }
                T retorno = registro.Modelo(sessao);
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
