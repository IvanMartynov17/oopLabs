namespace Lab1;

public static class Task1
{
     public static void Run()
     {
          int n; 
          Console.WriteLine("Enter a number of patient: ");
          n = int.Parse(Console.ReadLine());
          
          double[] patientWeights = new double[n];
          for (int i = 0; i < n; i++)
          {
               Console.WriteLine($"Enter weight of patient {i + 1}: ");
               patientWeights[i] = double.Parse(Console.ReadLine());
          }

          double sum = 0;
          double min = patientWeights[0];
          double max = patientWeights[0];
          
          foreach (double weight in patientWeights)
          {
               sum +=  weight;
               
               if (weight < min) 
                    min = weight;
               
               if (weight > max) 
                    max = weight;
          }
          Console.WriteLine($"Sum of patient weights: {sum:F1} ");
          Console.WriteLine($"Min weight {min:F1}");
          Console.WriteLine($"Max weight {max:F1}");
          
          double average = sum / n;
          Console.WriteLine($"Average of patient weights: {average:F1}");

          double count = 0;
          for (int i = 0; i < n; i++)
          {
               if (patientWeights[i] > average)
                    count++;
          }
          if (count == 0)
               Console.WriteLine("No patients above average");
          else
               Console.WriteLine($"{count} patients above average");
     }
}