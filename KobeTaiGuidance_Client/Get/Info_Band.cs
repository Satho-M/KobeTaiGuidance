using HtmlAgilityPack;
using KobeTaiGuidane_ClassLibrary;
using System.Text.RegularExpressions;

namespace KobeTaiGuidance_Client.Get
{
    public class Info_Band
    {
        HtmlWeb web;
        public Info_Band()
        {
            web = new();
        }

        public Band GetBandBaseInformation(string bandId)
        {
            Band band = new();
            var htmlDoc = web.Load("https://73.popmundo.com/World/Popmundo.aspx/Band/" + bandId);

            //Get Name
            string name = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='entityLogoNoImg gameimage idTrigger']/h2").InnerText;
            band.Name = name;

            //Get Rank
            string rank = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='content']//p").InnerText;

            string pattern = @"(#\d*)";
            rank = Regex.Match(rank, pattern).Groups[1].Value;

            band.Rank = short.Parse(rank);


            //Get Members
            List<string> characterIds = new();
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//div[@id='ctl00_cphLeftColumn_ctl01_divCurrentMembers']//a[@id='ctl00_cphLeftColumn_ctl01_repArtistMembers_']"))
            {
                characterIds.Add(node.Attributes["href"].Value.ToString().Split('/')[5]);
            }
        }
    }
}
