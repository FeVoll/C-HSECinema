//Алина прости, код из говна и палок, не хватало времени красоту навести с коментами как в прошлой дз))))) Удачной проверки!

List<film> films = new List<film>();
List<hall> halls = new List<hall>();
List<seanse> seanses = new List<seanse>();
List<user> users = new List<user>();
int filmsnum;
string age;
string hallname;
string filmname;
int[,] prices;
int totalFree;
int totalSell;
double UserBalance;
string Username = "";
int totalmoney = 0;
DateTime now  = DateTime.Now;
void newfilm()
{
    //Запришиваем инфу фильмов
    Console.WriteLine("Сколько всего фильмов");
    filmsnum = int.Parse(Console.ReadLine());
    for (int i = 0; i < filmsnum; i++)
    {
        string[,] places;
        List<string> hallstemp = new List<string>();
        Console.WriteLine("Введите название фильма номер " + (i+1));
        filmname = Console.ReadLine();
        Console.WriteLine("Выберите возрастной рейтинг фильма");
        Console.WriteLine("0+(1)/6+(2)/12+(3)/16+(4)/18+(5)");
        while (true)
        {
            try
            {
                int agechoose = int.Parse(Console.ReadLine());
                if (agechoose == 1)
                {
                    age = "0+";
                    break;
                }
                else if (agechoose == 2)
                {
                    age = "6+";
                    break;
                }
                else if (agechoose == 3)
                {
                    age = "12+";
                    break;
                }
                else if (agechoose == 4)
                {
                    age = "16+";
                    break;
                }
                else if (agechoose == 5)
                {
                    age = "18+";
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
            catch
            {
                Console.WriteLine("Неверный ввод");
            }
        }
        Console.WriteLine("В скольки залах показыают фильм?");
        int hallsforfilm = int.Parse(Console.ReadLine());
        for (int j = 0; j < hallsforfilm; j++)
        {
            List<DateTime> seansestime = new List<DateTime>();

            Console.WriteLine("Введите имя зала №" + j+1);
            hallname = Console.ReadLine();

            Console.WriteLine("Размер зала");
            string hallsize = Console.ReadLine();
            string[] TempSeatsList = hallsize.Split(' ');
            var SeatsList = TempSeatsList.Select(int.Parse).ToList();
            int N = SeatsList[0];
            int M = SeatsList[1];
            totalFree = M * N;//Общеее количество мест

            Console.WriteLine("Дефолтные цены на места в зале");
            var P = new int[N, M];
            for (var r = 0; r < N; r++)
            {
                var seatList = new string[N];
                seatList = Console.ReadLine().Split();
                for (var l = 0; l < M; l++)
                {
                    P[r, l] = Convert.ToInt32(seatList[l]);

                }
            }
            prices = P;

            Admin admin = new Admin();
            places = admin.NewSession(N, M);

            Console.WriteLine("количество слотов на фильм");
            int seansescount = int.Parse(Console.ReadLine());
            for (int k = 0; k < seansescount; k++)
            {
                DateTime seanstime = DateTime.Parse(Console.ReadLine());
                //Добавляем в класс сеансес
                seansestime.Add(seanstime);
                seanses.Add(new seanse(filmname,hallname, seanstime, places, prices, N, M, totalFree));
            }




            //Добавляем в класс холс
            hallstemp.Add(hallname);
            halls.Add(new hall(hallname, filmname, N, M, prices, seansestime));

        }
        //Добавляем в класс фильмс
        films.Add(new film(filmname, hallstemp, age));
    }

}

void Menu()
{
    Console.WriteLine("Выберите действие");
    Console.WriteLine("1) Войти в юзер панель");
    Console.WriteLine("2) Войти в админпанель");
    int nextstep = int.Parse(Console.ReadLine());
    if (nextstep == 1)
    {
        Console.WriteLine("Введи ваш логин (или ФИО). В дальшейшем будете использовать его для входа в ЛК");
        string username = Console.ReadLine();
        int kr = 0;
        //Проверяем новый пользователь или уже есть
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].GetUsername() == username)
            {
                kr++;
            }
        }
        //Если новый то добаляем элемент в класс
        if (kr == 0)
        {
            users.Add(new user(username));
        }
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].GetUsername() == username)
            {
                
                UserBalance = users[i].GetBalance();
                Username = users[i].GetUsername();
                users[i].Welcome();
            }
        }
        UserMenu();
    }
    else if (nextstep == 2)
    {
        AdminMenu();
    }
}


