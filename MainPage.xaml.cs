
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using Grid = Microsoft.Maui.Controls.Grid;

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

		string currentCell = "";
		string currentCellExpr = "";
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

		private async void Entry_Focused()
		{

		}

		private void Entry_Unfocused(object? sender, FocusEventArgs e)
		{
			if (sender is not Entry entry)
			{
				return;
			}
			var row = Grid.GetRow(entry) - 1;
			var col = Grid.GetColumn(entry) - 1;
			var content = entry.Text;
		}

		private async void CalculateButton_Clicked(object sender, EventArgs e)
		{
			
		}
		private void SaveButton_Clicked(object sender, EventArgs e)
		{

		}
		private void ReadButton_Clicked(object sender, EventArgs e)
		{

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

			// Add a new column definition
			grid.ColumnDefinitions.Add(new ColumnDefinition());

			// Add label for the column name
			var label = new Label
			{
				Text = GetColumnName(newColumn),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};
			Grid.SetRow(label, 0);
			Grid.SetColumn(label, newColumn);
			grid.Children.Add(label);

			// Add entry cells for the new column
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

