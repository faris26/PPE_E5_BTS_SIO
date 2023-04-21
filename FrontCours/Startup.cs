using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrontCours.Startup))]
namespace FrontCours
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
