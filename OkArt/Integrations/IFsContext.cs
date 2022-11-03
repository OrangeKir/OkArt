using FileInfo = OkArt.Integrations.GlobalContracts.FileInfo;

namespace OkArt.Integrations;

public interface IFsContext
{
    public Task<string> WriteFile(FileInfo fileInfo);
    public Task<FileInfo> ReadFile(string name, CancellationToken token);
}