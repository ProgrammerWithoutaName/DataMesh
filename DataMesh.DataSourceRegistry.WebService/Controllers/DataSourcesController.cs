using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataMesh.DataSourceRegistry.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSourcesController : ControllerBase
    {
        private readonly ILogger<DataSourcesController> Logger;
        private readonly IDataSourceRegistry Registry;

        public DataSourcesController(ILogger<DataSourcesController> logger, IDataSourceRegistry registry)
        {
            Logger = logger;
            Registry = registry;
        }

        [HttpGet()]
        public async Task<IEnumerable<DataSource>> Get()
        {
            var sources = await Registry.GetAllSources();
            return sources.Select(CreateResponse);
        }

        [HttpGet("{sourceKey}")]
        public async Task<DataSource> Get([FromRoute]string sourceKey)
        {
            var source = await Registry.GetSource(sourceKey);
            return CreateResponse(source);
        }

        //TODO: We need to add some form of service layer that validates the types provided by the data source to the Type Registries Type
        [HttpPost("Register")]
        public Task Register([FromBody] DataSource source)
            => Registry.RegisterSource(source);

        public DataSource CreateResponse(IDataSource source)
            => new DataSource()
            {
                SourceKey = source.SourceKey,
                TypeDefinitionKey = source.TypeDefinitionKey,
                PartialDataProvider = source.PartialDataProvider,
                HealthCheck = source.HealthCheck,
                TypeDefinition = source.TypeDefinition,
                Retrieve = source.Retrieve,
                RelinquishOwnership = source.RelinquishOwnership
            };
    }
}
