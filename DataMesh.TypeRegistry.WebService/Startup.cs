using DataMesh.Composites.MongoDb;
using DataMesh.Composites.MongoDb.Configuration;
using DataMesh.Composites.MongoDb.Implementations;
using DataMesh.TypeDefinitions;
using DataMesh.TypeRegistry.WebService.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace DataMesh.TypeRegistry.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Type Registry API",
                    Version = "1" // not actually the version, TODO: put in a correct version number.
                });

            });

            //TODO: make this better, needs to be moved.
            services.Configure<MongoStoreDatabaseSettings>(
                Configuration.GetSection("MongoStoreDatabaseSettings"));
            services.AddTransient(typeof(ISimpleMongoStore<>), typeof(SimpleMongoStore<>));

            // TODO: Also, we should not use the json file to store this. This is a POC, but really this needs to be environmental.
            services.AddSingleton<IMongoCollection<MongoSerializableTypeDefinition>>(sp
                => TypeRegistryConfiguration.CreateTypeDefinitionStoreClient(
                    sp.GetRequiredService<IOptions<MongoStoreDatabaseSettings>>().Value));

            services.AddTransient<IMongoTypeDefinitionFactory, MongoTypeDefinitionFactory>();
            services.AddTransient<ITypeRegistry, MongoDbTypeRegistry>();

            services.AddTransient<ITypeDefinitionFactory, TypeDefinitionFactory>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Type Registry API POC");
            });
        }
    }
}
