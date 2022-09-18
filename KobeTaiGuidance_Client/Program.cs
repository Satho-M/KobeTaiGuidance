using KobeTaiGuidance_Client.Get;
using KobeTaiGuidane_ClassLibrary;

Info_Character info_Character = new();
Info_Band info_Band = new();

Console.WriteLine("Character ID?");
var id = Console.ReadLine();
var character = info_Character.GetCharacterBaseInformation(id);
Console.WriteLine(
    "Name: " + character.Name +
    "\nHealth: " + character.Health +
    "\nMood: " + character.Mood +
    "\nStar Quality: " + character.StarQuality);

var bandId = info_Character.GetCharacterBandId(id);
character.Band = info_Band.GetBandBaseInformation(bandId);
Console.WriteLine("\nBand:");
Console.WriteLine(
    "Name: " + character.Band.Name +
    "\nRank: " + character.Band.Rank);

Console.WriteLine("\nOther Band Members:");
List<Character> members = new();
foreach (var str in info_Band.GetBandMembersId(bandId))
{
    var member = info_Character.GetCharacterBaseInformation(str);
    members.Add(member);
    if (member.Id != character.Id)
    {
        Console.WriteLine(
    "Name: " + member.Name +
    "\nHealth: " + member.Health +
    "\nMood: " + member.Mood +
    "\nStar Quality: " + member.StarQuality);
    }
}

Console.ReadKey();