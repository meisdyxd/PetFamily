using PetFamily.Contracts.Contracts.FormFIleDtos;

namespace PetFamily.API.Processors;

public class FormFIleProcessor: IAsyncDisposable
{
    private readonly List<CreateFileDto>  _files = [];

    public List<CreateFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            _files.Add(new CreateFileDto(stream, file.FileName));
        }
        
        return _files;
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _files)
        {
            await file.Stream.DisposeAsync();
        }
    }
}