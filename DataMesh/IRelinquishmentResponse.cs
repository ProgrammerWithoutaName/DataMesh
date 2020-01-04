namespace DataMesh
{
    public interface IRelinquishmentResponse
    {
        string EntityId { get; }
        string PropertyKey { get; }
        bool OwnershipRelinquished { get; }
        string ResponseDescription { get; }
    }

    public interface IRelinquishmentRequest
    {
        string EntityId { get; }
        string PropertyKey { get; }
        string ResourceId { get; }
    }

    public interface IOwnershipRequest
    {
        string EntityId { get; }
        string PropertyKey { get; }
        string DataSourceKey { get; }
        string ResourceId { get; }
    }
}