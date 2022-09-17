using KobeTaiGuidance_Client.Get;

Info_Character info_Character = new();

Console.WriteLine("Character ID?");
var id = Console.ReadLine();
var character = info_Character.GetCharacterBaseInformation(id);
Console.WriteLine(
    "Name: " + character.Name +
    "\nHealth: " + character.Health +
    "\nMood: " + character.Mood +
    "\nStar Quality: " + character.StarQuality);
character.Skills = info_Character.GetCharacterSkills(id);
Console.WriteLine("\nSkills:");
foreach (var skill in character.Skills)
{
    Console.Write(skill.Name + " ");
    for (int i = 0; i < skill.Level; i++)
    {
        Console.Write("*");
    }
    Console.WriteLine();
}

Console.ReadKey();