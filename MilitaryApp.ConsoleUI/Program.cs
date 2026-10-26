using System;
using MilitaryApp.Core;

namespace MilitaryApp.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== ВОИНСКАЯ ЧАСТЬ (Консоль) ===");
                Console.WriteLine("1. Показать всех военнослужащих");
                Console.WriteLine("2. Добавить военнослужащего");
                Console.WriteLine("3. Редактировать данные");
                Console.WriteLine("4. Удалить из списка");
                Console.WriteLine("5. Найти по званию");
                Console.WriteLine("6. Группировать по должностям");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n--- Список личного состава ---");
                        foreach (var soldier in logic.ReadAllSoldiers())
                            Console.WriteLine(soldier.ToString());
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("\n--- Добавление ---");
                        Console.Write("ФИО: "); string name = Console.ReadLine();
                        Console.Write("Звание: "); string rank = Console.ReadLine();
                        Console.Write("Должность: "); string pos = Console.ReadLine();
                        logic.CreateSoldier(new Soldier { FullName = name, Rank = rank, Position = pos });
                        Console.WriteLine("Добавлено! Нажмите клавишу...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("\nВведите ID для редактирования: ");
                        if (int.TryParse(Console.ReadLine(), out int idUpd))
                        {
                            Console.Write("Новое ФИО: "); string n = Console.ReadLine();
                            Console.Write("Новое звание: "); string r = Console.ReadLine();
                            Console.Write("Новая должность: "); string p = Console.ReadLine();
                            logic.UpdateSoldier(new Soldier { Id = idUpd, FullName = n, Rank = r, Position = p });
                            Console.WriteLine("Обновлено!");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("\nВведите ID для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int idDel))
                        {
                            logic.DeleteSoldier(idDel);
                            Console.WriteLine("Удалено!");
                        }
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Write("\nВведите звание для поиска: ");
                        string searchRank = Console.ReadLine();
                        var found = logic.GetSoldiersByRank(searchRank);
                        Console.WriteLine($"\nНайдено: {found.Count}");
                        foreach (var s in found) Console.WriteLine(s.ToString());
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.WriteLine("\n--- Группировка по должностям ---");
                        var groups = logic.GroupSoldiersByPosition();
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"\nДолжность: {group.Key}");
                            foreach (var s in group.Value)
                                Console.WriteLine($"  - {s.FullName} ({s.Rank})");
                        }
                        Console.ReadKey();
                        break;
                    case "0": exit = true; break;
                }
            }
        }
    }
}