using AutoMapper;
using NHibernate.Mapping.ByCode.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public static class AutoMapperExtentions
    {
        private static IMapper _mapper;

        public static void Initialize(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)_mapper.Map(self, self.GetType(), typeof(TResult));
        }

        public static TResult DynamicMapTo<TResult>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)_mapper.Map(self, self.GetType(), typeof(TResult));
        }

        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (List<TResult>)_mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }

        public static TResult MapPropertiesToInstance<TResult>(this object self, TResult value)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)_mapper.Map(self, value, self.GetType(), typeof(TResult));
        }
    }
}
