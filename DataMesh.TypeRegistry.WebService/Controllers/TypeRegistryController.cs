using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;
using DataMesh.TypeRegistry.WebService.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace DataMesh.TypeRegistry.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeRegistryController : ControllerBase
    {
        // TODO: HOOK AUTHENTICATION INTO THIS?
        private readonly ITypeRegistry Registry;
        private readonly ITypeDefinitionFactory DefinitionFactory;

        public TypeRegistryController(ITypeRegistry registry, ITypeDefinitionFactory definitionFactory)
        {
            Registry = registry;
            DefinitionFactory = definitionFactory;
        }

        [HttpGet("{typeKey}")]
        public async Task<TypeDefinition> Get([FromRoute]string typeKey)
            => DefinitionFactory.CreateResponse(await Registry.GetDefinition(typeKey, ""));

        [HttpGet]
        public async Task<IEnumerable<TypeDefinition>> Get()
        {
            var results = await Registry.GetDefinitions("");
            return results.Select(DefinitionFactory.CreateResponse);
        }

        [HttpPost]
        public Task Set([FromBody] TypeDefinition typeDefinition)
            => Registry.SetDefinition(DefinitionFactory.CreateSaveable(typeDefinition), "");
    }
}