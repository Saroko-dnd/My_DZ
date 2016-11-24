using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess
{
    class RaceWebsiteDBInitializer : CreateDatabaseIfNotExists<RaceApplicationDataContext>
    {
        protected override void Seed(RaceApplicationDataContext Context)
        {
            List<Racer> TestListOfRacers = new List<Racer>();

            TestListOfRacers.Add(new Racer("Alain", "Prost", "Alain Prost is a famous French racing driver. A four-time Formula One Drivers’ Champion, only Michael Schumacher (seven-time championship) has equalled or surpassed his number of titles. From 1987 until 2001, Prost held the record for most Grand Prix victories. In 1999, Prost received the World Sports Awards of the Century in the motor sport category.During the 1980’s and early 1990’s, Prost formed fierce rivalries, mainly with Ayrton Senna, but also Nelson Piquet and Nigel Mansell.In 1986, at the last race of the season, he beat Mansell and Piquet of Williams to the title after Mansell retired late on in the race, and Piquet was pulled in for a late precautionary pit stop.Senna joined Prost at McLaren in 1988, and the two had a series of controversial clashes, including a collision at the 1989 Japanese Grand Prix that gave Prost his third Drivers’ Championship.Prost employed a smooth, relaxed style behind the wheel, deliberately modeling himself on personal heroes like Jackie Stewart and Jim Clark.He was nicknamed “The Professor” for his intellectual approach to competition, though it was a name he did not particularly care for. Skilled at setting up his car for race conditions, Prost would often conserve his brakes and tyres early on in a race, leaving them fresher for a challenge at the end.", "FF0000", "Porsche 911 RSR", 250));
            TestListOfRacers.Add(new Racer("Nigel", "Mansell", "Nigel Mansell is a retired British racing driver who won both the Formula One World Championship (1992) and the CART Indy Car World Series (1993). Mansell was the reigning F1 champion when he moved over to CART, becoming the first person to win the CART title in his debut season, and making him the only person to hold both titles simultaneously.His career in Formula One spanned 15 seasons, with his final two full seasons of top - level racing being spent in the CART series.Mansell is the second most successful British Formula One driver of all time in terms of race wins, with 31 victories, and is seventh overall on the Formula One race winners list behind Michael Schumacher, Alain Prost, Ayrton Senna, Sebastian Vettel, Fernando Alonso, and Lewis Hamilton.He held the record for the most number of poles set in a single season, which was broken in 2011 by Sebastian Vettel.He was rated in the top 5 Formula One drivers of all time by longtime Formula One commentator Murray Walker.In 2008, Entertainment and Sports Programming Network put him in their top drivers of all - time.He was also ranked No. 9 of the 50 greatest race drivers of all time by the Times Online, on a list that also included such drivers as Alain Prost, Ayrton Senna, Jackie Stewart, and Jim Clark.Mansell raced in the Grand Prix Masters series in 2005, and won the championship title.He was inducted to the International Motorsports Hall of Fame in 2005.","0011FF", "Nissan GT-R LM", 200));
            TestListOfRacers.Add(new Racer("Dale", "Earnhardt", "Dale Earnhardt, also known as The Intimidator, was an American race car driver and team owner, best known for his involvement in stock car racing for NASCAR. Earnhardt began his career in 1975 when he participated in the 1975 World 600 at Charlotte Motor Speedway as part of the Winston Cup Series (later the Sprint Cup Series).Considered one of the best NASCAR drivers of all time, Earnhardt won a total of 76 Winston Cup races over the course of his career, including one Daytona 500 victory in 1998.He also earned 7 NASCAR Winston Cup Championships, which is tied for the most all time with Richard Petty.His aggressive driving style earned him the nickname “The Intimidator”.On February 18, 2001, at Daytona International Speedway, while participating in the Daytona 500, Earnhardt was involved in a last-lap crash and died of a basilar skull fracture. He has been inducted into numerous halls of fame, including the inaugural class of the NASCAR Hall of Fame.","48A41A", "Subaru VT15R STI", 225));

            foreach (Racer CurrentRacer in TestListOfRacers)
            {
                Context.Racers.Add(CurrentRacer);
            }

            base.Seed(Context);
        }
    }
}
                       