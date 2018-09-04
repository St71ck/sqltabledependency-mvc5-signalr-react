using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(SignalR.Startup))]
namespace SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }
}