namespace DataMesh.TypeDefinitions
{
    public interface ITypeDefinitionItem
    {
        string TypeKey { get; }
        bool Nullable { get; }
        bool Optional { get; }
        bool Array { get; }
    }
}