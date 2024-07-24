using BusinessLayer.URLOperations;
using Models.Client;

namespace ConsumeLayer.URLOperators
{
    public interface IURLConsumes
    {
        IURLOperators _urlOperators { get; set; }

        string RedirectUrl(string url);
        URLClient UrlUpload(URLClient uRLClient);
    }
}