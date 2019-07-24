using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp36.Entities
{
    public class Endereco
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Unidade { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string datadeConsulta { get; set; }   = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff");


        public Endereco()
        {

        }

        public Endereco(string cep, string logradouro, string complemento, string bairro, string localidade, string uf, string unidade, string ibge, string gia)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
            Unidade = unidade;
            Ibge = ibge;
            Gia = gia;

        }

        public void PopularaBase(List<string> endereco)
        {

            {
                endereco.Add("03707030");
                endereco.Add("08340030");
                endereco.Add("08373750");
                endereco.Add("08431030");
                endereco.Add("05782430");

                endereco.Add("08452480");
                endereco.Add("04939220");
                endereco.Add("01417030");
                endereco.Add("04717903");
                endereco.Add("05458050");

                endereco.Add("08021270");
                endereco.Add("02220365");
                endereco.Add("03312000");
                endereco.Add("03807000");
                endereco.Add("04709130");

                endereco.Add("08090310");
                endereco.Add("01540070");
                endereco.Add("04544140");
                endereco.Add("02807120");
                endereco.Add("02817050");

                endereco.Add("01415010");
                endereco.Add("01529000");
                endereco.Add("02710000");
                endereco.Add("02956020");
                endereco.Add("02262060");

                endereco.Add("04867010");
                endereco.Add("03736005");
                endereco.Add("03424010");
                endereco.Add("03310010");
                endereco.Add("02175040");

                endereco.Add("04006030");
                endereco.Add("02237044");
                endereco.Add("05893040");
                endereco.Add("03254250");
                endereco.Add("08473627");

                endereco.Add("05131040");
                endereco.Add("08121473");
                endereco.Add("02723007");
                endereco.Add("04646030");
                endereco.Add("01415009");

                endereco.Add("03245090");
                endereco.Add("08071220");
                endereco.Add("05281020");
                endereco.Add("02218040");
                endereco.Add("03515110");

                endereco.Add("04429190");
                endereco.Add("01444060");
                endereco.Add("01004010");
                endereco.Add("03516040");
                endereco.Add("02231030");

            }

        }
    }
}
                        