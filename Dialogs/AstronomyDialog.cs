using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ProtostarAlphaBot.Models;

namespace ProtostarAlphaBot.Dialogs
{
    [Serializable]
    public class AstronomyDialog: IDialog<object>
    {
        //protected int Count = 1;
        protected bool GreetedYet = false;
        protected string LastMessage = string.Empty;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (!GreetedYet)
            {
                var rnd = new Random(DateTime.UtcNow.Millisecond);
                var next = rnd.Next(0, 6);
                var greetingChoices = new[]
                {
                    "Hello!", "Hey there!", "Another human?  Ok!", "This should be fun...", "Sit.  Stay.  Chat awhile.",
                    "You're not an orc.  You may live.", "You're not Ambassador Spock..."
                };
                await context.PostAsync(greetingChoices[next]);
                await context.PostAsync("I specialize in astronomy.  Name a planet, star, or some other object, and I'll see if I have that info!");
                GreetedYet = true;
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                // Store this response to process based on user input.
                LastMessage = message.Text.ToLowerInvariant();

                switch (message.Text.ToLowerInvariant())
                {
                    case "reset":
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "We got off on the wrong foot...  start over?",
                            "Hmm?  Didn't quite get that...");
                        break;
                    case "sun":
                    case "sol":
                        // correct for alias
                        LastMessage = "sun";
                        await context.PostAsync(
                            "Earth's sun, Sol...  the only known star inside our starsystem (unless you count that Nemesis theory...)");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about the sun?",
                            "I'm sorry, what?");
                        break;
                    case "mercury":
                        await context.PostAsync("Mercury...  little planet that's a bit close to the sun.");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Mercury?",
                            "I'm sorry, what?");
                        break;
                    case "venus":
                        await context.PostAsync("Venus?  Almost as big as the Earth.");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Venus?",
                            "I'm sorry, what?");
                        break;
                    case "earth":
                        await context.PostAsync("You don't know about Earth already?");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Earth?",
                            "I'm sorry, what?");
                        break;
                    case "mars":
                        await context.PostAsync("Mars, the angry little red planet.");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Mars?",
                            "I'm sorry, what?");
                        break;
                    case "jupiter":
                        await context.PostAsync("Jupiter.  It's big.");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Jupiter?",
                            "I'm sorry, what?");
                        break;
                    case "saturn":
                        await context.PostAsync("Saturn...  rings upon rings.");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Saturn?",
                            "I'm sorry, what?");
                        break;
                    case "uranus":
                        await context.PostAsync("Uranus is an odd world...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Uranus?",
                            "I'm sorry, what?");
                        break;
                    case "neptune":
                        await context.PostAsync("Neptune, lord of the Seas...  oh wait, we're talking the planet...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Neptune?",
                            "I'm sorry, what?");
                        break;
                    case "pluto":
                        await context.PostAsync("Didn't Pluto get demoted?");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Pluto?",
                            "I'm sorry, what?");
                        break;
                    case "eris":
                        await context.PostAsync("Eris...  such an instigator...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Eris?",
                            "I'm sorry, what?");
                        break;
                    case "haumea":
                        await context.PostAsync("Haumea...  that's an interesting name");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Venus?",
                            "I'm sorry, what?");
                        break;
                    case "halley's comet":
                    case "haley's comet":
                    case "hailey's comet":
                        // correct for alias
                        LastMessage = "halley's comet";
                        await context.PostAsync("Ah, you're going cold here with Halley's Comet...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Halley's Comet?",
                            "I'm sorry, what?");
                        break;
                    case "ceres":
                        await context.PostAsync(
                            "Ceres could have been some planet's moon if the solar system formed differently, maybe?");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Ceres?",
                            "I'm sorry, what?");
                        break;
                    case "vesta":
                        await context.PostAsync("Vesta's got a nice name to it...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Vesta?",
                            "I'm sorry, what?");
                        break;
                    case "algol":
                        await context.PostAsync("Such an evil, evil star, Algol...");
                        PromptDialog.Confirm(
                            context,
                            AfterResponseAsync,
                            "Learn more about Algol?",
                            "I'm sorry, what?");
                        break;
                    default:
                        var rnd = new Random(DateTime.UtcNow.Millisecond);
                        var choices = new[]
                        {
                            "I'm not famliar with that...", "I'm not sure I understand...", "Whatever you say...",
                            "What?",
                            "Umm, I'm confused...", "Where am I, and why am I in this handbasket?",
                            "You're not Alan Turing, are you?", "I am Bender, please insert girder.",
                            "Has V'Ger returned yet?",
                            "We are the Borg.  You will be assimilated.  Resistance is futile."
                        };
                        var next = rnd.Next(0, choices.Length);
                        await context.PostAsync(choices[next]);
                        context.Wait(MessageReceivedAsync);
                        break;
                }
            }
        }

        public async Task AfterResponseAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm && LastMessage == "reset")
            {
                GreetedYet = false;
                await context.PostAsync("Starting over!");
            } else if (confirm)
            {
                //get the Json filepath  
                var file = HttpContext.Current.Server.MapPath("~/Data/celestialdata.json");
                //deserialize JSON from file  
                var json = System.IO.File.ReadAllText(file);
                var ser = new JavaScriptSerializer();
                var celestialDataObjects = ser.Deserialize<List<CelestialDataObject>>(json);
                var thisObject = celestialDataObjects.First(x => x.Name.ToLower() == LastMessage);

                if (thisObject == null)
                {
                    await context.PostAsync("Aw crap, I didn't find that one...");
                }
                else
                {
                    var message = context.MakeMessage();
                    var attachment = new HeroCard {
                        Title = thisObject.Name, Subtitle = thisObject.Type == CelestialDataObject.CelestialObjectType.DwarfPlanet ? "Dwarf Planet" : thisObject.Type.ToString(),
                        Text = thisObject.Info
                    }.ToAttachment();

                    message.Attachments.Add(attachment);

                    await context.PostAsync(message);
                    var rnd = new Random(DateTime.UtcNow.Millisecond);
                    var choices = new[]
                    {
                        "Let's examine another celestial body...", "Name another planet?", "Pick something else?",
                        "Don't stop now...  let's see something else!",
                        "I know other bodies too.  Ask me.",
                        "Not done yet, I hope."
                    };
                    var next = rnd.Next(0, choices.Length);
                    await context.PostAsync(choices[next]);
                }
            }
            else
            {
                await context.PostAsync("Let's keep going then.");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}