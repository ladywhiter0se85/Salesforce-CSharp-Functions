namespace Tests.Utilities
{
    public class MockHttpRequestData : HttpRequestData
    {
        public override Stream Body => _body;
        public override HttpHeadersCollection Headers => _headers;
        public override NameValueCollection Query => _query;
        public override IReadOnlyCollection<IHttpCookie> Cookies => new List<IHttpCookie>().AsReadOnly();
        public override Uri Url => _url;
        public override IEnumerable<ClaimsIdentity> Identities => Array.Empty<ClaimsIdentity>();
        public override string Method => _method;
        private readonly Stream _body;
        private readonly HttpHeadersCollection _headers;
        private readonly NameValueCollection _query;
        private readonly string _method;
        private readonly Uri _url;
        private readonly FunctionContext _functionContext;

        public MockHttpRequestData(
            FunctionContext functionContext,
            Stream? body = null,
            HttpHeadersCollection? headers = null,
            NameValueCollection? query = null,
            string? method = null,
            Uri? url = null
        ) : base(functionContext)
        {
            _functionContext = functionContext;
            _body = body ?? new MemoryStream();
            _headers = headers ?? new HttpHeadersCollection();
            _query = query ?? new NameValueCollection();
            _method = method ?? "GET";
            _url = url ?? new Uri("http://localhost");
        }

        public override HttpResponseData CreateResponse()
        {
            return new MockHttpResponseData(_functionContext, HttpStatusCode.OK, new MemoryStream());
        }
    }
}