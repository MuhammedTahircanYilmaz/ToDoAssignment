﻿namespace Core.Entities;

public class ReturnModel<TData>
{

    public TData Data { get; set; }

    public bool Success { get; set; }

    public string  Message { get; set; }

    public int Status { get; set; }


    public static ReturnModel<TData> ReturnModelOfException(Exception exception, int status) // The Exception is any exception thrown either by the program itself, or the programmer. The Status is the code for the Http Status
    {
        return new ReturnModel<TData>
        {
            Data = default,
            Message = exception.Message,
            Success = false,
            Status = status
        };
    }

    public static ReturnModel<TData> ReturnModelOfSuccess(TData data, int status, string? message = null)
    {
        return new ReturnModel<TData>
        {
            Data = data,
            Message = message,
            Status = status,
            Success = true
        };
    }

}