void UserMenu()
{
    Console.WriteLine("Выберите действие");
    Console.WriteLine("1) Посмотреть информацию о фильмах");
    Console.WriteLine("2) Посмотреть и пополнить баланс");
    Console.WriteLine("3) Купить билет");
    Console.WriteLine("4) Посмотреть свои покупки");
    Console.WriteLine("5) Купить еду или напитки (Доп. Логика)");
    Console.WriteLine("6) Посмотреть свои покупки еды и напитков (Доп. Логика)");
    Console.WriteLine("0) Выйти обратно в меню");
    int nextstep = int.Parse (Console.ReadLine());
    if (nextstep == 1)
    {
        //Просто выводим список фильмов хз вообще зачем я это сделал но пусть для вида будет не зря же функцию писал
        for (int k = 0; k < films.Count; k++)
        {
            films[k].GetInfo();
        }

    }
    else if (nextstep == 2)
    {
        //Баланс изначально 0, но его можно пополнить 
        for (int k = 0; k < users.Count; k++)
        {
            if (Username == users[k].GetUsername())
            {
                Console.WriteLine("Ваш баланс " + users[k].GetBalance() + ".");
                Console.WriteLine("Введите на какую сумму хотите его пополнить");
                double sumtoalance = double.Parse (Console.ReadLine());
                users[k].UpBalance(sumtoalance);
                UserMenu();
            }
        }
    }
    else if (nextstep == 3)
    {
        string chosenfilmname;
        //Выводим доступные фильмы
        Console.WriteLine("Доступные фильмы:");
        for (int k = 0;k < films.Count; k++)
        {
            Console.WriteLine((k+1) + ") " + films[k].GetName());
        }
        Places places = new Places();
        //Получаем желаемый фильм
        Console.WriteLine("Введите номер фильма");
        int chosenfilm = int.Parse(Console.ReadLine());
        //Сохраняем в переменную название фильма
        for (int k = 0; k<films.Count; k++)
        {
            if (k == (chosenfilm - 1))
            {
                chosenfilmname = films[k].GetName();
                //Ищем залы где идет этот фильм и выводим инфу о времени
                for (int c = 0; c < halls.Count; c++)
                {
                    if (halls[c].GetFilmName() != chosenfilmname)
                    {
                        continue;
                    }
                    halls[c].GetHallSeanses();
                    string chosenhall = halls[c].GetHallName();
                }

                // Вводим желаемое время и ищем инфу о нем
                Console.WriteLine("Введите время, на которое хотите пойти");
                DateTime chosentime = DateTime.Parse(Console.ReadLine());
                for (int i = 0; i < seanses.Count; i++)
                {
                    if (seanses[i].GetTime() == chosentime && seanses[i].GetFilm() == chosenfilmname)
                    {
                        for (int j = 0; j < users.Count; j++)
                        {
                            if (users[j].GetUsername() == Username)
                            {
                                //Добавялем информацию везде куда надо
                                UserBalance = users[j].GetBalance();
                                places.PlacePrint(seanses[i].GetN(), seanses[i].GetM(), seanses[i].Places);
                                places.BuyAndCheck(ref users[j].balance, seanses[i].Prices, seanses[i].Places, ref totalmoney, ref seanses[i].totalSell, ref seanses[i].totalFree, ref users[j].cartfilms, ref users[j].carttimes,chosenfilmname,chosentime, ref users[j].totalSpend);
                                UserMenu();
                            }
                        }

                    }

                }
                Console.WriteLine("Такой сеанс не найден, попробуйте ещё раз");
                UserMenu();
            }
        }

    }
    else if (nextstep == 4)
    {
        //Просто принт корзину из класса по логину
        for (int i = 0;i < users.Count; i++)
        {
            if (users[i].GetUsername() == Username)
            {
                users[i].GetCart();
            }
        }
        UserMenu();
    }
    else if (nextstep == 5)//Даже коментить не буду всё очевидно
    {
        Food food = new Food();
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].GetUsername() == Username)
            {
                food.FoodShop(ref users[i].balance, ref users[i].food);
            }
        }
        UserMenu();

    }
    else if (nextstep == 6)//Аналогично
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].GetUsername() == Username)
            {
                List<string> foodListToShow = users[i].GetCartFood();
                Console.WriteLine("Ваши покупки:");
                for (int j = 0; j < foodListToShow.Count; j++)
                {
                    Console.WriteLine(foodListToShow[j]);
                }
            }
        }
        UserMenu();
    }
    else if (nextstep == 0)
    {
        Menu();
    }
}

