using Dapper;
using DataObject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataRepository
{
    public class StudentRepository
    {
        public static string connectionStr = Connection.GetSqlConnection();

        public SubjectListVM GetSubjects()
        {
            SubjectListVM report = new SubjectListVM();
            try
            {
                using (IDbConnection db = new SqlConnection(connectionStr))
                {
                    var result = db.Query<SubjectObject>("GetSubjects", commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        report.status = 1;
                        report.message = "Subject list returned successfully.";
                        report._list = result.ToList();
                    }
                }
            }
            catch (SqlException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (DataException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (Exception ex)
            {
                report.status = -100;
                report.message = ex.Message.ToString();
            }
            return report;
        }

        public StudentListVM GetStudents(ParamObject _params)
        {
            StudentListVM report = new StudentListVM();
            try
            {
                var param = new
                {
                    @SearchValue = _params.SearchValue,
                    @FilterTypeID = _params.FilterTypeID,
                    @PageNumber = _params.PageNumber,
                    @PageSize = _params.PageSize
                };
                using (IDbConnection db = new SqlConnection(connectionStr))
                {
                    using (var result = db.QueryMultiple("GetStudents", param: param, commandType: CommandType.StoredProcedure))
                    {
                        report.status = 1;
                        report.message = "Student list return Successfully.";
                        report._studentList = result.Read<StudentObject>().ToList();
                        report._subjectList = result.Read<SubjectObject>().ToList();
                    }
                }
            }
            catch (SqlException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (DataException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (Exception ex)
            {
                report.status = -100;
                report.message = ex.Message.ToString();
            }
            return report;
        }

        public StudentObjectVM GetStudentDetail(int StudentID)
        {
            StudentObjectVM report = new StudentObjectVM();
            try
            {
                var param = new
                {
                    StudentID = StudentID
                };
                using (IDbConnection db = new SqlConnection(connectionStr))
                {
                    using (var result = db.QueryMultiple("GetStudentDetail", param: param, commandType: CommandType.StoredProcedure))
                    {
                        report.status = 1;
                        report.message = "Student return Successfully.";
                        report._studentObj = result.Read<StudentObject>().FirstOrDefault();
                        report._subjectList = result.Read<SubjectObject>().ToList();
                    }
                }
            }
            catch (SqlException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (DataException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (Exception ex)
            {
                report.status = -100;
                report.message = ex.Message.ToString();
            }
            return report;
        }

        public Response SaveStudent(StudentSaveObject _obj, DataTable dt)
        {
            Response report = new Response();
            try
            {
                var param = new
                {
                    StudentID = _obj.studentID,
                    FirstName = _obj.firstName,
                    LastName = _obj.lastName,
                    ClassName = _obj.className,
                    datatable = dt
                };

                using (IDbConnection db = new SqlConnection(connectionStr))
                {
                    var result = db.Query<int>("SaveStudent", param: param, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        report.status = result.FirstOrDefault();
                        report.message = "Student saved successfully.";
                    }
                }


            }
            catch (SqlException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (DataException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (Exception ex)
            {
                report.status = -100;
                report.message = ex.Message.ToString();
            }
            return report;
        }


        public Response DeleteStudent(int StudentID)
        {
            Response report = new Response();
            try
            {
                var param = new
                {
                    StudentID = StudentID
                };

                using (IDbConnection db = new SqlConnection(connectionStr))
                {
                    var result = db.Query<int>("DeleteStudent", param: param, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        report.status = result.FirstOrDefault();
                        report.message = "Student deleted successfully.";
                    }
                }


            }
            catch (SqlException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (DataException ex)
            {
                report.status = -100;
                report.message = ex.Message;
            }
            catch (Exception ex)
            {
                report.status = -100;
                report.message = ex.Message.ToString();
            }
            return report;
        }
    }
}
