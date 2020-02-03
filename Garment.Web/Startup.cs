using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Garment.Web.Startup))]
namespace Garment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
