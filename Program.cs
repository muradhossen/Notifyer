using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Notifyer.Application.Factories;
using Notifyer.Application.Services;
using Notifyer.Application.Services.Abstraction;
using Notifyer.Application.Services.FileParserCommands;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddOpenApi();

builder.Services.AddTransient<INotificationStrategy, BLNotification>();
builder.Services.AddTransient<INotificationStrategy, RobiNotification>();
builder.Services.AddTransient<INotificationStrategy, GPNotification>();
builder.Services.AddTransient<IFileParseCommand, CsvFileParseCommand>(); 

builder.Services.AddSingleton<FileParserInvoker>();
builder.Services.AddSingleton<NotificationFactory>();
builder.Services.AddSingleton<FileTypeFactory>();

builder.Services.AddControllers();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseHttpsRedirection(); 
 
app.MapPost("/send-sms", async (HttpContext context, [FromQuery] string @operator,
    [FromServices] FileTypeFactory fileTypeFactory, [FromServices] FileParserInvoker invoker
    ) =>
{
    var form = await context.Request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();

    if (file == null || file.Length == 0)
    {
        return Results.BadRequest(new {IsSuccess = false, Message = "No file uploaded." });
    }

    string fileType = fileTypeFactory.GetFileType(file.FileName);
    var strategy = NotificationFactory.GetNotificationStrategy(@operator);
    string filePath = SaveFile(file);

    await invoker.ExecuteCommand(fileType, filePath, strategy);   

    return Results.Ok(new { IsSuccess = true, Message = "Success" });
})
.WithName("SendSms");

app.Run();

string SaveFile(IFormFile file)
{
    // Save the file temporarily
    var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        file.CopyTo(stream);
    }

    return filePath;

}



