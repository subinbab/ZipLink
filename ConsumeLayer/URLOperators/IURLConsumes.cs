using BusinessLayer.URLOperations;
using Models.Client;

namespace ConsumeLayer.URLOperators
{
    public interface IURLConsumes
    {
        IURLOperators _urlOperators { get; set; }

        URLClient GetRandomBanner();
        string RedirectUrl(URLClient url);
        URLClient UrlUpload(URLClient uRLClient);
    }
}