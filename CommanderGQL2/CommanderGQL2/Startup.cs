using CommanderGQL2.Data;
using CommanderGQL2.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Server.Ui.Voyager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL2.GraphQL.Platforms;
using CommanderGQL2.GraphQL.Commands;

namespace CommanderGQL2
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      //  public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("con")));
            services.AddGraphQLServer().AddQueryType<Query>().
                AddType<PlatformType>().
                AddType<CommandType>().
                AddMutationType<Mutation>().
                AddSubscriptionType<Subscription>().   
                
                AddSorting().
                AddFiltering().
                AddInMemorySubscriptions();
               
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                 
                endpoints.MapGraphQL();
            });
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path="/graphql-voyager"

            });
           
        }
    }
}
