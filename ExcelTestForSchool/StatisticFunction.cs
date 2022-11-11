using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelTestForSchool.DataBase;


namespace ExcelTestForSchool
{
	public class StatisticFunction
	{
		public class TeacherWork 
		{
			public int IdTeach { get; set;}
			public int CountWork { get; set;}
		}
		public class GroupStat
        {
			public int IDStud { get; set; }
			public int IDLess { get; set; }
			public int Visited { get; set; }
		}
		public static  List<TeacherWork> TeacherWorkIsHard() 
		{
			var teachers = BdConnection.connection.Teacher.ToList();
			var lessons = BdConnection.connection.Lesson.ToList();
			List<TeacherWork> teacherWorks = new List<TeacherWork>();
			foreach(var i in teachers) 
			{
				TeacherWork teacher = new TeacherWork();
				teacher.IdTeach = i.ID;
				teacher.CountWork = 0;
				foreach (var j in lessons)
				{
					if(i.ID == j.IDTeacher) 
					{
						teacher.CountWork++;
					}
				}
				teacherWorks.Add(teacher);
			}
			return teacherWorks;	
		}
		public static List<GroupStat> VisitStat() 
		{
			var lessons = BdConnection.connection.Lesson.ToList();
			var groupStat = BdConnection.connection.GroupStatistic.ToList();
			var groupTime = BdConnection.connection.GroupTime.ToList();
			var timetable = BdConnection.connection.TimeLesson.ToList();
			var student = BdConnection.connection.Student.ToList();
			List<GroupStat> groupStats = new List<GroupStat>();

			foreach (var les in lessons)
			{
				int countLess = timetable.Where(x => x.IDLesson == les.ID).Count();
				GroupStat group = new GroupStat();
				group.IDLess = les.ID;
				group.Visited = 0;
				foreach (var gr in groupTime)
                {
					
					foreach (var stud in student)
                    {
						group.IDStud = stud.ID;
						if(les.ID == gr.GroupStatistic.IDLesson && stud.ID == gr.GroupStatistic.IDStudent && gr.IsVisited)
                        {
							group.Visited++;
                        }
					}
				}
				groupStats.Add(group);
			}
			return groupStats;

		}

	}
}
