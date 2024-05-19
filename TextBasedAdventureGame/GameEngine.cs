﻿using Spectre.Console;
using System.Collections;

namespace TextBasedAdventureGame;

internal class GameEngine
{
    private Location[] _locationsList;
    private Player _player;
    private readonly TimeSpan _sleep = TimeSpan.FromMilliseconds(5000);
    private readonly TimeSpan _timeToFight = TimeSpan.FromMilliseconds(2000);

    public GameEngine()
    {
        _locationsList = new Location[10];
    }

    public void BuildAllLevels()
    {
        // Level1
        Item item = new Item("Agua Ultrasagrada", ItemType.SANITY, "El agua ultrasagrada es un líquido diferente al agua corriente y sirve para incrementar la fuerza y la agilidad.");
        QuestionCharacter questionCharacter = new QuestionCharacter(QuestionConstants.Question1, "", item);
        BuildLevelWithObstruction("Level 1", "Prepara tu camino", questionCharacter, 0);

        // Level2
        Item item2 = new Item("KAME HAME HA", ItemType.POWER, "gran cantidad de energía en las manos");
        QuestionCharacter questionCharacter2 = new QuestionCharacter(QuestionConstants.Question2, "", item2);
        BuildLevelWithObstruction("Level 2", "Siente el poder en tus manos", questionCharacter2, 1);

        // Level3
        Item item3 = new Item("Semillas del hermitanio", ItemType.SANITY, "Recupera tus energias");
        Boss boss = new Boss("Piccolo Maligno", "Es un guerreno del mal de la raza Namekusei", item3, 1300, 300);
        BuildLevelWithObstruction("Level 3", "Gran rey demonio Piccolo", boss, 2);

        // Level4
        Item item4 = new Item("Nube Voladora", ItemType.VELOCITY, "es un objeto mágico que sirve para transportar al guerrero que lo utiliza, siempre y cuando sea de corazón puro y mente limpia.");
        QuestionCharacter questionCharacter4 = new QuestionCharacter(QuestionConstants.Question3, "", item4);
        BuildLevelWithObstruction("Level 4", "Vuela alto a una gran velocidad", questionCharacter4, 3);

        // Level5
        Item item5 = new Item("Kaioken", ItemType.VELOCITY, "es una técnica poderosa que permite a los guerreros aumentar su fuerza y velocidad temporalmente.");
        QuestionCharacter questionCharacter5 = new QuestionCharacter(QuestionConstants.Question5, "", item5);
        BuildLevelWithObstruction("Level 5", "Aumenta tu poder por 10", questionCharacter5, 4);

        // Level6
        Item item6 = new Item("Super Saiyajin", ItemType.POWER, "Es el primer nivel que puede alcanzar un saiyajin");
        Boss boss2 = new Boss("Freezer", "Es un tirano intergaláctico", item6, 1800, 800);
        BuildLevelWithObstruction("Level 6", "El mundo esta en peligro", boss2, 5);

        // Level7
        Item item7 = new Item("Resplandor Final", ItemType.POWER, "El ataque definitivo de Vegeta");
        QuestionCharacter questionCharacter7 = new QuestionCharacter(QuestionConstants.Question6, "", item7);
        BuildLevelWithObstruction("Level 7", "Destruccion Masiva", questionCharacter7, 6);

        // Level8
        Item item8 = new Item("Genki-dama", ItemType.POWER, "Es una técnica de combate de naturaleza ofensiva que requiere una parte de la energía de todas las criaturas");
        QuestionCharacter questionCharacter8 = new QuestionCharacter(QuestionConstants.Question7, "", item8);
        BuildLevelWithObstruction("Level 8", "Siente el poder de todo un planeta", questionCharacter8, 7);

        // Level9
        Item item9 = new Item("Super Saiyajin Fase 2", ItemType.POWER, "Es el segundo nivel que puede alcanzar un saiyajin");
        QuestionCharacter questionCharacter9 = new QuestionCharacter(QuestionConstants.Question9, "", item9);
        BuildLevelWithObstruction("Level 9", "Un nuevo poder ha despertado", questionCharacter9, 8);

        // Level10
        Item item10 = new Item("Esferas del Dragon", ItemType.SANITY, "Son 7 esferas que te ayudan a llamar a Shen Long para pedir un deseo.");
        Boss boss3 = new Boss("Cell", "Es un bioandroide creado por la computadora del Dr. Gero", item10, 3000, 1000);
        BuildLevelWithObstruction("Level 10", "Es tu oportunidad de revivir a tus seres queridos", boss3, 9);
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
        Thread.Sleep(_sleep*3);
        Console.WriteLine(GameConstants.PressKeyToStartGame);
        Console.ReadKey();
        Console.Clear();
    }

