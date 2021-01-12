using System;
using System.Threading.Tasks;
using Autofac;
using PokerHandSorter.Services;

namespace PokerHandSorter
{
    public class PokerHandApplication
    {
        public IContainer Container;

        public PokerHandApplication(IContainer container)
        {
            Container = container;
        }

        internal async Task<string> Start()
        {
            var result = "And the winner is ";
            using (Container.BeginLifetimeScope())
            {
                var appHandler = Container.Resolve<IPokerGameService>();
                var args = "";
                //var count = appHandler.CountWinningHands(args);

                //result = string.Concat(result, count);
            }

            return result;
        }
    }
}
