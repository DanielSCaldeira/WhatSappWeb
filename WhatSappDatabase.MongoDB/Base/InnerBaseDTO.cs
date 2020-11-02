using System;
using System.Collections.Generic;
using System.Text;

namespace WhatSappDatabase.MongoDB.Base
{
    public abstract class InnerBaseDTO<T> where T : class
    {
        public InnerBaseDTO()
        {

        }

        public InnerBaseDTO(T model)
        {

        }
    }
}
