using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Caristaniyaan.Startup))]
namespace Caristaniyaan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
