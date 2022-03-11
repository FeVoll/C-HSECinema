class film
{
    private string Hallfilmname;
    private List<string> Halls;
    private string Hallage;

    public film(string filmname, List<string> halls, string age)
    {
        Hallfilmname = filmname;
        Halls = halls;
        Hallage = age;
    }


    public void GetInfo()
    {
        Console.WriteLine("Название фильма - " + Hallfilmname);
        Console.WriteLine("Возрастной рейтинг фильма - " + Hallage);
        Console.WriteLine("Идет в залах");
        for (int i = 0; i < Halls.Count; i++)
        {
            Console.WriteLine(Halls[i]);
        }
    }
    public string GetName()
    {
        return Hallfilmname;
    }

    public void editAge(string age)
    {
        Hallage = age;
    }
    public void editName(string name)
    {
        Hallfilmname = name;
    }


}








class hall
{
    public List<DateTime> times;
    private string hallname;
    private string filminhall;
    private int NPlace;
    private int MPlaces;
    public int[,] hallprices;
    public int totalmoney = 0;
 

    public hall(string hall, string film, int N, int M, int[,] prices, List<DateTime> seanses)
    {
        times = seanses;
        hallname = hall;
        hallprices = prices;
        NPlace = N;
        MPlaces = M;
        filminhall = film;

    }


    public void GetInfo()
    {
        Console.WriteLine();

        Console.WriteLine("Название зала - " + hallname);
        Console.WriteLine("Фильм идущий в зале - " + filminhall);

        

        Console.WriteLine();

    }

    public void GetPrices()
    {
        Admin admin = new Admin();
        admin.PricePrint(NPlace, MPlaces, hallprices);
    }

    public string GetFilmName()
    {
        return filminhall;
    }
    public string GetHallName()
    {
        return hallname;
    }

    public void EditFilmName(string newfilm)
    {
        filminhall = newfilm;
    }

    public void GetHallSeanses()
    {
        Console.WriteLine("Время сеансов для зала " + hallname + " (Фильм " + filminhall + ")");
        for (int i = 0; i < times.Count; i++)
        {
            if (times[i] >= DateTime.Now)
            {
                Console.WriteLine(times[i]);
            }

        }

    }
    public void editName(string name)
    {
        filminhall = name;
    }

}







class seanse
{
    private string Hallname;

    public string[,] Places;
    public int[,] Prices;
    public int NPlace;
    public int MPlaces;
    public int totalFree;
    public DateTime time;
    private string name;
    public int totalSell = 0;

    public seanse(string filmname, string hallname, DateTime seanstime, string[,] places, int[,] prices,int N, int M, int total)
    {
        
        name = filmname;
        Places = places;
        Prices = prices;
        Hallname = hallname;
        time = seanstime;
        NPlace = N;
        MPlaces = M;
        totalFree = total;
    }

    public void GetInfo()
    {
        Console.WriteLine();

        Console.WriteLine("Название фильма - " + name);
        Console.WriteLine("Название зала - " + Hallname);
        Console.WriteLine("Время сенса - " + time);

        Places places = new Places();
        places.PlacePrint(NPlace, MPlaces, Places);
        Console.WriteLine("Всего свободных мест " + totalFree);
        Console.WriteLine("Всего занятых мест " + (NPlace*MPlaces - totalFree));
        Console.WriteLine("Цены на места");

        Admin admin = new Admin();
        admin.PricePrint(NPlace, MPlaces, Prices);

        Console.WriteLine();

    }
    public void editName(string newname)
    {
        name = newname;
    }

    public string GetFilm()
    {
        return name;
    }

    public DateTime GetTime()
    {
        return time;
    }

    public int GetN()
    {
        return NPlace;
    }
    public int GetM()
    {
        return MPlaces;
    }
    public int GetTotal()
    {
        return totalFree;
    }

    public int GetAllPlaces()
    {
        return NPlace * MPlaces;
    }

    public int[,] GetPrices()
    {
        return Prices;
    }
    public string[,] GetPlaces()
    {
        return Places;
    }
    public string GetHall()
    {
        return Hallname;
    }
}





class user
{
    private string username;

    public double balance = 0;
    public List<string> cartfilms = new List<string>();
    public List<DateTime> carttimes = new List<DateTime>();
    public List<string> food = new List<string>();
    public double totalSpend;

    public user(string user)
    {
        username = user;

    }
    public int GetCountTickets()
    {
        return cartfilms.Count;
    }
    public string GetUsername()
    {
        return username;
    }

    public double GetBalance()
    {
        return balance;
    }

    public double GetTotalSpend()
    {
        return totalSpend;
    }

    public void UpBalance(double amount)
    {
        balance += amount;
    }

    public void GetCart()
    {
        Console.WriteLine("Купленные билеты");
        for (int i = 0; i < cartfilms.Count; i++)
        {
            Console.WriteLine("Фильм: " + cartfilms[i] + "      Время: " + carttimes[i]);
        }
    }

    public List<string> GetCartFilms()
    {
        return cartfilms;
    }
    public List<string> GetCartFood()
    {
        return food;
    }

    public void Welcome()
    {
        Console.WriteLine();
        Console.WriteLine("Добро пожаловать, " + username + " !");
        Console.WriteLine("Ваш баланс " + balance + " рублей.");
    }
}