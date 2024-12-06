using Spectre.Console;
using System;
using System.Collections;
using TextBasedAdventureGame.Constants;
using TextBasedAdventureGame.Enums;
using TextBasedAdventureGame.Interfaces;

namespace TextBasedAdventureGame.Classes;

internal class GameEngine
{
    private List<Location> _locationsList;
    private Player _player;
    private readonly TimeSpan _sleep = TimeSpan.FromMilliseconds(5000);
    private readonly TimeSpan _timeToFight = TimeSpan.FromMilliseconds(2000);

    public GameEngine()
    {
        _locationsList = [];
    }

    public void BuildAllLevels()
    {
        // Level1
        Item item = new Item("Agua Ultrasagrada", ItemType.SANITY, "El agua ultrasagrada es un líquido diferente al agua corriente y sirve para incrementar la fuerza y la agilidad.");
        YesNoCharacter yesNoCharacter = new YesNoCharacter(QuestionConstants.Question1, "", item, QuestionConstants.Answer1);
        BuildLevelWithQuestion("Level 1", "Prepara tu camino", yesNoCharacter, 0);

        // Level2
        Item item2 = new Item("KAME HAME HA", ItemType.POWER, "gran cantidad de energía en las manos");
        QuestionCharacter questionCharacter2 = new QuestionCharacter(QuestionConstants.Question2, "", item2, QuestionConstants.Answer2);
        BuildLevelWithQuestion("Level 2", "Siente el poder en tus manos", questionCharacter2, 1);

        // Level3
        Item item3 = new Item("Semillas del hermitanio", ItemType.SANITY, "Recupera tus energias");
        Boss boss = new Boss("Piccolo Maligno", "Es un guerreno del mal de la raza Namekusei", item3, 1300, 300);
        BuildLevelWithBoss("Level 3", "Gran rey demonio Piccolo", boss, 2);

        // Level4
        Item item4 = new Item("Nube Voladora", ItemType.VELOCITY, "es un objeto mágico que sirve para transportar al guerrero que lo utiliza, siempre y cuando sea de corazón puro y mente limpia.");
        QuestionCharacter questionCharacter4 = new QuestionCharacter(QuestionConstants.Question3, "", item4, QuestionConstants.Answer3);
        BuildLevelWithQuestion("Level 4", "Vuela alto a una gran velocidad", questionCharacter4, 3);

        // Level5
        Item item5 = new Item("Kaioken", ItemType.VELOCITY, "es una técnica poderosa que permite a los guerreros aumentar su fuerza y velocidad temporalmente.");
        QuestionCharacter questionCharacter5 = new QuestionCharacter(QuestionConstants.Question5, "", item5, QuestionConstants.Answer5);
        BuildLevelWithQuestion("Level 5", "Aumenta tu poder por 10", questionCharacter5, 4);

        // Level6
        Item item6 = new Item("Super Saiyajin", ItemType.POWER, "Es el primer nivel que puede alcanzar un saiyajin");
        Boss boss2 = new Boss("Freezer", "Es un tirano intergaláctico", item6, 1800, 800);
        BuildLevelWithBoss("Level 6", "El mundo esta en peligro", boss2, 5);

        // Level7
        Item item7 = new Item("Resplandor Final", ItemType.POWER, "El ataque definitivo de Vegeta");
        QuestionCharacter questionCharacter7 = new QuestionCharacter(QuestionConstants.Question6, "", item7, QuestionConstants.Answer6);
        BuildLevelWithQuestion("Level 7", "Destruccion Masiva", questionCharacter7, 6);

        // Level8
        Item item8 = new Item("Genki-dama", ItemType.POWER, "Es una técnica de combate de naturaleza ofensiva que requiere una parte de la energía de todas las criaturas");
        QuestionCharacter questionCharacter8 = new QuestionCharacter(QuestionConstants.Question7, "", item8, QuestionConstants.Answer7);
        BuildLevelWithQuestion("Level 8", "Siente el poder de todo un planeta", questionCharacter8, 7);

        // Level9
        Item item9 = new Item("Super Saiyajin Fase 2", ItemType.POWER, "Es el segundo nivel que puede alcanzar un saiyajin");
        QuestionCharacter questionCharacter9 = new QuestionCharacter(QuestionConstants.Question9, "", item9, QuestionConstants.Answer9);
        BuildLevelWithQuestion("Level 9", "Un nuevo poder ha despertado", questionCharacter9, 8);

        // Level10
        Item item10 = new Item("Esferas del Dragon", ItemType.SANITY, "Son 7 esferas que te ayudan a llamar a Shen Long para pedir un deseo.");
        Boss boss3 = new Boss("Cell", "Es un bioandroide creado por la computadora del Dr. Gero", item10, 3000, 1000);
        BuildLevelWithBoss("Level 10", "Es tu oportunidad de revivir a tus seres queridos", boss3, 9);
    }

