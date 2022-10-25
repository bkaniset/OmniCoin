// See https://aka.ms/new-console-template for more information
using System;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;

namespace _125350929
{
    class Program
    {
        static void Main(string[] args)
        {
            SpentCoinsSum();
            Console.WriteLine("--------------------------------------");
            RecievedCoinsSum();
            
        }

        // Get the specifics of CoinBaseTransactionSpecifics. 
        public static void CoinBaseTransactionSpecifics()
        {
            var transactionId = FindInitalCoinBaseTransaction();
            //NBitcoin.Transaction transaction = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94"); 
            //GetTransactionResponse respose = transaction.
        }

        public static void RecievedCoinsSum()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            Money money = Money.Zero;

            var spentcoints = response.ReceivedCoins;

            foreach (var coin in spentcoints)
            {
                money = (Money)coin.Amount.Add(money);
            }
            Console.WriteLine(money.ToDecimal(MoneyUnit.BTC));


        }

        public static void SpentCoinsSum()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            Money money = Money.Zero; 

            var spentcoints = response.SpentCoins;

            foreach(var coin in spentcoints)
            {
                money = (Money)coin.Amount.Add(money);
            }    
            Console.WriteLine(money.ToDecimal(MoneyUnit.BTC));
 
        }

        public static uint256 FindInitalCoinBaseTransaction()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            while(transaction.IsCoinBase == false)
            {
                
                OutPoint previousOutPoint = transaction.Inputs[0].PrevOut;
                transaction = qBitNinjaClient.GetTransaction(previousOutPoint.Hash).Result.Transaction;
                Console.WriteLine(transaction.IsCoinBase);
            
            }
            Console.WriteLine(transaction.IsCoinBase);
            Console.WriteLine(transaction.GetHash());
            return transaction.GetHash();

        }

        public static void CreateTrnxUsingTxOut()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            Money twentyOneBTC = new Money(21, MoneyUnit.BTC);
            var scriptPubKey = transaction.Outputs[0].ScriptPubKey;

        }

        public static void TxInCoinsNBit()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            var inputs = transaction.Inputs;

            foreach(TxIn input in inputs)
            {
                OutPoint outpoint = input.PrevOut;
                Console.WriteLine(outpoint.Hash);
                Console.WriteLine(outpoint.N);
                Console.WriteLine();
            }
        }

        public static void TxOutCoinsUnitNBit()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            var outputs = transaction.Outputs;

            foreach(TxOut output in outputs)
            {
                Money amount = output.Value;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC));
                var paymentScript = output.ScriptPubKey;
                Console.WriteLine(paymentScript);
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address);
                Console.WriteLine();
            }
        }

        


        public static void SpentCoinsExercise()
        {
            QBitNinjaClient qBitNinjaClient = new QBitNinjaClient(Network.Main);

            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse response = qBitNinjaClient.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = response.Transaction;

            List<ICoin> recievedCoins = response.SpentCoins;

            foreach (Coin coin in recievedCoins)
            {

                Money money = (Money)coin.Amount;

                Console.WriteLine(money.ToDecimal(MoneyUnit.BTC));
                var paymentScript = coin.TxOut.ScriptPubKey;
                Console.WriteLine("Script PubKey. " + paymentScript);
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address);
                Console.WriteLine();

            }

        }

    }
}