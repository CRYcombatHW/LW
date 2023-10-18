namespace LW
{
	internal class Program
	{
		static int? Min;
		static int? Avr;
		static int? Max;

		static void Main(string[] args) {
			char taskc;

		input:
            Console.Write("Task N to run: ");
			taskc = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (taskc) {
				case '1': {
					Task1();
					break;
				}
				case '2': {
					Task2();
					break;
				}
				case '3': {
					Task3();
					break;
				}
				case '4': {
					Task4();
					break;
				}
				case '5': {
					Task5();
					break;
				}
				default: {
                    Console.WriteLine("Wrong input");
                    goto input;
				}
			}
        }

		public static void Task1() {
			Thread thread = createRangeThread(0, 50);
			thread.Start();
			thread.Join();
		}
		public static void Task2() {
			Console.Write("Start: ");
			int start = int.Parse(Console.ReadLine());
			Console.Write("End: ");
			int end = int.Parse(Console.ReadLine());

			Thread thread = createRangeThread(start, end);
			thread.Start();
			thread.Join();
		}
		public static void Task3() {
			Console.Write("Start: ");
			int start = int.Parse(Console.ReadLine());
			Console.Write("End: ");
			int end = int.Parse(Console.ReadLine());
			Console.Write("Threads count: ");
			int tcount = int.Parse(Console.ReadLine());
			int tnc = (end - start) / tcount;

			Thread[] threads = new Thread[tcount];
			for (int i = 0; i < tcount; i++) {
				threads[i] = createRangeThread(i * tnc + start, (i + 1) * tnc + start);
				threads[i].Start();
			}
		}
		public static void Task4() {
			Random random = new Random();

			int[] arr = new int[10000];
			for (int i = 0; i < arr.Length; i++) {
				arr[i] = random.Next(-10000, 10000);
			}

			Thread threadMin = new Thread(getMin);
			Thread threadAvr = new Thread(getAvr);
			Thread threadMax = new Thread(getMax);

			threadMin.Start(arr);
			threadAvr.Start(arr);
			threadMax.Start(arr);

			threadMin.Join();
			Console.WriteLine(Min);
			threadAvr.Join();
			Console.WriteLine(Avr);
			threadMax.Join();
			Console.WriteLine(Max);
		}
		public static void Task5() {
			Random random = new Random();

			int[] arr = new int[10000];
			for (int i = 0; i < arr.Length; i++) {
				arr[i] = random.Next(-10000, 10000);
			}

			Thread threadMin = new Thread(getMin);
			Thread threadAvr = new Thread(getAvr);
			Thread threadMax = new Thread(getMax);

			threadMin.Start(arr);
			threadAvr.Start(arr);
			threadMax.Start(arr);

			threadMin.Join();
			Console.WriteLine($"Minimum: {Min}");
			threadAvr.Join();
			Console.WriteLine($"Average: {Avr}");
			threadMax.Join();
			Console.WriteLine($"Maximum: {Max}");

			saveResults();
		}

		static Thread createRangeThread(int start, int end) {
			return new Thread(() => {
				for (int i = start; i < end; i++) {
					Console.WriteLine(i);
				}
			});
		}

		static void getMin(object? intArrObj) {
			if (intArrObj is null)
				return;

			int[] arr = (int[])intArrObj;
			int min = arr[0];

			for (int i = 1; i < arr.Length; i++) {
				if (arr[i] < min) {
					min = arr[i];
				}
			}

			Min = min;
		}
		static void getAvr(object? intArrObj) {
			if (intArrObj is null)
				return;

			int[] arr = (int[])intArrObj;
			int avr = 0;

			for (int i = 1; i < arr.Length; i++) {
				avr += arr[i];
			}

			Avr = avr / arr.Length;
		}
		static void getMax(object? intArrObj) {
			if (intArrObj is null)
				return;

			int[] arr = (int[])intArrObj;
			int max = arr[0];

			for (int i = 1; i < arr.Length; i++) {
				if (arr[i] > max) {
					max = arr[i];
				}
			}

			Max = max;
		}
		static void saveResults() {
			FileStream fs = File.Open("t5.txt", FileMode.OpenOrCreate);
			StreamWriter writer = new StreamWriter(fs);

			writer.WriteLine($"Minimum: {Min}");
			writer.WriteLine($"Average: {Avr}");
			writer.WriteLine($"Maximum: {Max}");

			writer.Flush();
			writer.Close();
		}
	}
}