    public void ShowGameName()
    {
        AnsiConsole.MarkupInterpolated($"[bold darkgreen]{AnimationStrings.gameName}[/]");
    }

    public string GetPlayerName()
    {
        var name = AnsiConsole.Ask<string>("Cual es tu [bold orange1]nombre[/]?");
        var initialItem = new Item("Baculo Sagrado", ItemType.VELOCITY, "El báculo sagrado era de color rojo y fue un obsequio del abuelo Gohan para que el pequeño Son Gokú ingresara a la Torre del Maestro Karim, pues este objeto místico tenía la habilidad de incrementar de tamaño, pero su función principal era la de conectar esta localización con el Palacio del Dios de la Tierra, Kami- Sama.");
        _player = new Player(name, "Un nuevo heroe aparece", 1000, 100);
        _player.AddItem(initialItem);
        var welcome = $"\nBienvenido {name}!!!\n";
        AnsiConsole.MarkupInterpolated($"[bold orange1]{welcome}[/]");
        Thread.Sleep(_sleep);

        return name;
    }

    public void ShowIntroduction()
    {
        Console.Clear();
        var panel = new Panel($"[orange1]{GameConstants.Introduction}[/]");
        panel.Header = new PanelHeader($"[bold]Dragon Ball Game[/]");
        panel.Expand = true;
        AnsiConsole.Write(panel);
        Console.WriteLine(GameConstants.PressKeyToStartGame);
        Console.ReadKey();
        Console.Clear();
    }

    public void ShowInformationForPlayer()
    {
        AnsiConsole.MarkupLine($"[bold]{GameConstants.InformationForPlayer}\n[/]");
        AnsiConsole.MarkupInterpolated($"[orange1]{_player.Name} {GameConstants.InformationFirstSentence}\n[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationSecondSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationThirdSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationFourthSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationFifthSentence}[/]");
        ContinueWithGame();
        Console.Clear();
    }

