namespace Notifyer.Application.Factories
{
    public class FileTypeFactory
    {
        public string GetFileType(string fileName)
        {
           return Path.GetExtension(fileName).ToLower() switch
            {
                ".csv" => "csv", 
                _ => throw new NotSupportedException("File type not supported.")
            };
        }
    }
}
