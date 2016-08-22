using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpBlog.Startup))]
namespace CSharpBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
