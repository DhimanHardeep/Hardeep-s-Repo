using DataObject;
using DataRepository;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataManager
{
    public class StudentManager
    {
        StudentRepository _repo = new StudentRepository();

        public SubjectListVM GetSubjects()
        {
            try
            {
                return _repo.GetSubjects();
            }
            catch (Exception ex)
            {
                return new SubjectListVM { status = -101, message = ex.Message, _list = null };
            }
        }

        public StudentListVM GetStudents(ParamObject _params)
        {
            try
            {
                return _repo.GetStudents(_params);
            }
            catch (Exception ex)
            {
                return new StudentListVM();
            }
        }

        public Response SaveStudent(StudentSaveObject _obj)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SubjectID", typeof(int));
                dt.Columns.Add("SubjectMarks", typeof(string));
                DataRow dr = dt.NewRow();


                foreach (SubjectObject item in _obj.subjectList)
                {
                    dr["SubjectID"] = item.subjectID;
                    dr["SubjectMarks"] = item.subjectMarks;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
                return _repo.SaveStudent(_obj, dt);
            }
            catch (Exception ex)
            {
                return new StudentObjectVM();
            }
        }
        public StudentObjectVM GetStudentDetail(int StudentID)
        {
            try
            {
                return _repo.GetStudentDetail(StudentID);
            }
            catch (Exception ex)
            {
                return new StudentObjectVM();
            }
        }


        public Response DeleteStudent(int StudentID)
        {
            try
            {
                return _repo.DeleteStudent(StudentID);
            }
            catch (Exception ex)
            {
                return new StudentObjectVM();
            }
        }

    }
}
