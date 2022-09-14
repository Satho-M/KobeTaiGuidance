using KobeTaiGuidane_ClassLibrary;

namespace KobeTaiGuidance_Client.Get
{
    public class Info_Character
    {
        Browser_Client client;

        public Info_Character(Browser_Client client)
        {
            this.client = client;
        }

        public async Task<Character> GetCharacterBaseInformation(string characterId)
        {
            Character character = new()
            {
                Id = uint.Parse(characterId)
            };

            var _page = await client.OpenNewPage();
            await _page.Page.GotoAsync("https://73.popmundo.com/World/Popmundo.aspx/Character/" + characterId);

            //Get Name
            var h2 = await _page.Page.QuerySelectorAsync(".charPresBox h2");
            character.Name = (await h2.InnerHTMLAsync()).Split('<')[0];

            //Get Health, Humor, SQ
            var table = await _page.Page.QuerySelectorAllAsync(".charMainValues table tbody tr");
            //Iterates the three bars for Mood, Health, Star Quality
            int i = 0;
            foreach (var item in table)
            {
                var bar = await item.QuerySelectorAsync(".sortkey");
                if (bar != null)
                {
                    var value = byte.Parse(await bar.InnerTextAsync());
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

        public async Task<List<Skill>> GetCharacterSkills(string characterId)
        {
            List<Skill> skillList = new();

            var _page = await client.OpenNewPage();
            await _page.Page.GotoAsync("https://73.popmundo.com/World/Popmundo.aspx/Character/Skills/" + characterId);

            //Iterates the skill table in a characters page
            var table = await _page.Page.QuerySelectorAllAsync(".data tbody tr");
            foreach (var row in table)
            {
                Skill skill = new();
                var tds = await row.QuerySelectorAllAsync("td");

                //Only rows with two tds interest us for the Skill + Level
                if (tds.Count != 2)
                {
                    continue;
                }

                foreach (var td in tds)
                {

                    var tagA = await td.QuerySelectorAsync("a");
                    if (tagA != null)
                    {
                        skill.Id = uint.Parse((await tagA.GetAttributeAsync("href")).Split('/')[5]);
                        skill.Name = await tagA.InnerTextAsync();
                    }
                    else
                    {
                        var span = await (await td.QuerySelectorAsync("span")).InnerTextAsync();
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
                    }


                }

                skillList.Add(skill);
            }

            return skillList;
        }
    }
}
