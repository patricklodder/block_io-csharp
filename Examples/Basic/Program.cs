﻿using BlockIoLib;
using dotenv.net;
using dotenv.net.Utilities;
using System;
using System.IO;
using System.Text;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory());
            path = Path.GetFullPath(path) + "/.env";
            DotEnv.Config(true, path);
            DotEnv.Config(true, path, Encoding.Unicode, false);
            var envReader = new EnvReader();

            BlockIo blockIo = new BlockIo(envReader.GetStringValue("API_KEY"), envReader.GetStringValue("PIN"));

            Console.WriteLine("Get New Address: " + blockIo.GetNewAddress(new { label = "testDest2" }).Data);
            Console.WriteLine("Withdraw from labels: " + blockIo.WithdrawFromLabels(new { from_labels = "testDest2", to_label = "default", amount = "0.00061440" }).Data);
            Console.WriteLine("Get Address Balance: " + blockIo.GetAddressBalance(new { labels = "default,testDest2" }).Data);
            Console.WriteLine("Get Sent Transactions: " + blockIo.GetTransactions(new { type = "sent" }).Data);
            Console.WriteLine("Get Received Transactions: " + blockIo.GetTransactions(new { type = "received" }).Data);
            Console.WriteLine("Get Current Price: " + blockIo.GetCurrentPrice(new { base_price = "BTC" }).Data);
        }
    }
}
