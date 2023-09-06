import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActiveList } from 'src/Models/activeList';
import { HttpservicesService } from 'src/Services/httpservices.service';

@Component({
  selector: 'app-completed-task',
  templateUrl: './completed-task.component.html',
  styleUrls: ['./completed-task.component.css'],
})
export class CompletedTaskComponent {
  name: any;
  data: ActiveList[] = [];
  constructor(
    private service: HttpservicesService,
    private snackbar: MatSnackBar
  ) {
    this.name = sessionStorage.getItem('Username');
  }
  ngOnInit(): void {
    this.getCompletedList();
  }

  getCompletedList() {
    this.service.CompletedList(this.name).subscribe((data) => {
      this.data = data;
    
    });
  }

  deleteTask(toDoListId: number) {
    this.service.DeleteTask(toDoListId).subscribe((response) => {
      this.getCompletedList();
      this.snackbar.open('Task Deleted', '', {
        duration: 3000,
        horizontalPosition: 'end',
        verticalPosition: 'top',
      });
    });
  }
}
