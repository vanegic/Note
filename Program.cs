using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Note
{
    public class Notebook
    {
        public static void Main(string[] args)
        {
            Inf human1 = new Inf("Ivan", "Buz", 89992055515, "Russia");
            Inf human2 = new Inf("Igor", "Sav", 89993355515, "Russia");
            Inf human3 = new Inf("Pasha", "Iva", 8999306626, "China");
            human3.middleName = "Alexandrovich";
            human3.other = "Worker";
            human3.date = new DateTime(1998, 11, 05);
            string s;
            bool b = true;
            int i;
            while (b)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Привет! Я программа записная книжка\r\nЯ могу <Добавить>, <Редактировать>, <Удалить> запись, организовать <Просмотр всех> или <Просмотр> определённой записи. Для выхода введите <Готово>");
                s = Console.ReadLine();
                switch (s)
                {
                    case "Добавить": Inf.Add(); break;
                    case "Редактировать": Console.WriteLine("Какую запись необходимо отредактировать?"); i = Convert.ToInt32(Console.ReadLine()); Inf.Edit(Inf.notes[i-1]); break;
                    case "Удалить": Console.WriteLine("Какую запись необходимо удалить?"); i = Convert.ToInt32(Console.ReadLine()); Inf.Del(i - 1); break;
                    case "Просмотр": Console.WriteLine("О какой записи вывести информацию?"); i = Convert.ToInt32(Console.ReadLine()); Inf.Show(Inf.notes[i - 1],true); break;
                    case "Просмотр всех": Inf.All(); break;
                    case "Готово": b = false; break;
                    default: Console.WriteLine("Упс! Что-то пошло не так. Введите команду снова, она отображается в <>"); Console.ReadKey(); Console.Clear(); break;
                }
            } 
        }
    }
    public class Inf
    {
        public string firstName;
        public string middleName;
        public string lastName;
        public long phoneNumber;
        public string country;
        public DateTime date;
        public string organization;
        public string position;
        public string other;
        public static int count;
        public int id;
        public static List<Inf> notes = new List<Inf>();
        public Inf(string name, string lastName, long number, string country)
        {
            this.firstName = name;
            this.lastName = lastName;
            this.phoneNumber = number;
            this.country = country;
            count++;
            this.id = count;
            Inf.notes.Add(this);

        }
        public static void Add()
        {
            string n, l, c;
            long p;
            Console.Clear();
            bool b = true;
            try { 
            Console.WriteLine("Введите обязательные сведения: Фамиля, Имя, Номер телефона, Страна. Введите каждое значение с новой строки");
            l = Console.ReadLine();
            n = Console.ReadLine();
            // s = Console.ReadLine();
            p = Convert.ToInt64(Console.ReadLine());
            c = Console.ReadLine();
            new Inf(n, l, p, c); }
            catch (FormatException) { Console.WriteLine("Неверно указаны сведения (убедитесь, что для ввода номера телефона использовались только цифры), запись не создана"); b = false; }
            catch (OverflowException) { Console.WriteLine("Номер телефона слишком длинныйю. Запись не создана"); b = false; }
            if (b) { 
            Console.WriteLine("Хотите добавить необязательные сведения?\r\nДа/Нет");
            n = Console.ReadLine();
            if (n == "Да") 
            {
                Console.WriteLine("Введите Отчество, Дату рождения в формате, Организацию, Должность и Прочие заметки. Оставьте поля пустыми, если не хотите их добавлть\r\n");
                Console.WriteLine("Отчество"); Inf.notes[Inf.notes.Count-1].middleName = Console.ReadLine();
                Console.WriteLine("Дата рождения в формате 20.02.1998"); DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime f); Inf.notes[Inf.notes.Count - 1].date = f;
                Console.WriteLine("Организация"); Inf.notes[Inf.notes.Count - 1].organization = Console.ReadLine();
                Console.WriteLine("Должность"); Inf.notes[Inf.notes.Count - 1].position = Console.ReadLine();
                Console.WriteLine("Прочие заметки"); Inf.notes[Inf.notes.Count - 1].other = Console.ReadLine();
            }
            Console.WriteLine("Запись успешно добавлена");
             }
            Console.ReadKey();
            Console.Clear();

        }
        public static void Show(Inf inf, bool b) 
        {
            if (b) { Console.Clear(); }
            Console.WriteLine("\r\nИмя: " + inf.firstName);
            if ((inf.middleName != null)&&(inf.middleName != "")) { Console.WriteLine("Отчество: " + inf.middleName); }
            Console.WriteLine("Фамилия: " + inf.lastName);
            Console.WriteLine("Телефон: " + inf.phoneNumber);
            Console.WriteLine("Страна: " + inf.country);
            if (inf.date != DateTime.MinValue) { Console.WriteLine("Дата рождения: " + inf.date.ToShortDateString()); }
            if ((inf.organization != null)&&(inf.organization != "")) { Console.WriteLine("Организация: " + inf.organization); }
            if ((inf.position != null)&&(inf.position != "")) { Console.WriteLine("Должность: " + inf.position); }
            if ((inf.other != null) && (inf.other != null)) { Console.WriteLine("Прочие заметки: " + inf.other); }
            Console.WriteLine();
            if (b) 
            {
            Console.ReadKey();
            Console.Clear();
            }
        }
        public static void All()
        {
            Console.Clear();
            Console.WriteLine("Всего записей " + Inf.count + ":");
           
            foreach (Inf n in Inf.notes) 
            {
                Console.WriteLine("\r\nАнкета № " + n.id) ;
                Console.WriteLine("Фамилия: " + n.lastName);
                Console.WriteLine("Имя: " + n.firstName);
                Console.WriteLine("Номер телефона: " + n.phoneNumber);
            }
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }
        public static void Del (int n)
        {
            Console.Clear();
            foreach (Inf i in notes )
            {
                if (i.id == n) { notes.Remove(i); Inf.count--; break; }
            }
            foreach ( Inf i in notes)
            {
              if (i.id>n) { i.id--; }
            }
            Console.WriteLine("Запись успешно удалена");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Edit (Inf i)
        {
            Console.Clear();
            Console.WriteLine("Какой параметр вы хотите изменить? Имя, Фамилия, Отчество, Номер телефона, " +
                "Страна, Дата рождения, Организация, Должность, Прочие заметки \r\nДля заевршения редактирования введите Готово");
            string s;
            bool b = true;
            Show(i,false);
            while (b)
            {
               s = Console.ReadLine();
                try {  
                switch (s)
                {
                    case "Имя": Console.WriteLine("Введите новое имя"); i.firstName = Console.ReadLine(); break;
                    case "Отчество": Console.WriteLine("Введите новое отчество"); i.middleName = Console.ReadLine(); break;
                    case "Номер телефона": Console.WriteLine("Введите новый номер телефона"); i.phoneNumber = Convert.ToInt64(Console.ReadLine()); break;
                    case "Страна": Console.WriteLine("Введите новую страну"); i.country = Console.ReadLine(); break;
                    case "Организация": Console.WriteLine("Введите новое название организации"); i.organization = Console.ReadLine(); break;
                    case "Должность": Console.WriteLine("Введите новую должность"); i.position = Console.ReadLine(); break;
                    case "Прочие заметки": Console.WriteLine("Введите новую заметку"); i.other = Console.ReadLine(); break;
                    case "Фамилия": Console.WriteLine("Введите новую фамилию"); i.lastName = Console.ReadLine(); break;
                    case "Дата рождения": Console.WriteLine("Введите новую дату рождения в формате 20.02.1998"); DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime f); i.date = f; break;
                    case "Готово": b = false;  break;
                    default: Console.WriteLine("Ошибка! Попробуйте ввести имя параметра заново с большой буквы");  break;
                }
                    }
                catch (FormatException) { Console.WriteLine("Для ввода номера телефона использовались не только цифры, номер телефона не изменён"); }
                catch (OverflowException) { Console.WriteLine("Номер телефона слишком длинный. Номер телефона не изменён"); }
                if (b) { Console.WriteLine("Хотите изменить какой-нибудь другой параметр? Если нет, введите Готово"); }
            }
            Console.WriteLine("Запись теперь выглядит так:");
            
            Show(i,false);
            Console.ReadKey();
            Console.Clear();
        }

    }
}
