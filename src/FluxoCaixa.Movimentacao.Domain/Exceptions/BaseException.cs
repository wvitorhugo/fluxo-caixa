﻿using FluxoCaixa.Movimentacao.Domain.Common;
using System;

namespace FluxoCaixa.Movimentacao.Domain.Exceptions
{

    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public BaseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message)
        : base(message)
        {
            StatusCode = statusCode;
        }
    }

}