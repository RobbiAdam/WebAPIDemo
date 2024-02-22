﻿
namespace WebApp.Data
{
    public interface IWebApiExecute
    {
        Task<T?> InvokeGet<T>(string relativeUrl);
        Task<T?> InvokePost<T>(string relativeUrl, T obj);
        Task InvokePut<T>(string relativeUrl, T obj);
        Task InvokeDelete(string relativeUrl);
    }
}