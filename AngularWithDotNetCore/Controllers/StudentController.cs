using DataManager;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularWithDotNetCore.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentManager _manager = new StudentManager();

        [HttpGet]
        [Route("GetSubjects")]
        public SubjectListVM GetSubjects()
        {
            try
            {
                return _manager.GetSubjects();
            }
            catch (Exception ex)
            {
                return new SubjectListVM { status = -101, message = ex.Message, _list = null };
            }
        }

        [HttpPost]
        [Route("GetStudents")]
        public StudentListVM GetStudents([FromBody]ParamObject _params)
        {
            try
            {
                return _manager.GetStudents(_params);
            }
            catch (Exception ex)
            {
                return new StudentListVM { status = -101, message = ex.Message, _studentList = null, _subjectList = null };
            }
        }

        [HttpPost]
        [Route("SaveStudent")]
        public Response SaveStudent([FromBody]StudentSaveObject _obj)
        {
            try
            {
                return _manager.SaveStudent(_obj);
            }
            catch (Exception ex)
            {
                return new Response { status = -101, message = ex.Message };
            }
        }

        [HttpPut]
        [Route("GetStudentDetail/{StudentID}")]
        public StudentObjectVM GetStudentDetail(int StudentID)
        {
            try
            {
                return _manager.GetStudentDetail(StudentID);
            }
            catch (Exception ex)
            {
                return new StudentObjectVM { status = -101, message = ex.Message, _studentObj = null, _subjectList = null };
            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{StudentID}")]
        public Response DeleteStudent(int StudentID)
        {
            try
            {
                return _manager.DeleteStudent(StudentID);
            }
            catch (Exception ex)
            {
                return new StudentObjectVM { status = -101, message = ex.Message, _studentObj = null, _subjectList = null };
            }
        }

    }
}