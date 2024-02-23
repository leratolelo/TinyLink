
using TinyLink;

Writer.Write("=====================");
Writer.Write("Please enter the Long  URL :");
var longURL = Console.ReadLine();
if(longURL == null)
{
    Console.WriteLine("ERROR : LONG URL IS NULL! ");
    return;
}
//var shortURL = LinkGenerator.GenerateShortLink(longURL);
//Writer.Write($"{shortURL}");


