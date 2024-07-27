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

                    var charlength = Convert.ToInt32(ConfigurationManager.AppSettings["urlLength"].ToString());
                    var generatedUrl = GenerateUrlLink(charlength);
                    uRLClient.generatedUrl = generatedUrl;
                    uRLClient.shortenurl = ConfigurationManager.AppSettings["baseurl"].ToString() + "/Home/index?url=" + generatedUrl;
                    // Mapping URLClient to URLDTO 
                    var dto = _mapper.Map<URLDTO>(uRLClient);
                    dto.userId = uRLClient.userId;
                    dto.user = uRLClient.appUser;
                    dto.createdby = uRLClient.userId;
                dto.imageUrl = uRLClient.imageUrl;
                dto.text = uRLClient.text;
                    var result = _urlOperators.Add(dto);
                    var returnData = _mapper.Map<URLClient>(result);
                    returnData.shortenurl = uRLClient.shortenurl;
                    return returnData;

                

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

        public string RedirectUrl(URLClient url)
        {
            try
            {
                var result = _urlOperators.GetDataByParms("url", url.url, "string");

                return result.url.ToString();
            }
            catch (Exception ex) { 
            }
            return "";
        }
        public URLClient GetRandomBanner()
        {
            var totaleCount = _urlOperators.Get();
            if (totaleCount == null || totaleCount.Count()==0)
            {
                return null;
            }
            else
            {
                var randomIndex = new Random().Next(totaleCount.Count());
                var obj = _urlOperators.Get().Select(c => c.id).ToList()[randomIndex];
                var signleData = _urlOperators.GetById(obj);
                var result = _mapper.Map<URLClient>(signleData);
                result.id = signleData.id;
                result.shortenurl = signleData.shortenUrl;
                return result;
            }
           

            
        }
    }
}
