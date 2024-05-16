namespace TextBasedAdventureGame;

using Spectre.Console;

internal static class Question
{
    public static bool ConfirmationQuestionWithNo(string question)
    {
        if (!AnsiConsole.Confirm($"[green]{question}[/]"))
        {
            AnsiConsole.MarkupLine("Muy bien, la respuesta es correcta.");

            return true;
        }

        return false;
    }

    public static bool ConfirmationQuestionWithYes(string question)
    {
        if (AnsiConsole.Confirm($"[green]{question}[/]"))
        {
            AnsiConsole.MarkupLine("Muy bien, la respuesta es correcta.");

            return false;
        }

        return true;
    }

    public static string SelectAnswer(string question, string[] options)
    {
        var answer = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]{question}[/]")
                .AddChoices(options));

        return answer;

    }
}
