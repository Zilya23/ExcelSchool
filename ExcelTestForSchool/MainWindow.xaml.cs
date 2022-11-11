﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelTestForSchool.DataBase;

namespace ExcelTestForSchool
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
		}

		private void btn_Click(object sender, RoutedEventArgs e)
		{
			var teach = StatisticFunction.TeacherWorkIsHard();
			var teachBd = BdConnection.connection.Teacher.ToList();
			var les = StatisticFunction.VisitStat();
			var students = BdConnection.connection.Student.ToList();

			int rowIndex = 2;
			var application = new Excel.Application();
			application.SheetsInNewWorkbook = 4;

			Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
			Excel.Worksheet worksheet = application.Worksheets.Item[1];
			

			foreach (var i in teach)
			{
				Teacher teacher = teachBd.Where(x => x.ID == i.IdTeach).FirstOrDefault();							
				worksheet.Name = $"Учитель";
				worksheet.Columns.AutoFit();
				worksheet.Rows.AutoFit();
				worksheet.Cells[1][1] = "Фамилия";
				worksheet.Cells[2][1] = "Имя";
				worksheet.Cells[3][1] = "Отчечтво";
				worksheet.Cells[4][1] = "Количество уроков";

				worksheet.Cells[1][rowIndex] = teacher.LastName;
				worksheet.Cells[2][rowIndex] = teacher.Name;
				worksheet.Cells[3][rowIndex] = teacher.Patronic;
				worksheet.Cells[4][rowIndex] = i.CountWork;

				rowIndex++;
			}
			
			Excel.Worksheet worksheet1 = application.Worksheets.Item[2];
			foreach (var l in les)
            {
				Student stud = students.Where(x => x.ID == l.IDStud).FirstOrDefault();
				worksheet1.Name = $"Посещаемость";
				worksheet1.Columns.AutoFit();
				worksheet1.Rows.AutoFit();
				worksheet1.Cells[1][1] = "Фамилия";
				worksheet1.Cells[2][1] = "Имя";
				worksheet1.Cells[3][1] = "Отчечтво";
				worksheet1.Cells[4][1] = "Количество посещенных уроков";

				worksheet1.Cells[1][rowIndex] = stud.LastName;
				worksheet1.Cells[2][rowIndex] = stud.Name;
				worksheet1.Cells[3][rowIndex] = stud.Patronic;
				worksheet1.Cells[4][rowIndex] = l.Visited;

				rowIndex++;
			}
			application.Visible = true;
		}
	}
}