    public void ShowInformationForPlayer()
    {
        AnsiConsole.MarkupLine($"[bold]{GameConstants.InformationForPlayer}\n[/]");
        AnsiConsole.MarkupInterpolated($"[orange1]{_player.Name}{GameConstants.InformationFirstSentence}\n[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationSecondSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationThirdSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationFourthSentence}[/]");
        AnsiConsole.MarkupLine($"[orange1]{GameConstants.InformationFifthSentence}[/]");
        Thread.Sleep(_sleep);
        ContinueWithGame();
        Console.Clear();
    }

    public void PlayGame()
    {
        var level = 1;
        var flag = false;
        string answer = string.Empty;
        var finishFlag = false;

        while(_player.LifePoints > 0 && !finishFlag)
        {
            var tablePointsOfPlayer = new Table();
            var tablePointsOfBoss = new Table();
            var recoverLifePoints = 0;
            Item item;

            switch (level)
            {
                case 1:
                    GetLocation(0).ShowLocationInformation();
                    flag = QuestionHandler.AskQuestionWithNoConfirmation(GetLocation(0).NonPlayerCharacter.Name);
                    if (flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(0).NonPlayerCharacter.Item);
                        Console.WriteLine($"Felicidades, ganaste el {GetLocation(0).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {QuestionConstants.Answer1}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 2:
                    GetLocation(1).ShowLocationInformation();
                    string[] options = {"Oolong", "Puar", "Ten Ten"};
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(1).NonPlayerCharacter.Name, options);
                    if (answer.Equals(QuestionConstants.Answer2))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        _player.AddItem(GetLocation(1).NonPlayerCharacter.Item);
                        Console.WriteLine($"Ahora podras hacer un {GetLocation(1).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, su mejor amigo es {QuestionConstants.Answer2}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 3:
                    GetLocation(2).ShowLocationInformation();
                    var firstBoss = (Boss)GetLocation(2).NonPlayerCharacter;
                    Console.WriteLine($"Un jefe esta frente a ti!!!\nEs {firstBoss.Name}.");
                    firstBoss.ShowBossPoints();
                    Thread.Sleep(_sleep);
                    _player.ShowInformation();
                    item = _player.SelectItemToFight();
                    _player.IncreasePower(item);
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    while (firstBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        Console.WriteLine("Ataca con todo tu poder!!!");
                        Console.Write($"{AnimationStrings.animation1}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation2}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        firstBoss.ReceiveAttack(_player.AttackPoints);
                        if (firstBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        Console.WriteLine($"Preparate para el ataque de {firstBoss.Name}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        _player.ReceiveAttack(firstBoss.AttackPoints);
                        recoverLifePoints += firstBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        Console.WriteLine($"Felicidades!!!, lograste vencer a {firstBoss.Name}." );
                        Console.WriteLine($"Ganaste las {GetLocation(2).NonPlayerCharacter.Item.Name}");
                        _player.AddItem(GetLocation(2).NonPlayerCharacter.Item);
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
                    flag = QuestionHandler.AskQuestionWithYesConfirmation(GetLocation(3).NonPlayerCharacter.Name);
                    if (!flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(3).NonPlayerCharacter.Item);
                        Console.WriteLine($"Felicidades, ganaste una {GetLocation(3).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {QuestionConstants.Answer3}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 5:
                    GetLocation(4).ShowLocationInformation();
                    string[] options2 = { "Resplandor Final", "Genki-dama", "Makankosappo" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(4).NonPlayerCharacter.Name, options2);
                    if (answer.Equals(QuestionConstants.Answer5))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(4).NonPlayerCharacter.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras hacer el {GetLocation(4).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, La tecnica de Piccolo es {QuestionConstants.Answer5}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 6:
                    GetLocation(5).ShowLocationInformation();
                    var secondBoss = (Boss)GetLocation(5).NonPlayerCharacter;
                    Console.WriteLine($"Un jefe esta frente a ti!!!\nEs el gran {secondBoss.Name}.");
                    secondBoss.ShowBossPoints();
                    Thread.Sleep(_sleep);
                    _player.ShowInformation();
                    item = _player.SelectItemToFight();
                    _player.IncreasePower(item);
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    while (secondBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        Console.WriteLine("Ataca con todo tu poder!!!");
                        Console.Write($"{AnimationStrings.animation1}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation2}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        secondBoss.ReceiveAttack(_player.AttackPoints);
                        if (secondBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        Console.WriteLine($"Preparate para el ataque de {secondBoss.Name}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        _player.ReceiveAttack(secondBoss.AttackPoints);
                        recoverLifePoints += secondBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        Console.WriteLine($"Felicidades!!!, lograste vencer a {secondBoss.Name}.");
                        Console.WriteLine($"Ahora puedes convertirte en un {GetLocation(5).NonPlayerCharacter.Item.Name}");
                        _player.AddItem(GetLocation(5).NonPlayerCharacter.Item);
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
                    flag = QuestionHandler.AskQuestionWithNoConfirmation(GetLocation(6).NonPlayerCharacter.Name);
                    if (flag)
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(6).NonPlayerCharacter.Item);
                        Console.WriteLine($"Felicidades, podras usar el {GetLocation(6).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrecto, {QuestionConstants.Answer6}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 8:
                    GetLocation(7).ShowLocationInformation();
                    string[] options3 = { "Mark", "Miguel", "Kenny" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(7).NonPlayerCharacter.Name, options3);
                    if (answer.Equals(QuestionConstants.Answer7))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(7).NonPlayerCharacter.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras hacer la {GetLocation(7).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, Su verdadero nombre es {QuestionConstants.Answer7}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 9:
                    GetLocation(8).ShowLocationInformation();
                    string[] options4 = { "Gohan", "Vegeta", "Goku" };
                    answer = QuestionHandler.PromptQuestionWithSimpleSelect(GetLocation(8).NonPlayerCharacter.Name, options4);
                    if (answer.Equals(QuestionConstants.Answer9))
                    {
                        _player.IncreaseLifePoints(PlayerConstants.LifePointsByQuestion);
                        _player.AddItem(GetLocation(8).NonPlayerCharacter.Item);
                        Console.WriteLine("Felicidades!, la respuesta es correcta.");
                        Console.WriteLine($"Ahora podras convertirte en un {GetLocation(8).NonPlayerCharacter.Item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Mala respuesta, Fue {QuestionConstants.Answer9}");
                    }

                    level++;
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    ContinueWithGame();
                    Console.Clear();
                    break;

                case 10:
                    GetLocation(9).ShowLocationInformation();
                    var thirdBoss = (Boss)GetLocation(9).NonPlayerCharacter;
                    Console.WriteLine($"Un jefe esta frente a ti!!!\nEs el poderoso {thirdBoss.Name}.");
                    thirdBoss.ShowBossPoints();
                    Thread.Sleep(_sleep);
                    _player.ShowInformation();
                    item = _player.SelectItemToFight();
                    _player.IncreasePower(item);
                    _player.ShowPlayerPoints();
                    Thread.Sleep(_sleep);
                    while (thirdBoss.LifePoints > 0 && _player.LifePoints > 0)
                    {
                        Console.WriteLine("Ataca con todo tu poder!!!");
                        Console.Write($"{AnimationStrings.animation1}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation2}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        thirdBoss.ReceiveAttack(_player.AttackPoints);
                        if (thirdBoss.LifePoints <= 0)
                        {
                            break;
                        }
                        Console.WriteLine($"Preparate para el ataque de {thirdBoss.Name}");
                        Thread.Sleep(_timeToFight);
                        Console.WriteLine($"{AnimationStrings.animation3}");
                        Thread.Sleep(_timeToFight);
                        _player.ReceiveAttack(thirdBoss.AttackPoints);
                        recoverLifePoints += thirdBoss.AttackPoints;
                    }

                    if (_player.LifePoints > 0)
                    {
                        _player.AddItem(GetLocation(9).NonPlayerCharacter.Item);
                        _player.LifePoints += recoverLifePoints;
                        Console.WriteLine($"Felicidades!!!, lograste vencer a {thirdBoss.Name}.");
                        Console.WriteLine($"Lograste obtener todas las {GetLocation(9).NonPlayerCharacter.Item.Name}, eres un verdadero Guerrero Z.");
                        Thread.Sleep(_sleep);
                        ContinueWithGame();
                        Console.Clear();
                        Console.WriteLine("Es hora de llamar a Shen Long para pedir el deseo que mas anhelas.");
                        Thread.Sleep(_sleep);
                        PrintDragon();
                        Thread.Sleep(_sleep);
                        Console.WriteLine($"\n{_player.Name} piensa bien y pide un deseo!");
                        Thread.Sleep(_sleep);
                        Console.WriteLine("Como lo pediste, todos los que murieron durante la invasión al planeta Tierra fueron revividos.");
                        Thread.Sleep(_sleep);
                        Console.WriteLine("Hasta una proxima ocasión.");
                        Thread.Sleep(_sleep);
                    }
                    else
                    {
                        Console.WriteLine("Perdiste, la siguiente vez lo haras mejor.\nEntrena para ser mas fuerte.");
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

    private void BuildLevelWithObstruction(string name, string description, NonPlayerCharacter obstruction, int position)
    {
        Location location = new Location(name, description, obstruction);
        _locationsList[position] = location;
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
