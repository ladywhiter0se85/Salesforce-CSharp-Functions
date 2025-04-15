using System.Text;

namespace Tests.Utilities
{
    public class FunctionsUtility
    {
        public static MockHttpRequestData MockedHttpRequestData<T>(HttpHeadersCollection? headers, NameValueCollection? query, T body)
        {
            var context = new Mock<FunctionContext>().Object;
            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(body);
            writer.Flush();
            bodyStream.Position = 0;

            return new MockHttpRequestData(context, bodyStream, headers, query, null, null);
        }

        public static MockHttpResponseData MockedHttpResponseData<T>(HttpStatusCode statusCode, T body)
        {
            var context = new Mock<FunctionContext>().Object;
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));
            return new MockHttpResponseData(context, statusCode, bodyStream);
        }

        public async static Task<T> GetBodyFromStream<T>(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream);
            var bodyContent = await reader.ReadToEndAsync();

            try
            {
                var resp = JsonConvert.DeserializeObject<T>(bodyContent);
                if (resp == null)
                    throw new JsonException($"Deserialization of the response body into type '{typeof(T).Name}' returned null. The input content might not match the expected format or structure.");

                return resp;
            }
            catch (JsonException)
            {
                // If the target type is string, just return the raw content
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)bodyContent;
                }

                throw; // rethrow original error if it wasn't a string target
            }
        }
    }
}