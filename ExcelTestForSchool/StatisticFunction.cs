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
			public Student Student { get; set; }
			public Dictionary<Lesson, string> Lessons { get; set; }
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
			var timetable = BdConnection.connection.TimeLesson.ToList();
			var students = BdConnection.connection.Student.ToList();
			List<GroupStat> groupStats = new List<GroupStat>();

            foreach (var student in students)
            {
				var groupStat = new GroupStat() { Student = student, Lessons = new Dictionary<Lesson, string>() };
                foreach (var groupStatis in student.GroupStatistic)
                {
					var allLessons = 0;
					var visitedLessons = 0;
                    foreach (var groupTime in groupStatis.GroupTime)
                    {
						var lesson = groupTime.GroupStatistic.Lesson;

						if (groupTime.IsVisited)
							visitedLessons++;

						if (!groupStat.Lessons.ContainsKey(lesson))
						{
							allLessons = 0;
						}

						allLessons++;
						groupStat.Lessons[lesson] = $"{visitedLessons} из {allLessons}";

                    }
				}
				groupStats.Add(groupStat);
            }
			return groupStats;

		}

	}
}
