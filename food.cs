class Food
{
    public void FoodShop(ref double UserBalance, ref List<string> FoodCart)
    {
        while (true)
        {
            //Заданное меню
            string[][] Menu =
                {
                new string[] { "Поп-корн соленый", "300" },
                new string[] { "Поп-корн сладкий", "300" },
                new string[] { "Кока-кола", "150" },
                new string[] { "Начос сырный", "450" },
            };

            //Вывод меню в консоль
            for (int i = 0; i < Menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {Menu[i][0]}       Цена:{Menu[i][1]} р.");
            }

            //Запрос на ввод товара
            int choosenGood = int.Parse(Console.ReadLine());

            //Проверка денег на балансе
            if (UserBalance > int.Parse(Menu[(choosenGood - 1)][1]))
            {
                //Подтверждение покупки
                Console.WriteLine("Подтвердите покупку (1 - Подтведить, любой другой символ - отменить)");
                string next = Console.ReadLine();
                if (next == "1")
                {
                    Console.WriteLine("Покупка подтверждена");
                    UserBalance -= int.Parse(Menu[(choosenGood - 1)][1]);
                    FoodCart.Add(Menu[(choosenGood - 1)][0]);
                    break;
                }
                else
                {
                    Console.WriteLine("Покупка отменена");
                }
            }
            else
            {
                Console.WriteLine("Недостаточно средств");
            }
        }
    }
}