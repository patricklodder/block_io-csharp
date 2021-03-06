﻿using BlockIoLib;
using dotenv.net;
using dotenv.net.Utilities;
using System;
using System.IO;
using System.Text;

namespace MaxWithdrawal
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory());
            path = Path.GetFullPath(path) + "/.env";
            DotEnv.Config(true, path);
            DotEnv.Config(true, path, Encoding.Unicode, false);
            EnvReader envReader = new EnvReader();

            BlockIo blockIo = new BlockIo(envReader.GetStringValue("API_KEY"), envReader.GetStringValue("PIN"));

            var balance = blockIo.GetBalance().Data.available_balance;

            Console.WriteLine("Balance: " + balance);

            while (true)
            {
                var res = blockIo.Withdraw(new { to_address = envReader.GetStringValue("TO_ADDRESS"), amount = balance.ToString() });
                double maxWithdraw = res.Data.max_withdrawal_available;

                Console.WriteLine("Max Withdraw Available: " + maxWithdraw.ToString());

                if (maxWithdraw == 0) break;
                blockIo.Withdraw(new { to_address = envReader.GetStringValue("TO_ADDRESS"), amount = maxWithdraw.ToString() });
            }

            balance = blockIo.GetBalance().Data.available_balance;

            Console.WriteLine("Final Balance: " + balance);
        }
    }
}
