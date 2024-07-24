using AutoMapper;
using AutoMapper.Configuration;
using BusinessLayer.URLOperations;
using Models.Client;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeLayer.URLOperators
{
    public class URLConsumes : IURLConsumes
    {
        private readonly IMapper _mapper;
        public URLConsumes(IMapper mapper, IURLOperators urlOperators)
        {
            _mapper = mapper;
            _urlOperators = urlOperators;
        }
        public IURLOperators _urlOperators { get; set; }

        public URLClient UrlUpload(URLClient uRLClient)
        {
            try
            {

                // Limits scope of myRes
                URLDTO urlDto = new URLDTO();
                try
                {
                    var charlength = Convert.ToInt32(ConfigurationManager.AppSettings["urlLength"].ToString());
                    var generatedUrl = GenerateUrlLink(charlength);
                    uRLClient.generatedUrl = generatedUrl;
                    uRLClient.shortenurl = ConfigurationManager.AppSettings["baseurl"].ToString() + "/" + generatedUrl;
                    // Mapping URLClient to URLDTO 
                    var dto = _mapper.Map<URLDTO>(uRLClient);
                    var result = _urlOperators.Add(dto);
                    return _mapper.Map<URLClient>(result);
                }
                finally
                {
                    // Check for a null resource.
                    if (urlDto != null)
                        // Call the object's Dispose method.
                        ((IDisposable)urlDto).Dispose();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static string GenerateUrlLink(int length)
        {
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RedirectUrl(string url)
        {
            try
            {
                var result = _urlOperators.GetDataByParms("url", url, "string");
                return result.url.ToString();
            }
            catch (Exception ex) { 
            }
            return "";
        }
    }
}
