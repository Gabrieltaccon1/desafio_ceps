﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using ConsoleApp36.Entities;
using System.Linq;


namespace ConsoleApp36
{
    class Program
    {
        static void Main(string[] args)
        {
            Endereco e = new Endereco();
            List<string> endereco = new List<string>();
            List<Endereco> Zipcode = new List<Endereco>();
            e.PopularaBase(endereco);
            int contagem = 0;
            int decisao;
            string decisaotwo;


            foreach (var zipcode in endereco)
            {
                try
                {

                    var requisicaoWeb = WebRequest.CreateHttp($"https://viacep.com.br/ws/{Zipcode}/json");
                    requisicaoWeb.Method = "GET";
                    requisicaoWeb.UserAgent = "RequisicaoWebDemo";

                    using (var resposta = requisicaoWeb.GetResponse())
                    {
                        var streamDados = resposta.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();
                        var local = JsonConvert.DeserializeObject<Endereco>(objResponse.ToString());
                        local.datadeConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff");
                        Zipcode.Add(local);
                        contagem++;
                    }
                    Zipcode = Zipcode.OrderBy(x => x.Uf).ThenBy(x => x.datadeConsulta).ToList();
                    string path = @"C:\Users\Treinamento 4\Documents\consultaCep/consultaceps.txt";
                    // convertendo e escrevendo o json 
                    StreamWriter sw2 = new StreamWriter(path);
                    string g2 = JsonConvert.SerializeObject(Zipcode);
                    sw2.WriteLine(g2);
                    sw2.Close();
                }
                catch (WebException f)
                {
                    Console.WriteLine(f.Message);
                }
                Console.WriteLine($"FORAM REGISTRADOS UM TOTAL DE: {contagem} CEP'S");
            }




            Console.WriteLine("QUANTOS REGISTROS POR PAGINA VOCÊ DESEJA CONSULTAR? ");
            int registrosDesejados = int.Parse(Console.ReadLine());
            int paginacao = 50 / registrosDesejados;

            if (50 % registrosDesejados !=0)
            {
                paginacao = paginacao + 1;
            }



            do
            {

                for (int i = 1; i < paginacao; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"DIGITE {i} PARA VISUALIZAR A PAGINA {i}");

                }

                Int32.TryParse(Console.ReadLine(), out decisao);

                if (decisao <= 0 || decisao > paginacao)
                {
                    Console.WriteLine();
                    Console.WriteLine("!!! A PAGINA DIGITADA NÃO EXISTE NO CONTEXTO ATUAL !!! ");
                    Console.WriteLine();
                }
                else
                {
                    var result = Zipcode.Skip((decisao - 1) * registrosDesejados).Take(registrosDesejados);

                    foreach (var zip in result)
                    {
                        Console.WriteLine($"CEP: {zip.Cep} | LOGRADOURO: {zip.Logradouro} | BAIRRO: {zip.Bairro} | UF: {zip.Uf} | HORARIO DE CONSULTA DO CEP: {zip.datadeConsulta}");
                    }
                }

                Console.WriteLine("DESEJA VISUALIZAR OUTRA PAGINA/CONTINUAR NO PROGRAMA? ");
                Console.WriteLine("             DIGITE ( SIM ) OU ( NÃO )                ");
                decisaotwo = Console.ReadLine().ToUpper();

            } while (decisaotwo == "SIM");


        }
    }
}
