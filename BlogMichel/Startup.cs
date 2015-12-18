using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogMichel.Startup))]
namespace BlogMichel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
