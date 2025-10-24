
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using Grid = Microsoft.Maui.Controls.Grid;
using Grammars;
using System.IO;
using Microsoft.Maui.Storage;

namespace ExcelMAUIApp
{
	public partial class MainPage : ContentPage
	{
		const int CountColumn = 20;
		const int CountRow = 50;

		public MainPage()
		{
			InitializeComponent();
			CreateGrid();
		}
		Entry currEntry = new Entry();

		private void CreateGrid()
		{
			AddColumnsAndColumnLabels();
			AddRowsAndCellEntries();
		}

		private void AddColumnsAndColumnLabels()
		{
			for (int col = 0; col < CountColumn + 1; col++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				if (col > 0)
				{
					var label = new Label
					{
						Text = GetColumnName(col),
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center
					};
					Grid.SetRow(label, 0);
					Grid.SetColumn(label, col);
					grid.Children.Add(label);
				}
			}
		}

		private void AddRowsAndCellEntries()
		{
			for (int row = 0; row < CountRow; row++)
			{
				grid.RowDefinitions.Add(new RowDefinition());

				var label = new Label
				{
					Text = (row + 1).ToString(),
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center
				};
				Grid.SetRow(label, row + 1);
				Grid.SetColumn(label, 0);
				grid.Children.Add(label);

				for (int col = 0; col < CountColumn; col++)
				{
					var entry = new Entry
					{
						Text = "",
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center
					};
					entry.Focused += Entry_Focused;
					entry.Unfocused += Entry_Unfocused;
					Grid.SetRow(entry, row + 1);
					Grid.SetColumn(entry, col + 1);
					grid.Children.Add(entry);
				}
			}
		}

		private string GetColumnName(int colIndex)
		{
			int dividend = colIndex;
			string columnName = string.Empty;

			while (dividend > 0)
			{
				int modulo = (dividend - 1) % 26;
				columnName = Convert.ToChar(65 + modulo) + columnName;
				dividend = (dividend - modulo) / 26;
			}
			return columnName;
		}

		private string GetCellName(int row, int col){
			string columnName = GetColumnName(col);
			string rowNumber = row.ToString();
			return columnName + rowNumber;
		}

		private async void Entry_Focused(object? sender, FocusEventArgs e)
		{
			if (sender is not Entry entry){
				return;
			}
			currEntry = entry;
			var row = Grid.GetRow(entry);
			var col = Grid.GetColumn(entry);
			var cellName = GetCellName(row,col);

			if(Calculator.sheet.Cells.TryGetValue(cellName, out Grammars.Cell cell)){
				entry.Text = cell.Value;
				textInput.Text = cell.Expression;
			} else
            {
				textInput.Text = entry.Text;
            }
		}
		private void ProcessCellUpdate(Entry entry)
        {
            var row = Grid.GetRow(entry);
			var col = Grid.GetColumn(entry);
			var cellName = GetCellName(row,col);
			var expression = entry.Text?.Trim() ?? "";
			string originalExpression = entry.Text;

			try {
				if (!Calculator.sheet.Cells.ContainsKey(cellName)){
					Calculator.sheet.Cells[cellName] = new Grammars.Cell();
				}
				if (expression.StartsWith("=")){
					Calculator.sheet.Cells[cellName].Expression = expression;
					var formula = expression.Substring(1);
					Calculator.sheet.EditCell(cellName, formula);
					entry.Text = Calculator.sheet.Cells[cellName].Value;
					textInput.Text = entry.Text;
				}else{
					Calculator.sheet.Cells[cellName].Expression = expression;
					Calculator.sheet.Cells[cellName].Value = expression;
					Calculator.sheet.RefreshRecursively(cellName);
					UpdateGridFromSheet();
					textInput.Text = entry.Text;
				}
			} catch(Exception ex){
				string errorMessage = ex.Message;
				DisplayAlert("Помилка обчислення", $"Комірка {cellName}: {errorMessage}", "ОК");
				entry.Text = originalExpression;
				if (Calculator.sheet.Cells.ContainsKey(cellName))
                {
                    Calculator.sheet.Cells[cellName].Value = "#ERROR"; 
                    UpdateGridFromSheet(); 
                }
			}
        }
		private void Entry_Unfocused(object? sender, FocusEventArgs e)
		{
			if (sender is not Entry entry) return;
            
            var row = Grid.GetRow(entry);
            var col = Grid.GetColumn(entry);
            var cellName = GetCellName(row, col);
            var expression = entry.Text?.Trim() ?? "";

            if (!Calculator.sheet.Cells.ContainsKey(cellName))
            {
                Calculator.sheet.Cells[cellName] = new Grammars.Cell();
            }
            Calculator.sheet.Cells[cellName].Expression = expression;
            Calculator.sheet.Cells[cellName].Value = expression;
            textInput.Text = expression;
		}

