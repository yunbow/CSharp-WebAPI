using WebApi.Common;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiService service = new ApiService();
            service.Start();
        }
    }
}
