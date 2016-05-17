using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoLWay.Startup))]
namespace LoLWay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
