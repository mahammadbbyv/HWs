// See https://aka.ms/new-console-template for more information
using Create_Character.Model;
#pragma warning disable CS8601

Character character = new();

Console.WriteLine("Enter character's name: ");
character.Name = Console.ReadLine();
Console.WriteLine("Enter character's power: ");
character.Power = Int32.Parse(Console.ReadLine());
Console.WriteLine("Enter character's group: ");
character.Group = Console.ReadLine();

#pragma warning restore CS8601
