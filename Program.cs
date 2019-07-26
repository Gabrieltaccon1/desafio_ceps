using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using ConsoleApp36.Entities;
using System.Linq;


namespace ConsoleApp36
{
    // USUARIO DEVE INFORMAR A QUANTIDADE DE REGISTROS QUE DESEJA VER POR PAGINA
    // POPULAR A BASE
    // IMPRIMIR AS PAGINAS EM MENU
    // USUÁRIO ESCOLHE UMA PAGINA E NAVEGA
    // IMPRESSAO DEVERÁ SER REDUZIDA(CEP,LOGRADOURO,BAIRRO E UF)
    // ORDER POR UF E DATA PESQUISA
    class Program
    {
        static void Main(string[] args)
        {
            Endereco e = new Endereco();
            Cores cores = new Cores();
            List<string> endereco = new List<string>();// INSTANCIAÇÃO DA LIST PARA CHAMAR O METODO DE POPULAR A BASE
            List<Endereco> Zipcode = new List<Endereco>(); // INSTANCIAÇÃO DA LISTA DE ENDEREÇOS COMO NOME GENERICO "ZIP CODE"
            e.PopularaBase(endereco);// METODO CRIADO PARA PREENCHER A LISTA COM OS 50 CEP'S
           int contagem = 0;
            int decisao;
            string decisaotwo;


            Console.WriteLine();
            Console.WriteLine("O PROGRAMA ESTA CONSULTANDO OS CEPS EXISTENTES NESTE PROGRAMA...");
            Console.WriteLine();
         

            foreach (var zipcode in endereco) // PARA CADA ITEM EM ENDEREÇOS (METODO POPULAR BASE) FAÇA vv
            {
                try
                {
                    // FAZENDO A REQUISIÇÃO A API
                    var requisicaoWeb = WebRequest.CreateHttp($"https://viacep.com.br/ws/{zipcode}/json");
                    requisicaoWeb.Method = "GET";
                    requisicaoWeb.UserAgent = "RequisicaoWebDemo";

                    // VARIAVEL RESPOSTA RECEBENDO REQUISIÇÃO 
                    using (var resposta = requisicaoWeb.GetResponse())
                    {
                        var streamDados = resposta.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();
                        var local = JsonConvert.DeserializeObject<Endereco>(objResponse.ToString());
                        //VARIAVEL LOCAL RECEBENDO A DESERIALIZAÇÃO DO OBJ QUE RECEBE UMA LISTA DE ENDEREÇOS
                        Zipcode.Add(local);
                        contagem++;
                    }
                    // DENTRO DA LISTA ENDEREÇO SE CONTEM UF = ORDER BY UF, E TAMBEM PELA DATA DE CONSULTA DENTRO DA LIST
                    Zipcode = Zipcode.OrderBy(x => x.Uf).ThenBy(x => x.datadeConsulta).ToList();
                    string path = @"C:\Users\Treinamento 4\Documents\consultaCep/consultaceps.txt";             
                    StreamWriter sw2 = new StreamWriter(path);
                    string g2 = JsonConvert.SerializeObject(Zipcode);
                    sw2.WriteLine(g2);
                    sw2.Close();
                }
                catch (WebException f)
                {
                    Console.WriteLine(f.Message);
                }

            }

            // exibindo quantidade de ceps registrados 

            Console.WriteLine($"FORAM REGISTRADOS UM TOTAL DE: {endereco.Count} CEP'S");
            cores.MudarCores();

            Console.WriteLine("");

            // pedindo a quantidade de registros que o usuario quer ver por paginas

            Console.WriteLine("QUANTOS REGISTROS POR PAGINA VOCÊ DESEJA CONSULTAR? ");
            int registrosDesejados = int.Parse(Console.ReadLine());

            // definindo as paginas 50 ceps registrados / pela quantidade de registros
            // que a pessoa deseja ter por pagina
            //           VV
            int paginacao = 50 / registrosDesejados;
        
            // caso a divisao entre 50 e a qtd de registros seja um numero impar add 1 pagina
            //para que seja exibida nela o restante da consulta por exemplo:
            // 50
            if (50 % registrosDesejados !=0)
            {
                paginacao = paginacao + 1;
            }

            #region paginacao
            do
            {
                for (int i = 1; i <= paginacao; i++)
                {
                    Console.WriteLine();
                    //Console.WriteLine($"DIGITE {i} PARA VISUALIZAR A PAGINA {i}");

                    Console.WriteLine($"╔═════════════════════════════════════════╗");
                    Console.WriteLine($"║ DIGITE {i} PARA VISUALIZAR A PAGINA {i}     ║");
                    Console.WriteLine($"╚═════════════════════════════════════════╝");
                }

                Int32.TryParse(Console.ReadLine(), out decisao);

                if (decisao <= 0 || decisao > paginacao)
                {
                    Console.WriteLine();
                    Console.WriteLine("   !!! A PAGINA DIGITADA NÃO EXISTE NO CONTEXTO ATUAL !!! ");
                    Console.WriteLine("═══════════════════════════════════════════════════════════════");

                    Console.WriteLine();
                }
                #endregion


                // LOGICA CORTES PAGINACAO
                else
                {
                    var result = Zipcode.Skip((decisao - 1) * registrosDesejados).Take(registrosDesejados);

                    foreach (var zip in result)
                    {
                        Console.WriteLine($"CEP: {zip.Cep} | LOGRADOURO: {zip.Logradouro} | BAIRRO: {zip.Bairro} | UF: {zip.Uf} | HORARIO DE CONSULTA DO CEP: {zip.datadeConsulta}");
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine($" ╔═════════════════════════════════════════════════════╗");
                Console.WriteLine($" ║DESEJA VISUALIZAR OUTRA PAGINA/CONTINUAR NO PROGRAMA?║");
                Console.WriteLine($" ║           DIGITE ( SIM ) OU ( NÃO )                 ║");
                Console.WriteLine($" ╚═════════════════════════════════════════════════════╝");
                decisaotwo = Console.ReadLine().ToUpper();


            } while (decisaotwo == "SIM");


        }
    }
}
