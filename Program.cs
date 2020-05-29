using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tab
{
    class Program
    {
        enum Type
        {
            Х,
            У,
            С
        }
        enum Action
        {
            ADD,
            DELETE,
            UPDATE
        }
        struct book
        {
            public string author;
            public int year;
            public string name;
            public char type;
            public void DisplayInfo()
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-20} {3,-10}", author,year,name,type );
            }
        }
        struct Log
        {
            public DateTime time;
            public Action action;
            public string name;

            public void DisplayLog()
            {
                switch (action)
                {
                    case Action.ADD:
                        Console.WriteLine($"{time.ToLongTimeString()} - Добавлена запись \"{name}\"");
                        break;
                    case Action.DELETE:
                        Console.WriteLine($"{time.ToLongTimeString()} - Удалена   запись \"{name}\"");
                        break;
                    case Action.UPDATE:
                        Console.WriteLine($"{time.ToLongTimeString()} - Обновлена запись \"{name}\"");
                        break;
                }
            }
        }
        static void CalcIdleTime(DateTime dt1, DateTime dt2, TimeSpan old_idle_time, out TimeSpan idle_time)
        {
            TimeSpan time = dt2 - dt1;
            if (time > old_idle_time)
            {
                idle_time = time;
            }
            else
            {
                idle_time = old_idle_time;
            }
        }

        static int GetNumber(int left_limit, int right_limit)
        {
            int select = Int32.MinValue;
            while (!(select >= left_limit && select <= right_limit))
            {
                Console.Write($"Введите значение от {left_limit} до {right_limit}:");
                try
                {
                    select = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Попробуйте заново!");
                }
            }
            return select;
        }
        
        static void ShowMenu()
        {
            Console.WriteLine("1 – Просмотр таблицы\n" +
                              "2 – Добавить запись\n" +
                              "3 – Удалить запись\n" +
                              "4 – Обновить запись\n" +
                              "5 – Поиск записей\n" +
                              "6 – Просмотреть лог\n" +
                              "7 - Выход");
        }
        static void ShowTab(book[] books)
        {
            string s = " ";
            Console.WriteLine(s);
            Console.WriteLine("Библиотека");
            Console.WriteLine(s);
            Console.WriteLine("{0,-20} {1,-15} {2,-20} {3,-10}","Автор","Год","Название","Тип");
            Console.WriteLine(s);
            for (int i = 0; i < books.Length; i++)
            {
                books[i].DisplayInfo();
                Console.WriteLine(s);
            }
            Console.WriteLine("Типы:Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра\t\t\t       |");
            Console.WriteLine(s);
        }
        static void ShowLog(Log[] logs, TimeSpan idle_time)
        {
            for (int i = 0; i < logs.Length; i++)
            {
                if (logs[i].name != null)
                {
                    logs[i].DisplayLog();
                }
            }
            Console.WriteLine($"\n{idle_time.Hours:00}:{idle_time.Minutes:00}:{idle_time.Seconds:00} - Самый долгий период бездействия пользователя");
        }

        static void AddNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Array.Resize(ref books, books.Length + 1);
            int last = books.Length - 1;

            Console.Write("Введите название книги");
            books[last].name = (Console.ReadLine());
            bool error = true;
            
            
                Console.WriteLine("Укажите тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
            if (Console.ReadLine() == "Х" || Console.ReadLine() == "У" || Console.ReadLine() == "С")
            {
                books[last].type = Convert.ToChar(Console.ReadLine());
                error = false;
            }
            else
            {
                Console.WriteLine("Укажите верный тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
            }

                Console.WriteLine("Укажите год издания");
                books[last].year = Convert.ToInt32(Console.ReadLine());
                error = false;

            
                Console.WriteLine("Введите верную дату");
            
            Console.WriteLine("Укажите автора");
            books[last].author = Convert.ToString(Console.ReadLine());
            AddLog(ref logs, DateTime.Now, 0, books[last].name, ref cnt);
        }
        static void DeleteNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Console.WriteLine("Укажите номер записи, которую хотите удалить");
            int number = GetNumber(0, books.Length - 1);
            AddLog(ref logs, DateTime.Now, 1, books[number].name, ref cnt);
            for (int i = number; i < books.Length - 1; i++)
            {
                books[i] = books[i + 1];
            }
            Array.Resize(ref books, books.Length - 1);
        }
        static void UpdateNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Console.WriteLine("Укажите номер записи, которую хотите обновить");
            int number = GetNumber(0, books.Length - 1);
            AddLog(ref logs, DateTime.Now, 2, books[number].name, ref cnt);
            Console.Write("Введите название книги");
            books[number].name = (Console.ReadLine());
            
                Console.WriteLine("Укажите тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
                books[number].type = Convert.ToChar(Console.ReadLine());
    
    
                Console.WriteLine("Укажите год издания");
                books[number].year = Convert.ToInt32(Console.ReadLine());
         
                Console.WriteLine("Введите верную дату");
            
            Console.WriteLine("Укажите автора");
            books[number].author = Convert.ToString(Console.ReadLine());
        }
        
        static void SearchNotes(book[] books)
        {
            start:
            try
            {
                Console.WriteLine("Введите тип книги для поиска");
            char searchtype = Convert.ToChar(Console.ReadLine());
            string s = " ";
                    if (searchtype == 'Х' || searchtype == 'У' || searchtype == 'С')
                    {
                        Console.WriteLine(s);
                        Console.WriteLine("Библиотека\t\t\t\t\t\t\t\t       |");
                        Console.WriteLine(s);
                        Console.WriteLine("Автор \tГод издания \tНазвание \tТип   ");
                        Console.WriteLine(s);
                        for (int i = 0; i < books.Length; i++)
                        {

                            if (books[i].type == searchtype)
                            {
                                books[i].DisplayInfo();
                                Console.WriteLine(s);
                            }
                        }
                        Console.WriteLine("Перечисляемый тип: Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра\t\t\t       ");
                        Console.WriteLine(s);
                        

                    }
                    else
                    {
                        Console.WriteLine("Укажите верный тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
                    goto start;
                    }
                }
                catch
                {
                    Console.WriteLine("Ex");
                }
            
            
        }
        static void AddLog(ref Log[] logs, DateTime time, int action, string name, ref int cnt)
        {
            if (cnt > 49)
            {
                for (int i = 0; i < logs.Length - 1; i++)
                {
                    logs[i] = logs[i + 1];
                }
                cnt = 49;
            }
            logs[cnt].time = time;
            logs[cnt].action = (Action)action;
            logs[cnt].name = name;

            cnt++;
        }
        static void Main(string[] args)
        {
            book[] books = new book[3];
            Log[] logs = new Log[50];

            books[0].author = "Санкевич";
            books[0].year = 1978;
            books[0].name = "Потоп";
            books[0].type = 'Х';

            books[1].author = "Ландау";
            books[1].year = 1989;
            books[1].name = "Механика";
            books[1].type = 'У';

            books[2].author = "Дойль";
            books[2].year = 1990;
            books[2].name = "Сумчатые";
            books[2].type = 'С';

            const int show = 1;
            const int add = 2;
            const int delete = 3;
            const int update = 4;
            const int search = 5;
            const int log = 6;
            int select = 0;
            int cnt = 0;
            TimeSpan idle_time = TimeSpan.Zero;
            DateTime time = DateTime.Now;
            while (select != 7)
            {
                switch (select)
                {
                    case show:
                        ShowTab(books);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case add:
                        AddNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case delete:
                        DeleteNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case update:
                        UpdateNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case search:
                        SearchNotes(books);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case log:
                        Console.Clear();
                        ShowLog(logs, idle_time);
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    default:
                        CalcIdleTime(time, DateTime.Now, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                }
            }
        }
    }
}