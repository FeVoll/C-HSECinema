class Admin
{
    public void Edit(int[,] Prices)
    {
        //Покупка места с проверкой
        Console.WriteLine("Выберите место, которое хотите изменить: (В формате n m, где n - ряд m - место)");
        string ChosenPlaseToList = Console.ReadLine();
        string[] TempChosenPlase = ChosenPlaseToList.Split(' ');
        var ChosenPlase = TempChosenPlase.Select(int.Parse).ToList();
        int ChosenNSeat = ChosenPlase[0];
        int ChosenMSeat = ChosenPlase[1];

        Console.WriteLine($"Введите новую цену");
        Prices[ChosenNSeat - 1, ChosenMSeat - 1] = int.Parse(Console.ReadLine());
    }

    public void PricePrint(int NSeats, int MSeats, int[,] Prices)
    {
        //Выводит цены на места
        int kr = 1;
        for (int a = 0; a < NSeats; a++)
        {

            for (var b = 0; b < MSeats; b++)
            {
                if (kr % MSeats == 0)
                {
                    Console.WriteLine(Prices[a, b] + " ");
                }
                else
                {
                    Console.Write(Prices[a, b] + " ");
                }
                kr++;
            }

        }
    }



    public string[,] NewSession(int NSeat, int MSeat) //Формирует пустой зал исходя из заданых рядов (N и M)
    {
        var AvailableSeats = new string[NSeat, MSeat];
        for (var i = 0; i < NSeat; i++)
        {
            for (var j = 0; j < MSeat; j++)
            {
                AvailableSeats[i, j] = "0";
            }
        }
        return AvailableSeats;
    }

}