using AngleSharp.Dom.Html;

namespace Parser.Models.ParserCore
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
