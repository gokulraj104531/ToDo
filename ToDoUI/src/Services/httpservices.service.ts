import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/Models/User';
import { ToDoList } from 'src/Models/ToDoList';
import { ActiveList } from 'src/Models/activeList';

@Injectable({
  providedIn: 'root'
})
export class HttpservicesService {

  constructor(private httpclient:HttpClient) { }
  baseurl="https://localhost:7036/";

  AddUser(user:User):Observable<User>{
    return this.httpclient.post<User>(this.baseurl+"api/User/AddUser",user);
  }

  Login(Username:string,Password:string):Observable<any>{
    return this.httpclient.get(this.baseurl+"api/User/Login/"+Username+"/"+Password,{
      responseType: 'text',
    });
  }  

  AddToDoList(toDo:ToDoList):Observable<ToDoList>{
    return this.httpclient.post<ToDoList>(this.baseurl+"api/ToDoList/AddToDoList",toDo);
  }

  EditToDoList(toDo:ToDoList):Observable<any>{
    return this.httpclient.put(this.baseurl+"api/ToDoList/EditToDoList",toDo);
  }

  GetToDoListByUsername(userName:string){
    return this.httpclient.get(this.baseurl+"api/ToDoList/GetToDoListByUserName/"+userName);
  }

  GetToDoListById(toDoListId:number):Observable<any>{
    return this.httpclient.get(this.baseurl+"api/ToDoList/GetToDoListById/"+toDoListId);
  }

  ActiveList(userName:string):Observable<ActiveList[]>{
    return this.httpclient.get<ActiveList[]>(this.baseurl+"api/ToDoList/ActiveList/"+ userName);
  }

  CompletedList(userName:string):Observable<ActiveList[]>{
    return this.httpclient.get<ActiveList[]>(this.baseurl+"api/ToDoList/CompletedList/"+userName);
  }

  ToComplete(toDoListId:number){
    return this.httpclient.get(this.baseurl+"api/ToDoList/ToComplete/"+toDoListId);
  }

  DeleteTask(toDoListId:number){
    return this.httpclient.delete(this.baseurl+"api/ToDoList/DeleteToDoList/"+toDoListId);
  }

}
