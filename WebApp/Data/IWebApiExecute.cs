
namespace WebApp.Data
{
    public interface IWebApiExecute
    {
        Task<T?> InvokeGet<T>(string relativeUrl);
    }
}