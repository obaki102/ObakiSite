using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Shared
{
    public class Result<TValue> : Result
    {   
        private readonly TValue? _value;
        protected internal Result(TValue? value, bool isSuccess, string error) : base(isSuccess, error) => _value = value;

        public TValue? Value => IsSuccess
            ? _value! : throw new InvalidOperationException("Value of failure result is inaccessible");

        public static implicit operator Result<TValue>(TValue value) => new(value,true,string.Empty);
       
    }
}
