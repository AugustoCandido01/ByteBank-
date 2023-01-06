using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace Banco
{
    class Program
    {
        static int option; static int optionToCheckingAccount; static string name; static string role; static int clientKey; static int getClientKey; static int adminKey;
        static int getAdminKey; static int count = 1; static int idConfiguration; static string[] argsMain;

        static List<string> titulares = new List<string>(); static List<string> cpfs = new List<string>(); static List<string> telefones = new List<string>();
        static List<double> saldo = new List<double>(); static List<string> ids = new List<string>();

        static void ShowOptionsAdmin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===============================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1 - Cadastrar usuário novo ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("2 - Deletar usuário ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("3 - Listar contas registradas");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("4 - Detalhes de contas ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("5 - Total disponível no banco");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("6 - Conta corrente -- disponivel apenas ao cliente");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
            Console.Write("Digite a alternativa desejada: ");
        }

        static void ErrorMessageInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" XXXXX Parece que você informou um valor não válido XXXXX");
            Console.ResetColor();
        }

        static void GeneateID()
        {
            do
            {
                if (titulares.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("     Tipos de sequencia   : ");
                    Console.WriteLine("     aleatoria   (1)");
                    Console.WriteLine("     Nao aleatoria   (2)");
                    Console.Write("Digite o número da identificação : ");
                    Console.ResetColor();

                    idConfiguration = int.Parse(Console.ReadLine());
                }
                if (idConfiguration == 1)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    ids.Add(finalString);
                }
                else if (idConfiguration == 2)
                {
                    string sequential = Convert.ToString(count++);

                    ids.Add(sequential);
                }
                else
                {
                    Console.WriteLine();
                    ErrorMessageInput();
                    Console.WriteLine();
                }
            } while (idConfiguration != 1 && idConfiguration != 2);
        }

        static void CreateNewUser()
        {
            Console.WriteLine();
            GeneateID();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(" Para criar um novo usuário, nos forneça seus dados ");
            Console.Write("Digite o nome do titular:      ");
            Console.ResetColor();
            string titular = Console.ReadLine();
            titulares.Add(titular);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Digite numeros do CPF:  ");
            Console.ResetColor();
            string cpf = Console.ReadLine();
            cpfs.Add(cpf);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Digite o telefone:   +55  62  9");
            Console.ResetColor();
            string telefone = Console.ReadLine();
            telefones.Add(telefone);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Qual o saldo inicial:      R$");
            Console.ResetColor();
            double saldoInicial = double.Parse(Console.ReadLine());
            saldo.Add(saldoInicial);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Confirme seu cadastro . (1 para sim 2 para nao) ");
            Console.ResetColor();
            string confirmacao = Console.ReadLine().ToLower();

            Console.ForegroundColor = ConsoleColor.Blue;
            if (confirmacao == "1")
            {
                Console.WriteLine();
                Console.WriteLine("Usuário registrado com sucesso!");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                titulares.RemoveAt(titulares.Count - 1);
                cpfs.RemoveAt(cpfs.Count - 1);
                telefones.RemoveAt(telefones.Count - 1);
                saldo.RemoveAt(saldo.Count - 1);
                ids.RemoveAt(ids.Count - 1);
                Console.WriteLine();
                Console.WriteLine("Cadastro cancelado.");
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void DeleteUser()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write("Digite o CPF: ");
            string CPFtoDelete = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFtoDelete);

            if (indexToFind == -1)
            {
                Console.WriteLine();
                ErrorMessageInput();
            }
            else
            {
                titulares.RemoveAt(indexToFind);
                cpfs.RemoveAt(indexToFind);
                telefones.RemoveAt(indexToFind);
                saldo.RemoveAt(indexToFind);

                Console.WriteLine();
                Console.WriteLine("Usuário deletado com sucesso!");
                Console.WriteLine();
                Console.WriteLine();

            }
            Console.ResetColor();
        }

        static void ShowAllUser()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            int indexOfList = titulares.Count();
            Console.WriteLine($"Total de contas registradas no sistema: {indexOfList} ");
            Console.WriteLine("===========================================================");

            for (int i = 0; i < indexOfList; i++)
            {
                Console.WriteLine($"             > {titulares[i]}");
                Console.WriteLine($"               {cpfs[i]}");
                Console.WriteLine($"               {ids[i]}");
                Console.WriteLine("====================================");
            }
            Console.ResetColor();
        }

        static void ShowInfosUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Digite o CPF do usuário para exibir os detalhes: ");
            string cpf = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(cpf);
            if (indexToFind == -1)
            {
                Console.WriteLine("CPF inválido");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("     Titular: " + titulares[indexToFind]);
                Console.WriteLine("     CPF: " + cpfs[indexToFind]);
                Console.WriteLine("     Telefone: " + telefones[indexToFind]);
                Console.WriteLine("     Saldo: " + saldo[indexToFind].ToString("C"));
                Console.WriteLine("     ID: " + ids[indexToFind]);

                Console.ResetColor();
            }
        }

        static void AllValueStored()
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Total armazenado no banco: R${saldo.Sum():F2}");

            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();
        }

        static string messageOptionCheckingAccount()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("             Atenção            ");
            Console.ResetColor();
            Console.WriteLine(" Essa opção esta disponivel para clientes ");
            Console.WriteLine(" logue novamente como cliente ");
            Console.Write(" entrar como cliente ? ( s para sim n para não ): ");

            string checkLogin = Console.ReadLine();

            if (checkLogin == "s")
            {
                option = 0;
                return checkLogin;
            }
            return "n";
        }

        static void showIntroBankToAdmin()
        {

            string checkLogin = "n";
            Console.Write(" Informe a senha: ");
            adminKey = 321;
            getAdminKey = int.Parse(Console.ReadLine());
            if (getAdminKey == adminKey)
            {
                Console.WriteLine($" {name}, você tem autorização às seguintes ações ");
                Console.WriteLine();

                do
                {
                    ShowOptionsAdmin();
                    option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            CreateNewUser();
                            break;
                        case 2:
                            DeleteUser();
                            break;
                        case 3:
                            ShowAllUser();
                            break;
                        case 4:
                            ShowInfosUser();
                            break;
                        case 5:
                            AllValueStored();
                            break;
                        case 6:
                            checkLogin = messageOptionCheckingAccount();
                            break;
                        default:
                            Console.WriteLine("Opção inválida, tente novamente");
                            break;
                    }
                } while (option != 0);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" logout de admin feito com sucesso ");
                Console.ResetColor();

                if (checkLogin == "s")
                {
                    Main(argsMain);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Algo deu errado, certifique que a senha esteja correta");
                Console.ResetColor();

                showIntroBankToAdmin();
            }
        }


        static void ShowOptionsUser()
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("===========================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  1 - Saque  ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("  2 - Deposito  ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("  3 - Transferencia ");
                Console.ResetColor();

                Console.WriteLine("  0 - Encerrar ");
                Console.WriteLine();
                Console.Write("Digite a alternativa desejada: ");
                optionToCheckingAccount = int.Parse(Console.ReadLine());

                switch (optionToCheckingAccount)
                {
                    case 1:
                        Withdraw();
                        break;
                    case 2:
                        Deposit();
                        break;
                    case 3:
                        Transfer();
                        break;
                }
            } while (optionToCheckingAccount != 0);
            Console.WriteLine("A sessão foi encerrada.");
        }

        static void Withdraw()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Insira seu CPF para prosseguir : ");
            Console.ResetColor();
            string CPFWithdraw = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int indexToFind = cpfs.IndexOf(CPFWithdraw);
            if (indexToFind == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.Write("Qual o valor: R$");
                Console.ResetColor();
                double valueToWithdraw = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToWithdraw > saldo[indexToFind])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        Console.ResetColor();
                        tryAgainOrNot = Console.ReadLine();
                    }
                    else
                    {
                        saldo[indexToFind] -= valueToWithdraw;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Sacado realizado com sucesso,seu saldo atual é de : {saldo[indexToFind]:F2}");
                        tryAgainOrNot = "n";
                    }

                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: ");
                        Console.ResetColor();
                        valueToWithdraw = double.Parse(Console.ReadLine().ToLower());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void Deposit()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Insira seu CPF para processeguir a ação: ");
            Console.ResetColor();
            string CPFDeposit = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            int indexToFind = cpfs.IndexOf(CPFDeposit);
            if (indexToFind == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.Write("Informe o valor: R$");
                Console.ResetColor();

                double valueToDeposit = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToDeposit > saldo[indexToFind])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        Console.ResetColor();

                        tryAgainOrNot = Console.ReadLine();
                    }
                    else
                    {
                        saldo[indexToFind] += valueToDeposit;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"Depositado com sucesso, agora o saldo atual é: {saldo[indexToFind]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        Console.ResetColor();

                        valueToDeposit = double.Parse(Console.ReadLine().ToLower());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void Transfer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Insira seu CPF para prosseguir a ação: ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string CPFTransferUser1 = Console.ReadLine();
            int indexUser1 = cpfs.IndexOf(CPFTransferUser1);
            if (indexUser1 == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.Write("Insira o CPF do destinatário: ");
                Console.ResetColor();

                string CPFTransferUser2 = Console.ReadLine();
                int indexUser2 = cpfs.IndexOf(CPFTransferUser2);
                if (indexUser2 == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Usuario não localizado");
                }

                Console.Write("Informe o valor: R$");
                Console.ResetColor();

                double valueToTransfer = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToTransfer > saldo[indexUser1])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        Console.ResetColor();

                        tryAgainOrNot = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        saldo[indexUser1] -= valueToTransfer;
                        saldo[indexUser2] += valueToTransfer;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Transferido com sucesso, agora o seu saldo atual é: R${saldo[indexUser1]:F2}");
                        Console.WriteLine($"E o saldo atual de {titulares[indexUser2]} é: R${saldo[indexUser2]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        Console.ResetColor();

                        valueToTransfer = double.Parse(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void showIntroBankToUser()
        {
            do
            {
                Console.Write(" Informe a senha: ");
                clientKey = 111;
                getClientKey = int.Parse(Console.ReadLine());

                if (getClientKey == clientKey)
                {
                    Console.WriteLine($" {name}, você tem autorização às seguintes ações");
                    Console.WriteLine();

                    ShowOptionsUser();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Encerrado");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Algo deu errado, certifique que a senha esteja correta ");
                    Console.ResetColor();
                }
            } while (getClientKey != clientKey);
        }

        static void Main(string[] args)
        {
            argsMain = args;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Banco Big ");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write(" Qual seu nome ? (a) : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            name = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine($" Bem vindo ao Banco Big : {name}  ");
            Console.WriteLine($" você é um Admin ou um  cliente ? ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            role = Console.ReadLine().ToLower();
            Console.ResetColor();
            Console.WriteLine();

            if (role == "admin")
            {
                showIntroBankToAdmin();
            }
            else if (role == "cliente")
            {
                showIntroBankToUser();
            }
            else if (role != "cliente" || role != "admin")
            {
                Console.WriteLine("Algo deu errado, certifique que esta escrevendo corretamente");
                Console.Write("admin ou cliente : ");

                Main(args);
            }
        }
    }
}