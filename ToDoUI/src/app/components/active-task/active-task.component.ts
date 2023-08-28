import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToDoList } from 'src/Models/ToDoList';
import { ActiveList } from 'src/Models/activeList';
import { HttpservicesService } from 'src/Services/httpservices.service';

@Component({
  selector: 'app-active-task',
  templateUrl: './active-task.component.html',
  styleUrls: ['./active-task.component.css']
})
export class ActiveTaskComponent implements OnInit {
  name:any;
  data:ActiveList[]=[];
constructor(private service:HttpservicesService,private snackbar:MatSnackBar){
  this.name=sessionStorage.getItem('Username'); 
}
  ngOnInit(): void {
     this.getActiveList();
    }

    getActiveList(){
      this.service.ActiveList(this.name).subscribe((data)=>{
        this.data=data;
        console.warn(data)
      });
    }

    toComplete(toDoListId:number){
      this.service.ToComplete(toDoListId).subscribe((response)=>{
        this.getActiveList();
        this.snackbar.open('Task Completed','',{
          duration:3000,
          horizontalPosition:'end',
          verticalPosition:'top',
        })
      })
    }

    
  deleteTask(toDoListId:number){
    this.service.DeleteTask(toDoListId).subscribe((response)=>{
      this.getActiveList();
      this.snackbar.open('Task Deleted', '', {
        duration: 3000,
        horizontalPosition: 'end',
        verticalPosition: 'top',
      });
    })
  }
}


