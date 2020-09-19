using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Judoca.ModelsEF;

namespace Judoca.Services
{
    public static class Extension
    {

        public static void AddContexts(this IServiceCollection services){
            services.AddTransient<ITesteFacade, Teste>();
            services.AddEntityFrameworkSqlServer().AddDbContext<JudocaContext>();
        }
    }
}