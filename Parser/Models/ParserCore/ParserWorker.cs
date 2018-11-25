using AngleSharp.Parser.Html;
using System;
using System.Threading.Tasks;


namespace Parser.Models.ParserCore
{
    public class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        HtmlLoader loader;
        bool isActive;

        #region Properties
        public IParser<T> Parser
        {
            get => parser;
            set => parser = value;
        }
        public IParserSettings ParserSettings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings settings) : this(parser)
        {
            parserSettings = settings;
        }

        public async Task<bool> Start()
        {
            isActive = true;
            return await Worker();
        }
        public void Abort()
        {
            isActive = false;
        }

        private async Task<bool> Worker()
        {
            for (int i = parserSettings.StartPage; i <= parserSettings.EndPage; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return false;
                }
                var sourse = await loader.GetSourseByPageId(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseAsync(sourse);
                var result = parser.Parse(document);
                OnNewData?.Invoke(this, result);
            }
            OnCompleted?.Invoke(this);
            isActive = false;
            return true;
        }

    }
}