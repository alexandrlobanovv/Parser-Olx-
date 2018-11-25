namespace Parser.Models.ParserCore
{
    public interface IParserSettings
    {
        string BaseUrl { get; set; }
        string Argument { get; set; }
        int StartPage { get; set; }
        int EndPage { get; set; }
    }
}
