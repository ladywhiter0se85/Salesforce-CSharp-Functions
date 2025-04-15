namespace Tests.Utilities
{
    public class MockHttpResponseData : HttpResponseData
    {
        private Stream _body;

        public MockHttpResponseData(FunctionContext functionContext, HttpStatusCode statusCode, Stream body)
            : base(functionContext)
        {
            StatusCode = statusCode;
            _body = body;
            if (_body.CanSeek) _body.Position = 0;

            Headers = new HttpHeadersCollection();
        }

        public override HttpStatusCode StatusCode { get; set; }

        public override HttpHeadersCollection Headers { get; set; }
        public override HttpCookies Cookies { get; } = null!;

        public override Stream Body
        {
            get => _body;
            set => _body = value;
        }
    }
}