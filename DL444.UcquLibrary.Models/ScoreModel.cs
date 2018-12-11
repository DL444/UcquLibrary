using System;
using System.Collections;
using System.Collections.Generic;

namespace DL444.UcquLibrary.Models
{
    public class Score
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AdmissionYear { get; set; }
        public string Major { get; set; }
        public string ManagementClass { get; set; }
        public double GPA
        {
            get
            {
                double sumGP = 0;
                double sumCredit = 0;
                foreach (Term t in Terms)
                {
                    foreach (Course c in t.Courses)
                    {
                        double coursePA = c.GPA;
                        if (coursePA == 0.0) { continue; }
                        sumGP += c.Credit * coursePA;
                        sumCredit += c.Credit;
                    }
                }
                if (sumCredit == 0) { return 0; }
                return sumGP / sumCredit;
            }
        }
        public bool IsMajor { get; set; }

        public List<Term> Terms { get; } = new List<Term>();

        public Score(string id, string name, int admissionYear, string major, string manageClass, bool isMajor)
        {
            Id = id;
            Name = name;
            AdmissionYear = admissionYear;
            Major = major;
            ManagementClass = manageClass;
            IsMajor = isMajor;
        }
        public Score() : this("", "", DateTime.Today.Year, "", "", true) { }

        public double GetGPA(int beginningYear)
        {
            // This is different than Term.GPA in that it calculates whole year, not a term.
            double sumGP = 0;
            double sumCredit = 0;
            foreach (Term t in Terms)
            {
                if (t.BeginningYear != beginningYear) { continue; }
                foreach (Course c in t.Courses)
                {
                    double coursePA = c.GPA;
                    if (coursePA == 0.0) { continue; }
                    sumGP += c.Credit * coursePA;
                    sumCredit += c.Credit;
                }
            }
            if (sumCredit == 0) { return 0; }
            return sumGP / sumCredit;
        }

        public void AddTerm(Term term)
        {
            Terms.Add(term);
        }
        public bool RemoveTerm(Term term)
        {
            return Terms.Remove(term);
        }

        public void Reverse()
        {
            Terms.Sort();
            Terms.Reverse();
        }

        public override string ToString()
        {
            if(Id == null || Name == null) { return ""; }
            return $"{Id} {Name} " + (IsMajor ? "主修" : "辅修");
        }
    }

    public class Term : IComparable<Term>
    {
        public int BeginningYear { get; set; }
        public int EndingYear { get => BeginningYear + 1; }
        public int TermNumber { get; set; }
        public double GPA
        {
            get
            {
                double sumGP = 0;
                double sumCredit = 0;
                foreach (Course c in Courses)
                {
                    double coursePA = c.GPA;
                    if (coursePA == 0.0) { continue; }
                    sumGP += c.Credit * coursePA;
                    sumCredit += c.Credit;
                }
                if (sumCredit == 0) { return 0; }
                return sumGP / sumCredit;
            }
        }

        public List<Course> Courses { get; } = new List<Course>();

        public Term(int beginYear, int termNumber)
        {
            BeginningYear = beginYear;
            TermNumber = termNumber;
        }
        public Term() : this(DateTime.Today.Year, 0) { }

        public void AddCourse(Course subject)
        {
            Courses.Add(subject);
        }
        public bool RemoveCourse(Course subject)
        {
            return Courses.Remove(subject);
        }

        public override string ToString()
        {
            string str = $"{BeginningYear}-{EndingYear}学年第";
            switch (TermNumber)
            {
                case 1: str += "一"; break;
                case 2: str += "二"; break;
                case 3: str += "三"; break;
                default: str += "零"; break;
            }
            return str + "学期";
        }

        public int CompareTo(Term other)
        {
            if (other == null) { return 1; }
            if (this.BeginningYear == other.BeginningYear)
            {
                return this.TermNumber - other.TermNumber;
            }
            else
            {
                return this.BeginningYear - other.BeginningYear;
            }
        }
    }

    public class Course
    {
        public string Name { get; set; }
        public string SimplifiedName
        {
            get
            {
                if(string.IsNullOrWhiteSpace(Name)) { return ""; }
                String[] strs = Name.Split(']');
                if (strs.Length < 2) { return ""; }
                else { return strs[1]; }
            }
        }
        public double Credit { get; set; }
        public string Category { get; set; }
        public bool IsInitialTake { get; set; }
        public int Score { get; set; }

        public bool IsMajor { get; set; }
        public string Comment { get; set; }
        public string Lecturer { get; set; }
        public string SimplifiedLecturer
        {
            get
            {
                if(string.IsNullOrWhiteSpace(Lecturer)) { return ""; }
                String[] strs = Lecturer.Split(']');
                if (strs.Length < 2) { return ""; }
                else { return strs[1]; }
            }
        }
        public DateTime ObtainedTime { get; set; }
        public double GPA
        {
            get
            {
                if (Comment == "补考" || IsInitialTake == false || Comment == "缺考" || Comment == "补考(缺考)")
                {
                    if (Score >= 60) { return 1.0; }
                    else { return 0.0; }
                }
                if (Score > 90) { return 4.0; }
                if (Score < 60) { return 0.0; }
                return 1.0 + 0.1 * (Score - 60);
            }
        }

        public Course(string name, double credit, string category, bool isInitialTake, int score, bool isMajor, string comment, string lecturer, DateTime obtainedTime)
        {
            Name = name;
            Credit = credit;
            Category = category;
            IsInitialTake = isInitialTake;
            Score = score;
            IsMajor = isMajor;
            Comment = comment;
            Lecturer = lecturer;
            ObtainedTime = obtainedTime;
        }
        public Course() : this("", 0, "", true, 0, true, "", "", DateTime.Today) { }

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}
