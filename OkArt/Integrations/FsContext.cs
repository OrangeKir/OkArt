using System.Text.Json;
using System.Text.Json.Serialization;
using FileInfo = OkArt.Integrations.GlobalContracts.FileInfo;

namespace OkArt.Integrations;

public class FsContext : IFsContext
{
    private readonly string _connectionString;

    public FsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<string> WriteFile(FileInfo fileInfo)
    {
        var name = Guid.NewGuid().ToString();

        var fileBytes = await JsonContent.Create(fileInfo).ReadAsStringAsync();
        while (true)
        {
            try
            {
                await WriteFileInternal(fileBytes.ToCharArray(), name);
            }
            catch (IOException) when (File.Exists(Path.Combine(_connectionString, name)))
            {
                name = Guid.NewGuid().ToString();
                continue;
            }
            
            break;
        }

        return name;
    }

    private async Task WriteFileInternal(char[] file, string name)
    {
        await using var stream = new StreamWriter(Path.Combine(_connectionString, name));
        await stream.WriteAsync(file);
    }

    public async Task<FileInfo> ReadFile(string name, CancellationToken token)
    {
        using var stream = new StreamReader(Path.Combine(_connectionString, name));
        var fileBytes = new Memory<char>();
        await stream.ReadAsync(fileBytes, token);

        return JsonDocument.Parse(fileBytes).Deserialize<FileInfo>()!;
    }
}