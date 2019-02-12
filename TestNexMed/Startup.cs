using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestNexMed.Startup))]
namespace TestNexMed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
