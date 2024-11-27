using System.Globalization;
class Program
{
    static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
        Console.Title = "Lab 4";
        Console.Write("Введiть кiлькiсть елементiв (G): ");
        if (!int.TryParse(Console.ReadLine(), out var n) || n < 2 || n > 15)
        {
            Console.WriteLine("Кiлькiсть елементiв має бути бiльше 1 i менше 16.");
            return;
        }
        // Створення матриці і заповнення її головної діагоналі
        var matrix = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            matrix[i, i] = 1.0;
        }
        // Введення елементів нижче головної діагоналі
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.Write($"Введiть значення елемента: ");
                if (!double.TryParse(Console.ReadLine(), out double value) || value <= 0)
                {
                    Console.WriteLine("Помилкове значення. Спробуйте знову.");
                    j--;
                    continue;
                }
                matrix[i, j] = value;
                matrix[j, i] = 1 / value; // Симетричне значення вище діагоналі
            }
        }
        Console.Clear(); // Очищення консолі після введення елементів
                         // Заголовок матриці
        Console.Write("\t");
        for (int j = 0; j < n; j++)
        {
            Console.Write($"G{j + 1}\t");
        }
        Console.WriteLine("Vi\tPi");
        // Обчислення Vi
        var Vi = new List<double>(Enumerable.Repeat(1.0, n));
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Vi[i] *= matrix[i, j];
            }
            Vi[i] = Math.Pow(Vi[i], 1.0 / n);
        }
        var sumVi = Vi.Sum(); // Сума Vi
                              // Виведення матриці та обчислення En1
        var En1 = new List<double>(Enumerable.Repeat(0.0, n));
        for (int i = 0; i < n; i++)
        {
            Console.Write($"G{i + 1}\t");
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j]:F2}\t");
            }
            var Pi = Vi[i] / sumVi;
            Console.WriteLine($"{Math.Round(Vi[i], 2)}\t{Math.Round(Pi, 2)}");
            for (int j = 0; j < n; j++)
            {
                En1[i] += matrix[i, j] * (Vi[j] / sumVi);
            }
        }
        Console.WriteLine($"\nСума Vi = {sumVi:F2}"); // Виведення суми Vi
                                                      // Заголовок для En1, En2
        Console.Write("\n\t");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"G{i + 1}\t");
        }
        Console.WriteLine();
        // Виведення En1
        Console.Write("En1\t");
        foreach (var en1 in En1)
        {
            Console.Write($"{en1:F2}\t");
        }
        Console.WriteLine();
        // Обчислення En2
        var En2 = new List<double>(n);
        for (int i = 0; i < n; i++)
        {
            double Pi = Vi[i] / sumVi;
            En2.Add(En1[i] / Pi);
        }
        // Виведення En2
        Console.Write("En2\t");
        foreach (var en2 in En2)
        {
            Console.Write($"{en2:F2}\t");
        }
        // Обчислення і виведення суми En2
        var sumEn2 = En2.Sum();
        Console.WriteLine($"\n\nСума En2 = {sumEn2:F2}");
        // Обчислення і виведення лямбди
        var lambda_max = sumEn2 / n;
        Console.WriteLine($"\nlambda(max) = {lambda_max:F2}");
        // Обчислення і виведення ІП
        var IP = (lambda_max - n) / (n - 1);
        Console.WriteLine($"IП = {IP:F2}");
        // Визначення і виведення ВІ
        double[] VIs = { 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49, 1.51, 1.54, 1.56, 1.57, 1.59 };
        var VI = VIs[n - 2];
        Console.WriteLine($"ВI = {VI:F2}");
        // Обчислення і виведення ВП
        var VP = IP / VI;
        Console.WriteLine($"BП = {VP:F2}");
        if (VP <= 0.1)
        {
            Console.WriteLine("Погодженість задовільна");
        }
        else if (VP >= 0.1 && VP <= 0.3)
        {
            Console.WriteLine("Ступінь погодженості прийнятний");
        }
        else
        {
            Console.WriteLine("Експерту рекомендується переглянути свої рішення");
        }
        Console.ReadKey();
    }
}