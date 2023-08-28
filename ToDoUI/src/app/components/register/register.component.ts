import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpservicesService } from 'src/Services/httpservices.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
Userform?:FormGroup;

  constructor(private builder:FormBuilder,private service:HttpservicesService,private router:Router) {}
  ngOnInit(): void {
   this.initForm();
  }

  initForm(){
    this.Userform=this.builder.group({
      UserName:new FormControl('',Validators.required),
      Name:new FormControl('',Validators.required),
      Email:new FormControl('',[Validators.required,Validators.email]),
      Password:new FormControl('',[
        Validators.required,
        Validators.minLength(8),
      ]),
      PhoneNumber:new FormControl('',Validators.required),
    })
  }


  saveUser(){
    this.service.AddUser(this.Userform?.value).subscribe((response)=>{
      this.Userform?.setValue({
        UserName:'',
        Name:'',
        Email:'',
        Password:'',
        PhoneNumber:'',
      })
      this.router.navigateByUrl('login');
    })
  }

}