    public void PlayGame()
    {
        var level = 1;
        var flag = false;
        string answer = string.Empty;
        var finishFlag = false;

        while (_player.LifePoints > 0 && !finishFlag)
        {
            var tablePointsOfPlayer = new Table();
            var tablePointsOfBoss = new Table();
            var recoverLifePoints = 0;
            Item item;

            switch (level)
            {
                case 1:
                    GetLocation(0).ShowLocationInformation();
                    flag = QuestionHandler.AskQuestionWithNoConfirmation(GetLocation(0).Question.Name);
                    if (flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(0).Question.Item);
                        Console.WriteLine($"Felicidades, ganaste el {GetLocation(0).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {GetLocation(0).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 2:
                    GetLocation(1).ShowLocationInformation();
                    string[] options = { "Oolong", "Puar", "Ten Ten" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(1).Question.Name, options);
                    if (answer.Equals(GetLocation(1).Question.Answer))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        _player.AddItem(GetLocation(1).Question.Item);
                        Console.WriteLine($"Ahora podras hacer un {GetLocation(1).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, su mejor amigo es {GetLocation(1).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 3:
                    GetLocation(2).ShowLocationInformation();
                    var firstBoss = (Boss)GetLocation(2).Character;
                    firstBoss.InitialInteraction();
                    Thread.Sleep(_sleep);
                    _player.InitialInteraction();
                    Thread.Sleep(_sleep);
                    while (firstBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        _player.InteractInGame(firstBoss);
                        if (firstBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        firstBoss.InteractInGame(_player);
                        recoverLifePoints += firstBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        Console.WriteLine($"Felicidades!!!, lograste vencer a {firstBoss.Name}.");
                        Console.WriteLine($"Ganaste las {GetLocation(2).Character.Item.Name}");
                        _player.AddItem(GetLocation(2).Character.Item);
                        _player.LifePoints += recoverLifePoints;
                        Thread.Sleep(_sleep);
                        ContinueWithGame();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Perdiste, la siguiente vez lo haras mejor.");
                        Thread.Sleep(_sleep);
                    }

                    level++;
                    break;

                case 4:
                    GetLocation(3).ShowLocationInformation();
                    flag = QuestionHandler.AskQuestionWithYesConfirmation(GetLocation(3).Question.Name);
                    if (!flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(3).Question.Item);
                        Console.WriteLine($"Felicidades, ganaste una {GetLocation(3).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {GetLocation(3).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 5:
                    GetLocation(4).ShowLocationInformation();
                    string[] options2 = { "Resplandor Final", "Genki-dama", "Makankosappo" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(4).Question.Name, options2);
                    if (answer.Equals(GetLocation(4).Question.Answer))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(4).Question.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras hacer el {GetLocation(4).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, La tecnica de Piccolo es {GetLocation(4).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 6:
                    GetLocation(5).ShowLocationInformation();
                    var secondBoss = (Boss)GetLocation(5).Character;
                    secondBoss.InitialInteraction();
                    Thread.Sleep(_sleep);
                    _player.InitialInteraction();
                    Thread.Sleep(_sleep);
                    while (secondBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        _player.InteractInGame(secondBoss);
                        if (secondBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        secondBoss.InteractInGame(_player);
                        recoverLifePoints += secondBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        Console.WriteLine($"Felicidades!!!, lograste vencer a {secondBoss.Name}.");
                        Console.WriteLine($"Ahora puedes convertirte en un {GetLocation(5).Character.Item.Name}");
                        _player.AddItem(GetLocation(5).Character.Item);
                        _player.LifePoints += recoverLifePoints;
                        Thread.Sleep(_sleep);
                        ContinueWithGame();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Perdiste, la siguiente vez lo haras mejor.");
                        Thread.Sleep(_sleep);
                    }

                    level++;

                    break;

                case 7:
                    GetLocation(6).ShowLocationInformation();
                    flag = QuestionHandler.AskQuestionWithNoConfirmation(GetLocation(6).Question.Name);
                    if (flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(6).Question.Item);
                        Console.WriteLine($"Felicidades, podras usar el {GetLocation(6).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {GetLocation(6).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 8:
                    GetLocation(7).ShowLocationInformation();
                    string[] options3 = { "Mark", "Miguel", "Kenny" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(7).Question.Name, options3);
                    if (answer.Equals(GetLocation(7).Question.Answer))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(7).Question.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras hacer la {GetLocation(7).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, Su verdadero nombre es {GetLocation(7).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 9:
                    GetLocation(8).ShowLocationInformation();
                    string[] options4 = { "Gohan", "Vegeta", "Goku" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(8).Question.Name, options4);
                    if (answer.Equals(GetLocation(8).Question.Answer))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(8).Question.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras convertirte en un {GetLocation(8).Question.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, Fue {GetLocation(8).Question.Answer}");
                    }

                    level++;
                    _player.ShowPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 10:
                    GetLocation(9).ShowLocationInformation();
                    var thirdBoss = (Boss)GetLocation(9).Character;
                    thirdBoss.InitialInteraction();
                    Thread.Sleep(_sleep);
                    _player.InitialInteraction();
                    Thread.Sleep(_sleep);
                    while (thirdBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        _player.InteractInGame(thirdBoss);
                        if (thirdBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        thirdBoss.InteractInGame(_player);
                        recoverLifePoints += thirdBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        _player.AddItem(GetLocation(9).Character.Item);
                        _player.LifePoints += recoverLifePoints;
                        Console.WriteLine($"{GameConstants.FinalPartFirstSentence} {thirdBoss.Name}.");
                        Console.WriteLine($"{GameConstants.FinalPartSecondSentence} {GetLocation(9).Character.Item.Name}, {GameConstants.FinalPartThirdSentence}");
                        Thread.Sleep(_sleep);
                        ContinueWithGame();
                        Console.Clear();
                        Console.WriteLine($"{GameConstants.FinalPartFourthSentence}");
                        Thread.Sleep(_sleep);
                        PrintDragon();
                        Thread.Sleep(_sleep);
                        Console.WriteLine($"\n{_player.Name} {GameConstants.FinalPartFifthSentence}");
                        Thread.Sleep(_sleep);
                        Console.WriteLine($"{GameConstants.FinalPartSixthSentence}");
                        Thread.Sleep(_sleep);
                        Console.WriteLine($"{GameConstants.FinalPartSevenSentence}");
                        Thread.Sleep(_sleep);
                    }
                    else
                    {
                        Console.WriteLine($"{GameConstants.FinalPartLoseSentence}");
                    }

                    level++;
                    Thread.Sleep(_sleep);
                    Console.Clear();
                    break;

                default:
                    finishFlag = true;
                    break;
            }
        }
    }

    private void BuildLevelWithBoss(string name, string description, ICharacter character, int position)
    {
        Location location = new Location(name, description, character);
        _locationsList.Add(location);
    }

    private void BuildLevelWithQuestion(string name, string description, IQuestion question, int position)
    {
        Location location = new Location(name, description, question);
        _locationsList.Add(location);
    }

    private Location GetLocation(int position)
    {
        return _locationsList[position];
    }

    private void PrintDragon()
    {
        AnsiConsole.MarkupInterpolated($"[bold darkgreen]{AnimationStrings.dragon}[/]");
    }

    private void ContinueWithGame()
    {
        Console.WriteLine(GameConstants.PressKeyToContinueGame);
        Console.ReadKey();
    }
}
