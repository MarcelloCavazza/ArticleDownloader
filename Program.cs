using ceTe.DynamicPDF.Conversion;
using System.Net;
using System.Text.RegularExpressions;

HtmlConversionOptions htmlConversionOptions = new HtmlConversionOptions(false);
Console.WriteLine("Insert the article URL: ");
var url = Console.ReadLine();

WebClient webClient = new();
string source = webClient.DownloadString(url);

string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
    RegexOptions.IgnoreCase).Groups["Title"].Value
    .Replace("_", "")
    .Replace("*", "")
    .Replace("?", "")
    .Replace("/", "")
    .Replace("\\", "")
    .Replace("|", "")
    .Replace(":", "")
    .Replace("\"", "");


HtmlConverter htmlConverter = new HtmlConverter(new Uri(@"" + url), htmlConversionOptions);

string pathToSaveFile = $"C:\\Users\\{Environment.UserName}\\Downloads\\";

htmlConverter.Convert($"{pathToSaveFile}{title}.pdf");

// Opening folder automatically for the user
System.Diagnostics.Process process = new System.Diagnostics.Process();
System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
// executing the cmd window in a hidden way
startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
startInfo.FileName = "cmd.exe";
startInfo.Arguments = $"/C cd {pathToSaveFile} && explorer .";
process.StartInfo = startInfo;
process.Start();
Console.WriteLine("Press any key to exit.");
Console.ReadKey();