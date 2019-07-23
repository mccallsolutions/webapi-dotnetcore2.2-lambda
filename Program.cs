using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
// Even though these using directives are highlighted as not used, they must be here to compile the project using lambda tools.
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        private static readonly LambdaEntryPoint LambdaEntryPoint = new LambdaEntryPoint();
        private static readonly Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>> Func = LambdaEntryPoint.FunctionHandlerAsync;

        public static async Task Main(string[] args)
        {
#if DEBUG
            // This will run only when in debug on the local machine.
            await Task.Run(() =>
            {
                CreateWebHostBuilder(args).Build().Run();
            });
#else
            // AWS will run this code and use the bootstrap wrapper so we can run .net core 2.2 functions.
            using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(Func, new JsonSerializer()))
            {
                using (var bootstrap = new LambdaBootstrap(handlerWrapper))
                {
                    await bootstrap.RunAsync();
                }
            }
#endif
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