		private void TextInput_Unfocused(object sender, FocusEventArgs e)
        {
            if (currEntry != null)
            {
				currEntry.Text = textInput.Text;
				ProcessCellUpdate(currEntry);
            }
        }

		private async void CalculateButton_Clicked(object sender, EventArgs e)
		{
			if (currEntry != null)
            {
                currEntry.Text = textInput.Text;
                ProcessCellUpdate(currEntry);
            }
            
			try
            {
                var allFormulaCells = Calculator.sheet.Cells
                    .Where(c => c.Value.Expression.StartsWith("=")) 
                    .Select(c => c.Key)
                    .ToList();
                    
                foreach (var cellName in allFormulaCells)
                {
                     if (Calculator.sheet.Cells.TryGetValue(cellName, out Grammars.Cell cell))
                     {
                         if (cell.Expression.StartsWith("="))
                         {
                             var formula = cell.Expression.Substring(1);
                             Calculator.sheet.EditCell(cellName, formula);
                         }
                     }
                }

                UpdateGridFromSheet();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка масового обчислення", $"Помилка: {ex.Message}", "ОК");
                UpdateGridFromSheet(); 
            }
		}


		private void UpdateGridFromSheet(bool fullUpdate = false)
		{
			foreach (var child in grid.Children)
			{
				if (child is Entry entry)
				{
					var row = Grid.GetRow(entry);
					var col = Grid.GetColumn(entry);

					if (row > 0 && col > 0)
					{
						var cellName = GetCellName(row, col);
						if (Calculator.sheet.Cells.TryGetValue(cellName, out Grammars.Cell cell))
						{

							entry.Text = cell.Value;

						}
						else if (fullUpdate)
							{
								entry.Text = "";
							}
					}
				}
				if (currEntry != null && Calculator.sheet.Cells.TryGetValue(GetCellName(Grid.GetRow(currEntry), Grid.GetColumn(currEntry)), out Grammars.Cell activeCell))
				{
					textInput.Text = activeCell.Expression;
				}
				else if (currEntry != null)
				{
					textInput.Text = currEntry.Text;
				}
			}

		}

private async void SaveButton_Clicked(object sender, EventArgs e)
{
    string fileName = FileNameEntry.Text?.Trim();
    if (string.IsNullOrEmpty(fileName))
    {
        await DisplayAlert("Помилка", "Будь ласка, введіть назву файлу у полі 'Введіть назву'.", "ОК");
        return;
    }

    string finalFileName = $"{fileName}.xlsx";
    
    string cacheDir = FileSystem.CacheDirectory;
    string tempFilePath = Path.Combine(cacheDir, finalFileName);

    try
    {
        var dataToSave = Calculator.sheet.Cells;
        var lines = new List<string>();
        
        foreach (var kvp in dataToSave)
        {
            if (!string.IsNullOrEmpty(kvp.Value.Expression))
            {
                lines.Add($"{kvp.Key}|{kvp.Value.Expression}");
            }
        }
        string content = string.Join(Environment.NewLine, lines);
        await File.WriteAllTextAsync(tempFilePath, content);

        var fileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".xlsx" } },
                { DevicePlatform.macOS, new[] { "xlsx" } },
                { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } },
                { DevicePlatform.iOS, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } },
            });
        
        var saveOptions = new SaveFileOptions
        {
            SuggestedFileName = finalFileName, 
            PickerTitle = "Зберегти таблицю",
            FileTypes = fileType
        };
        
        var result = await FileSaver.Default.SaveAsync(tempFilePath, saveOptions);

        if (string.IsNullOrEmpty(result))
        {
             await DisplayAlert("Збереження", "Збереження скасовано.", "ОК");
        }
        else
        {
            await DisplayAlert("Збереження", $"Файл '{finalFileName}' успішно збережено за вибраним шляхом.", "ОК");
        }
        
        File.Delete(tempFilePath);
    }
    catch (Exception ex)
    {
        await DisplayAlert("Помилка збереження", $"Не вдалося зберегти файл: {ex.Message}", "ОК");
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }
}

		private async void ReadButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				var customFileType = new FilePickerFileType(
					new Dictionary<DevicePlatform, IEnumerable<string>>
					{
				{ DevicePlatform.WinUI, new[] { ".xlsx", ".txt" } },
				{ DevicePlatform.macOS, new[] { "xlsx", "txt" } },
				{ DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/plain" } },
				{ DevicePlatform.iOS, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "public.plain-text" } },
					});

				var options = new PickOptions
				{
					PickerTitle = "Вибрати файл таблиці (.xlsx або .txt)",
					FileTypes = customFileType,
				};

				var result = await FilePicker.Default.PickAsync(options);

				if (result == null)
				{
					return; 
				}

				string fullPath = result.FullPath;
				string content = await File.ReadAllTextAsync(fullPath);
				var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				Calculator.sheet.Cells.Clear();

				foreach (var line in lines)
				{
					var parts = line.Split(new[] { '|' }, 2);
					if (parts.Length == 2)
					{
						string cellName = parts[0].Trim();
						string expression = parts[1].Trim();

						Calculator.sheet.Cells[cellName] = new Grammars.Cell(expression)
						{
							Value = expression,
							Expression = expression
						};
					}
				}
				var allFormulaCells = Calculator.sheet.Cells
					.Where(c => c.Value.Expression.StartsWith("="))
					.Select(c => c.Key)
					.ToList();

				foreach (var cellName in allFormulaCells)
				{
					if (Calculator.sheet.Cells.TryGetValue(cellName, out Grammars.Cell cell))
					{
						if (cell.Expression.StartsWith("="))
						{
							var formula = cell.Expression.Substring(1);
							Calculator.sheet.EditCell(cellName, formula);
						}
					}
				}
				UpdateGridFromSheet(true);
				FileNameEntry.Text = Path.GetFileNameWithoutExtension(result.FileName);

				await DisplayAlert("Завантаження", $"Файл '{result.FileName}' успішно завантажено. Виконано перерахунок.", "ОК");
			}
			catch (Exception ex)
			{
				await DisplayAlert("Помилка завантаження", $"Не вдалося прочитати файл: {ex.Message}", "ОК");
			}
		}


		private async void ExitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");
			if (answer)
			{
				System.Environment.Exit(0);
			}
		}

		private async void HelpButton_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Довідка", "Лабораторна робота 1. Студентки групи К-25 Погорілої Анни", "ОК");
		}
		private void DeleteRowButton_Clicked(object sender, EventArgs e)
		{
			if (grid.RowDefinitions.Count > 1)
			{
				int lastRowIndex = grid.RowDefinitions.Count - 1;
				grid.RowDefinitions.RemoveAt(lastRowIndex);
				grid.Children.RemoveAt(lastRowIndex * (CountColumn + 1));
				for (int col = 0; col < CountColumn; col++)
				{
					grid.Children.RemoveAt((lastRowIndex * CountColumn) + col + 1);
				}
			}
		}

		private void DeleteColumnButton_Clicked(object sender, EventArgs e)
		{
			if (grid.ColumnDefinitions.Count > 1)
			{
				int lastColumnIndex = grid.ColumnDefinitions.Count - 1;
				grid.ColumnDefinitions.RemoveAt(lastColumnIndex);
				grid.Children.RemoveAt(lastColumnIndex);
				for (int row = 0; row < CountRow; row++)
				{
					grid.Children.RemoveAt(row * (CountColumn + 1) + lastColumnIndex + 1);
				}
			}
		}

		private void AddRowButton_Clicked(object sender, EventArgs e)
		{
			int newRow = grid.RowDefinitions.Count;

			grid.RowDefinitions.Add(new RowDefinition());

			var label = new Label
			{
				Text = newRow.ToString(),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};
			Grid.SetRow(label, newRow);
			Grid.SetColumn(label, 0);
			grid.Children.Add(label);

			for (int col = 0; col < CountColumn; col++)
			{
				var entry = new Entry
				{
					Text = "",
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center
				};

				entry.Unfocused += Entry_Unfocused;

				Grid.SetRow(entry, newRow);
				Grid.SetColumn(entry, col + 1);
				grid.Children.Add(entry);
			}
		}
		private void AddColumnButton_Clicked(object sender, EventArgs e)
		{
			int newColumn = grid.ColumnDefinitions.Count;
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			var label = new Label
			{
				Text = GetColumnName(newColumn),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};
			Grid.SetRow(label, 0);
			Grid.SetColumn(label, newColumn);
			grid.Children.Add(label);
			for (int row = 0; row < CountRow; row++)
			{
				var entry = new Entry
				{
					Text = "",
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center
				};

				entry.Unfocused += Entry_Unfocused;
				Grid.SetRow(entry, row + 1);

				Grid.SetColumn(entry, newColumn);
				grid.Children.Add(entry);
			}
		}

	}
}

