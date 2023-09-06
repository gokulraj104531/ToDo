import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpservicesService } from 'src/Services/httpservices.service';
@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css'],
})
export class AddTaskComponent {
  Todoform?: FormGroup;
  name: string | null;

  constructor(
    private builder: FormBuilder,
    private todoservice: HttpservicesService,
    private router: Router,
    private snackbar: MatSnackBar,
    private arouter: ActivatedRoute
  ) {
    this.name = sessionStorage.getItem('Username');
    this.checkId();
  }
  ngOnInit(): void {
    console.log('Vlaue:',this.arouter.snapshot.params['toDoListId']);
    this.initForm();

  }

  initForm() {
    const currentDate = new Date();
    this.Todoform = this.builder.group({
      ToDoListId: new FormControl(0),
      UserName: new FormControl(this.name, Validators.required),
      CreatedAt: new FormControl(currentDate, Validators.required),
      DueTime: new FormControl('', Validators.required),
      ToDoTitle: new FormControl('', Validators.required),
      ToDoListDescription: new FormControl('', Validators.required),
      isCompleted: new FormControl(false, Validators.required),
    });
  }

  checkId() {
    if (this.arouter.snapshot.params['toDoListId'] != null) {
      let paramsvalue = this.arouter.snapshot.params['toDoListId']!;
      this.Todoform?.get('toDoListId')?.setValue(paramsvalue);
      this.todoservice.GetToDoListById(paramsvalue).subscribe((response) => {
        let data = response;
        let dataedit = data[0];
        if (response != null) {
          this.Todoform?.setValue({
            ToDoListId: dataedit.toDoListId,
            UserName: dataedit.userName,
            CreatedAt: dataedit.createdAt,
            DueTime: dataedit.dueTime,
            ToDoTitle: dataedit.toDoTitle,
            ToDoListDescription: dataedit.toDoListDescription,
            isCompleted: dataedit.isCompleted,
          });
        }
      });
    } else {
      alert('EditId is null');
    }
  }

  OnSubmit() {
    if (this.Todoform?.value.ToDoListId != 0) {
      this.todoservice
        .EditToDoList(this.Todoform?.value)
        .subscribe((response) => {
          this.Todoform?.setValue({
            ToDoListId: '',
            UserName: '',
            CreatedAt: '',
            DueTime: '',
            ToDoTitle: '',
            ToDoListDescription: '',
            isCompleted: '',
          });
          this.router.navigateByUrl('/active');
          this.snackbar.open('Task Edited', '', {
            duration: 4000,
            horizontalPosition: 'end',
            verticalPosition: 'top',
          });
        });
    } else {
      this.todoservice
        .AddToDoList(this.Todoform?.value)
        .subscribe((response) => {
          this.Todoform?.setValue({
            ToDoListId: '',
            UserName: '',
            CreatedAt: '',
            DueTime: '',
            ToDoTitle: '',
            ToDoListDescription: '',
            isCompleted: '',
          });
          this.snackbar.open('Task Added', '', {
            duration: 5000,
            horizontalPosition: 'end',
            verticalPosition: 'top',
          });
          this.router.navigateByUrl('/active');
        });
    }
  }
  clear() {
    this.Todoform?.reset();
  }
}
