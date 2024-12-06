namespace TextBasedAdventureGame.Classes;

using Spectre.Console;

internal static class QuestionHandler
{
    public static bool AskQuestionWithNoConfirmation(string question)
    {
        if (!AnsiConsole.Confirm($"[green]{question}[/]"))
        {
            AnsiConsole.MarkupLine("Muy bien, la respuesta es correcta.");

            return true;
        }

        return false;
    }

    public static bool AskQuestionWithYesConfirmation(string question)
    {
        if (AnsiConsole.Confirm($"[green]{question}[/]"))
        {
            AnsiConsole.MarkupLine("Muy bien, la respuesta es correcta.");

            return false;
        }

        return true;
    }

    public static string PromptQuestionWithSimpleSelect(string question, string[] options)
    {
        var answer = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]{question}[/]")
                .AddChoices(options));

        return answer;
    }
}
