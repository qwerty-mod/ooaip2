namespace SpaceBattle.Lib.Lab2;

public class EmptyCommand : ICommand
{
    public void Execute() { /* Ничего не делает */ }
}

// для безопасной остановки длительных процессов за константное время O(1) путем подмены текущей исполняемой команды на пустышку