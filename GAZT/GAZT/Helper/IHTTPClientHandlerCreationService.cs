using System;
using System.Net.Http;

namespace GAZT
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
