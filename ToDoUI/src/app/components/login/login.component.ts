import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/Models/User';
import { HttpservicesService } from 'src/Services/httpservices.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  // loginarray: User[] = [];
  Userform?: FormGroup;

  constructor(
    private service: HttpservicesService,
    private log: FormBuilder,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.initForm();
  }
  initForm() {
    this.Userform=this.log.group({
      Username: new FormControl('', Validators.required),
      Password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
      ]),
    });
  }

  onLogin() {
    this.service
      .Login(this.Userform?.value.Username, this.Userform?.value.Password)
      .subscribe(
        (token:string)=> {
          const jwtToken=token;
          if(jwtToken!=null){
            localStorage.setItem('authtoken', jwtToken);
            sessionStorage.setItem('Username', this.Userform?.value.Username);
            this.router.navigateByUrl('addtask/:toDoListId');
          }
          else{
            alert('Authentication failed. Please check your credentials.');
          }
        },
        (error) => {
          alert('An error occurred during authentication.');
        }
      );
  }
}
