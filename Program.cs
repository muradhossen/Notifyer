using Microsoft.AspNetCore.Mvc;
using Notifyer.Application.Factories;
using Notifyer.Application.Services;
using Notifyer.Application.Services.Abstraction;
using Notifyer.Application.Services.FileParserCommands;
using Notifyer.Domain;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddOpenApi();

builder.Services.AddTransient<INotificationStrategy, BLNotification>();
builder.Services.AddTransient<INotificationStrategy, RobiNotification>();
builder.Services.AddTransient<INotificationStrategy, GPNotification>();
builder.Services.AddTransient<IFileParseCommand, CsvFileParseCommand>();

builder.Services.AddTransient<IFileParseCommand>(provider =>
{
    var csvCommand = provider.GetRequiredService<CsvFileParseCommand>(); 
    var logger = provider.GetRequiredService<ILogger<LoggingFileParseCommand>>(); 
     
    return new LoggingFileParseCommand(csvCommand, logger);
});

builder.Services.AddSingleton<FileParserInvoker>();
builder.Services.AddSingleton<NotificationFactory>();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/send-sms", async (IFormFile file, [FromQuery] string @operator,
    [FromServices] FileTypeFactory fileTypeFactory, [FromServices] IFileParseCommand fileParseCommand
    ) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }

    string fileType = fileTypeFactory.GetFileType(file.FileName);
    var result = NotificationFactory.GetNotificationStrategy(@operator);
    string filePath = GetFilePath(file);

    await fileParseCommand.ExecuteAsync(filePath, result);   

    return Results.Ok();
})
.WithName("SendSms");

app.Run();

string GetFilePath(IFormFile file)
{
    // Save the file temporarily
    var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        file.CopyTo(stream);
    }

    return filePath;

}



