using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOAN3.Startup))]
namespace DOAN3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
