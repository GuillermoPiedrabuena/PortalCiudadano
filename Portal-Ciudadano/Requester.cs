using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Net.Http.Json;
using Newtonsoft.Json;




namespace Portal_Ciudadano
{

    public class Client {

        public string clients_name { get; set; }
        public string clients_rut { get; set; }
        public string clients_address { get; set; }
        public string clients_civilStatus { get; set; }
        public string clients_contact { get; set; }
        public string clients_id { get; set; }
        public string clients_job { get; set; }
        public string clients_nationality { get; set; }
    
    }

    public class Requester 
    {

        
        public async static Task<List<Client>> GetRequest(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {

                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        
                        myContent = myContent.Substring(9);
                        myContent = myContent.Substring(0, myContent.Length - 3);
                        myContent = myContent.Replace("\"", "'");
                        
                        List<string> stringifyJsonsList = new List<string>();
                        List<Client> serializedJsonsList = new List<Client>();

                        string strToCut = myContent;

                        while (strToCut.Contains("},{")) {
                        
                            string newItem = strToCut.Substring(0, strToCut.IndexOf("},{") + 1);
                            stringifyJsonsList.Add(newItem); //SE AGREGA AL LIST CADA JSON DE LA RESPUESTA
                            strToCut = strToCut.Remove(0, strToCut.IndexOf("},{") + 2);
                        }

                        stringifyJsonsList.ForEach((item) => {
                            
                            Client deserializedJson = JsonConvert.DeserializeObject<Client>(item);// ACA EL PROBLEMA
                            
                            serializedJsonsList.Add(deserializedJson);

                        });
                        
                        return serializedJsonsList;

                    }

                }

            }

        }

    }
}
