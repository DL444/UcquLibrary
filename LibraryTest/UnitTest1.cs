using Microsoft.VisualStudio.TestTools.UnitTesting;
using DL444.UcquLibrary.Models;

namespace LibraryTest
{
    [TestClass]
    public class Test_Course
    {
        [TestMethod]
        public void Test_CourseGPA()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83, 
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual(3.3, course.GPA, 0.01);
        }
        [TestMethod]
        public void Test_CourseGPA_Full()
        {
            Course course = new Course("Course", 2, "Test Category", true, 95,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual(4.0, course.GPA, 0.01);
        }
        [TestMethod]
        public void Test_CourseGPA_Fail()
        {
            Course course = new Course("Course", 2, "Test Category", true, 59,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual(0, course.GPA, 0.01);
        }
        [TestMethod]
        public void Test_CourseGPA_Retake()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83,
                true, "补考", "Lecturer", System.DateTime.Today);
            Assert.AreEqual(1.0, course.GPA, 0.01);
        }

        [TestMethod]
        public void Test_SimplifiedName()
        {
            Course course = new Course("[SE0001]程序设计基础", 2, "Test Category", true, 83,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual("程序设计基础", course.SimplifiedName);
        }
        [TestMethod]
        public void Test_SimplifiedName_Null()
        {
            Course course = new Course(null, 2, "Test Category", true, 83,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedName);
        }
        [TestMethod]
        public void Test_SimplifiedName_Empty()
        {
            Course course = new Course("   ", 2, "Test Category", true, 83,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedName);
        }
        [TestMethod]
        public void Test_SimplifiedName_Nonstandard()
        {
            Course course = new Course("Something Nasty", 2, "Test Category", true, 83,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedName);
        }

        [TestMethod]
        public void Test_SimplifiedLecturer()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83,
                true, "", "[44444]David Lee", System.DateTime.Today);
            Assert.AreEqual("David Lee", course.SimplifiedLecturer);
        }
        [TestMethod]
        public void Test_SimplifiedLecturer_Null()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83,
                true, "", null, System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedLecturer);
        }
        [TestMethod]
        public void Test_SimplifiedLecturer_Empty()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83,
                true, "", "  ", System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedLecturer);
        }
        [TestMethod]
        public void Test_SimplifiedLecturer_Nonstandard()
        {
            Course course = new Course("Course", 2, "Test Category", true, 83,
                true, "", "Lecturer", System.DateTime.Today);
            Assert.AreEqual("", course.SimplifiedLecturer);
        }

        [TestMethod]
        public void Test_DefaultConstructor()
        {
            Course course = new Course();
            Assert.AreEqual("", course.SimplifiedName);
            Assert.AreEqual("", course.SimplifiedLecturer);
            Assert.AreEqual(0, course.GPA);
            Assert.AreEqual("", course.ToString());
        }
    }

    [TestClass]
    public class Test_Term
    {
        [TestMethod]
        public void Test_TermGPA()
        {
            Course[] courses = new Course[]
            {
                new Course("Course", 1, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 2, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 4, "Test Category", true, 67,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 72,
                    true, "补考", "Lecturer", System.DateTime.Today)

            };

            Term term = new Term(2018, 1);
            foreach(Course c in courses)
            {
                term.AddCourse(c);
            }

            Assert.AreEqual(2.11, term.GPA, 0.01);
        }
        [TestMethod]
        public void Test_TermGPA_Empty()
        {
            Term term = new Term();
            Assert.AreEqual(0, term.GPA, 0.01);
        }

        [TestMethod]
        public void Test_TermCompareTo()
        {
            Assert.AreEqual(1, new Term(2018, 1).CompareTo(new Term(2017, 2)));
            Assert.AreEqual(-1, new Term(2017, 1).CompareTo(new Term(2017, 2)));
            Assert.AreEqual(0, new Term(2018, 1).CompareTo(new Term(2018, 1)));
        }
        [TestMethod]
        public void Test_TermCompareTo_Null()
        {
            Assert.AreEqual(1, new Term().CompareTo(null));
        }

        [TestMethod]
        public void Test_TermToString()
        {
            Term term = new Term(2018, 2);
            Assert.AreEqual("2018-2019学年第二学期", term.ToString());
            term = new Term(2017, 1);
            Assert.AreEqual("2017-2018学年第一学期", term.ToString());
            term = new Term(2019, 3);
            Assert.AreEqual("2019-2020学年第三学期", term.ToString());
            term = new Term(2019, 10);
            Assert.AreEqual("2019-2020学年第零学期", term.ToString());
        }
    }

    [TestClass]
    public class Test_Score
    {
        [TestMethod]
        public void Test_ScoreGPA()
        {
            Course[] courses1 = new Course[]
            {
                new Course("Course", 1, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 2, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 4, "Test Category", true, 67,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 72,
                    true, "补考", "Lecturer", System.DateTime.Today)
            };
            // 2.11

            Term term1 = new Term(2018, 1);
            foreach (Course c in courses1)
            {
                term1.AddCourse(c);
            }

            Course[] courses2 = new Course[]
            {
                new Course("Course", 2, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 1, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
            };
            // 3.72

            Term term2 = new Term(2017, 3);
            foreach (Course c in courses2)
            {
                term2.AddCourse(c);
            }

            Score score = new Score("20444444", "DL444", 2044, "SE", "SE01", true);
            score.AddTerm(term1);
            score.AddTerm(term2);

            Assert.AreEqual(2.65, score.GPA, 0.01);
        }
        [TestMethod]
        public void Test_ScoreGPA_Empty()
        {
            Score score = new Score();
            Assert.AreEqual(0, score.GPA, 0.01);
        }
        [TestMethod]
        public void Test_Score_GPA_Year()
        {
            Course[] courses1 = new Course[]
            {
                new Course("Course", 1, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 2, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 4, "Test Category", true, 67,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 72,
                    true, "补考", "Lecturer", System.DateTime.Today)
            };
            // 2.11

            Term term1 = new Term(2018, 1);
            foreach (Course c in courses1)
            {
                term1.AddCourse(c);
            }

            Course[] courses2 = new Course[]
            {
                new Course("Course", 2, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 1, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
            };
            // 3.72

            Term term2 = new Term(2018, 3);
            foreach (Course c in courses2)
            {
                term2.AddCourse(c);
            }

            Score score = new Score("20444444", "DL444", 2044, "SE", "SE01", true);
            score.AddTerm(term1);
            score.AddTerm(term2);

            Assert.AreEqual(2.65, score.GetGPA(2018), 0.01);
            term2.BeginningYear = 2017;
            Assert.AreEqual(2.11, score.GetGPA(2018), 0.01);
        }

        [TestMethod]
        public void Test_ScoreReverse()
        {
            Course[] courses1 = new Course[]
            {
                new Course("Course", 1, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 2, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 4, "Test Category", true, 67,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 72,
                    true, "补考", "Lecturer", System.DateTime.Today)
            };
            // 2.11

            Term term1 = new Term(2018, 1);
            foreach (Course c in courses1)
            {
                term1.AddCourse(c);
            }

            Course[] courses2 = new Course[]
            {
                new Course("Course", 2, "Test Category", true, 83,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 3, "Test Category", true, 96,
                    true, "", "Lecturer", System.DateTime.Today),
                new Course("Course", 1, "Test Category", true, 42,
                    true, "", "Lecturer", System.DateTime.Today),
            };
            // 3.72

            Term term2 = new Term(2017, 3);
            foreach (Course c in courses2)
            {
                term2.AddCourse(c);
            }

            Score score = new Score("20444444", "DL444", 2044, "SE", "SE01", true);
            score.AddTerm(term1);
            score.AddTerm(term2);

            score.Reverse();
            Assert.AreSame(term1, score.Terms[0]);
            Assert.AreSame(term2, score.Terms[1]);
        }
    }
}
