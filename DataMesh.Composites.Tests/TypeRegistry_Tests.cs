using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;
using Xunit;

// What do I need: 
// Ability to register source
// Ability to retrieve from source
// Ability to pass security token
// System to request Property Ownership
// System to Receive Property Ownership Requests
// Event System!!
// Ability to Request Data(Registry)
// Ability to Request Data(Source)


// Therefore:

//Registry:
// Needs the ability to register a Type/check the type
// Needs the ability to register a Source
// Needs the ability to Have Ownership of a property requested
// Needs the ability to Request Data (All or part ala GraphQL)
// Must be able to pass security tokens
// Needs an API for Prospective Data Retrieval (Ownership Transfer)
// Needs an API for providing Fields
// Needs to throw the following events: Ownership Requested, Ownership Transferred, Property Updated, Entity Created, (Entity Deleted?)
// Needs APIs For the following: Register Source, Create Entity, Request Ownership, Retrieve (some/all), Register Type, Check Type

// Stretch Goal: Cascading Partial Request- an API may provide some or all, when it registers it says whether or not it can provide that ability.
//  The service will provide that ability for it.

// Source:
// Needs API for Ownership Relinquishment that must accept a token
// Needs API for Health Check
// Needs API for Data Retrieval that must accept a token and a ResourceID
// Needs Events for the following: Ownership Relinquished, Property Updated
// Needs Following APIs: HealthCheck, RelinquishOwnership, Retrieve, CheckType


// Acceptable Types:
// string, number, array, object, boolean, a type can be nullable and/or optional
// so what we actually have: string, number, object, boolean
// and those can be optional, nullable, or array

// Type Registry:

public class TypeRegistry_Tests
{

    public async Task AssertTypeRegistryImplementation(ITypeRegistry registry)
    {
        var expectedKey = "Thing";
        var expectedDefinition = CreateDefinition(expectedKey);
        await registry.SetDefinition(expectedDefinition);
        await AssertDefinitionSet(expectedKey, expectedDefinition, registry);
    }

    public ITypeDefinition CreateDefinition(string typeKey)
    {
        var fakeDefinitions = new []
        {
            new KeyValuePair<string,ITypeDefinitionItem>("A", new TestTypeDefinitionItem() {TypeKey = "number", Optional = true, Nullable = false, Array = false}),
            new KeyValuePair<string,ITypeDefinitionItem>("B", new TestTypeDefinitionItem() {TypeKey = "string", Optional = false, Nullable = true, Array = false}),
            new KeyValuePair<string,ITypeDefinitionItem>("C", new TestTypeDefinitionItem() {TypeKey = "boolean", Optional = false, Nullable = false, Array = true}),
        }.ToDictionary(kv => kv.Key, kv => kv.Value);
        var definition = new TestTypeDefinition() {TypeKey = typeKey, Properties = fakeDefinitions };
        return definition;
    }

    public async Task AssertDefinitionSet(string typeName, ITypeDefinition expectedDefinition, ITypeRegistry registry)
    {
        var results = await registry.GetDefinition(typeName);

        Assert.Equal(results.TypeKey, expectedDefinition.TypeKey);
        Assert.Equal(expectedDefinition.Properties.Count, results.Properties.Count);
        foreach (var (key, expectedValue) in expectedDefinition.Properties)
        {
            VerifyPropertyMatches(expectedValue, results.Properties[key]);
        }
    }

    public void VerifyPropertyMatches(ITypeDefinitionItem expected, ITypeDefinitionItem actual)
    {
        Assert.Equal(expected.TypeKey, actual.TypeKey);
        Assert.Equal(expected.Array, actual.Array);
        Assert.Equal(expected.Nullable, actual.Nullable);

        Assert.Equal(expected.Optional, actual.Optional);
    }

    public class TestTypeDefinition : ITypeDefinition
    {
        public string TypeKey { get; set; }
        public IDictionary<string, ITypeDefinitionItem> Properties { get; set; }
    }

    public class TestTypeDefinitionItem: ITypeDefinitionItem
    {
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }
}

//can this be tested? it can, but only really at the source. 

// How do we deal with a cascading definition? Actually, we shouldn't need to do a full cascade! it is per item.
// How do we define an Array of stuff?