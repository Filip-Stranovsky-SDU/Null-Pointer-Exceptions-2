using System.Collections.Generic;
using System.Collections.ObjectModel;
using homework3_livecharts.ViewModels;

public static class UndoRedoHandler
{
    
    private static Stack<KeyValuePair<bool, ChartViewModel>> undoStack = new();
    private static Stack<KeyValuePair<bool, ChartViewModel>> redoStack = new();


    public static void Undo(ObservableCollection<ChartViewModel> charts)
    {
        if (undoStack.Count==0) return;
        if (undoStack.Peek().Key) charts.Remove(undoStack.Peek().Value);
        if (!undoStack.Peek().Key) charts.Add(undoStack.Peek().Value);
        redoStack.Push(undoStack.Peek());
        undoStack.Pop();
    }

    public static void Redo(ObservableCollection<ChartViewModel> charts)
    {
        if (redoStack.Count==0) return;
        if (!redoStack.Peek().Key) charts.Remove(redoStack.Peek().Value);
        if (redoStack.Peek().Key) charts.Add(redoStack.Peek().Value);
        undoStack.Push(redoStack.Peek());
        redoStack.Pop();
    }

    public static void AddChart(ChartViewModel chart)
    {
        undoStack.Push(new(true, chart));
        redoStack.Clear();
    }

    public static void RemoveChart(ChartViewModel chart)
    {
        redoStack.Clear();
        undoStack.Push(new(false, chart));
    }
}