using System;
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
			var popular = StatisticFunction.MostPopularLesson();

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
			rowIndex = 2;
			Excel.Worksheet worksheet1 = application.Worksheets.Item[2];
			foreach (var l in les)
			{
				Student stud = l.Student;
				worksheet1.Name = $"Посещаемость";
				worksheet1.Columns.AutoFit();
				worksheet1.Rows.AutoFit();
				worksheet1.Cells[1][1] = "Фамилия";
				worksheet1.Cells[2][1] = "Имя";
				worksheet1.Cells[3][1] = "Отчечтво";
				worksheet1.Cells[4][1] = "Название предмета";
				worksheet1.Cells[5][1] = "Количество посещенных уроков";

				foreach (var lesson in l.Lessons.Keys)
				{
					worksheet1.Cells[1][rowIndex] = stud.LastName;
					worksheet1.Cells[2][rowIndex] = stud.Name;
					worksheet1.Cells[3][rowIndex] = stud.Patronic;
					worksheet1.Cells[4][rowIndex] = lesson.Name;
					worksheet1.Cells[5][rowIndex] = l.Lessons[lesson];
					rowIndex++;

				}
				rowIndex = 2;
				Excel.Worksheet worksheet2 = application.Worksheets.Item[3];
				foreach (var p in popular)
				{
					worksheet2.Name = $"Популярность кружков";
					worksheet2.Columns.AutoFit();
					worksheet2.Rows.AutoFit();
					worksheet2.Cells[1][1] = "Название";
					worksheet2.Cells[2][1] = "Количество учеников";
					//worksheet2.Cells[3][1] = "Отчечтво";
					//worksheet2.Cells[4][1] = "Название предмета";
					//worksheet2.Cells[5][1] = "Количество посещенных уроков";

					worksheet2.Cells[1][rowIndex] = p.lesson1.Name;
					worksheet2.Cells[2][rowIndex] = p.count + " из " + p.lesson1.CountChildren;
					//worksheet2.Cells[3][rowIndex] = stud.Patronic;
					//worksheet2.Cells[4][rowIndex] = lesson.Name;
					//worksheet2.Cells[5][rowIndex] = l.Lessons[lesson];
					rowIndex++;

				}
				application.Visible = true;
			}
		}
	}
}
