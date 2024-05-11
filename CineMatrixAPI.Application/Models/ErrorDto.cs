using System.Text.Json.Serialization;

namespace CineMatrixAPI
{
    public class ErrorDto
    {
        public List<String>? Errors { get; private set; } = new List<String>();

        public ErrorDto(string error)
        {
            Errors.Add(error);
        }

        public ErrorDto(List<string> errors)
        {
            Errors = errors;
        }

        public ErrorDto()
        {

        }
    }
}
