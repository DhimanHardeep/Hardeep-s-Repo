
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, FormGroup, FormControl, Validators, FormBuilder, FormArray, MaxLengthValidator, EmailValidator } from '@angular/forms';
import { PagerService } from '../services/pager-service.service';


@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  //---- variables declarations---
  public _paramObj: ParamObject;
  public StudentForm: FormGroup;
  public StudentList: any = [];
  public StudentSubjectList: any = [];
  public SubjectList: any = [];
  public FilterList: any = [];
  public isListView: boolean = true;
  public Submitted: boolean = false;
  public SubjectFieldError: boolean = false;
  public SubjectMarksFieldError: boolean = false;
  public SearchValue: string = "";
  public FilterTypeID: number = 0;
  public PageSize: number = 5;
  public TotalRecords: number = 0;
  pager: any = {};

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private fb: FormBuilder, private pagerService: PagerService) { }

  ngOnInit() {
  //---- variables initialization---

    this.StudentForm = this.fb.group({
      StudentID: [],
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      ClassName: ['', Validators.required],
      SubjectList: this.fb.array([
      ])
    });
    this._paramObj = new ParamObject();

    this.FilterList.push(
      { FilterTypeID: 1, FilterName: "Filter By Name" },
      { FilterTypeID: 2, FilterName: "Filter By Class" },
      { FilterTypeID: 3, FilterName: "Filter By Subject" }
    )
    this.SearchValue = "";
    this.FilterTypeID = 1;
    this.GetSubjectList();
    this.GetStudents(1);

  }

  
  GetSubjectList() {
    this.http.get(this.baseUrl + 'api/Student/GetSubjects').subscribe((result: any) => {
      if (result.status > 0) {
        this.SubjectList = result._list;
      } else {
        this.SubjectList = [];
      }

    }, error => console.error(error));
  }

  GetStudents(pageNumber) {
    this._paramObj.FilterTypeID = this.FilterTypeID;
    this._paramObj.SearchValue = this.SearchValue;
    this._paramObj.PageNumber = pageNumber;
    //this._paramObj.PageSize = this.PageSize;
    this.isListView = true;
    this.StudentList = [];
    this.StudentSubjectList = [];
    this.TotalRecords = 0;
    this.http.post(this.baseUrl + 'api/Student/GetStudents', this._paramObj).subscribe((result: any) => {
      if (result != null && result.status > 0) {
        this.StudentList = result._studentList;
        this.StudentSubjectList = result._subjectList;
        this.TotalRecords = this.StudentList[0].totalRows;
        this.pager = this.pagerService.getPager(this.StudentList ? this.StudentList[0].totalRows : 0, this._paramObj.PageNumber, this._paramObj.PageSize);
      }
    }, error => console.error(error));
  }

  submitFormEvent() {
    this.CheckSubjectSelection().then(() => {
      this.SaveUpdateStudent();
    });
  }

  AddNewEvent() {
    this.GetSubjectList();
    this.isListView = false;
    this.StudentForm.reset();
    this.Submitted = false;
  }

  CheckSubjectSelection() {
    return new Promise((resolve) => {
      this.SubjectFieldError = false;
      this.SubjectMarksFieldError = false;
      var _filterList = this.SubjectList.filter(x => x.isChecked == true);
      if (!(_filterList != null && _filterList.length > 0)) {
        this.SubjectFieldError = true;
      } else {
        var _filterList2 = this.SubjectList.filter(x => x.isChecked == true && x.subjectMarks != null && x.subjectMarks != "");
        if ((_filterList2 != null && _filterList2.length != _filterList.length) || _filterList2 == null) {
          this.SubjectMarksFieldError = true;
        }
      }
      resolve(true);
    });
  }

  SaveUpdateStudent() {
    this.Submitted = true;
    if (this.StudentForm.valid && !this.SubjectFieldError && !this.SubjectMarksFieldError) {
      var _obj = {
        StudentID: this.StudentForm.value.StudentID == null ? 0 : this.StudentForm.value.StudentID, FirstName: this.StudentForm.value.FirstName, LastName: this.StudentForm.value.LastName
        , ClassName: this.StudentForm.value.ClassName, SubjectList: this.SubjectList.filter(x => x.isChecked == true)
      };
      this.http.post(this.baseUrl + 'api/Student/SaveStudent', _obj).subscribe((result: any) => {
        if (result != null && result.status > 0) {
          alert(result.message);
          this.GetStudents(1);
        }
      }, error => console.error(error));
    }
  }

  GetStudentDetail(studentID) {
    this.http.put(this.baseUrl + 'api/Student/GetStudentDetail' + "/" + studentID, {}).subscribe((result: any) => {
      if (result != null && result.status > 0) {
        this.isListView = false;
        var _obj = result._studentObj;
        this.StudentForm.controls['StudentID'].setValue(_obj.studentID);
        this.StudentForm.controls['FirstName'].setValue(_obj.firstName);
        this.StudentForm.controls['LastName'].setValue(_obj.lastName);
        this.StudentForm.controls['ClassName'].setValue(_obj.className);
        this.SubjectList = result._subjectList;
      }
    }, error => console.error(error));
  }

  DeleteStudent(studentID) {
    this.http.delete(this.baseUrl + 'api/Student/DeleteStudent' + "/" + studentID).subscribe((result: any) => {
      if (result != null && result.status > 0) {
        alert(result.message);
        this.GetStudents(1);
      }
    }, error => console.error(error));
  }

  reset() {
    this.StudentForm.reset();
    this.Submitted = false;
    this.isListView = true;
  }

  checkboxEvent(_obj) {
    if (!_obj.isChecked) {
      _obj.subjectMarks = "";
    }
  }

}

class ParamObject {
  SearchValue: string="";
  FilterTypeID: number=1;
  PageNumber: number=1;
  PageSize: number=5;
}
