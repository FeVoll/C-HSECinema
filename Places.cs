class Places
{
    public void PlacePrint(int NSeat, int MSeat, string[,] AvailableSeats)
    {
        //Выводит доступные места
        Console.WriteLine("Доступные места (0 - место доступно, x - место занято):");
        int kr = 1;
        for (int a = 0; a < NSeat; a++)
        {

            for (var b = 0; b < MSeat; b++)
            {
                if (kr % MSeat == 0)
                {
                    Console.WriteLine(AvailableSeats[a, b] + " ");
                }
                else
                {
                    Console.Write(AvailableSeats[a, b] + " ");
                }
                kr++;
            }

        }
    }
    public void BuyAndCheck(ref double UserBalance, int[,] Prices, string[,] FreeSeats, ref int totalMoney, ref int totalSell, ref int totalFree, ref List<string> cartfimlslist, ref List<DateTime> carttimelist, string filmname, DateTime filmtime, ref double TotalSpend)
    {
        //Покупка места с проверкой его наличия
        Console.WriteLine("Выберите место, которое хотите купить: (В формате n m, где n - ряд m - место)");
        string ChosenPlaseToList = Console.ReadLine();
        string[] TempChosenPlase = ChosenPlaseToList.Split(' ');
        var ChosenPlase = TempChosenPlase.Select(int.Parse).ToList();
        int ChosenNSeat = ChosenPlase[0];
        int ChosenMSeat = ChosenPlase[1];

        Console.WriteLine($"Вы выбрали место за {Prices[ChosenNSeat - 1, ChosenMSeat - 1]}");

        if (Prices[ChosenNSeat - 1, ChosenMSeat - 1] <= UserBalance && FreeSeats[ChosenNSeat - 1, ChosenMSeat - 1] == "0")
        {
            Console.WriteLine("Место куплено");
            UserBalance = UserBalance - Prices[ChosenNSeat - 1, ChosenMSeat - 1];
            FreeSeats[ChosenNSeat - 1, ChosenMSeat - 1] = "x";
            totalMoney += Prices[ChosenNSeat - 1, ChosenMSeat - 1];
            TotalSpend += Prices[ChosenNSeat - 1, ChosenMSeat - 1];
            totalSell++;
            totalFree--;
            cartfimlslist.Add(filmname);
            carttimelist.Add(filmtime);
        }
        else
        {
            Console.WriteLine("Место занято или на счету недостаточно денег");
        }
    }


}



