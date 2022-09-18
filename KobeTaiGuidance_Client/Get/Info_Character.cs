using HtmlAgilityPack;
using KobeTaiGuidane_ClassLibrary;

namespace KobeTaiGuidance_Client.Get
{
    public class Info_Character
    {
        HtmlWeb web;

        public Info_Character()
        {
            this.web = new HtmlWeb();
        }

        public Character GetCharacterBaseInformation(string characterId)
        {
            Character character = new()
            {
                Id = uint.Parse(characterId)
            };

            var htmlDoc = web.Load("https://73.popmundo.com/World/Popmundo.aspx/Character/" + characterId);

            //Get Name
            string name = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='box ofauto charPresBox']/h2").InnerText;
            character.Name = name;

            //Iterates the three bars for Mood, Health, Star Quality
            int i = 0;
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//div[@class='charMainValues']//span[@class='sortkey']"))
            {
                var bar = node.InnerText;
                if (!string.IsNullOrEmpty(bar))
                {
                    var value = byte.Parse(bar);
                    switch (i)
                    {
                        case 0:
                            character.Mood = value;
                            break;
                        case 1:
                            character.Health = value;
                            break;
                        case 2:
                            character.StarQuality = value;
                            break;
                        default:
                            break;
                    }
                    i++;
                }
            }

            return character;
        }

        public List<Skill> GetCharacterSkills(string characterId)
        {
            List<Skill> skillList = new();

            var htmlDoc = web.Load("https://73.popmundo.com/World/Popmundo.aspx/Character/Skills/" + characterId);

            //Iterates the skill table in a characters page
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//table[@class='data']//tr[contains(@id, 'ctl00_cphLeftColumn_ctl00_repSkillGroups_')]"))
            {
                Skill skill = new();

                skill.Id = uint.Parse(node
                    .SelectSingleNode(".//td/a")
                    .Attributes["href"]
                    .Value
                    .ToString().Split('/')[5]);

                skill.Name = node.SelectSingleNode(".//td/a").InnerText;

                var span = node.SelectSingleNode(".//td/span[@class='sortkey']").InnerText;
                switch (span)
                {
                    case "10":
                        skill.Level = 1;
                        break;
                    case "20":
                        skill.Level = 2;
                        break;
                    case "30":
                        skill.Level = 3;
                        break;
                    case "40":
                        skill.Level = 4;
                        break;
                    case "50":
                        skill.Level = 5;
                        break;
                    default:
                        break;
                }

                skillList.Add(skill);
            }

            return skillList;
        }

        public string GetCharacterBandId(string characterId)
        {
            var htmlDoc = web.Load("https://73.popmundo.com/World/Popmundo.aspx/Character/Skills/" + characterId);

            var bandId = htmlDoc.DocumentNode
                .SelectNodes("//div[@class='float_left characterPresentation']//a")
                .First()
                .Attributes["href"]
                .Value
                .Split('/')[4];

            return bandId;
        }
    }
}
