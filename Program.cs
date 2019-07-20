using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using ConsoleApp36.Entities;


namespace ConsoleApp36
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("QUANTOS REGISTROS POR PAGINA? ");
            // int decisao = int.Parse(Console.ReadLine());


            List<Endereco> buscaceps = new List<Endereco>()
            {
                new Endereco(){Cep = "03707030" },
                new Endereco(){Cep = "08340030"},
                new Endereco(){Cep = "08373750" },
                new Endereco(){Cep = "08431030" },
                new Endereco(){Cep = "05782430" },

                new Endereco(){Cep = "08452480"},
                new Endereco(){Cep = "04939220"},
                new Endereco(){Cep = "01417030" },
                new Endereco(){Cep = "04717903" },
                new Endereco(){Cep = "05458050"},

                new Endereco(){Cep = "08021270"},
                new Endereco(){Cep = "02220365"},
                new Endereco(){Cep = "03312000"},
                new Endereco(){Cep = "03807000"},
                new Endereco(){Cep = "04709130"},

                new Endereco(){Cep = "08090310" },
                new Endereco(){Cep = "01540070" },
                new Endereco(){Cep = "04544140" },
                new Endereco(){Cep = "02807120" },
                new Endereco(){Cep = "02817050" },

                new Endereco(){Cep = "01415010" },
                new Endereco(){Cep = "01529000" },
                new Endereco(){Cep = "02710000" },
                new Endereco(){Cep = "02956020" },
                new Endereco(){Cep = "02262060" },

                new Endereco(){Cep = "04867010" },
                new Endereco(){Cep = "03736005" },
                new Endereco(){Cep = "03424010" },
                new Endereco(){Cep = "03310010" },
                new Endereco(){Cep = "02175040" },

                new Endereco(){Cep = "04006030" },
                new Endereco(){Cep = "02237044" },
                new Endereco(){Cep = "05893040" },
                new Endereco(){Cep = "03254250" },
                new Endereco(){Cep = "08473627" },

                new Endereco(){Cep = "05131040"},
                new Endereco(){Cep = "08121473"},
                new Endereco(){Cep = "02723007"},
                new Endereco(){Cep = "04646030"},
                new Endereco(){Cep = "01415009"},

                new Endereco(){Cep = "03245090"},
                new Endereco(){Cep = "08071220"},
                new Endereco(){Cep = "05281020"},
                new Endereco(){Cep = "02218040"},
                new Endereco(){Cep = "03515110"},

                new Endereco(){Cep = "04429190"},
                new Endereco(){Cep = "01444060"},
                new Endereco(){Cep = "01004010"},
                new Endereco(){Cep = "03516040"},
                new Endereco(){Cep = "02231030"}
            };

            foreach (Endereco findcep in buscaceps)
            {
                
                string https = string.Empty;
                string url = $"https://viacep.com.br/ws/{findcep.Cep}/json";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    https = reader.ReadToEnd();
                }

                Console.WriteLine(https);



                StreamWriter sw2 = new StreamWriter(@"C:\Users\Treinamento 4\Desktop\Aula05");
                Endereco g2 = JsonConvert.DeserializeObject<Endereco>(https);
                buscaceps.Add(findcep);
                sw2.WriteLine(g2);

                sw2.Close();

            }







            //string cep;

            //Console.WriteLine("Informe o Cep Desejado");
            //cep = Console.ReadLine();

            // while (cep < 10000000 || cep > 99999999) 
            //  {
            // Console.WriteLine("Informe o Cep Desejado");
            // cep = Console.ReadLine();
            //  }












        }
    }
}
