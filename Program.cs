using System;
using System.Collections.Generic;
using System.IO;

class Program
{
	static void Main()
	{
		Console.WriteLine(@"
█▀▀ █░█ █▀▀ █▀ █▀   ▀█▀ █░█ █▀▀   █░█░█ █▀█ █▀█ █▀▄
█▄█ █▄█ ██▄ ▄█ ▄█   ░█░ █▀█ ██▄   ▀▄▀▄▀ █▄█ █▀▄ █▄▀");
		
		List<string> categories = new List<string> { "Animals", "Fruits", "Countries" };
		
		while (true)
		{
			Console.WriteLine("\nSelect a category:");
			
			for (int i = 0; i < categories.Count; i++)
			{
				Console.WriteLine((i + 1) + ". " + categories[i]);
			}
			
			Console.WriteLine("0. Quit");
			
			int categoryChoice;
			while (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 0 || categoryChoice > categories.Count)
			{
				Console.WriteLine("Invalid choice. Please enter a number between 0 and " + categories.Count);
			}
			
			if (categoryChoice == 0)
			{
				Console.WriteLine("Thank you for playing!");
				Console.ReadKey();
				break;
			}
			
			string selectedCategory = categories[categoryChoice - 1];
			string secretWord = GetRandomWord(selectedCategory);
			string guessedWord = new string('_', secretWord.Length);
			int attempts = 0;
			const int maxAttempts = 5;
			
			Console.WriteLine("\nCategory: " + selectedCategory);
			Console.WriteLine("Guessed word: " + guessedWord);
			
			while (attempts < maxAttempts && guessedWord != secretWord)
			{
				Console.WriteLine("\n1. Guess a letter");
				Console.WriteLine("2. Hint");
				Console.WriteLine("3. Pause");
				Console.WriteLine("4. Quit");
				
				int userChoice;
				while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 4)
				{
					Console.WriteLine("Invalid choice. Please enter a number between 1 and 4");
				}
				
				switch (userChoice)
				{
					case 1:
						Console.Write("Enter a letter: ");
						string guess = Console.ReadKey().KeyChar.ToString();
						
						if (secretWord.Contains(guess))
						{
							for (int i = 0; i < secretWord.Length; i++)
							{
								if (secretWord[i].ToString() == guess)
								{
									guessedWord = guessedWord.Remove(i, 1).Insert(i, guess.ToString());
								}
							}
						}
						else
						{
							Console.WriteLine("\nIncorrect guess. Try again!");
							attempts++;
						}
						
						Console.WriteLine("\nGuessed word: " + guessedWord);
						break;
						
					case 2:
						DisplayHint(selectedCategory, secretWord);
						break;
						
					case 3:
						Console.WriteLine("\nGame paused. Press any key to resume...");
						Console.ReadKey();
						break;
						
					case 4:
						Console.WriteLine("\nSorry, you've decided to quit. The word was " + secretWord + ".");
						Console.ReadKey();
						return;
				}
			}
			
			if (guessedWord == secretWord)
			{
				Console.WriteLine("Congratulations! You guess the word!");
			}
			else
			{
				Console.WriteLine("Sorry, you've run out of attempts. The word was " + secretWord + ".");
			}
		}
	}
	
	static string GetRandomWord(string category)
	{
		string directoryPath = @"C:\Users\ADMIN\Documents\SharpDevelop Projects\guess the word game\guess the word game\bin\Debug";
		string filePath = Path.Combine(directoryPath, string.Format("{0}_words.txt", category.ToLower()));
		List<string> words = new List<string>();
		
		if (File.Exists(filePath))
		{
			words.AddRange(File.ReadAllLines(filePath));
		}
		
		if (words.Count == 0)
		{
			words.Add("default"); // Provide a default word if the file is empty or missing
		}
		
		Random random = new Random();
		return words[random.Next(words.Count)];
	}
	
	static void DisplayHint(string category, string word)
	{
		Console.WriteLine("\nHint: It's a " + category.ToLower() + " and has " + word.Length + " letters.");
	}
}