using DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor
{
    public static class DependencyInjectionRegistrations
    {
        public static void RegisterDemoItemEditorService(this IServiceCollection services)
        {
            services.AddTransient<IItemEditorStore, MongoItemEditorStore>();
            services.AddTransient<IItemWebEditorDataMeshAdapter, ItemWebEditorDataMeshAdapter>();
            
            services.AddSingleton<IMongoCollection<EditorItem>>(sp
                => CreateItemMongoStore(
                    sp.GetRequiredService<IOptions<ItemEditorMongoStoreSettings>>().Value));


        }

        public static IMongoCollection<EditorItem> CreateItemMongoStore(ItemEditorMongoStoreSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            var collection = database.GetCollection<EditorItem>(settings.CollectionName);
            return collection;
        }
    }
}