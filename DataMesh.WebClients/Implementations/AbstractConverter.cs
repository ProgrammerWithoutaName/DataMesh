using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataMesh.Composites
{
    public class AbstractConverter<TReal, TAbstract> : JsonConverter<TAbstract> where TReal : TAbstract
    {
        public override TAbstract Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => JsonSerializer.Deserialize<TReal>(ref reader, options);
        // Not Cool, breaking Liskov. Fix this?
        public override void Write(Utf8JsonWriter writer, TAbstract value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}