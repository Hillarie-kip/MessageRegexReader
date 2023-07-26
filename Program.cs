using System;
using System.IO;
using System.Text.RegularExpressions;




public class MessageDataExtractor
{
    public static void Main()
    {
        
        string message = "RGO7AAY2AB Confirmed on 24/7/23 at 7:17 AM Ksh500.00 received from 400088-CO-OP TO TILL:FRANKPAT MOTORS::AB05BD697723. New Account balance is Ksh884,477.35.";
        string message2 = "RGO6CW7U5U Confirmed.on 24/7/23 at 10:45 PMKsh25.00 received from 254722811960 VIDESIOUS MURAGE KAGIRI. New Account balance is Ksh128,141.00. Transactioncost, Ksh0.00.".Replace("Transactioncost", "Transaction cost");

        TransModel extractedData = ExtractData(message2);

        Console.WriteLine(extractedData);
    }

    public static TransModel ExtractData(string message)
    {

        string patternPhoneToMpesaTill = @"^([A-Z0-9]+)\sConfirmed\.on\s(\d{1,2}/\d{1,2}/\d{2})\sat\s(\d{1,2}:\d{2}\s[APM]{2})Ksh(\d{1,3}(,\d{3})*(\.\d{2})?)\sreceived\sfrom\s(\d+)\s([A-Za-z\s]+)\.\sNew\sAccount\sbalance\sis\sKsh(\d{1,3}(,\d{3})*(\.\d{2})?)\.\sTransaction\scost,\sKsh(\d+(\.\d{2})?)\.$";
        string patternNCBAToMpesaTill = @"^([A-Z0-9]+)\sConfirmed\.on\s(\d{1,2}/\d{1,2}/\d{2})\sat\s(\d{1,2}:\d{2}\s[APM]{2})Ksh(\d{1,3}(,\d{3})*(\.\d{2})?)\sreceived\sfrom\s(\d+)\s([A-Za-z\s]+)\.\sNew\sAccount\sbalance\sis\sKsh(\d{1,3}(,\d{3})*(\.\d{2})?)\.\sTransaction\scost,\sKsh(\d+(\.\d{2})?)\.$";




        if (Regex.IsMatch(message, patternPhoneToMpesaTill))
        {
            Match match = Regex.Match(message, patternPhoneToMpesaTill);
        
            return ProcessPhoneToMpesaTill(match);
        }


        else if (Regex.IsMatch(message, patternNCBAToMpesaTill))
        {
            Match match = Regex.Match(message, patternNCBAToMpesaTill);
            return ProcessNCBAToMpesaTill(match);
        }
        else
        {
            TransModel transModel = new TransModel();
            transModel.Message = "Message format does not match any known pattern.";
            return transModel;
        }
    }

    public class TransModel
    {
        public string TransID { get; set; }
        public string TransDate { get; set; }
        public string TransTime { get; set; }
        public decimal AmountReceived { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal TransactionCost { get; set; }
        public string Message { get; set; }

    }


    public static TransModel ProcessPhoneToMpesaTill(Match match)
    {
        // Implement the logic to extract and format data for format 1 here
        string transactionId = match.Groups[1].Value;
        string date = match.Groups[2].Value;
        string time = match.Groups[3].Value;
        decimal amountReceived = decimal.Parse(match.Groups[4].Value.Replace(",", ""));
        string senderPhoneNumber = match.Groups[7].Value;
        string senderName = match.Groups[8].Value;
        decimal newAccountBalance = decimal.Parse(match.Groups[9].Value.Replace(",", ""));
        decimal transactionCost = decimal.Parse(match.Groups[11].Value);

        TransModel transModel = new TransModel();
        transModel.TransID = transactionId;
        transModel.TransDate = date;
        transModel.TransTime = time;
        transModel.AmountReceived = amountReceived;
        transModel.AccountBalance = newAccountBalance;
        transModel.UserName = senderName;
        transModel.PhoneNumber = senderPhoneNumber;
        transModel.TransactionCost = transactionCost;
        transModel.Message = "Matching Phone To Mpesa Till Message";

        return transModel;
    }
    public static TransModel ProcessNCBAToMpesaTill(Match match)
    {
        // Implement the logic to extract and format data for format 1 here
        string transactionId = match.Groups[1].Value;
        string date = match.Groups[2].Value;
        string time = match.Groups[3].Value;
        decimal amountReceived = decimal.Parse(match.Groups[4].Value.Replace(",", ""));
        string senderPhoneNumber = match.Groups[7].Value;
        string senderName = match.Groups[8].Value;
        decimal newAccountBalance = decimal.Parse(match.Groups[9].Value.Replace(",", ""));
        decimal transactionCost = decimal.Parse(match.Groups[11].Value);

        TransModel transModel = new TransModel();
        transModel.TransID = transactionId;
        transModel.TransDate = date;
        transModel.TransTime = time;
        transModel.AmountReceived = amountReceived;
        transModel.AccountBalance = newAccountBalance;
        transModel.UserName = senderName;
        transModel.PhoneNumber = senderPhoneNumber;
        transModel.TransactionCost = transactionCost;
        transModel.Message = "Matching NCBA To Mpesa Till Message";

        return transModel;
    }

    public static string ProcessFormat2(Match match)
    {
        string transactionId = match.Groups[1].Value;
        string date = match.Groups[2].Value;
        string time = match.Groups[3].Value;
        decimal amountReceived = decimal.Parse(match.Groups[4].Value.Replace(",", ""));
        string phoneNumber = match.Groups[5].Value;
        string senderInfo = match.Groups[6].Value;
        string newAccountBalance = match.Groups[7].Value;

        Console.WriteLine($"Transaction ID: {transactionId}");
        Console.WriteLine($"Date: {date}");
        Console.WriteLine($"Time: {time}");
        Console.WriteLine($"Amount Received: {amountReceived:F2}");
        Console.WriteLine($"PhoneNumber: {phoneNumber}");
        Console.WriteLine($"Sender Information: {senderInfo}");
        Console.WriteLine($"New Account Balance: {newAccountBalance}");

        return transactionId;

    }
}



















