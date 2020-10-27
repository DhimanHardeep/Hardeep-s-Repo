using System;
using System.Collections.Generic;

namespace DataObject
{
    public class Response
    {
        public int status { get; set; }

        public string message { get; set; }
    }

    public class StudentObject
    {
        public int studentID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string className { get; set; }

        public int totalRows { get; set; }
    }

    public class StudentSaveObject : StudentObject
    {
        public List<SubjectObject> subjectList { get; set; }
    }

    public class StudentObjectVM : Response
    {
        public StudentObject _studentObj { get; set; }

        public List<SubjectObject> _subjectList { get; set; }
    }

    public class StudentListVM : Response
    {
        public List<StudentObject> _studentList { get; set; }

        public List<SubjectObject> _subjectList { get; set; }
    }

    public class SubjectObject
    {
        public int studentID { get; set; }

        public int subjectID { get; set; }

        public string subjectName { get; set; }

        public string subjectMarks { get; set; }

        public bool isChecked { get; set; }
    }


    public class SubjectListVM : Response
    {
        public List<SubjectObject> _list { get; set; }
    }


    public class ParamObject
    {
        public string SearchValue { get; set; }
        public int FilterTypeID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