//Вот тут самый ад))0))000 писал на лютых скоростях сориии
void AdminMenu()
{
    Console.WriteLine("");
    Console.WriteLine("Доступные действия:");
    Console.WriteLine("");
    Console.WriteLine("Фильмы");
    Console.WriteLine("1) Добавить новый фильм");
    Console.WriteLine("2) Удалить фильм");
    Console.WriteLine("3) Изменить информацию о фильме");
    Console.WriteLine("");
    Console.WriteLine("Залы");
    Console.WriteLine("4) Изменить цену на места");
    Console.WriteLine("");
    Console.WriteLine("Сеанцы");
    Console.WriteLine("5) Добавить сеанс в зал");
    Console.WriteLine("6) Изменить время сеанса");
    Console.WriteLine("7) Удалить сеанс");
    Console.WriteLine("");
    Console.WriteLine("Аналитика");
    Console.WriteLine("8) Посмотреть информацию о проданных местах");
    Console.WriteLine("9) Посмотреть топ клиентов");
    Console.WriteLine("10) Посмотреть кто купил больше всего билетов");
    Console.WriteLine("");
    Console.WriteLine("0) Выйти из меню админа");

    int nextstep = int.Parse(Console.ReadLine());
    if (nextstep == 0)
    {
        Menu();
    }
    else if (nextstep == 1)
    {
        //Вызов функции сверху
        newfilm();
    }

    else if (nextstep == 2)
    {
        Console.WriteLine("Какой фильм хотите удалить?    Внимание!!! Удалится так-же и зал, привязанный к фильму");
        for (int k = 0; k < films.Count; k++)
        {
            Console.WriteLine((k + 1) + ") " + films[k].GetName());
        }
        //Получаем желаемый фильм
        Console.WriteLine("Введите номер фильма");
        int chosenfilm = int.Parse(Console.ReadLine());
        //Получаем название фильма для дальшейших проверок
        string chosenfilmname = films[chosenfilm - 1].GetName();

        int deletecheck = 0;
        //Проверяем что нет такого фильма в купленных билетах))0 да легкий вариант
        for (int k = 0; k < users.Count; k++)
        {
            List<string> cartforcheck = users[k].GetCartFilms();
            for (int j = 0; j < cartforcheck.Count; j++)
            {
                if (cartforcheck[j] == chosenfilmname)
                {
                    deletecheck++;
                }
            }
        }
        //Если есть пишем пока если нет то погнали изменять удалять и че там ещё
        if (deletecheck != 0)
        {
            Console.WriteLine("Нельзя удалить данный фильм так как на него уже купленны билеты");
        }
        else
        {
            for (int k = 0; k < films.Count; k++)
            {
                if (films[k].GetName() == chosenfilmname)
                {
                    films.RemoveAt(k);
                }
            }
            for (int k = 0; k < seanses.Count; k++)
            {
                if (seanses[k].GetFilm() == chosenfilmname)
                {
                    seanses.RemoveAt(k);
                }
            }
            for (int k = 0; k < halls.Count; k++)
            {
                if (halls[k].GetFilmName() == chosenfilmname)
                {
                    halls.RemoveAt(k);
                }
            }
            Console.WriteLine("Фильм удален");
        }

        AdminMenu();
    }

    else if (nextstep == 3)
    {

        Console.WriteLine("Какой фильм хотите изменить?");
        for (int k = 0; k < films.Count; k++)
        {
            Console.WriteLine((k + 1) + ") " + films[k].GetName());
        }
        //Получаем желаемый фильм
        Console.WriteLine("Введите номер фильма");
        int chosenfilm = int.Parse(Console.ReadLine());
        //Получаем название фильма для дальшейших проверок
        string chosenfilmname = films[chosenfilm - 1].GetName();
        Console.WriteLine("Что вы хотите изменить?");
        Console.WriteLine("1) Название фильма");
        Console.WriteLine("2) Возрастной рейтинг");
        int choose = int.Parse(Console.ReadLine());
        if (choose == 1)
        {
            int deletecheck = 0;

            for (int k = 0; k < users.Count; k++)
            {
                List<string> cartforcheck = users[k].GetCartFilms();
                for (int j = 0; j < cartforcheck.Count; j++)
                {
                    if (cartforcheck[j] == chosenfilmname)
                    {
                        deletecheck++;
                    }
                }
            }

            if (deletecheck != 0)//Выводим аллерт на случай если реално в 1-2 букве ошибку сделали, не вводить же всё заново
            {
                Console.WriteLine("Билеты на данный фильм купленны, используйте данный пункт только если хотите незначительно поменять название!1!!!111");
            }
            Console.WriteLine("Введите новое название");
            //Получаем новое название
            string newname = Console.ReadLine();
            films[chosenfilm - 1].editName(newname);
            //Поехали изменять где только можно а тоесть везде 
            for (int k = 0; k < seanses.Count; k++)
            {
                if (seanses[k].GetFilm() == chosenfilmname)
                {
                    seanses[k].editName(newname);
                }
            }
            for (int k = 0; k < halls.Count; k++)
            {
                if (halls[k].GetFilmName() == chosenfilmname)
                {
                    halls[k].editName(newname);
                }
            }
            for (int k = 0; k < users.Count; k++)
            {
                List<string> cartforcheck = users[k].GetCartFilms();
                for (int j = 0; j < cartforcheck.Count; j++)
                {
                    if (cartforcheck[j] == chosenfilmname)
                    {
                        cartforcheck[j] = newname;
                    }
                }
            }

        }
        else if (choose == 2)//Тут аналогично верхнему вроде всё читаемо 
        {
            try
            {
                string agetochange;
                Console.WriteLine("Выберите возрастной рейтинг фильма");
                Console.WriteLine("0+(1)/6+(2)/12+(3)/16+(4)/18+(5)");
                while (true)//У меня возраст выборкой идет через пунктики 
                {
                    try
                    {
                        int newage = int.Parse(Console.ReadLine());
                        if (newage == 1)
                        {
                            agetochange = "0+";
                            break;
                        }
                        else if (newage == 2)
                        {
                            agetochange = "6+";
                            break;
                        }
                        else if (newage == 3)
                        {
                            agetochange = "12+";
                            break;
                        }
                        else if (newage == 4)
                        {
                            agetochange = "16+";
                            break;
                        }
                        else if (newage == 5)
                        {
                            agetochange = "18+";
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Неверный ввод");
                    }
                }
                //Ну и само изменение прописал в классе там в 1 строку
                films[chosenfilm - 1].editAge(agetochange);
            }
            catch
            {
                Console.WriteLine("Ошибка");
            }
        }
    }

    else if (nextstep == 4)
    {
        Console.WriteLine("Изменить во всем зале (1) или на конкретный сеанс (2)?");
        int choose = int.Parse(Console.ReadLine());
        if(choose == 1)
        {

            //Выводим список залов и запрашивем какое место хотим изменить
            for (int k = 0;k < halls.Count; k++)
            {
                Console.WriteLine("Выберите зал который хотите изменить");
                Console.WriteLine(k+1 + ") " + halls[k].GetHallName());
                halls[k].GetPrices();
            }
            int chosenhall = int.Parse(Console.ReadLine());
            string chosenfilmname = halls[chosenhall - 1].GetFilmName();
            int deletecheck = 0;
            //Проверка
            for (int k = 0; k < users.Count; k++)
            {
                List<string> cartforcheck = users[k].GetCartFilms();
                for (int j = 0; j < cartforcheck.Count; j++)
                {
                    if (cartforcheck[j] == chosenfilmname)
                    {
                        deletecheck++;
                    }
                }
            }

            if (deletecheck != 0)
            {
                Console.WriteLine("Нельзя изменить данный зал так как в него уже купленны билеты");
            }
            else
            {
                for (int j = 0; j < halls.Count; j++)
                {
                    if (halls[chosenhall - 1].GetHallName() == halls[j].GetHallName())
                    {
                        Admin admin = new Admin();
                        admin.Edit(halls[j].hallprices);
                    }
                }
                for (int k = 0; k < seanses.Count; k++)
                {
                    if (halls[chosenhall - 1].GetHallName() == seanses[k].GetHall())
                    {
                        seanses[k].Prices = halls[chosenhall - 1].hallprices;
                    }
                }
            }
            AdminMenu();
                

        }
        else if (choose == 2)
        {

            //Выводим список сенсов и запрашивем какое место хотим изменить

            while (true)
            {
                try
                {
                    for (int k = 0; (k < seanses.Count); k++)
                    {
                        Console.WriteLine(k + 1 + ") " + seanses[k].GetHall() + " " + seanses[k].GetFilm() + " " + seanses[k].GetTime());
                    }
                    int chosenseanse = int.Parse(Console.ReadLine());
                    string chosenfilmname = seanses[chosenseanse - 1].GetFilm();
                    int deletecheck = 0;

                    for (int k = 0; k < users.Count; k++)
                    {
                        List<string> cartforcheck = users[k].GetCartFilms();
                        for (int j = 0; j < cartforcheck.Count; j++)
                        {
                            if (cartforcheck[j] == chosenfilmname)
                            {
                                deletecheck++;
                            }
                        }
                    }

                    if (deletecheck != 0)
                    {
                        Console.WriteLine("Нельзя удалить данный фильм так как на него уже купленны билеты");
                    }
                    else
                    {
                        Admin admin = new Admin();
                        admin.PricePrint(seanses[chosenseanse - 1].GetN(), seanses[chosenseanse - 1].GetM(), seanses[chosenseanse - 1].GetPrices());
                        admin.Edit(seanses[chosenseanse - 1].Prices);
                        break;
                    }


                }
                catch
                {
                    Console.WriteLine("Ошибка ввода, убедитесь в верности введенных данных попробуйте ещё раз");
                }
            }

            AdminMenu();

        }


    }

    else if (nextstep == 5)
    {
        Console.WriteLine("Выберите зал в который хотите добавить сеанс");
        for (int k = 0; k < halls.Count; k++)
        {
            Console.WriteLine(k + 1 + ") Название фильма: " + halls[k].GetFilmName() + "Название зала: " + halls[k].GetHallName()); //Попов Виктор Андреевич 
        }
        int choosenhall = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите время которое хотите добавить");
        DateTime timetoadd = DateTime.Parse(Console.ReadLine());
        halls[choosenhall-1].times.Add(timetoadd);
        for (int k = 0; k < seanses.Count; k++)
        {
            if (seanses[k].GetHall() == halls[choosenhall-1].GetHallName())
            {
                //добавить проверку что нет такого сеанса
                int checktoadd = 0;
                for (int j = 0; j < seanses.Count; j++)
                {
                    if (seanses[j].GetTime() == timetoadd)
                    {
                        checktoadd++;
                    }
                }
                if (checktoadd != 0)
                {
                    Console.WriteLine("Такой сеанс в таком жале уже есть");
                    break;
                }
                else
                {
                    //Строчка длиною как кхм
                    seanses.Add(new seanse(seanses[k].GetFilm(), seanses[k].GetHall(), timetoadd, seanses[k].GetPlaces(), seanses[k].GetPrices(), seanses[k].GetN(), seanses[k].GetM(), seanses[k].GetTotal()));
                    break;
                }

            }   
        }
    }

    else if (nextstep == 6)
    {
        Console.WriteLine("Доступные сеансы:");
        for (int k = 0;k < seanses.Count; k++)
        {
            Console.WriteLine(k + 1 + ") " + seanses[k].GetTime() + " Название фильма: " + seanses[k].GetFilm());
        }
        int chooseseanse = int.Parse(Console.ReadLine());


        string chosenfilmname = seanses[chooseseanse - 1].GetFilm();
        int deletecheck = 0;

        for (int k = 0; k < users.Count; k++)
        {
            List<string> cartforcheck = users[k].GetCartFilms();
            for (int j = 0; j < cartforcheck.Count; j++)
            {
                if (cartforcheck[j] == chosenfilmname)
                {
                    deletecheck++;
                }
            }
        }

        if (deletecheck != 0)
        {
            Console.WriteLine("Нельзя удалить данный фильм так как на него уже купленны билеты");
        }
        else
        {
            try
            {
                DateTime newdatetime = DateTime.Parse(Console.ReadLine());//Проверку что время 
                string choosenhallname = seanses[chooseseanse - 1].GetHall();
                DateTime choosendatetime = seanses[chooseseanse - 1].GetTime();
                seanses[chooseseanse - 1].time = newdatetime;
                for (int k = 0; k < halls.Count; k++)
                {
                    if (halls[k].GetHallName() == choosenhallname)
                    {
                        for (int k2 = 0; k2 < halls[k].times.Count; k2++)
                        {
                            if (halls[k].times[k2] == choosendatetime)
                            {
                                halls[k].times[k2] = newdatetime;
                            }
                        }
                    }
                }

            }
            catch
            {
                Console.WriteLine("Введена строка не формата DateTime");
            }
        }

           
        AdminMenu();

    }

    else if (nextstep == 7)
    {
        Console.WriteLine("Доступные сеансы:");
        for (int k = 0; k < seanses.Count; k++)
        {
            Console.WriteLine(k + 1 + ") " + seanses[k].GetTime() + " Название фильма: " + seanses[k].GetFilm());
        }
        int chooseseanse = int.Parse(Console.ReadLine());


        string chosenfilmname = seanses[chooseseanse - 1].GetFilm();
        int deletecheck = 0;

        for (int k = 0; k < users.Count; k++)
        {
            List<string> cartforcheck = users[k].GetCartFilms();
            for (int j = 0; j < cartforcheck.Count; j++)
            {
                if (cartforcheck[j] == chosenfilmname)
                {
                    deletecheck++;
                }
            }
        }

        if (deletecheck != 0)
        {
            Console.WriteLine("Нельзя удалить данный фильм так как на него уже купленны билеты");
        }
        else
        {
            seanses.RemoveAt(chooseseanse - 1);
            string choosenhallname = seanses[chooseseanse - 1].GetHall();
            DateTime choosendatetime = seanses[chooseseanse - 1].GetTime();
            for (int k = 0; k < halls.Count; k++)
            {
                if (halls[k].GetHallName() == choosenhallname)
                {
                    for (int k2 = 0; k2 < halls[k].times.Count; k2++)
                    {
                        if (halls[k].times[k2] == choosendatetime)
                        {
                            halls[k].times.RemoveAt(k2 - 1);
                        }
                    }
                }
            }
        }
        AdminMenu();
    }

    else if (nextstep == 8)
    {
        Console.WriteLine("Что хотите посмотреть?");
        Console.WriteLine("1) По залам");
        Console.WriteLine("2) По фильмам?");
        Console.WriteLine("3) По конкретному сеансу?");
        Console.WriteLine("4) За всё время?");
        int totalFreeinhall = 0;
        int totalSellinhall = 0;
        int achoose = int.Parse(Console.ReadLine());
        if (achoose == 1)
        {
            for (int k = 0;k < halls.Count; k++)
            {
                Console.WriteLine((k + 1) + ") " + halls[k].GetHallName());
            }
            int chosenfilm = int.Parse(Console.ReadLine());
            string chosenfilmname = halls[chosenfilm - 1].GetHallName();
            for (int k = 0; k < seanses.Count; k++)
            {
                if (seanses[k].GetHall() == chosenfilmname)
                {
                    totalSellinhall += (seanses[k].GetAllPlaces() - seanses[k].GetTotal());
                    totalFreeinhall += seanses[k].GetTotal();
                }
            }
            Console.WriteLine("Всего свободных мест на фильм:" + totalFreeinhall);
            Console.WriteLine("Всего занятых мест на фильм:" + totalSellinhall);
            AdminMenu();
        }
        if (achoose == 2)
        {
            for (int k = 0; k < films.Count; k++)
            {
                Console.WriteLine((k+1) + ") " + films[k].GetName());
            }
            int chosenfilm = int.Parse(Console.ReadLine());
            string chosenfilmname = films[chosenfilm - 1].GetName();
            for (int k = 0; k < seanses.Count; k++)
            {
                if (seanses[k].GetFilm() == chosenfilmname)
                {
                    totalSellinhall += (seanses[k].GetAllPlaces() - seanses[k].GetTotal());
                    totalFreeinhall += seanses[k].GetTotal();
                }
            }
            Console.WriteLine("Всего свободных мест на фильм:" + totalFreeinhall);
            Console.WriteLine("Всего занятых мест на фильм:" + totalSellinhall);
            AdminMenu();
        }
        if (achoose == 3)
        {
            for (int k = 0;k < seanses.Count; k++)
            {
                Console.WriteLine(((k + 1) + " " + seanses[k].GetTime()) + " " + seanses[k].GetHall() + " " + seanses[k].GetFilm());
            }
            int choosedseanse = int.Parse(Console.ReadLine());
            seanses[choosedseanse-1].GetInfo();
            AdminMenu();
        }
        if (achoose == 4)
        {
            for (int k = 0; k < seanses.Count; k++)
            {
                totalSellinhall += (seanses[k].GetAllPlaces() - seanses[k].GetTotal());
                totalFreeinhall += seanses[k].GetTotal();
            }
            Console.WriteLine("Всего свободных мест на фильмы:" + totalFreeinhall);
            Console.WriteLine("Всего занятых мест на фильмы:" + totalSellinhall);
            AdminMenu();
        }
    }

    else if (nextstep == 9)
    {
        List<double> topintlist = new List<double>();
        for (int k = 0;k < users.Count; k++)
        {
            topintlist.Add(users[k].GetTotalSpend());
        }
        topintlist.Sort();
        topintlist.Reverse();
        Console.WriteLine("Топ сколько вы хотите увидеть?");
        int chosentop = int.Parse(Console.ReadLine());
        for (int j = 0; j < chosentop; j++)
        {
            for (int k = 0; k < users.Count; k++)
            {
                if (users[k].GetTotalSpend() == topintlist[j])
                {
                    Console.WriteLine("Место " + (j+1) + "  - " + users[k].GetUsername() + ". Потачено: " + users[k].GetTotalSpend());
                }
            }
        }

        AdminMenu();
    }

    else if (nextstep == 10)
    {
        string maxticketsuser = "";
        int maxticketcount = 0;
        for (int k = 0; k< users.Count; k++)
        {

            if (users[k].GetCountTickets() > maxticketcount)
            {
                maxticketsuser = users[k].GetUsername();
                maxticketcount = users[k].GetCountTickets();
            }
            if (k == users.Count - 1)
            {
                if (maxticketcount != 0)
                {
                    Console.WriteLine(maxticketsuser + "купил наиболее количество билетов: " + maxticketcount + " шт.");
                }
                else
                {
                    Console.WriteLine("Никто не покупал билеты");
                }
            }
        }


    }
}

while (true)
{
    try
    {
        newfilm();
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
    }
}

//давай нападай крашай ДАВАЙ ДАВАЙ ЛОМАЙ МЕНЯ
while (true)
{
    try
    {
        Menu();
    }
    catch(Exception ex)
    {
        Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
    }
}



