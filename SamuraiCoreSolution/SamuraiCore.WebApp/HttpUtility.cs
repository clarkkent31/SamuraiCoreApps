using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiCore.WebApp
{
    public class HttpUtility
            
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a request. </summary>
        ///
        /// <remarks>   Mudassir Dholiya, 23-01-2018. </remarks>
        ///
        /// <param name="httpUri">                      . </param>
        /// <param name="uriMethodAndParams">           Options for controlling the URI method and. </param>
        /// <param name="isJsonRequest">                (Optional) True if this object is JSON request. </param>
        /// <param name="authHeader">                   (Optional) The authentication header. </param>
        /// <param name="isAuthorizationHeader">        (Optional) True if this object is authorization
        ///                                             header. </param>
        /// <param name="defaultServiceTimeoutMinutes"> (Optional) The default service timeout minutes. </param>
        ///
        /// <returns>   The request. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HttpResponseMessage GetRequest(string httpUri, string uriMethodAndParams, bool isJsonRequest = true, Dictionary<string, string> authHeader = null, bool isAuthorizationHeader = false, int defaultServiceTimeoutMinutes = 5)
        {
            string contentType = "application/json";

            if (!isJsonRequest)
            {
                contentType = "application/xml";
            }
            using (var client = new HttpClient(new HttpClientHandler()))
            {
                client.BaseAddress = new Uri(httpUri);
                client.DefaultRequestHeaders.Accept.Clear();

                if (authHeader != null)
                {
                    foreach (var item in authHeader)
                    {
                        if (isAuthorizationHeader)
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(item.Key, item.Value);
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                client.Timeout = new TimeSpan(0, defaultServiceTimeoutMinutes, 0);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                return client.GetAsync(httpUri + "/" + uriMethodAndParams).Result;
            }
        }

        public HttpResponseMessage GetAsync(string serviceUri, int defaultServiceTimeoutMinutes = 5)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, defaultServiceTimeoutMinutes, 0);
                return client.GetAsync(serviceUri).Result;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post object.
        /// <para>Convert the result to your object by using the following line</para>
        /// <para>result.Content.ReadAsAsync&lt;[TheClassObject]&gt;().Result</para>
        /// </summary>
        ///
        /// <remarks>   Mddholiya, 14-06-2018. </remarks>
        ///
        /// <param name="httpUri">                      . </param>
        /// <param name="uriMethod">                    . </param>
        /// <param name="requestData">                  . </param>
        /// <param name="authHeader">                   (Optional) </param>
        /// <param name="isAuthorizationHeader">        (Optional) </param>
        /// <param name="defaultServiceTimeoutMinutes"> (Optional) The default service timeout minutes. </param>
        ///
        /// <returns>   A HttpResponseMessage. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HttpResponseMessage PostRequest(string httpUri, string uriMethod, object requestData, Dictionary<string, string> authHeader = null, bool isAuthorizationHeader = false, int defaultServiceTimeoutMinutes = 5)
        {
            using (var client = new HttpClient(new HttpClientHandler()))
            {
                client.BaseAddress = new Uri(httpUri);
                client.DefaultRequestHeaders.Accept.Clear();

                if (authHeader != null)
                {
                    foreach (var item in authHeader)
                    {
                        if (isAuthorizationHeader)
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(item.Key, item.Value);
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }

                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, defaultServiceTimeoutMinutes, 0);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                return client.PostAsJsonAsync(uriMethod, requestData).Result;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Delete Request
        /// <para>Convert the result to your object by using the following line</para>
        /// <para>result.Content.ReadAsAsync&lt;[TheClassObject]&gt;().Result</para>
        /// </summary>
        ///
        /// <remarks>   Mddholiya, 14-06-2018. </remarks>
        ///
        /// <param name="httpUri">                      . </param>
        /// <param name="uriMethod">                    . </param>
        /// <param name="authHeader">                   (Optional) </param>
        /// <param name="isAuthorizationHeader">        (Optional) </param>
        /// <param name="defaultServiceTimeoutMinutes"> (Optional) The default service timeout minutes. </param>
        ///
        /// <returns>   A HttpResponseMessage. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HttpResponseMessage DeleteRequest(string httpUri, string uriMethod, Dictionary<string, string> authHeader = null, bool isAuthorizationHeader = false, int defaultServiceTimeoutMinutes = 5)
        {
            using (var client = new HttpClient(new HttpClientHandler()))
            {
                client.BaseAddress = new Uri(httpUri);
                client.DefaultRequestHeaders.Accept.Clear();

                if (authHeader != null)
                {
                    foreach (var item in authHeader)
                    {
                        if (isAuthorizationHeader)
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(item.Key, item.Value);
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, defaultServiceTimeoutMinutes, 0);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                return client.DeleteAsync(httpUri + uriMethod).Result;
            }
        }
    }
}
