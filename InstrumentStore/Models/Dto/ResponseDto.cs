namespace InstrumentStore.Models.Dto;

/// <summary>
/// This is the Dto for the response model.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object Result { get; set; }
    public List<string> ErrorMessage { get; set; }
}