using JetBrains.Annotations;

namespace OkArt.Integrations.GlobalContracts;

[PublicAPI]
public class FileInfo
{
    public string Language { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}