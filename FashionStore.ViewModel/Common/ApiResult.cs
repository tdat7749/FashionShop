﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ApiResult()
        {

        }
    }
}
