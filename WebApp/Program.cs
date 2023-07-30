using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Custom certificate configuration
//app.ListenAnyIP(80); // Listen on HTTP port 80
//var httpsPort = builder.Configuration.GetValue<int>("HttpsPort");
//app.ListenAnyIP(httpsPort, listenOptions =>
//{
//    // Load the custom certificate
//    var certificatePath = Path.Combine("C:\\Keys", "MyCustomCertificate.pfx");
//    var certificatePassword = "your_password_here";

//    // Create the X.509 certificate object
//    var certificate = new X509Certificate2(certificatePath, certificatePassword);

//    // Bind the certificate to the HTTPS port
//    listenOptions.UseHttps(certificate);
//});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
